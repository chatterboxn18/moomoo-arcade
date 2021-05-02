using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LWMain : MonoBehaviour
{
	[Serializable]
	public struct CharacterAudio
	{
		public string Quote;
		public AudioClip Clip;
	}

	[SerializeField] private TextMeshProUGUI _buttonText;
	[SerializeField] private AudioSource _audioSource;
	[SerializeField] private CharacterAudio[] _characterAudio;
	[SerializeField] private Transform _groupBubble;
	private bool _isBubbleVisible;

	private bool _isActivePlant;
	[SerializeField] private LWFlowerGroup _activeFlower;

	[SerializeField] private TextMeshProUGUI _textCoin;

	private DateTime _todayDate;

	[Header("Controllers")]
	[SerializeField] private LWShopController _shopController;
	[SerializeField] private LWWaterController _waterController;

	private IEnumerator Start()
	{
		while (!LWResourceManager.IsLoaded)
			yield return null;

		//Always just show last displayed flower 
		
		var date = DateTime.Today;
		var dict = LWData.current.FlowerDictionary;
		var key = date.Month + "/" + date.Year;
		if (dict.ContainsKey(key))
		{
			// this should always work because when I add the key, I fill out the entire list with the days
			LWData.current.CurrentFlower = dict[key][date.Day];
		}
		else
		{
			LWData.current.CurrentFlower = new LWData.FlowerMonth();
			LWData.current.CurrentFlower.Date = date.ToShortDateString();
		}

		// sorta don't see the point of the code below anymore
		/*if (_todayDate == new DateTime() && PlayerPrefs.HasKey(LWConfig.TodayDateKey))
		{
			if (DateTime.TryParse(PlayerPrefs.GetString(LWConfig.TodayDateKey), out DateTime dateTime))
			{
				Debug.Log("Player Pref datetime: " + dateTime.Date);
				if (dateTime.Date != DateTime.Today.Date)
				{
					LWData.current.CurrentFlower = new LWData.FlowerMonth();
				}
				else
				{
					//LWData.current.CurrentFlower = LWData.current.FlowerDictionary[dateTime.Month];
				}
			}
			PlayerPrefs.SetString(LWConfig.TodayDateKey, DateTime.Today.ToShortDateString());
			_todayDate = DateTime.Today;
		}*/

	}

	private void UpdateActiveFlower()
	{
		if (LWData.current.CurrentFlower.PlantIndex == -1)
			return;
		var data = LWData.current.CurrentFlower;
		var sprites = LWResourceManager.Sprites[data.PlantIndex];
		var index = data.SpriteIndex * 2;
		var sprite = new []{sprites[index], sprites[index+1]};
		_activeFlower.SetPlant(0, sprite, LWData.current.CurrentFlower.Date);
	}

	public void ButtonEvt_FlowerPot()
	{
		if (LWData.current.CurrentFlower.PlantIndex == -1)
		{
			_shopController.gameObject.SetActive(true);
		}
		else
		{
			//small infographic of current flower
		}
	}

	public void ButtonEvt_OpenWater()
	{
		if (LWData.current.CurrentFlower.PlantIndex == -1)
		{
			_shopController.gameObject.SetActive(true);
		}
		else
		{
			_waterController.gameObject.SetActive(true);
		}
	}
	
	public void ButtonEvt_PlayAudio()
	{
		if (!_isBubbleVisible) return;
		var randomInt = Random.Range(0, _characterAudio.Length);
		_buttonText.text = _characterAudio[randomInt].Quote;
		_audioSource.clip = _characterAudio[randomInt].Clip;
		_audioSource.Play();
	}

	public void ButtonEvt_ToggleBubble()
	{
		_isBubbleVisible = !_isBubbleVisible;
		_groupBubble.gameObject.SetActive(_isBubbleVisible);
		ButtonEvt_PlayAudio();
	}
}

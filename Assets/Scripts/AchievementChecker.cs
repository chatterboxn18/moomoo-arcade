using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AchievementChecker : MonoBehaviour
{
	private float _timer = 0f;
	private float _fadeTime = 0.5f;
	private bool _isFadingIn;
	private bool _isFadingOut;

	[SerializeField] private Text _iceCream;
	[SerializeField] private Text _iceBar;
	[SerializeField] private Text _toppings;
	[SerializeField] private Text _cream;
	[SerializeField] private Text _mmm;
	[SerializeField] private Text _earnings;

	[SerializeField] private CanvasGroup _stats;
	[SerializeField] private CanvasGroup _goals;

	[SerializeField] private GameObject _achievementPrefab;
	[SerializeField] private Transform _achievementGroups;
	
	[Serializable]
	public struct AchievementItem
	{
		public Sprite Icon;
		public string Title;
		public string Subtitle;
	}

	[SerializeField] private AchievementItem[] achievementsItem;
	private List<Achievement> _achievements = new List<Achievement>();
	

	private CanvasGroup _canvasGroup;


	private void Awake()
	{
		_canvasGroup = GetComponent<CanvasGroup>();
	}

	private void OnEnable()
	{
		_isFadingIn = true;
		_timer = 0;
	}

	public void SetValues(int iceCream, int iceBar, int toppings, int cream, int mmm, float earnings, int misses)
	{
		//setting strings
		_iceCream.text = iceCream.ToString();
		_iceBar.text = iceBar.ToString();
		_mmm.text = mmm.ToString();
		_earnings.text = earnings.ToString(CultureInfo.CurrentCulture);
		
		//setting data
		var IC = PlayerPrefs.GetInt("IceCreams");
		PlayerPrefs.SetInt("IceCreams", IC + iceCream);
		
		var IB = PlayerPrefs.GetInt("IceBars");
		PlayerPrefs.SetInt("IceBars", IB + iceBar);

		var profits = PlayerPrefs.GetFloat("Earnings");
		PlayerPrefs.SetFloat("Earnings", profits + earnings);

		var missed = PlayerPrefs.GetInt("Trashed");
		PlayerPrefs.SetInt("Trashed", missed + misses);
		
		if (misses == 0) PlayerPrefs.SetInt("Perfect",0);
		
		CheckAchievements();
	}

	public void ClosePanel()
	{
		_isFadingOut = true;
	}

	private void Update()
	{
		if (_timer > _fadeTime)
		{
			_isFadingIn = false;
			if (_isFadingOut)
			{
				_isFadingOut = false;
				gameObject.SetActive(false);
			}
		}
		
		if (_isFadingIn && _timer < _fadeTime)
		{
			var lerp = Mathf.Lerp(0, 1, _timer / _fadeTime);
			_canvasGroup.alpha = lerp;
			_timer += Time.deltaTime;
		}
		
	}

	public void ButtonEvt_DisplayStats(bool on)
	{
		StartCoroutine(SwitchDisplays(on));
	}
	
	private IEnumerator SwitchDisplays(bool isStats)
	{
		var timer = 0f;
		var fadeTime = 0.5f;
		if (isStats)
		{
			_stats.gameObject.SetActive(true);
			while (timer < fadeTime)
			{
				var lerp = Mathf.Lerp(0, 1, timer / fadeTime);
				timer += Time.deltaTime;
				_stats.alpha = lerp;
				_goals.alpha = 1 - lerp;
				yield return null;
			}
			_goals.gameObject.SetActive(false);
		}
		else
		{
			_goals.gameObject.SetActive(true);
			while (timer < fadeTime)
			{
				var lerp = Mathf.Lerp(0, 1, timer / fadeTime);
				timer += Time.deltaTime;
				_stats.alpha = 1- lerp;
				_goals.alpha = lerp;
				yield return null;
			}
			_stats.gameObject.SetActive(false);
		}
	}
	
	public void DisplayAchievements()
	{
		foreach (var achievement in achievementsItem)
		{
			var item = Instantiate(_achievementPrefab, _achievementGroups).GetComponent<Achievement>();
			item.SetIcon(achievement.Icon);
			item.SetTitle(achievement.Title);
			item.SetSubtitle(achievement.Subtitle);
			_achievements.Add(item);
		}
	}

	public void CheckAchievements()
	{
		var earnings = PlayerPrefs.GetFloat("Earnings");
		if (earnings >= 100000)
			_achievements[0].Activate();

		if (earnings >= 500000)
			_achievements[1].Activate();

		var iceCreams = PlayerPrefs.GetInt("IceCreams");
		if (iceCreams >= 25) _achievements[2].Activate();
		if (iceCreams >= 50) _achievements[4].Activate();

		var iceBars = PlayerPrefs.GetInt("IceBars");
		if (iceBars >= 25) _achievements[3].Activate();
		if (iceBars >= 50) _achievements[5].Activate();

		var missed = PlayerPrefs.GetInt("Trashed");
		if (missed >= 10) _achievements[7].Activate();
		
		if (PlayerPrefs.HasKey("Perfect")) _achievements[6].Activate();

		if (PlayerPrefs.GetString("Mamamoo").Split(',').Length > 3) _achievements[8].Activate();
		
		if (PlayerPrefs.HasKey("FoundMamamoo")) _achievements[9].Activate();
		
		Debug.Log("Earnings: " + earnings);
		Debug.Log("IceCreams: " + iceCreams);
		Debug.Log("IceBars: " + iceBars);
		Debug.Log("Missed: " + missed);
	}


}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace MooClicker
{
	public class SpazzClicker : MonoBehaviour
	{
		[SerializeField] private Camera _camera;
		
		[Header("Prefabs")]
		[SerializeField] private SpazzButton _spazzButton;
		[SerializeField] private MooEffect _mooEffect;
	
		[SerializeField] private Text _spazzNumberText;
		public static int CurrentSpazz;

		[Header("ClickerItems")] 
		private int _mooSpazz = 1;

		[SerializeField] private Transform _effects;
		
		[SerializeField] private RectTransform _spazzers;
		[SerializeField] private RectTransform _upgrades;

		[SerializeField] private RectTransform[] _panels;

		private Dictionary<string, int> _characterIndex = new Dictionary<string, int>();

		[Header("Data")] [SerializeField] private MooItems _mooItems;
		[SerializeField] private List<SpazzMiner> _mooData = new List<SpazzMiner>();
		private Dictionary<string, SpazzMiner> _mooDataDictionary = new Dictionary<string, SpazzMiner>();

		[Header("Upgrade Panels")] 
		[SerializeField] private RectTransform _arrow;
		[SerializeField] private RectTransform _upgradePanel;
		private bool _isOpen;
		private float _upgradePanelTime = 0.5f;
		
		private void Awake()
		{
			foreach (var data in _mooData)
			{
				_mooDataDictionary.Add(data.Name, data);
				_characterIndex.Add(data.Name, 0);
			}
		}

		private IEnumerator Start()
		{
			//_spazzButton.Evt_AddSpazz += Evt_AddSpazz;
			var index = 0;

			/*var uwr = new UnityWebRequest("https://moomooarcade.s3-us-west-1.amazonaws.com/mooclicker/moomootravel.unity3d");
				string path = Path.Combine(Application.persistentDataPath, "moomootravel.unity3d");
				yield return uwr.downloadHandler = new DownloadHandlerFile(path);
				yield return uwr.SendWebRequest();*/
			
			var myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "moomootravel.unity3d"));
			if (myLoadedAssetBundle == null)
			{
				Debug.Log("Failed to load AssetBundle!");
				yield break;
			}
			
			foreach (var mamamoo in _mooItems.Mamamoos)
			{
				var miner = myLoadedAssetBundle.LoadAsset<GameObject>(mamamoo.dataPath);
				var minerObject = Instantiate(miner, _spazzers, false);
				var spazzminer = minerObject.GetComponent<SpazzMiner>();
				minerObject.GetComponent<RectTransform>().anchoredPosition = spazzminer.Position;
				spazzminer.SetUpMiner(_upgrades, mamamoo.SpazzAmount);
				spazzminer.Evt_UpdateSpazz += Evt_AddSpazz;
			}

			yield return null;
		}

		public void ButtonEvt_ChangePanels(int index)
		{
			_panels[index].transform.SetAsLastSibling();
		}

		public void ButtonEvt_OpenPanel()
		{
			_isOpen = !_isOpen;
			StartCoroutine(OpenPanel(_isOpen));
		}
		
		public void Evt_AddMiner(string name, int index)
		{
			if (!_characterIndex.ContainsKey(name))
			{
				_characterIndex.Add(name, 0);
			}
			else
				_characterIndex[name] = index;
			_mooDataDictionary[name].UpgradeMiner();
		}

		private IEnumerator OpenPanel(bool on)
		{
			var timer = 0f;
			var size = _upgradePanel.sizeDelta.y;
			_upgradePanel.pivot = on? new Vector2(0.5f,0): new Vector2(0.5f, 1);
			while (timer <= _upgradePanelTime)
			{
				_arrow.rotation = _arrow.rotation.RotateZ(on? Mathf.Lerp(0,180, timer/_upgradePanelTime):Mathf.Lerp(180, 0, timer / _upgradePanelTime));
				_upgradePanel.position = _upgradePanel.position.SetY(@on ? Mathf.Lerp(-1 * size, 0, timer / _upgradePanelTime) : Mathf.Lerp( size, 0,timer / _upgradePanelTime));
				timer += Time.deltaTime;
				yield return null;
			}
			_arrow.rotation = _arrow.rotation.RotateZ(on? 180:0);
			_upgradePanel.position = _upgradePanel.position.SetY(0);
		}
		
		private void Update()
		{
			_spazzNumberText.text = CurrentSpazz.ToString();
		}

		public void ButtonEvt_ClickSpazz(PointerEventData data)
		{
			CurrentSpazz += _mooSpazz;
			var screenPos = Input.mousePosition;
			screenPos.z = 100;
			var effect = Instantiate(_mooEffect, _effects);
			effect.RectTransform.position = data.position;
			effect.Setup("+" + _mooSpazz);
		}
		
		public void Evt_AddSpazz(int spazzNumber)
		{
			CurrentSpazz += spazzNumber;
		}
	}

}


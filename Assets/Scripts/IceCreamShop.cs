using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using Random = UnityEngine.Random;
// ReSharper disable All

public enum IceCreamType
{
	Popbar = 0, 
	Cone = 1,
	None = -1
}
public class IceCreamShop : MonoBehaviour
{
	[Serializable]
	public struct IceCream
	{
		public Sprite IceCreamSprite;
		public Sprite[] Color;
		public Sprite Cream;
		public Sprite[] Toppings;
		public Sprite Outline;
	}

	public enum Characters
	{
		Moonbyul = 0, 
		Solar = 1, 
		Wheein = 2, 
		Hwasa = 3
	}
	
	[Serializable]
	public struct SpecialIceCream
	{
		public Characters Character;
		public int[] IceCreamIndex;
		public int[] IceBarIndex;
	}

	public SpecialIceCream[] MMMIceCreams;
	
	public IceCream[] TwoIceCreams;

	[Serializable]
	public struct IceCreamModel
	{
		public Image IceCream;
		public Image Color;
		public Image Cream;
		public Image Topping;
		public Image Outline;
	}

	public IceCreamModel Model;

	private IceCreamType _currentType = IceCreamType.None;

	[SerializeField] private List<Client> _clients;

	private int[] _iceCreamIndex = {-1, -1, 0, -1};

	[SerializeField] private Sprite[] _characterSprites;
	[SerializeField] private Image _character;
	private bool _isCharacterSwitching;
	// ReSharper disable once InconsistentNaming
	[SerializeField] private RectTransform _window;
	private float _windowStartY = 95;
	private float _windowEndY = -26;
	private float _changeSpeed = 0.5f;

	[SerializeField] private Text _displayEarnings;
	private float _earnings;
	[SerializeField] private float _iceCreamPrice = 2000;
	[SerializeField] private float _toppingsPrice = 500;
	
	private float _timeForNewClient = 2f;
	private float _timer = 0f;

	private int _clientsServed = 0;
	
	//Start Variable 
	[SerializeField] private CanvasGroup _panelStart;
	[SerializeField] private CanvasGroup _panelInstructions;
	
	//Game Time Variables
	[SerializeField] private float _gameTime = 180f;
	private float _gameTimer;
	private bool _isGameRunning;
	[SerializeField] private Text _timeText;
	
	//achievement counts
	private int _iceCreamServed;
	private int _iceBarsServed;
	private int _toppingsServed;
	private int _creamServed;
	private int _mamamooServed;

	private int _missedServed;
	
	//achievement panel
	// ReSharper disable once InconsistentNaming
	[SerializeField] private AchievementChecker _panelGameOver;

	private void Start()
	{
		PrepareGame();
	}

	private void PrepareGame()
	{
		//prep PlayerPrefs
		if (!PlayerPrefs.HasKey("IceCreams"))PlayerPrefs.SetInt("IceCreams", 0);
		if (!PlayerPrefs.HasKey("IceBars"))PlayerPrefs.SetInt("IceBars", 0);
		if (!PlayerPrefs.HasKey("Trashed"))PlayerPrefs.SetInt("Trashed", 0);
		if (!PlayerPrefs.HasKey("Earnings"))PlayerPrefs.SetFloat("Earnings", 0f);
		if (!PlayerPrefs.HasKey("Mamamoo")) PlayerPrefs.SetString("Mamamoo", "");
		
		//prep achievements 
		_panelGameOver.DisplayAchievements();
		
		//prepare timer
		_gameTimer = _gameTime; 
		
		//prepare ice cream and clients
		ResetModel();
		foreach (var client in _clients)
		{
			var iceCreamType = Random.Range(0, 2);
			client.SetOrder(TwoIceCreams[iceCreamType], UpdateClient(iceCreamType));
			client.Evt_UpdateClient += () => Evt_PrepareClient(client, false);
			client.Evt_LosePoint += Evt_FailOrder;
		}

	}

	private IEnumerator FadeInGroup(CanvasGroup group, bool fadeIn, Action onComplete)
	{
		var timer = 0f;
		var start = 0;
		var end = 1;
		if (!fadeIn)
		{
			start = 1;
			end = 0;
		}
		group.gameObject.SetActive(true);
		while (timer < 0.5f)
		{
			var lerp = Mathf.Lerp(start, end, timer / 0.5f);
			group.alpha = lerp;
			timer += Time.deltaTime;
			yield return null;
		}

		onComplete();
	}
	
	private void ResetGame()
	{
		ResetModel();
		foreach (var client in _clients)
		{
			client.Reset();
			var iceCreamType = Random.Range(0, 2);
			client.SetOrder(TwoIceCreams[iceCreamType], UpdateClient(iceCreamType));
			client.StopClient(false);
		}
		
		//prepare stats
		_mamamooServed = 0;
		_iceBarsServed = 0;
		_iceCreamServed = 0;
		_toppingsServed = 0;
		_creamServed = 0;

		_panelGameOver.ClosePanel();
		_gameTimer = _gameTime;
		_isGameRunning = true;
	}
	private void Update()
	{
		if (!_isGameRunning)
			return;
		
		// Manage Game Time 
		if (_gameTimer <= 0)
		{
			_timeText.text = "0:00";
			_isGameRunning = false;
			foreach (var client in _clients)
			{
				client.StopClient(true);
			}
			_panelGameOver.SetValues(_iceCreamServed, _iceBarsServed, _toppingsServed, _creamServed, _mamamooServed, _earnings, _missedServed);
			_panelGameOver.gameObject.SetActive(true);
			return;
		}
		_gameTimer -= Time.deltaTime;
		var minutes = Mathf.Floor(_gameTimer / 60).ToString("0");
		var seconds = Mathf.Floor(_gameTimer % 60).ToString("00");
		_timeText.text = minutes + ":" + seconds;
		
		// press icecreams 
		if (Input.GetKey(KeyCode.Q)) ButtonEvt_CreateIceCream(0);
		if (Input.GetKey(KeyCode.A)) ButtonEvt_CreateIceCream(1);
		
		//keypresses flavors
		if (Input.GetKey(KeyCode.W)) ButtonEvt_AddFlavor(0);
		if (Input.GetKey(KeyCode.E)) ButtonEvt_AddFlavor(1);
		if (Input.GetKey(KeyCode.R)) ButtonEvt_AddFlavor(2);
		if (Input.GetKey(KeyCode.S)) ButtonEvt_AddFlavor(3);
		if (Input.GetKey(KeyCode.D)) ButtonEvt_AddFlavor(4);
		if (Input.GetKey(KeyCode.F)) ButtonEvt_AddFlavor(5);
			
		//Keypress keypad
		if (Input.GetKey(KeyCode.Keypad1)) ButtonEvt_CreateToppings(0);
		if (Input.GetKey(KeyCode.Keypad2)) ButtonEvt_CreateToppings(1);
		if (Input.GetKey(KeyCode.Keypad3)) ButtonEvt_CreateToppings(2);
		if (Input.GetKey(KeyCode.Keypad0)) ButtonEvt_AddCream();
		
		if (Input.GetKeyUp(KeyCode.KeypadEnter)) CheckCorrectIceCream();
		
		
		
		// Manage clients
		if (_timer > _timeForNewClient)
		{
			foreach (var client in _clients)
			{
				if (!client.IsDisplaying)
				{
					client.DisplayItem();
					break;
				}
			}

			_timer = 0;
			return;
		}
		_timer += Time.deltaTime;
	}

	private void Evt_FailOrder()
	{
		_missedServed++;
		_earnings -= 1000;
		var client = _clients[0];
		_clients.Remove(_clients[0]);
		_clients.Add(client);
	}

	private int[] UpdateClient(int index)
	{
		var iceCreamList = new int[] {-1, -1, 0, -1};
		iceCreamList[0] = index;
		iceCreamList[1] = Random.Range(0, TwoIceCreams[index].Color.Length);
		iceCreamList[2] = Random.Range(0,2);
		iceCreamList[3] = Random.Range(-1, TwoIceCreams[index].Toppings.Length);
		return iceCreamList;
	}

	private IEnumerator SwitchCharacter(int index)
	{
		if (!PlayerPrefs.HasKey("FoundMamamoo")) PlayerPrefs.SetInt("FoundMamamoo", 0);
		_isCharacterSwitching = true;
		var timer = 0f;
		var windowPos = _window.localPosition;
		while (timer < _changeSpeed)
		{
			var lerpNumber = Mathf.Lerp(_windowStartY, _windowEndY, timer / _changeSpeed);
			_window.localPosition = new Vector3(windowPos.x, lerpNumber);
			timer += Time.deltaTime;
			yield return null;
		}
		_character.sprite = _characterSprites[index];
		timer = 0;
		while (timer < _changeSpeed)
		{
			var lerpNumber = Mathf.Lerp(_windowEndY, _windowStartY, timer / _changeSpeed);
			_window.localPosition = new Vector3(windowPos.x, lerpNumber);
			timer += Time.deltaTime;
			yield return null;
		}

		_isCharacterSwitching = false;
	}

	public void ResetModel()
	{
		Model.Color.color = new Color(0,0,0,0);
		Model.IceCream.color = new Color(0,0,0,0);
		Model.Cream.color = new Color(0,0,0,0);
		Model.Topping.color = new Color(0,0,0,0);
		Model.Outline.color = new Color(0,0,0,0);
		_iceCreamIndex = new[] {-1, -1, 0, -1};
		_currentType = IceCreamType.None;
	}

	public void CheckCorrectIceCream()
	{
		if (_iceCreamIndex[0] == -1)
			return;
		foreach (var client in _clients)
		{
			foreach (var list in client.GetIceCreamIndex())
			{
				if (CompareArrays(list, _iceCreamIndex))
				{
					SuccessfullyServed();
					CalculateStats(client.GetSpecialIndex());
					ResetModel();
					client.FinsihItem();
					_clients.Remove(client);
					_clients.Add(client);
					return;
				}
			}
			
		}
		
		//Achievement 
		_missedServed++;
		
		_earnings -= CalculateIceCream();
		ResetModel();
		//meaning none of them match 
		_displayEarnings.text = "₩ " + _earnings;
	}

	private void CalculateStats(int index)
	{
		if (index != -1)
		{
			_mamamooServed++;
			var mmm = PlayerPrefs.GetString("Mamamoo");
			var character = (Characters) index;
			var name = character.ToString();
			if (!mmm.Contains(name))
			{
				if (string.IsNullOrEmpty(mmm))
					PlayerPrefs.SetString("Mamamoo", name);
				else
					PlayerPrefs.SetString("Mamamoo", mmm + "," + name);
				
			}
			

		}
		if (_iceCreamIndex[0] == 0) _iceBarsServed++; 
		if (_iceCreamIndex[0] == 1) _iceCreamServed++;
		if (_iceCreamIndex[2] == 1) _creamServed++;
		if (_iceCreamIndex[3] != -1) _toppingsServed++;

	}

	private void SuccessfullyServed()
	{
		_earnings += CalculateIceCream();
		_displayEarnings.text = "₩ " + _earnings;
		_clientsServed++;
	}

	private float CalculateIceCream()
	{
		var earnings = _iceCreamPrice;
		if (_iceCreamIndex[2] != 0) earnings += _toppingsPrice;
		if (_iceCreamIndex[3] != -1) earnings += _toppingsPrice;
		return earnings;
	}
	private bool CompareArrays(int[] array1, int[] array2)
	{
		if (array1.Length != array2.Length)
			return false;
		for (var index = 0; index < array1.Length; index++)
		{
			if (array1[index] != array2[index])
			{
				return false;
			}
		}

		return true;
	}
	
	//Events
	private void Evt_PrepareClient(Client client, bool reset)
	{
		if (reset)
			ResetModel();
		client.ResetModel();
		var iceCreamType = Random.Range(0, 2);
		
		if (_clientsServed > 0 && _clientsServed % 8 == 0)
		{
			var randomMMM = Random.Range(0, 4);
			client.SetSpecialOrder(randomMMM, MMMIceCreams[randomMMM].IceCreamIndex, MMMIceCreams[randomMMM].IceBarIndex);
			return;
		}
		client.SetOrder(TwoIceCreams[iceCreamType], UpdateClient(iceCreamType));
	}
	
	//Button Evts
	public void ButtonEvt_StartGame()
	{
		StartCoroutine(FadeInGroup(_panelStart, false,() =>
		{
			_panelStart.gameObject.SetActive(false);
			_isGameRunning = true;
		}));
	}
	
	public void ButtonEvt_ResetGame()
	{
		ResetGame();
	}
	
	public void ButtonEvt_CreateIceCream(int index)
    {
    	if (_currentType != (IceCreamType) index)
    		ResetModel();
    	_currentType = (IceCreamType) index;
    	Model.IceCream.sprite = TwoIceCreams[index].IceCreamSprite;
    	Model.Outline.sprite = TwoIceCreams[index].Outline;
    	Model.IceCream.color = new Color(255, 255, 255, 1);
    	Model.Outline.color = new Color(255, 255, 255, 1);
    	_iceCreamIndex[0] = index;
    }

    public void ButtonEvt_AddFlavor(int index)
    {
    	if (_currentType == IceCreamType.None)
    		return;
    	Model.Color.sprite = TwoIceCreams[(int) _currentType].Color[index];
    	Model.Color.color = new Color(255, 255, 255, 1);
    	_iceCreamIndex[1] = index;
    }

    public void ButtonEvt_AddCream()
    {
    	if (_currentType == IceCreamType.None)
    		return;
    	Model.Cream.sprite = TwoIceCreams[(int) _currentType].Cream;
    	Model.Cream.color = new Color(255, 255, 255, 1);
    	_iceCreamIndex[2] = 1;
    }

    public void ButtonEvt_CreateToppings(int index)
    {
    	if (_currentType == IceCreamType.None)
    		return;
    	Model.Topping.sprite = TwoIceCreams[(int) _currentType].Toppings[index];
    	Model.Topping.color = new Color(255, 255, 255, 1);
    	_iceCreamIndex[3] = index;
    }

    public void ButtonEvt_SpecialCharacterChange()
    {
    	if (_isCharacterSwitching)
    		return;
    	var randomIndex = Random.Range(0, 4);
    	StartCoroutine(SwitchCharacter(randomIndex));
    }

    public void ButtonEvt_Pause()
    {
	    if (_panelStart.gameObject.activeSelf)
	    {
		    StartCoroutine(FadeInGroup(_panelInstructions, false, () => { _panelInstructions.gameObject.SetActive(false);}));
		    return;
	    }
	    
	    foreach (var client in _clients)
	    {
		    client.StopClient(_isGameRunning);
	    }
	    _isGameRunning = !_isGameRunning;  
	    StartCoroutine(FadeInGroup(_panelInstructions, !_isGameRunning, () =>
	    {
		    _panelInstructions.gameObject.SetActive(!_isGameRunning);
	    }));
    }

    public void ButtonEvt_OpenInstructions()
    {
	    StartCoroutine(FadeInGroup(_panelInstructions, true, () => { }));
    }
    
	//DEBUG 
	public void DEBUG_ButtonEvt_ClearPlayerPrefs()
	{
		Debug.Log(PlayerPrefs.GetString("Mamamoo"));
		PlayerPrefs.DeleteAll();
	}
	
}

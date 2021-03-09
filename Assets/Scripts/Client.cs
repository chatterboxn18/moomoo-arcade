using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Client : MonoBehaviour
{
	private int _index;

	private bool _isRevealingClient;
	private float _startingY;
	private float _hiddenX = 709;
	private float _startingX = 465;
	private int _totalItems = 15;
	private bool _isHidingClient;

	private float _fadeTime = .75f;

	private List<int[]> _iceCreamIndex = new List<int[]>();

	private RectTransform _rectTransform;

	private float _lerpValue = 0f;

	public Action Evt_LosePoint= delegate {  };
	
	public Action Evt_UpdateClient = delegate {  };

	public IceCreamShop.IceCreamModel Model;

	[SerializeField] private Sprite[] _specialOrders;
	[SerializeField] private Image _imageSpecial;
	private int _specialIndex = -1;
	
	public bool IsDisplaying { get; private set; }
	private float _clientTimer = 21f;
	[SerializeField] private RectTransform _clientTransform;
	[SerializeField] private Image _clientOverlay;
	[SerializeField] private Color[] _clientColors;
	private float _clientWidth;
	private bool _isClientTimerRunning;
	private float _runningTimer = 0f;

	private bool _stopped;

	public void SetClientTimer(float time)
	{
		_clientTimer = time;
	}

	public int GetSpecialIndex()
	{
		return _specialIndex;
	}
	
	public void StopClient(bool on)
	{
		_stopped = on;
	}

	public void Reset()
	{
		transform.localPosition = new Vector3(_hiddenX, _startingY);
		_isHidingClient = false;
		_isRevealingClient = false;
		_isClientTimerRunning = false;
		_clientTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, _rectTransform.sizeDelta.y);
		IsDisplaying = false;
		_clientOverlay.color = _clientColors[2];
	}
	
	public void SetOrder(IceCreamShop.IceCream iceCream, int[] iceCreamIndex)
	{
		ApplySprite(iceCream.IceCreamSprite, true, Model.IceCream);
		ApplySprite(iceCream.Outline, true, Model.Outline);
		ApplySprite(iceCream.Color[iceCreamIndex[1]], true, Model.Color);
		ApplySprite(iceCream.Cream, iceCreamIndex[2] != 0, Model.Cream);
		ApplySprite(iceCreamIndex[3] == -1 ? null : iceCream.Toppings[iceCreamIndex[3]], iceCreamIndex[3] != -1,
			Model.Topping);
		_iceCreamIndex.Add(iceCreamIndex);
	}

	public void SetSpecialOrder(int characterIndex, int[] iceCreamIndex, int[] iceBarIndex)
	{
		_specialIndex = characterIndex;
		ApplySprite(null, false, Model.IceCream);
		ApplySprite(null, false, Model.Outline);
		ApplySprite(null, false, Model.Color);
		ApplySprite(null, false, Model.Cream);
		ApplySprite(null, false, Model.Topping);
		ApplySprite(_specialOrders[characterIndex], true, _imageSpecial);
		_iceCreamIndex.Add(iceCreamIndex);
		_iceCreamIndex.Add(iceBarIndex);
	}
	private void ApplySprite(Sprite sprite, bool on, Image image)
	{
		if (!on || sprite == null)
		{
			image.color = new Color(255, 255, 255, 0);
		}
		else
		{
			image.sprite = sprite;
			image.color = new Color(255, 255, 255, 1);
		}
	}

	public List<int[]> GetIceCreamIndex()
	{
		return _iceCreamIndex;
	}

	private void Awake()
	{
		_startingY = transform.localPosition.y;
		_rectTransform = GetComponent<RectTransform>();
		_clientWidth = _rectTransform.sizeDelta.x;
	}

	public void ResetModel()
	{
		//Mamamoo index
		_specialIndex = -1;
		
		//Model
		Model.Color.color = new Color(0, 0, 0, 0);
		Model.IceCream.color = new Color(0, 0, 0, 0);
		Model.Cream.color = new Color(0, 0, 0, 0);
		Model.Topping.color = new Color(0, 0, 0, 0);
		Model.Outline.color = new Color(0, 0, 0, 0);
		_imageSpecial.color = new Color(0, 0, 0, 0);
		_iceCreamIndex = new List<int[]>();
	}

	public void FinsihItem()
	{
		_isHidingClient = true;
		_isRevealingClient = false;
		_isClientTimerRunning = false;
		_runningTimer = 0;
	}

	public void DisplayItem()
	{
		_clientOverlay.color = _clientColors[2];
		_runningTimer = 0;
		IsDisplaying = true;
		_isRevealingClient = true;
	}

	private void Update()
	{
		if (_stopped) return;
		if (_isRevealingClient)
		{
			_lerpValue = Mathf.Lerp( _hiddenX, _startingX,_runningTimer/_fadeTime);
			transform.localPosition = new Vector3(_lerpValue, _startingY, 0);
			if (_runningTimer > _fadeTime)
			{
				_runningTimer = 0;
				_isRevealingClient = false;
				_isClientTimerRunning = true;
				return;
			}
			_runningTimer += Time.deltaTime;
		}

		else if (_isHidingClient)
		{
			_lerpValue = Mathf.Lerp(_startingX, _hiddenX, _runningTimer/ _fadeTime);
			transform.localPosition = new Vector3(_lerpValue, _startingY, 0);
			if (_runningTimer > _fadeTime)
			{
				_runningTimer = 0;
				_isHidingClient = false;
				Evt_UpdateClient();
				_clientTransform.sizeDelta = new Vector2(_clientWidth, _rectTransform.sizeDelta.y);
				IsDisplaying = false;
				return;
			}
			_runningTimer += Time.deltaTime;
		}
		else if (_isClientTimerRunning)
		{
			if (_runningTimer > _clientTimer)
			{
				Evt_LosePoint();
				_runningTimer = 0;
				_isClientTimerRunning = false;
				_isHidingClient = true;
				return;
			}

			if (_runningTimer / 7 > 2)
			{
				_clientOverlay.color = _clientColors[0];
			}
			else if (_runningTimer / 7 > 1)
			{
				var number = _runningTimer-7;
				var lerpColor = Color.Lerp(_clientColors[1], _clientColors[0], number / 7);
				_clientOverlay.color = lerpColor;
			}
			else
			{
				var lerpColor = Color.Lerp(_clientColors[2], _clientColors[1], _runningTimer / 7);
				_clientOverlay.color = lerpColor;
			}
			_lerpValue = Mathf.Lerp(_clientWidth, 0, _runningTimer / _clientTimer);
			_clientTransform.sizeDelta = new Vector2(_lerpValue, _rectTransform.sizeDelta.y);
			_runningTimer += Time.deltaTime;
		}

	}

}

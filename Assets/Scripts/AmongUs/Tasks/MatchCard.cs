using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchCard : MonoBehaviour
{
	[SerializeField] private Image _mainImage;
	[SerializeField] private Text _mainText;
	[SerializeField] private bool _startVisible;
	[SerializeField] private SimpleButton _button;

	private bool _isRevealing;
	[SerializeField] private float _revealTime;
	private float _revealTimer;

	[SerializeField] private bool _isText;
	

	private void Start()
	{
		if (!_startVisible && !_isText)
			_mainImage.color = _mainImage.color.SetAlpha(0f);
		if (!_startVisible && _isText)
			_mainText.color = _mainText.color.SetAlpha(0f);
	}

	public void SetText(string name)
	{
		_mainText.text = name;
	}

	private void Update()
	{
		if (!_isText && _mainImage.color.a >= 1)
		{
			_isRevealing = false;
			_revealTimer = 0;
		}
		
		if (_isText && _mainText.color.a >= 1)
		{
			_isRevealing = false;
			_revealTimer = 0;
		}
		
		if (_isRevealing)
		{
			if (_isText)
				_mainText.color = _mainText.color.SetAlpha(_revealTimer / _revealTime);
			else
				_mainImage.color = _mainImage.color.SetAlpha(_revealTimer/_revealTime);
			_revealTimer += Time.deltaTime;
		}
	}

	public SimpleButton GetButton()
	{
		return _button != null ? _button : null;
	}
	
	public void Evt_Reveal()
	{
		_isRevealing = true;
	}
}

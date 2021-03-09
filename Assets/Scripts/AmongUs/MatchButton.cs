using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MatchButton : SimpleButton
{
	public int Index;
	public Action<int, Action> Evt_CheckMatch = delegate(int i, Action onSuccess) {  };
	[SerializeField] private Text _text;
	[SerializeField] private Image _image;
	private float _matchTimer;
	private float _revealTime = 1;
	private bool _isRevealing;
	
	public override void OnPointerUp(PointerEventData eventData)
	{
		base.OnPointerUp(eventData);
		Evt_CheckMatch(Index, ButtonEvt_Ordered);
	}

	protected override void Update()
	{
		base.Update();
		if (_matchTimer >= _revealTime)
		{
			_image.color = _image.color.SetAlpha(0);
			return;
		}
		
		if (_isRevealing)
		{
			_image.color = _image.color.SetAlpha(1 - _matchTimer/_revealTime);
			_matchTimer += Time.deltaTime;
		}
	}

	public void ButtonEvt_Ordered()
	{
		_isRevealing = true;
	}
	
	public void SetText(string text, int fontSize = 100, Color color = default)
	{
		if (fontSize != 100)
			_text.fontSize = fontSize;
		if (color != default)
			_text.color = color;
		_text.text = text;
	}
}

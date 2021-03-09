using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpazzButton : SimpleButton
{
	private int _currentSpazz = 1;

	public Action<int> Evt_AddSpazz;
	
	public override void OnPointerUp(PointerEventData eventData)
	{
		base.OnPointerUp(eventData);
		_canvasGroup.transform.localScale = Vector3.one;
		//Evt_AddSpazz(_currentSpazz);
	}

	public override void OnPointerDown(PointerEventData eventData)
	{
		base.OnPointerDown(eventData);
		_canvasGroup.transform.localScale = new Vector3(0.9f,0.9f,0.9f);
	}

	public void SetCurrentSpazz(int number)
	{
		_currentSpazz = number;
	}
}

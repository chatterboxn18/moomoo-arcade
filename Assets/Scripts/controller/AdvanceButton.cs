using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AdvanceButton : SimpleButton
{
	[SerializeField] private bool _isActive;

	[SerializeField] private CanvasGroup _activeGroup;
	[SerializeField] private CanvasGroup _inactiveGroup;

	public Action Evt_BasicEvent_Up = delegate {  };
	public Action Evt_BasicEvent_Down = delegate {  };

	private float _activeTime = .5f;
	private float _activeTimer = 0f;

	public void SetActive(bool on)
	{
		_isActive = on;
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		base.OnPointerUp(eventData);
		Evt_BasicEvent_Up();
	}

	public override void OnPointerDown(PointerEventData eventData)
	{
		base.OnPointerDown(eventData);
		Evt_BasicEvent_Down();
	}

	protected override void Update()
	{
		base.Update();
		if (_isActive)
		{
			if (_activeGroup.alpha < 1)
			{
				_activeGroup.alpha = _activeTimer/_activeTime;
				_inactiveGroup.alpha = 1 - _activeTimer/_activeTime;
				_activeTimer += Time.deltaTime;
			}
			else
			{
				_activeGroup.alpha = 1;
				_inactiveGroup.alpha = 0;
				_activeTimer = 0f;
			}
			
		}

		if (!_isActive)
		{
			if (_activeGroup.alpha > 0)
			{
				_inactiveGroup.alpha = (_activeTimer/_activeTime);
				_activeGroup.alpha = 1 - _activeTimer / _activeTime;
				_activeTimer += Time.deltaTime;
			}
			else
			{
				_activeTimer = 0f;
				_activeGroup.alpha = 0;
				_inactiveGroup.alpha = 1;
			}
		}
	}
}

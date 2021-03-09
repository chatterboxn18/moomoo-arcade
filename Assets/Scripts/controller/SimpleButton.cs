using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimpleButton : Selectable, IPointerDownHandler, IPointerUpHandler
{
	private bool _isSelected;
	[SerializeField] private bool _isVisible;

	[SerializeField] protected CanvasGroup _canvasGroup;
	private float _visibility;
	private float _fadeTime = 0.1f;
	private float _timer = 0f;

	[Serializable]
	public class ButtonEvt : UnityEvent<PointerEventData> {}

	public ButtonEvt EvtPointerDown = new ButtonEvt();
	public ButtonEvt EvtPointerUp = new ButtonEvt();

	public void SetFadeTime(float time)
	{
		_fadeTime = time;
	}
	
	public void SetVisibility(bool on)
	{
		_isVisible = on;
	}

	protected override void Awake()
	{
		if (!_isVisible)
			_canvasGroup.alpha = 0;
	}

	protected virtual void Update()
	{
		if (_isVisible)
		{
			if (_canvasGroup.alpha < 1)
			{
				_canvasGroup.alpha = _timer/_fadeTime;
				_timer += Time.deltaTime;
			}
			else
			{
				_canvasGroup.alpha = 1;
				_timer = 0f;
			}
			
		}

		if (!_isVisible)
		{
			if (_canvasGroup.alpha > 0)
			{
				_canvasGroup.alpha = 1 - (_timer/_fadeTime);
				_timer += Time.deltaTime;
			}
			else
			{
				_timer = 0f;
				_canvasGroup.alpha = 0;
			}
		}
	}	
	
	public override void OnPointerUp(PointerEventData eventData)
	{
		if (_isVisible)
			EvtPointerUp.Invoke(eventData);
	}

	public override void OnPointerDown(PointerEventData eventData)
	{
		if (_isVisible)
			EvtPointerDown.Invoke(eventData);
	}
}

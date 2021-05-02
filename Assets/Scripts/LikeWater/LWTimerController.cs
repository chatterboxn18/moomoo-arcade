using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LWTimerController : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _timerText;
	private bool _isEdit;
	private bool _isRunning;
	private float _time;
	[SerializeField] private CanvasGroup _editGroup;
	[SerializeField] private LWTimerManager _timeController;

	private void Start()
	{
		_timeController.Evt_UpdateTime += UpdateTimer;
	}
	
	private void OnEnable()
	{
		_timeController.DisplayTimer(false);
		if (PlayerPrefs.HasKey(LWConfig.Timer))
			_timerText.text = PlayerPrefs.GetString(LWConfig.Timer);
	}

	private void CleanUp()
	{
		//should there ever be a clean up
		_timeController.Evt_UpdateTime -= UpdateTimer;
	}
	
	public void ButtonEvt_Add(int index)
	{
		var originalText = _timerText.text;
		var text = originalText.Substring(0,index);

		var character = int.Parse(originalText[index].ToString());
		character = (character == 9 || (index == 3 && character == 5)) ? 0 : (character + 1);
		text += character + originalText.Substring(index + 1, originalText.Length - (index +1));
		_timerText.text = text;
	}
	
	public void ButtonEvt_Minus(int index)
	{
		var originalText = _timerText.text;
		var text = originalText.Substring(0,index);

		var character = int.Parse(originalText[index].ToString());
		if (character == 0 && index == 3) character = 5; 
		else if (character == 0) character = 9;
		else character--;
		text += character + originalText.Substring(index + 1, originalText.Length - (index +1));
		_timerText.text = text;
	}
	
	private float GetMinutes()
	{
		var text = _timerText.text.Split(':');
		var hours = int.Parse(text[0]);
		var minutes = int.Parse(text[1]);
		_time = minutes + hours * 60;
		return _time;
	}

	public void UpdateTimer(string time)
	{
		_timerText.text = time;
	}
	
	public void ButtonEvt_Edit()
	{
		if (_isRunning) return;
		_isEdit = !_isEdit;
		if (!_isEdit)
		{
			PlayerPrefs.SetString(LWConfig.Timer, _timerText.text);
		}
		_editGroup.gameObject.SetActive(_isEdit);
	}

	public void ButtonEvt_Close()
	{
		_timeController.DisplayTimer(true);
	}
	
	public void ButtonEvt_StartTimer()
	{
		if (_isEdit) ButtonEvt_Edit();
		var time = GetMinutes();
		_timeController.Evt_StartTimer(time);
	}

	public void ButtonEvt_StopTimer()
	{
		_timerText.text = PlayerPrefs.HasKey(LWConfig.Timer) ? PlayerPrefs.GetString(LWConfig.Timer) : "00:00:00";
		_timeController.Evt_StopTimer();
	}
}

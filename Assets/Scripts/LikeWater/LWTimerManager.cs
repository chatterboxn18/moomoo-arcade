using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Jobs;
using Unity.Notifications.Android;
using UnityEngine;

public class LWTimerManager : MonoBehaviour
{
	private DateTime _futureTime;
	[SerializeField] private TextMeshProUGUI _homeTimer;
	[SerializeField] private CanvasGroup _timerCanvas;
	
	private bool _isRunning;
	
	public Action<string> Evt_UpdateTime = delegate {  };

	[SerializeField] private AudioController _audioController;

	private int _currentNotification;

	private void Start()
	{
		Evt_UpdateTime += UpdateHomeTimer;
		var channel = new AndroidNotificationChannel()
		{
			Id = "like_water",
			Name = "Like Water Channel",
			Importance =  Importance.High, 
			Description = "Channel for Like Water App"
		};
		AndroidNotificationCenter.RegisterNotificationChannel(channel);
	}
	
	private void CreateNotification(float time)
	{
		var notification = new AndroidNotification();
		notification.Title = "Like Water Reminder";
		notification.Text = "It's okay to take a break sometimes!";
		notification.SmallIcon = "icon_0";
		notification.FireTime = DateTime.Now.AddMinutes(time);

		_currentNotification = AndroidNotificationCenter.SendNotification(notification, "like_water");
	}

	public void DisplayTimer(bool on)
	{
		if (!_isRunning) return;
		StartCoroutine(FadeGroup(on));
	}

	private IEnumerator FadeGroup(bool on)
	{
		var time = 0f;
		if (on)
		{
			if (_timerCanvas.alpha >= 1) 
				yield break;
			while (time < 0.2f)
			{
				time += Time.deltaTime;
				_timerCanvas.alpha = Mathf.Lerp(0, 1, time / 0.2f);
				yield return null;
			}
		}
		else
		{
			if (_timerCanvas.alpha <= 0) 
				yield break;
			while (time < 0.2f)
			{
				time += Time.deltaTime;
				_timerCanvas.alpha = Mathf.Lerp(1, 0, time / 0.2f);
				yield return null;
			}
		}
	}
	
	private void UpdateHomeTimer(string time)
	{
		_homeTimer.text = time;
	}
	
	public void Evt_StartTimer(float time)
	{
		CreateNotification(time);
		_futureTime = DateTime.Now.AddMinutes(time);
		_isRunning = true;
		_audioController.FadeAudio(true, 0.2f);
	}

	public void Evt_StopTimer()
	{
		if (_currentNotification != -1)
			AndroidNotificationCenter.CancelNotification(_currentNotification);
		DisplayTimer(false);
		_isRunning = false;
		Evt_UpdateTime(PlayerPrefs.HasKey(LWConfig.Timer) ? PlayerPrefs.GetString(LWConfig.Timer) : "00:00:00");
		_audioController.FadeAudio(false, 0.2f);
	}

	private void Update()
	{
		if (!_isRunning)
			return;
		var time = _futureTime.Subtract(DateTime.Now);
		
		if (time.Minutes <= 0 && time.Hours <= 0 && time.Seconds <= 0)
		{
			DisplayTimer(false);
			_isRunning = false;
			Evt_UpdateTime("00:00:00");
			if (_audioController.Source.isPlaying)
				_audioController.FadeAudio(false, 0.2f);
			return;
		}
		Evt_UpdateTime($"{time.Hours:00}:{time.Minutes:00}:{time.Seconds:00}");
	}
}

using System;
using UnityEngine;
using UnityEngine.UI;

public class VideoDisplay: MonoBehaviour
{
	[SerializeField] private Image _thumbnail;
	[SerializeField] private Text _title;
	[SerializeField] private Text _date;
	[SerializeField] private SimpleButton _playButton;
	[SerializeField] private Image _win;

	public void SetVideo(Sprite thumbnail, string title, string date, Action button, bool isWin = false)
	{
		_thumbnail.sprite = thumbnail;
		_title.text = title;
		_date.text = date;
		_playButton.Evt_BasicEvent_Click += button;
		_win.gameObject.SetActive(isWin);
	}
}

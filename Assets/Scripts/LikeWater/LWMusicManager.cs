using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Notifications.Android;
using UnityEngine;

public class LWMusicManager : MonoBehaviour
{
	private List<LWResourceManager.VideoItem> _videoList;

	[SerializeField] private LWMediaCard _mediaPrefab;
	[SerializeField] private Transform _mediaParent;
	[SerializeField] private TextMeshProUGUI _cardTitle;
	
	
	private void Start()
	{
		_videoList = LWResourceManager.MusicList[0].Videos;
		_cardTitle.text = LWResourceManager.MusicList[0].Title;
		foreach (var video in _videoList)
		{
			var newVideo = Instantiate(_mediaPrefab, _mediaParent);
			newVideo.SetMediaCard(video);
		}
	}
}

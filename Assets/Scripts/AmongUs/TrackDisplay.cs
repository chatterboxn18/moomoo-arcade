using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrackDisplay : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _title;
	[SerializeField] private TextMeshProUGUI _subtitle;
	[SerializeField] private TextMeshProUGUI _description;
	[SerializeField] private TextMeshProUGUI _time;
	[SerializeField] private List<Links> _links;
	[SerializeField] private CanvasGroup _canvasGroup;

	private void SetTrack(string key, int index)
	{
		var currentAlbum = MMMAUResourceManager.Albums[key].TrackList[index];
		_title.text = currentAlbum.Title;
		_subtitle.text = currentAlbum.Subtitle;
		_description.text = currentAlbum.Description;
		_time.text = currentAlbum.Time;
		for (var i = 0; i < _links.Count; i++)
		{
			if (i > currentAlbum.Links.Count - 1)
			{
				_links[i].Button.gameObject.SetActive(false);
				continue;
			}
			var link = currentAlbum.Links[i].URL;
			_links[i].Icon.sprite = currentAlbum.Links[i].Icon;
			_links[i].Button.Evt_BasicEvent_Click += () =>
			{
				Application.OpenURL(link);
			};
			_links[i].Button.gameObject.SetActive(true);
		}
	}

	[Serializable]
	private struct Links
	{
		public Image Icon;
		public SimpleButton Button;
	}

	public IEnumerator SwitchTrack(string key, int index)
	{
		var timer = 0f;
		var totalTime = .25f;
		if (_canvasGroup.alpha > 0)
		{
			while (timer < totalTime)
			{
				_canvasGroup.alpha = (1 - timer / totalTime);
				timer += Time.deltaTime;
				yield return null;
			}
		}
		_canvasGroup.alpha = 0;
		SetTrack(key, index);
		timer = 0;
		while (timer < totalTime)
		{
			_canvasGroup.alpha = timer / totalTime;
			timer += Time.deltaTime;
			yield return null;
		}

		_canvasGroup.alpha = 1;
	}
}

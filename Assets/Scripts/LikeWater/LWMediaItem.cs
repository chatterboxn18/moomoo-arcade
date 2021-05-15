using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LWMediaItem : MonoBehaviour
{
	[SerializeField] private Image _thumbnail;
	[SerializeField] private TextMeshProUGUI _titleText;
	[SerializeField] private SimpleButton _button;
	private RectTransform _rectTransform;
	private float _videoHeight = 575f;

	private void Awake()
	{
		_rectTransform = GetComponent<RectTransform>();
		_button = GetComponent<SimpleButton>();
	}

	public void SetItem(bool isNews, string name, string url, Sprite sprite = null)
	{
		if (!isNews)
		{
			var size = _rectTransform.sizeDelta;
			_rectTransform.sizeDelta = new Vector2(size.x, _videoHeight);
			_thumbnail.transform.parent.gameObject.SetActive(true);
			_thumbnail.sprite = sprite;
		}

		_titleText.text = name;
		_button.Evt_BasicEvent_Click += () => Application.OpenURL(url);
	}

}

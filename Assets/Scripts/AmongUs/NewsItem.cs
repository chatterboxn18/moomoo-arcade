using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewsItem : MonoBehaviour
{
	[Serializable]
	public struct Item
	{
		public SimpleButton Button;
		public TextMeshProUGUI Title;
		public TextMeshProUGUI Body;
		private string Url;
		public TextMeshProUGUI Subtitle;
	}

	[SerializeField] private Item[] Items;

	[SerializeField] private float _spacing = 30f;
	[SerializeField] private int _itemCount;
	[SerializeField] private float _offset;
	[SerializeField] private bool _noSubtitle;

	public void CreateNews(List<MMMAUResourceManager.News> list)
	{
		var parent = transform.parent.GetComponent<RectTransform>();
		var rect = GetComponent<RectTransform>();
		rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, parent.rect.width);
		var counter = 0;
		var rec = list[0].Thumbnail.rect;
		var ratio = rec.height / rec.width;
		var width = 0f;
		if (_itemCount != 0)
			 width = (rect.rect.width - _spacing * (list.Count + 1))/_itemCount;
		var spacing = _spacing;
		foreach (var newItem in list)
		{
			Items[counter].Title.text = newItem.Title;
			Items[counter].Body.text = newItem.Body;
			if (!_noSubtitle)
				Items[counter].Subtitle.text = newItem.Subtitle;
			Items[counter].Button.MainImage.sprite = newItem.Thumbnail;
			Items[counter].Button.Evt_BasicEvent_Click += () => Application.OpenURL(newItem.Link);
			Items[counter].Button.RectTransform.anchoredPosition = new Vector2(spacing, 0);
			Items[counter].Button.RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
			Items[counter].Button.RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, width * ratio);
			spacing += width + _spacing;
			counter++;
		}
		var height = Items[0].Button.RectTransform.rect.height;
		rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height + _offset);
	}
}

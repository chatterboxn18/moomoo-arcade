using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AmongUs
{
	public class News : Task
	{
		[SerializeField] private List<NewsItem> _layouts;
		[SerializeField] private RectTransform _layoutParent;

		[SerializeField] private string _albumKey;

		protected override IEnumerator Start()
		{
			yield return base.Start();

			if (!MMMAUResourceManager.IsDataReady)
				yield return null;
			
			foreach (var item in MMMAUResourceManager.NewsList[_albumKey])
			{
				var currentLayout = Instantiate(_layouts[(int) item.Type], _layoutParent);
				currentLayout.CreateNews(item.NewsItems);
			}
		}
	}
}


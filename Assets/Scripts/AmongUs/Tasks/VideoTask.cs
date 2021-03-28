using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AmongUs.Tasks
{
	public class VideoTask : Task
	{
		private Dictionary<string, MMMAUResourceManager.AlbumPerformances> _performances = MMMAUResourceManager.PerformanceVideos;

		[SerializeField] private VideoDisplay _videoPrefab;
		[SerializeField] private NestedScroller _videoContainerPrefab;
		[SerializeField] private ScrollRect _videoContent;
		[SerializeField] private Image _background;
		protected override IEnumerator Start()
		{
			yield return base.Start();
			var containerSize = _background.rectTransform.rect.width - 100;
			_background.color = MMMAUResourceManager.Albums[_parameter].AlbumColor;
			foreach (var video in _performances[_parameter].Performances)
			{
				var videoContainer = Instantiate(_videoContainerPrefab, _videoContent.content);
				videoContainer.RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, containerSize);
				var headerText = videoContainer.GetComponentInChildren<Text>();
				videoContainer.DragEvt_Ongoing += _videoContent.OnDrag;
				videoContainer.DragEvt_End += _videoContent.OnEndDrag;
				videoContainer.DragEvt_Begin += _videoContent.OnBeginDrag;
				headerText.text = video.Key;
				foreach (var performance in video.Value)
				{
					var display = Instantiate(_videoPrefab, videoContainer.content);
					display.SetVideo(performance.Image, performance.Title, performance.Date, () =>
					{
						Application.OpenURL(performance.VideoURL);
					});
				}
			}
		}
	}
}

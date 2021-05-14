using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LWMenuController : LWBaseController
{
	[SerializeField] private CanvasGroup _buttonContainer;
	[SerializeField] private RectTransform _outlineMask;
	[SerializeField] private Transform _titlePosition;
	[SerializeField] private Transform[] _pages;
	[SerializeField] private Transform _pageTransform;

	private int _currentPage = -1;
	
	private Vector2 MinAnchor = new Vector2(0,0.05f);
	private Vector2 MaxAnchor = new Vector2(1,0.85f);
 
	public enum Pages
	{
		About = 0, 
		Reminder = 1, 
		News =3
	}

	private void OnEnable()
	{
		canvasGroup.LeanAlpha(1, LWConfig.FadeTime);
	}

	public void ButtonEvt_Open(int index)
	{
		_currentPage = index;
		TransitionOn((Pages) index);
	}

	public void ButtonEvt_Close()
	{
		//StartCoroutine(_pageTransform.gameObject.activeSelf ? TransitionOff((Pages) _currentPage) : FadeOn(false));
		if (_pageTransform.gameObject.activeSelf)
			TransitionOff((Pages) _currentPage);
		else
		{
			canvasGroup.LeanAlpha(0, LWConfig.FadeTime).setOnComplete(() =>
			{
				gameObject.SetActive(false);
			});
		}
		
	}

	private void TransitionOn(Pages to)
	{
		
		LeanTween.alphaCanvas(_buttonContainer, 0, LWConfig.FadeTime).setOnComplete(() =>
		{
			_buttonContainer.gameObject.SetActive(false);
		});
		_pageTransform.gameObject.SetActive(true);
		_pages[(int)to].gameObject.SetActive(true);
		_outlineMask.transform.localScale = new Vector3(1, 0, 1);
		_outlineMask.gameObject.LeanScaleY(1, LWConfig.FadeTime);
		//LeanTween.scaleY(_outlineMask.gameObject, 1, LWConfig.FadeTime);
		//LeanTween.value(_outlineMask.gameObject, )
	}
	
	private void TransitionOff(Pages to)
	{
		LeanTween.scaleY(_outlineMask.gameObject, 0, LWConfig.FadeTime).setOnComplete(() =>
		{
			_pageTransform.gameObject.SetActive(false);
			_pages[(int)to].gameObject.SetActive(false);
		});
		_buttonContainer.alpha = 0; 
		_buttonContainer.gameObject.SetActive(true);
		LeanTween.alphaCanvas(_buttonContainer, 1, LWConfig.FadeTime);
	}

	/*private IEnumerator TransitionOn(Pages to)
	{
		var timer = 0f;
		_buttonContainer.gameObject.SetActive(false);
		_outlineMask.anchorMin = new Vector2(MinAnchor.x, MinAnchor.y + .4f);
		_outlineMask.anchorMax = new Vector2(MaxAnchor.x, MaxAnchor.y - .4f);
		_outlineMask.gameObject.SetActive(true);
		_pageTransform.gameObject.SetActive(true);
		_pages[(int)to].gameObject.SetActive(true);
		while (timer < LWConfig.FadeTime)
		{
			var bottom = Mathf.Lerp(MinAnchor.y + .4f, MinAnchor.y, timer / LWConfig.FadeTime);
			var top = Mathf.Lerp(MaxAnchor.y - .4f, MaxAnchor.y, timer / LWConfig.FadeTime);
			_outlineMask.anchorMin = new Vector2(MinAnchor.x, bottom);
			_outlineMask.anchorMax = new Vector2(MaxAnchor.x, top);
			timer += Time.deltaTime;
			yield return null;
		}
		_outlineMask.anchorMin = new Vector2(MinAnchor.x, MinAnchor.y);
		_outlineMask.anchorMax = new Vector2(MaxAnchor.x, MaxAnchor.y);

	}
	
	private IEnumerator TransitionOff(Pages to)
	{
		var timer = 0f;
		_buttonContainer.gameObject.SetActive(true);
		
		
		while (timer < LWConfig.FadeTime)
		{
			var fade = Mathf.Lerp(0, 1, timer / LWConfig.FadeTime);
			_buttonContainer.alpha = fade;
			var bottom = Mathf.Lerp(MinAnchor.y, MinAnchor.y + .4f, timer / LWConfig.FadeTime);
			var top = Mathf.Lerp( MaxAnchor.y, MaxAnchor.y - .4f,timer / LWConfig.FadeTime);
			_outlineMask.anchorMin = new Vector2(MinAnchor.x, bottom);
			_outlineMask.anchorMax = new Vector2(MaxAnchor.x, top);
			timer += Time.deltaTime;
			yield return null;
		}
		_outlineMask.anchorMin = new Vector2(MinAnchor.x, MinAnchor.y + .4f);
		_outlineMask.anchorMax = new Vector2(MaxAnchor.x, MaxAnchor.y - .4f);
		_outlineMask.gameObject.SetActive(false);
		_pageTransform.gameObject.SetActive(false);
		_pages[(int)to].gameObject.SetActive(false);

	}*/
	
	private IEnumerator checkInternetConnection(Action<bool> action){
		WWW www = new WWW("http://google.com");
		yield return www;
		if (www.error != null) {
			action (false);
		} else {
			action (true);
		}
	}
	
	public void ButtonEvt_OpenURL(string url)
	{
		StartCoroutine(checkInternetConnection( (connected) =>
		{
			if (connected) Application.OpenURL(url);
			else
			{
				Debug.LogError("Missing Internet Connection");
			}
		}));
	}
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LWMusicController : LWBaseController
{
	private List<LWResourceManager.Music> _musicList;
	
	[SerializeField] private LWMusicPage _musicPagePrefab;
	[SerializeField] private RectTransform _currentPage; 
	[SerializeField] private RectTransform _nextPage;
	
	private Vector2 _nextLocation;
	private Vector2 _prevLocation;

	private int _currentIndex = 0;
	
	protected override void Start()
	{
		base.Start();
		var sizeDelta = _currentPage.rect.width;
		_nextLocation = new Vector2(sizeDelta, 0);
		_prevLocation = new Vector2( -1 * sizeDelta, 0);
		_musicList = LWResourceManager.MusicList;
		if (!PlayerPrefs.HasKey(LWConfig.PageIndexName))
		{
			PlayerPrefs.SetInt(LWConfig.PageIndexName, 0);
		}

		_currentIndex = PlayerPrefs.GetInt(LWConfig.PageIndexName);
		var page = Instantiate(_musicPagePrefab, _currentPage);
		page.SetPage(_currentIndex);
	}

	public void ButtonEvt_Next()
	{
		if (_currentIndex == _musicList.Count - 1)
			_currentIndex = 0;
		else
			_currentIndex++;
		var page = Instantiate(_musicPagePrefab, _nextPage);
		page.SetPage(_currentIndex);
		_nextPage.anchoredPosition = _nextLocation;
		LeanTween.moveX(_currentPage, _prevLocation.x, LWConfig.FadeTime);
		LeanTween.moveX(_nextPage, 0, LWConfig.FadeTime).setOnComplete(() =>
		{
			_nextPage.LeanSetLocalPosX(0);
			Destroy(_currentPage.GetChild(0).gameObject);
			Resources.UnloadUnusedAssets();
			_currentPage.anchoredPosition = new Vector2(0,0);
			page.transform.SetParent(_currentPage);
		});
		//_currentPage.LeanSetLocalPosX(_prevLocation.x);
		
	}

	public void ButtonEvt_Prev()
	{
		
	}
}

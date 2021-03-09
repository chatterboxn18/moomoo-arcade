using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlbumTask : MatchingTask
{
	[SerializeField] private Albums _albumData;
	private Albums.Album _currentAlbum;
	[SerializeField] private int _albumNumber;

	[SerializeField] private Transform _trackGroups;
	[SerializeField] private MatchCard _trackTitle;
	[SerializeField] private Image[] _albumImages;
	[SerializeField] private Text _titleText;
	
	protected override void CreateButtons()
	{
		
		_currentAlbum = _albumData.AlbumList[_albumNumber];
		
		_titleText.text = _currentAlbum.Title.ToUpper() + "\n" + "[TRACK LIST]";
		
		if (_order == null || _order.Count == 0)
		{
			_order = new List<int>();
			for (int i = 0; i < _currentAlbum.TrackList.Count; i++)
			{
				_order.Add(i);
			}
			_order = _order.Randomize();
		}
		
		_groupAnswers = new MatchCard[_order.Count];
		
		for (var i = 0; i < _order.Count; i++)
		{
			var button = Instantiate(_simpleButton, _buttonParent, false);
			button.Index = _order[i];
			var buttonColors = button.colors;
			buttonColors.highlightedColor = _currentAlbum.AlbumColor;
			button.colors = buttonColors;
			button.SetText(_currentAlbum.TrackList[_order[i]], 60, _currentAlbum.OutlineColor);
			button.Evt_CheckMatch = Evt_MatchButton;
			_groupButtons.Add(button);

			var title = Instantiate(_trackTitle, _trackGroups);
			title.SetText("0" + (i + 1) + "\n" + _currentAlbum.TrackList[i]);
			_groupAnswers[i] = title;
		}
		
		for (var i = 0; i < _albumImages.Length; i++)
		{
			if (i == 0)
				_albumImages[i].color = _currentAlbum.OutlineColor;
			else
				_albumImages[i].color = _currentAlbum.AlbumColor;
		}
	}
}

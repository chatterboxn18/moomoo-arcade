using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlbumTask : MatchingTask
{
	[SerializeField] private Albums _albumData;
	private Albums.Album _currentAlbum;
	[SerializeField] private int _albumNumber;

	[SerializeField] private VerticalLayoutGroup _trackGroups;
	[SerializeField] private MatchCard _trackTitle;
	[SerializeField] private Image[] _albumImages;
	[SerializeField] private Text _titleText;

	[SerializeField] private bool _isQuiz;
	[SerializeField] private TrackDisplay _trackDisplay;
	
	protected override void CreateButtons()
	{
		_currentAlbum = _albumData.AlbumList[_albumNumber];
		_titleText.text = _currentAlbum.Title.ToUpper() + "\n" + "[TRACK LIST]";

		if (!_isQuiz)
		{
			_trackGroups.spacing = 10;
			for (var i = 0; i < _currentAlbum.TrackList.Count; i++)
			{
				/*var title = Instantiate(_trackTitle, _trackGroups);
				title.SetText("0" + (i + 1) + "\n" + _currentAlbum.TrackList[i].Title);
				title.Evt_Reveal();*/

				var button = Instantiate(_simpleButton, _trackGroups.transform);
				var buttonColors = button.colors;
				buttonColors.highlightedColor = Color.black;
				button.colors = buttonColors;
				button.SetText("0" + (i + 1) + "\n" + _currentAlbum.TrackList[i].Title, 30, _currentAlbum.OutlineColor);
				var index = i;
				button.Evt_BasicEvent_Click += () => { StartCoroutine(_trackDisplay.SwitchTrack(_currentAlbum.Title, index)); };

				//var trackDisplay = Instantiate(_trackPrefab, _buttonParent);
				//trackDisplay.SetTrack(_currentAlbum.Title, i);
			}
			return;
		}

		_trackGroups.spacing = 60;
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
			button.SetText(_currentAlbum.TrackList[_order[i]].Title, 60, _currentAlbum.OutlineColor);
			button.Evt_CheckMatch = Evt_MatchButton;
			_groupButtons.Add(button);

			var title = Instantiate(_trackTitle, _trackGroups.transform);
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

	private void CreateSongInfo()
	{
		
	}
}

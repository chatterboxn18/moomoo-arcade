using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PinPadTask : Task
{
	[SerializeField] private MatchButton _matchButtonPrefab;
	[SerializeField] private RectTransform _pinPadTransform;
	[SerializeField] private Text _displayText;
	private string _inputText = "";

	[Serializable]
	public struct Pins
	{
		[TextArea] public string Clue;
		public string Pin;
	}

	[SerializeField] private Pins[] _pinsList;
	private Pins _currentPin;
	[SerializeField] private Text _clueText;
	
	protected override void Start()
	{
		base.Start();
		for (int i = 0; i < 9; i++)
		{
			SetButton(i + 1);
		}
		SetButton(-1, "<");
		SetButton(0);
		SetButton(-2, "C");

		var random = Random.Range(0, _pinsList.Length);
		_currentPin = _pinsList[random];
		_clueText.text = _currentPin.Clue;
	}

	protected override void Update()
	{
		_displayText.text = _inputText;
		base.Update();
	}

	private void SetButton(int index, string text = "")
	{
		var button = Instantiate(_matchButtonPrefab, _pinPadTransform);
		button.Index = index;
		if (string.IsNullOrEmpty(text))
			button.SetText(index.ToString());
		else
			button.SetText(text);
		button.Evt_CheckMatch += Evt_EnteredButton;
	}

	private void CheckInput()
	{
		if (_inputText == _currentPin.Pin)
		{
			Evt_FinishTask("","");
		}
	}
	
	public void Evt_EnteredButton(int index, Action onComplete)
	{
		
		if (index == -1)
			_inputText = _inputText.Substring(0, _inputText.Length-1);
		else if (index == -2)
			_inputText = "";
		else if (_inputText.Length >= 4)
			return;
		else
			_inputText += index;
		CheckInput();
	}
}

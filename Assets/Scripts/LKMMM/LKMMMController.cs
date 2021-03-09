using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LKMMMController : MonoBehaviour
{
	[SerializeField] private LanguageSet[] _wordList;

	public enum WordType
	{
		Noun = 0, 
		Adv = 1, 
		Adj = 2, 
		Verb = 3, 
		Other = 4
	}

	public enum MMMSpeaker
	{
		Solar = 0, 
		Moonbyul = 1, 
		Wheein = 2, 
		Hwasa = 3
	}
	
	[Serializable]
	public class LanguageSet
	{
		public string English;
		public string EnglishSentece;
		public string Korean;
		public string KoreanSentence;
		public AudioClip Audio;
		public WordType Type;
		public MMMSpeaker Speaker;
	}

	[SerializeField] private Text _spokenKoreanText;
	[SerializeField] private SuggestedAnswerButton _buttonPrefab;
	[SerializeField] private InputField _answerInput;
	
	[Header("Word Detail Components")] 
	[SerializeField] private Text _koreanWord;
	[SerializeField] private Text _englishWord;
	[SerializeField] private Text _koreanSentence;
	[SerializeField] private Text _englishSentence;

	private LanguageSet _currentWordSet;

	private AudioSource _audioSource;

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	private void Start()
	{
		_currentWordSet = _wordList[0];
		SetupWord();
	}

	private void SetupWord()
	{
		_spokenKoreanText.text = string.IsNullOrEmpty(_currentWordSet.Korean) ?_currentWordSet.KoreanSentence :  _currentWordSet.Korean;
		_koreanWord.text = _currentWordSet.Korean;
		_englishWord.text = _currentWordSet.English;
		_koreanSentence.text = _currentWordSet.KoreanSentence;
		_englishSentence.text = _currentWordSet.EnglishSentece;
	}

	public void ButtonEvt_CheckInput()
	{
		var input = _answerInput.text.ToLower().Replace(" ", string.Empty);
		var current = _currentWordSet.English.ToLower().Replace(" ", string.Empty);
		if (input == current)
		{
			Debug.Log("Correct");
			CleanWord();
		}
	}

	public void ButtonEvt_PlayAudioClip()
	{
		if (_currentWordSet == null) return;

		_audioSource.clip = _currentWordSet.Audio;
		_audioSource.Play();
	}

	private void CleanWord()
	{
		_answerInput.text = string.Empty;
	}
	
}

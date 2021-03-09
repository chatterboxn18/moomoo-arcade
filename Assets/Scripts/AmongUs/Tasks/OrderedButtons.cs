using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderedButtons : Task
{
	[SerializeField] private GridLayoutGroup _gridLayout;
	[SerializeField] private MatchButton _buttonPrefab;

	private int _buttonNumber;
	private int _index;

	protected override void Start()
	{
		base.Start();
		for (var i = 0; i < _buttonNumber; i++)
		{
			var button = Instantiate(_buttonPrefab, _gridLayout.transform);
			button.Index = i;
			button.Evt_CheckMatch += CheckNumber;
		}
	}

	private void CheckNumber(int index, Action onSuccess)
	{
		if (_index == index)
		{
			_index++;
			return;
		}

		_index = 0; 
		Debug.Log("Incorrect order");
	}
}

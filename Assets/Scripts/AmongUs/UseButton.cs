using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseButton : SimpleButton
{
	[SerializeField] private GameObject[] _useTypes;
	public void SetButtonType(TaskManager.TaskType type)
	{
		TurnTypeOn((int) type);
		SetVisibility(true);
	}

	private void TurnTypeOn(int index)
	{
		for (int i = 0; i < _useTypes.Length; i++)
		{
			if (index == i)
			{
				_useTypes[i].SetActive(true);
				continue;
			}
			_useTypes[i].SetActive(false);
		}
	}
}

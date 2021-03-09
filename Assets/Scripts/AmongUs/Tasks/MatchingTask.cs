using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MatchingTask : Task
{

	[SerializeField] protected List<int> _order;
	[SerializeField] private int _itemNumber;
	private int _index = 0;

	[SerializeField] protected MatchButton _simpleButton;
	[SerializeField] protected Transform _buttonParent;

	[SerializeField] protected MatchCard[] _groupAnswers;
	protected List<MatchButton> _groupButtons = new List<MatchButton>();

	[SerializeField] private bool isButtonReveal;
	
	protected override void Awake()
	{
		base.Awake();
		CreateButtons();
	}

	protected virtual void CreateButtons()
	{
		if (_order == null || _order.Count == 0)
		{
			_order = new List<int>();
			for (int i = 0; i < _itemNumber; i++)
			{
				_order.Add(i);
			}

			_order = _order.Randomize();
		}
		
		for (var i = 0; i < _order.Count; i++)
		{
			var button = Instantiate(_simpleButton, _buttonParent, false);
			button.Index = _order[i];
			button.SetText(_order[i].ToString());
			button.Evt_CheckMatch = Evt_MatchButton;
			_groupButtons.Add(button);
		}
	}

	public void Evt_MatchButton(int index, Action onSuccess)
	{
		if (index == _index)
		{
			if (!isButtonReveal)
				_groupAnswers[index].Evt_Reveal();
			else
				onSuccess();
			_index++;
			if (_index == _groupAnswers.Length || _index == _groupButtons.Count)
			{
				Evt_FinishTask("","");
			}
		}
	}
	
}

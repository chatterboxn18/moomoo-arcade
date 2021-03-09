using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SuggestedAnswerButton : SimpleButton
{
	private string _answer;
	public Action<string> Evt_CheckAnswer = delegate { };
	
	public override void OnPointerUp(PointerEventData eventData)
	{
		base.OnPointerUp(eventData);
		Evt_CheckAnswer(_answer);
	}

	public void SetAnswer(string answer)
	{
		_answer = answer;
	}


}

using System;
using System.Collections;
using System.Collections.Generic;
using TravelRPG;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillButton : SimpleButton
{
	[SerializeField] private Image _icon;
	private Skill _skill;
	public Action<Skill> Evt_UseSkill = delegate {};

	public void SetIcon(Sprite sprite)
	{
		_icon.sprite = sprite;
	}

	public void SetSkill(Skill skill)
	{
		_skill = skill;
	}

	public Skill GetSkill()
	{
		return _skill;
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		base.OnPointerUp(eventData);
		Evt_UseSkill(_skill);
	}
}

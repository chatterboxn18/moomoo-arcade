using System.Collections;
using System.Collections.Generic;
using TravelRPG;
using UnityEngine;

public class BattleController : MonoBehaviour
{
	[SerializeField] private Travel_Adventurer _player;
	[SerializeField] private Travel_Character _enemy;
	[SerializeField] private Transform _skillTray;
	[SerializeField] private SkillButton _skillButtonPrefab;

	private int _currentEnemyIndex;
	
	void Start()
	{
		foreach (var skill in _player.GetSkills())
		{
			var button = Instantiate(_skillButtonPrefab, _skillTray);
			button.SetSkill(skill);
			button.Evt_UseSkill = Evt_ActivateSkill;
		}
	}

	public void Evt_ActivateSkill(Skill skill)
	{
		skill.UseSkill(Skill.Type.Physical, _player, _enemy);
	}

}

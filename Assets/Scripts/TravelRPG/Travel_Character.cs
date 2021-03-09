using System;
using UnityEngine;
using UnityEngine.UI;

namespace TravelRPG
{
	public class Travel_Character : MonoBehaviour
	{
		//Info
		protected int _index;
		protected CharacterType _type;
		
		//Stats
		protected float _health = 100;
		protected float _totalHealth = 100;
		protected float _speed;
		protected float _defense;
		protected float _attack;
		protected float _magic;
		protected float _resistance; 
		
		//Items 
		[SerializeField] private Image _healthBar;
		
		//Abilities
		[SerializeField] protected Skill[] _skills;

		public Skill[] GetSkills()
		{
			return _skills;
		}
		
		public void IncrementHealth(bool _isIncreasing, float amount)
		{
			_health += amount * (_isIncreasing ? 1:-1);
			_healthBar.fillAmount = _health / _totalHealth;
		}

		public float GetHealth()
		{
			return _health;
		}
		
		public float GetDefense()
		{
			return _defense;
		}
		
		public enum CharacterType
		{
			Player = 0, 
			Enemy = 1, 
			NPC = 2
		}
		

	}
}

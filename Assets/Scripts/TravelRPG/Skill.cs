using System;
using UnityEngine;

namespace TravelRPG
{
	[Serializable]
	public class Skill
	{
		[SerializeField] private Sprite _icon;
		[SerializeField] private bool _isPlayer;
		[SerializeField] private Type _type;
		[SerializeField] private float _physicalDMG; 
		[SerializeField] private float _magicDMG;
		[SerializeField] private Tuple<Status, float> _statusEffect;

		public enum Status
		{
			Health = 0, 
			MP = 1, 
			ATK = 2, 
			DEF = 3, 
			MAG = 4
		}
		
		public enum Type
		{
			None = 0, 
			Physical = 1,
			Magic = 2, 
			Status = 3			
		}

		public void UseSkill(Type type, Travel_Character user, Travel_Character receiver)
		{
			if (type == Type.Physical)
			{
				receiver.IncrementHealth(false, _physicalDMG);
			}
		}
	}

}
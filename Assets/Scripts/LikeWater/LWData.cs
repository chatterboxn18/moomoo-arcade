using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class LWData
{
	private static LWData _current;

	public static LWData current
	{
		get
		{
			if (_current == null)
				_current = new LWData();
			return _current;
		}
	}

	public void Setup(LWData data)
	{
		_current = data;
	}

	public int Coins;

	private Dictionary<string, List<FlowerMonth>> _flowerDictionary;

	public Dictionary<string, List<FlowerMonth>> FlowerDictionary
	{
		get
		{
			if (_flowerDictionary == null) 
				return new Dictionary<string, List<FlowerMonth>>();
			return _flowerDictionary;
		}
		set => _flowerDictionary = value;
	}

	private FlowerMonth _currentFlower; 
	public FlowerMonth CurrentFlower {
		get
		{
			if (_currentFlower == null) 
				return new FlowerMonth();
			return _currentFlower;
		}
		set => _currentFlower = value;
	}

	public string MainFlower;
	
	[Serializable]
	public class FlowerMonth
	{
		public int PlantIndex;
		public int SpriteIndex;
		public string Date;
		public string Attributes;
		public string Goal;

		public FlowerMonth() {
			SpriteIndex = 0;
			PlantIndex = -1;
		}
	}


	[Serializable]
	public class Drink
	{
		public int SpriteIndex;
		public string Color;
		public string Attributes;
	}
	public List<Drink> DrinkAttributes
	{
		get
		{
			if (_drinkAttributes == null)
			{
				_drinkAttributes = new List<Drink>();
			}
			return _drinkAttributes;
		}
	}

	private List<Drink> _drinkAttributes;
}

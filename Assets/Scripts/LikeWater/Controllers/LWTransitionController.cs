using System;
using System.Collections;
using System.Collections.Generic;
using LikeWater;
using UnityEngine;

public class LWTransitionController : MonoBehaviour
{
	[Header("Controllers")]
	public LWWaterController waterController; 
	public LWFlowerController potController; 
	public LWMusicController musicController; 
	public LWTimerController timerController; 
	public LWPopupController popController;
	public LWShopController shopController;

	[Header("MenuItems")] 
	public CanvasGroup groupMenu;	

	public enum Controllers
	{
		Water = 0, 
		Pot = 1, 
		Music = 2, 
		Timer = 3, 
		Popup = 4,
		Shop = 5
	}
	
	public enum MenuPages
	{
		About, 
		Reminders, 
		News, 
		Stream
	}

	private static Dictionary<Controllers, LWBaseController> _dictionary;
	private static Dictionary<MenuPages, CanvasGroup> _menuDictionary;
	
	private void Start()
	{
		_dictionary = new Dictionary<Controllers, LWBaseController>
		{
			{Controllers.Water, waterController},
			{Controllers.Pot, potController},
			{Controllers.Music, musicController},
			{Controllers.Timer, timerController},
			{Controllers.Popup, popController},
			{Controllers.Shop, shopController}
		};
	}
	
	public static void TransitionTo(Controllers from, Controllers to)
	{
		
		_dictionary[from].TransitionTo(() => { _dictionary[to].TransitionOn(true); });
	}

	public static void TransitionOn(Controllers on)
	{
		_dictionary[on].TransitionOn();
	}
	
	public static void TransitionOff(Controllers off)
	{
		if (_dictionary[off].isActiveAndEnabled)
			_dictionary[off].TransitionOff();
	}

	public static void MenuTransitionOn()
	{
		
	}
}

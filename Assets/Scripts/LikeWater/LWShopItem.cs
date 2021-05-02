using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LWShopItem : MonoBehaviour
{
	[SerializeField] private Image _icon;
	[SerializeField] private TextMeshProUGUI _priceText;
	[SerializeField] private TextMeshProUGUI _earnsText;
	private SimpleButton _button;
	public SimpleButton Button => _button;
	private LWResourceManager.Flower _flower;

	private void Awake()
	{
		_button = GetComponent<SimpleButton>();
	}

	public void SetItem(Sprite sprite, LWResourceManager.Flower flower)
	{
		_icon.sprite = sprite;
		_priceText.text = flower.Cost.ToString();
		_earnsText.text = "+ " + flower.Earns;
		_flower = flower;
	}

	public void ButtonEvt_BuyFlower()
	{
		//extra check but it should be -1
		var currentFlower = LWData.current.CurrentFlower;
		var date = DateTime.Parse(currentFlower.Date);
		var dict = LWData.current.FlowerDictionary;
		if (currentFlower.PlantIndex == -1)
		{
			currentFlower.PlantIndex = _flower.Index;
			currentFlower.SpriteIndex = 0;

			dict[date.Month + "/" + date.Year][date.Day - 1] = currentFlower;
		}

		LWData.current.FlowerDictionary = dict;

		SerializationManager.Save(LWConfig.DataSaveName, LWData.current);
		/*
		if (LWData.current.CurrentFlower.PlantIndex == -1)
		{
			var date = DateTime.Today;
			var flower = LWData.current.CurrentFlower;
			if (string.IsNullOrEmpty(flower.Date))
				flower.Date = date.Day.ToString();
			flower.PlantIndex = _flower.Index;
			var dict = LWData.current.FlowerDictionary;
			var key = date.Month + "/" + date.Year;
			dict[key][date.Day] = flower;
			LWData.current.CurrentFlower = flower;
			LWData.current.FlowerDictionary = dict;
		}*/
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LikeWater
{
	public class LWPopupController : LWBaseController
	{
		[SerializeField] private LWAttribute[] _attributes;
		[SerializeField] private LWFlowerGroup _currentFlower;

		private void OnEnable()
		{
			DateTime.TryParse(LWData.current.MainFlower, out var date);
			var key = date.Month + "/" + date.Year;
			var flower = LWData.current.FlowerDictionary;
			if (flower.ContainsKey(key))
			{
				var currentFlower = flower[key][date.Day - 1];

				var flowers = LWResourceManager.Sprites[currentFlower.PlantIndex];
				var spriteIndex = currentFlower.SpriteIndex * 2;
				var sprites = new[] {flowers[spriteIndex], flowers[spriteIndex + 1]};


				_currentFlower.SetPlant(0, sprites, currentFlower.Date);

				//prepare attributes
				if (!currentFlower.Attributes.Contains(":"))
				{
					foreach (var attribute in _attributes)
					{
						attribute.SetAttributeCount(0);
					}

					return;
				}

				var attributes = currentFlower.Attributes.Split(',');
				var counter = 0;
				foreach (var attribute in attributes)
				{
					var values = attribute.Split(':');
					_attributes[counter].SetAttributeCount(int.Parse(values[1]));
					counter++;
				}
			}
		}

		public void Evt_OpenPopup()
		{
			var date = DateTime.Parse(LWData.current.MainFlower);
			if (LWData.current.FlowerDictionary.ContainsKey(date.Month + "/" + date.Year) &&
			    LWData.current.FlowerDictionary[date.Month + "/" + date.Year][date.Day - 1].PlantIndex != -1)
				gameObject.SetActive(true);
		}

		public void ButtonEvt_Display()
		{
			var current = LWData.current.MainFlower;
			LWData.current.DisplayFlower = current;
			SerializationManager.Save(LWConfig.DataSaveName, LWData.current);
		}


		public void ButtonEvt_Edit()
		{
			LWTransitionController.TransitionOff(LWTransitionController.Controllers.Popup);
			LWTransitionController.TransitionTo(LWTransitionController.Controllers.Pot,
				LWTransitionController.Controllers.Water);
		}

		public void Evt_ClosePopup()
		{
			gameObject.SetActive(false);
		}
	}
}
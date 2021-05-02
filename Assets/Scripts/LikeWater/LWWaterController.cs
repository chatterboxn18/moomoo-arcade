using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LWWaterController : MonoBehaviour
{
	private Animator _animator;
	
	[Header("Water Items")]
	[SerializeField] private LWDrinkController[] _drinkControllers;
	private LWDrinkController _selectedDrink;
	[SerializeField] private Transform _attributesContainer;
	private int _goal = 64;
	private int _drinkCount = 0;
	[SerializeField] private Image _fillOutline;
	private int _maxAttributeCount = 6;

	[SerializeField] private LWFlowerGroup _activeFlower;
	[SerializeField] private LWAttribute _attributePrefab;
	[SerializeField] private TextMeshProUGUI _drinkText;

	//Edit Values 
	[Header("Edit Items")]
	[SerializeField] private TMP_InputField _colorInput;
	[SerializeField] private TMP_InputField _goalInput;
	private Color _previousColor = Color.white;
	private bool _isEditMode;
	[SerializeField] private Attributes[] _attributes;
	private int _selectedAttribute;
	[SerializeField] private Color _selectedColor;
	[SerializeField] private Transform _drinkIconContainer;
	[SerializeField] private SimpleButton _drinkIconPrefab;
	
	[Serializable]
	public struct Attributes
	{
		public string Name;
		public LWAttribute Script;
		public Image Outline;
	}

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		
	}

	private void Start()
	{
		var drinks = LWResourceManager.DrinkIcons;
		for (var i = 0; i < drinks.Count; i++)
		{
			var drinkButton = Instantiate(_drinkIconPrefab, _drinkIconContainer);
			drinkButton.image.sprite = drinks[i];
			var i1 = i;
			drinkButton.Evt_BasicEvent_Click += () => { ButtonEvt_UpdateDrink(drinks[i1], i1);};
		}
	}

	
	private void OnEnable()
	{
		if (!PlayerPrefs.HasKey(LWConfig.TodayDateKey))
		{
			PlayerPrefs.SetString(LWConfig.TodayDateKey, DateTime.Today.ToShortDateString());
			PlayerPrefs.SetInt(LWConfig.DrinkCount, 0);
		}
		
		//reset drink count every day
		if (PlayerPrefs.GetString(LWConfig.TodayDateKey) != DateTime.Today.ToShortDateString())
		{
			_drinkCount = 0;
			PlayerPrefs.SetInt("DrinkCount", 0);
			PlayerPrefs.SetString(LWConfig.TodayDateKey, DateTime.Today.ToShortDateString());
		}

		if (PlayerPrefs.HasKey(LWConfig.DrinkCount))
			_drinkCount = PlayerPrefs.GetInt(LWConfig.DrinkCount);
		if (PlayerPrefs.HasKey(LWConfig.Goal))
			_goal = PlayerPrefs.GetInt(LWConfig.Goal);
		else
			PlayerPrefs.SetInt(LWConfig.Goal, _goal);
		_fillOutline.fillAmount = _drinkCount / (float) _goal;
		_drinkText.text = _drinkCount + "/" + _goal;
		Update_DrinkFill();

		var index = 0;
		if (_selectedDrink)
		{
			index = _selectedDrink.Index;
		}
		for (var i = 0; i < _drinkControllers.Length; i++)
		{
			LWData.Drink drink;
			if (i < LWData.current.DrinkAttributes.Count)
			{
				drink = LWData.current.DrinkAttributes[i];
			}
			else
			{
				var attributes = "";
				drink = new LWData.Drink
				{
					SpriteIndex = 0
				};
				foreach (var item in _attributes)
				{
					attributes += item.Name + ":0,";
				}

				attributes = attributes.Substring(0, attributes.Length - 1);
				drink.Attributes = attributes;
				LWData.current.DrinkAttributes.Add(drink);
			}
			_drinkControllers[i].Setup(_attributes);
			_drinkControllers[i].Evt_Selected(i == index);
			_drinkControllers[i].Image.sprite = LWResourceManager.DrinkIcons[drink.SpriteIndex];
			_drinkControllers[i].Evt_UpdateDrinkIndex(drink.SpriteIndex);
		}

		_selectedDrink = _drinkControllers[index];
		
		SetActiveFlower();
		// fill attributes;
		UpdateEditAttributes();
	}

	private void SetActiveFlower()
	{
		// grabs data from current flower 
		if (LWData.current.CurrentFlower.PlantIndex == -1)
		{
			Debug.LogError("There isn't a current flower? There should have been an earlier check");
			return;
		}
		var data = LWData.current.CurrentFlower;
		var spriteIndex = data.SpriteIndex * 2;
		var plantSprites = LWResourceManager.Sprites[data.PlantIndex];
		var sprites = new[] {plantSprites[spriteIndex], plantSprites[spriteIndex + 1]};
		_activeFlower.SetPlant(0, sprites, data.Date);
	}
	
	public void ButtonEvt_Drink()
	{
		var drinkAmount = 0;
		if (_selectedDrink.Attributes.ContainsKey(LWConfig.AttributeWaterKey))
			drinkAmount = _selectedDrink.Attributes[LWConfig.AttributeWaterKey];
		_drinkCount += drinkAmount;
		PlayerPrefs.SetInt(LWConfig.DrinkCount, _drinkCount);
		_drinkText.text = _drinkCount  + "/" + _goal;
		
		Update_DrinkFill(); //updates the Sprite Index in here so this must come first?
		SaveDrink();
	}

	private void SaveDrink()
	{
		var date = DateTime.Parse(LWData.current.CurrentFlower.Date);
		var newFlower = LWData.current.CurrentFlower;
		newFlower.Goal = _drinkCount + "/" + _goal;
		LWData.current.CurrentFlower = newFlower;
		LWData.current.FlowerDictionary[date.Month + "/" + date.Year][date.Day] = LWData.current.CurrentFlower;
		SerializationManager.Save(LWConfig.DataSaveName, LWData.current);
	}
	
	private void Update_DrinkFill()
	{
		if (_drinkCount < _goal)
		{
			var fillAmount = (float) _drinkCount / _goal;
			_fillOutline.fillAmount = fillAmount;
			var newFlower = LWData.current.CurrentFlower;
			if (fillAmount >= 0.5f)
			{
				newFlower.SpriteIndex = 1;
			}
			else if (fillAmount >= 1)
			{
				newFlower.SpriteIndex = 2;
			}
			else
			{
				newFlower.SpriteIndex = 0;
			}

			LWData.current.CurrentFlower = newFlower;
		}
		else
		{
			_fillOutline.fillAmount = 1;
		}
	}
	
	public void ButtonEvt_SelectDrink(int index)
	{
		_selectedDrink = _drinkControllers[index];
		for (int i = 0; i < _drinkControllers.Length; i++)
		{
			_drinkControllers[i].Evt_Selected(i == index);
		}

		UpdateEditAttributes();
		
	}

#region Edit
	
	public void ButtonEvt_Edit(bool on)
	{
		if (on)
		{
			_isEditMode = on;
			ButtonEvt_SelectAttribute(0);
			_animator.SetBool("IsEdit", _isEditMode);
		}
		else
		{
			SaveAttributes();
			_isEditMode = on;
			_animator.SetBool("IsEdit", _isEditMode);
			PlayerPrefs.SetInt("Goal", _goal);
			Update_DrinkFill();
			UpdateEditAttributes();
		}
	}
	
	private void UpdateEditAttributes()
	{
		for (var i = 0; i < _attributes.Length; i++)
		{
			_attributes[i].Script.SetAttributeCount(_selectedDrink.Attributes[_attributes[i].Name]);
		}
	}
	
	private void SaveAttributes()
	{
		for (var i = 0; i <_drinkControllers.Length; i++)
		{
			var attributeString = "";
			foreach (var value in _drinkControllers[i].Attributes)
			{
				attributeString += value.Key + ":" + value.Value + ",";
			}

			attributeString = attributeString.Substring(0, attributeString.Length - 1);
			Debug.Log(attributeString);
			if (i < LWData.current.DrinkAttributes.Count)
			{
				LWData.current.DrinkAttributes[i].Attributes = attributeString;
				LWData.current.DrinkAttributes[i].SpriteIndex = _drinkControllers[i].SpriteIndex;
			}
			else
			{
				var drink = new LWData.Drink
				{
					Attributes = attributeString, SpriteIndex = _drinkControllers[i].SpriteIndex
				};
				LWData.current.DrinkAttributes.Add(drink);
			}
			Debug.Log("Current Data at index " + i + " is: " + LWData.current.DrinkAttributes[i].Attributes);
			SerializationManager.Save(LWConfig.DataSaveName, LWData.current);
		}
	}

	public void ButtonEvt_ChangeGoal(bool isAdd)
	{
		if (isAdd)
		{
			if (_goal < 12)
			{
				_goal++;
			}
		}
		else
		{
			if (_goal != 0)
			{
				_goal--;
			} 
		}
		_drinkText.text = _goal + " DRINKS";
	}

	public void ButtonEvt_UpdateGoal()
	{
		if (_goalInput.textComponent.text.Length > 0)
		{
			var goal = int.Parse(_goalInput.textComponent.text);
			if (goal <= 0)
			{
				Debug.LogError("Goal can't be 0, that's pretty sad");
				return;
			}
			_goal = goal;
			_goalInput.textComponent.text = "";
			PlayerPrefs.SetInt(LWConfig.Goal, goal);
		}
	}
	
	private void ButtonEvt_UpdateDrink(Sprite sprite, int index)
	{
		_selectedDrink.Image.sprite = sprite;
		Debug.Log(index);
		_selectedDrink.Evt_UpdateDrinkIndex(index);
	}


	public void ButtonEvt_ChangeAttribute(bool isAdd)
	{
		if (isAdd)
		{
			_selectedDrink.AddAttribute(_attributes[_selectedAttribute].Name);
			//var attribute = Instantiate(_attributePrefab, _attributesContainer);
			//attribute.SetSprite(_selectedAttribute);
		}
		else
		{
			var index = _selectedDrink.RemoveAttribute(_attributes[_selectedAttribute].Name);
			if (index == 0)
			{
				_attributes[_selectedAttribute].Script.gameObject.SetActive(false);
				//Destroy(_attributesContainer.GetChild(index).gameObject);
			}
		}
		UpdateEditAttributes();
	}

	public void ButtonEvt_SelectAttribute(int index)
	{
		for (var i = 0; i < _attributes.Length; i++)
		{
			_attributes[i].Outline.color = (index == i) ? _selectedColor : Color.white;
		}

		_selectedAttribute = index;
	}

	public void Evt_ColorInputChange(string text)
	{
		if (text.Length == 6)
		{
			var hex = "#" + text;
			var value = Color.white;
			if (ColorUtility.TryParseHtmlString(hex, out value))
			{
				_colorInput.text = ColorUtility.ToHtmlStringRGB(value);
				_colorInput.textComponent.color = value;
				_previousColor = value;
			}
			else
			{
				_colorInput.text = ColorUtility.ToHtmlStringRGB(_previousColor);
			}
			
		}
	}

	#endregion
	
}

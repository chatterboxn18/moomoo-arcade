using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SimpleButton))]
public class LWDrinkController : MonoBehaviour
{
	private SimpleButton _button;

	public int Index => _index;
	[SerializeField] private int _index;
	public Image Image => _image;
	private Color _color;
	private Dictionary<string, int> _drinkAttributes = new Dictionary<string, int>();
	public Dictionary<string, int>  Attributes => _drinkAttributes;

	private bool _isSelected;

	public int SpriteIndex => _spriteIndex;

	private int _spriteIndex;
	
	[SerializeField] private Image _outline;
	[SerializeField] private Color _selectedColor;
	
	[SerializeField] private Image _image;
	
	private void Awake()
	{
		_button = GetComponent<SimpleButton>();
	}

	public void Evt_UpdateDrinkIndex(int index)
	{
		_spriteIndex = index;
	}
	
	public void Setup(LWWaterController.Attributes[] attributes)
	{
		// I don't have a nice dynamic way to add extra attributes yet :<
		if (_index < LWData.current.DrinkAttributes.Count)
		{
			var list = LWData.current.DrinkAttributes[_index].Attributes.Split(',');
			foreach (var item in list)
			{
				var items = item.Split(':');
				if (int.TryParse(items[1], out var number))
					_drinkAttributes[items[0]] = number;
				else
				{
					Debug.LogError("Failed to parse data correctly");
					_drinkAttributes[items[0]] = 0;
				}
			}
		}
		else
		{
			foreach (var item in attributes)
			{
				_drinkAttributes[item.Name] = 0;
			}
		}
	}

	public void Evt_Selected(bool on)
	{
		_outline.color = on ? _selectedColor : Color.white;
	}

	public int RemoveAttribute(string attribute)
	{
		return _drinkAttributes[attribute] <= 0 ? 0 : _drinkAttributes[attribute]--;
	}

	public void AddAttribute(string attribute)
	{
		_drinkAttributes[attribute]++;
	}

}

﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LWFlowerGroup : MonoBehaviour
{
	[SerializeField] private Plant[] _plants;

	[Serializable]
	public struct Plant
	{
		public bool isActive;
		public Image Image;
		public Sprite[] Sprites;
		public TextMeshProUGUI DateTag;
		public int Day;
	}
	
	public bool IsFull {get
	{
		var isFull = _plants.Count(plant => plant.Image.color.a >= 1);
		return isFull == 3;
	}}

	private int _frameCount = 60;
	private int _frame = 0;
	private int _currentFrame = 0;

	public void SetPlant(int indexOnShelf, Sprite[] sprites, string date = "")
	{
		if (!string.IsNullOrEmpty(date))
		{
			_plants[indexOnShelf].DateTag.text = date;
		}

		var color = _plants[indexOnShelf].Image.color;
		_plants[indexOnShelf].Image.sprite = sprites[0];
		_plants[indexOnShelf].Image.color = color.SetAlpha(1);
		_plants[indexOnShelf].Sprites = sprites;
		_plants[indexOnShelf].isActive = true;
	}

	public void SetDate(int indexOnShelf, int day)
	{
		_plants[indexOnShelf].Day = day;
	}
	
	public Plant GetPlant(int index)
	{
		if (index < _plants.Length)
			return _plants[index];
		throw new Exception();
	}

	private void Update()
	{
		if (_frame >= _frameCount)
		{
			var frame = _currentFrame == 0 ? 1 : 0;
			_currentFrame = frame;
			foreach (var item in _plants)
			{
				if (item.isActive)
					item.Image.sprite = item.Sprites[frame];
			}

			_frame = 0;
			return;
		}
		_frame++;
	}
}
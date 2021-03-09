using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AmongUs
{
	public class TaskController : MonoBehaviour
	{
		private bool _isSelected;
		private bool _isUnselected;
		[SerializeField] private SpriteRenderer _selected;
		[SerializeField] private SpriteRenderer _unselected;

		private float _fadeInTime = 0.2f;
		private float _timer = 0f;
		
		public Action<int> Evt_PlayerEnter = delegate{  };
		public Action Evt_PlayerExit = delegate {  };
		
		private int _taskNumber;
		
		////////////////////
		/// Monobehavior ///
		////////////////////
		private void Update()
		{
			if (_isSelected)
			{
				if (_timer >= _fadeInTime)
				{
					_timer = 0;
					_isSelected = false;
					_selected.color = _selected.color.SetAlpha(1);
					return;
				}

				_isUnselected = false;
				_selected.color = _selected.color.SetAlpha(_timer / _fadeInTime);
				_timer += Time.deltaTime;
			}
			if (_isUnselected)
			{
				if (_timer >= _fadeInTime)
				{
					_timer = 0;
					_isUnselected = false;
					_selected.color = _selected.color.SetAlpha(0);
					return;
				}

				_isSelected = false;
				_selected.color = _selected.color.SetAlpha(1 - (_timer / _fadeInTime));
				_timer += Time.deltaTime;
			}

		}
		
		///////////////////////
		/// Setters/Getters ///
		///////////////////////
		
		public void SetTaskNumber(int index)
		{
			_taskNumber = index;
		}

		public void SetSprites(Sprite unselected, Sprite selected)
		{
			_unselected.sprite = unselected;
			_selected.sprite = selected;
		}
		
		//////////////
		/// Events ///
		//////////////
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Player"))
			{
				Evt_PlayerEnter(_taskNumber);
				_isSelected = true;
			}
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (other.CompareTag("Player"))
			{
				Evt_PlayerExit();
				_isUnselected = true;
			}
		}
	}
}


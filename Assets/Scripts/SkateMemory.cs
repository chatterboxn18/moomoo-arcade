using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkateMemory : MonoBehaviour
{
	[SerializeField] private Client[] _clientList;

	private int _clientLength;
	private float _timeToNext = 2f;
	private float _timer = 0;
	private void Start()
	{
		_clientLength = _clientList.Length - 1;
	}

	private void Update()
	{
		if (_timer > _timeToNext && _clientLength > -1)
		{
			//_clientList[_clientLength].ShowItem();
			_timer = 0;
			_clientLength -= 1;
		}
		else if (_clientLength > -1)
		{
			_timer += Time.deltaTime;
		}
		
	}
}

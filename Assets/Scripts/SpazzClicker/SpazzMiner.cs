using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpazzMiner : MonoBehaviour
{
	[SerializeField] private string _name;
	[SerializeField] private Vector2 _position;
	
	[SerializeField] private Sprite[] _sprites;
	[SerializeField] private Image[] _upgrades;
	[SerializeField] private Image _image;
	private int _spazzPoints;
	private bool _isActive;
	
	private float _timeToSpazz = 1f;
	private float _timer = 0f;

	private float _animTime = 0.5f;
	private float _animTimer = 0f;

	private int _spriteIndex;
	private int _level = 0;

	private int _minersIndex = 1;

	[Header("Upgrades")] 
	[SerializeField] private MinerIcon _upgradePrefab;
	[SerializeField] private Sprite _characterPhoto;

	public string Name
	{
		get => _name;
		set => _name = value;
	}

	public Vector2 Position
	{
		get => _position;
		set => _position = value;
	}

	public Action<int> Evt_UpdateSpazz = delegate {  };

	public void SetUpMiner(RectTransform upgradeContainer, int spazzAmount)
	{
		var upgrade = Instantiate(_upgradePrefab, upgradeContainer);
		upgrade.Evt_BasicEvent_Up += () => ActivateMiner(spazzAmount);
		upgrade.AddButton.Evt_BasicEvent_Up += AddMiner;
		upgrade.SetPhoto(_characterPhoto);
		upgrade.UpgradeButton.Evt_BasicEvent_Up += () =>
		{
			UpgradeMiner();
			if (_level == _upgrades.Length)
				upgrade.UpgradeButton.SetActive(false);
		};
	}


	private void ActivateMiner(int spazz)
	{
		_image.gameObject.SetActive(true);
		_isActive = true;
		_spazzPoints = spazz;
	}
	public void UpgradeMiner()
	{
		if (_level >= _upgrades.Length) 
			return;
		_spazzPoints = _spazzPoints * 2;
		if (_upgrades.Length > 0)
			_upgrades[_level].gameObject.SetActive(true);
		_level++;
	}

	public void AddMiner()
	{
		_minersIndex++;
	}

	private void Update()
	{
		if (_isActive)
		{
			if (_animTimer >= _animTime)
			{
				if (_spriteIndex == _sprites.Length)
					_spriteIndex = 0;
				_image.sprite = _sprites[_spriteIndex];
				_image.SetNativeSize();
				_spriteIndex++;
				_animTimer = 0;
			}
			else
			{
				_animTimer += Time.deltaTime;
			}
		}
		
		if (_timer >= _timeToSpazz)
		{
			Evt_UpdateSpazz(_spazzPoints * _minersIndex);
			_timer = 0; 
			return;
		}

		_timer += Time.deltaTime;

	}
}

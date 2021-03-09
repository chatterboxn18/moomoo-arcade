using System.Collections;
using System.Collections.Generic;
using MooClicker;
using UnityEngine;
using UnityEngine.UI;

public class MinerIcon : AdvanceButton
{
	[SerializeField] private AdvanceButton _upgradeButton;
	[SerializeField] private AdvanceButton _addButton;
	[SerializeField] private Image _photo;
	public AdvanceButton UpgradeButton => _upgradeButton;
	public AdvanceButton AddButton => _addButton;

	public void SetPhoto(Sprite photo)
	{
		_photo.sprite = photo;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
	[SerializeField] private Image _icon;
	[SerializeField] private Text _title;
	[SerializeField] private Text _subtitle;
	[SerializeField] private Image _active;

	public void SetIcon(Sprite sprite)
	{
		_icon.sprite = sprite;
	}

	public void SetTitle(string title)
	{
		_title.text = title;
	}

	public void SetSubtitle(string subtitle)
	{
		_subtitle.text = subtitle;
	}
	
	public void SetAchievement(bool on)
	{
		_active.gameObject.SetActive(on);
	}

	public void Activate()
	{
		_icon.color = new Color(1,1,1,1);
		_active.gameObject.SetActive(true);
	}
}

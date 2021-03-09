using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MerchItem : DraggableItem
{
	[SerializeField] private int _index;
	[SerializeField] private Vector2 _startingPosition;
	[SerializeField] private Image _image;
	[SerializeField] private Sprite _sprite;
	[SerializeField] private MerchItem _prefab;

	public Action Evt_FulfilledRequest = delegate {  };

	public int GetIndex()
	{
		return _index;
	}

	public Image GetImage()
	{
		return _image;
	}

	public void SetItems(int index, Vector2 position, Sprite sprite)
	{
		_index = index;
		_startingPosition = position;
		_image.sprite = sprite;
		_sprite = sprite;
	}

	public override void OnBeginDrag(PointerEventData eventData)
	{
		base.OnBeginDrag(eventData);
		var item = Instantiate(_prefab, transform.parent);
		item.SetItems(_index, _startingPosition, _sprite);
		item.transform.localPosition = new Vector3(_startingPosition.x, _startingPosition.y, 0);
	}

	public override void OnEndDrag(PointerEventData eventData)
	{
		base.OnEndDrag(eventData);
		if (_image.color == Color.green)
		{
			Evt_FulfilledRequest();
			Debug.Log("Point");
		}
		
		Destroy(gameObject);
	}
}

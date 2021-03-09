using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
	[SerializeField] private SpriteRenderer _leftRenderer;
	[SerializeField] private SpriteRenderer _rightRenderer;
	
	[SerializeField] private Sprite _leftDoor;
	[SerializeField] private Sprite _rightDoor;

	private float _startPositionX;
	private float _originalPositionX;
	private float _endPositionX = 0.529f;

	private bool _isClosing;
	private bool _isOpening;
	private float _doorSpeed = 1f;

	private float _timer;
	
	private void Awake()
	{
		_leftRenderer.sprite = _leftDoor;
		_rightRenderer.sprite = _rightDoor;
		_originalPositionX = _leftRenderer.transform.localPosition.x;
	}

	private void Start()
	{
		
	}

	private void Update()
	{
		var rightTransform = _rightRenderer.transform;
		var leftTransform = _leftRenderer.transform;
		if (_isOpening)
		{
			if (_timer > _doorSpeed)
			{
				_isOpening = false;
				_timer = 0;
				rightTransform.localPosition = rightTransform.localPosition.SetX(_endPositionX);
				leftTransform.localPosition = leftTransform.localPosition.SetX(-1 * _endPositionX);
				return;
			}

			rightTransform.localPosition = rightTransform.localPosition.SetX(Mathf.Lerp(_startPositionX, _endPositionX,
				_timer / _doorSpeed));
			leftTransform.localPosition = leftTransform.localPosition.SetX(-1 * Mathf.Lerp(_startPositionX, _endPositionX,
				_timer / _doorSpeed));
		}

		if (_isClosing)
		{
			if (_timer > _doorSpeed)
			{
				_isClosing = false;
				_timer = 0; 
				rightTransform.localPosition = rightTransform.localPosition.SetX(0);
				leftTransform.localPosition = leftTransform.localPosition.SetX(0);
				return;
			}
			_rightRenderer.transform.localPosition = rightTransform.localPosition.SetX(Mathf.Lerp(_startPositionX, _originalPositionX,
				_timer / _doorSpeed));
			_leftRenderer.transform.localPosition = leftTransform.localPosition.SetX(-1 * Mathf.Lerp(_startPositionX, _originalPositionX,
				                                                                                   _timer / _doorSpeed));
		}
		
		_timer += Time.deltaTime;

	}

	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			_startPositionX = _rightRenderer.transform.localPosition.x;
			_isClosing = false;
			_isOpening = true;
			_timer = 0;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			_startPositionX = _rightRenderer.transform.localPosition.x;
			_isClosing = true;
			_isOpening = false;
			_timer = 0;
		}
	}
}

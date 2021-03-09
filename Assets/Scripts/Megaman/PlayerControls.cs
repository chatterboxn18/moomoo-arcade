using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

	private Rigidbody2D _rigidbody;
	private float _jumpTimer;
	[SerializeField] private float _jumpCooldown;
	[SerializeField] private Bullet _bullet;

	private Bullet _currentBullet;
	[SerializeField] private Transform _hand;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_jumpTimer = _jumpCooldown;
	}

	private void Update()
	{
		if (_jumpTimer <= 0)
		{
			if (Input.GetKey(KeyCode.UpArrow))
			{
				_rigidbody.AddForce(new Vector2(0,300));   
				_jumpTimer = _jumpCooldown;
			}
		}
		else
		{
			_jumpTimer -= Time.deltaTime;
		}
		
		if (Input.GetKey(KeyCode.RightArrow))
		{
			_rigidbody.AddForce(new Vector2(5, 0));
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			_rigidbody.AddForce(new Vector2(-5, 0));			
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (_currentBullet == null)
				_currentBullet = Instantiate(_bullet, _hand, false);
			
			_currentBullet.Evt_GrowBullet();
		}
		else if (Input.GetKeyUp(KeyCode.Space))
		{
			_currentBullet.Evt_ShootBullet();
			_currentBullet = null;
		}
	}
}

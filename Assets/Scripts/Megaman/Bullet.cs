using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private ParticleSystem _particles;
	[SerializeField] private Rigidbody2D _rigidbody2D;

	private float _lifetime = 2f;
	private float _timer;
	private bool _isLifetime;

	public void Evt_ShootBullet()
	{
		_rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
		_rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
		_rigidbody2D.velocity = Vector2.right * 10;
		_isLifetime = true;
	}

	private void Update()
	{
		if (_timer > _lifetime)
		{
			Destroy(gameObject);
		}

		if (_isLifetime)
		{
			_timer += Time.deltaTime;
		}
		
	}

	public void Evt_GrowBullet()
	{
		if (transform.localScale.x < 3)
			transform.localScale = transform.localScale.MultiplyVector3(1.1f);
	}
}
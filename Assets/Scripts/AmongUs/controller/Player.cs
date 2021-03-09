using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AmongUs
{
	
	public class Player : MonoBehaviour
	{
		private Animator _animator;

		private SpriteRenderer _spriteRenderer;
		
		//Movement Variables
		private Rigidbody2D _rigidbody;
		private CharacterMovement _playerMovement;
		// ReSharper disable once InconsistentNaming
		[SerializeField] private float _speed;

		public AmongUsEnums.Movement IntMovementNumber
		{
			get { return (AmongUsEnums.Movement) _animator.GetInteger(MovementNumber); }
			set
			{
				_animator.SetInteger(MovementNumber, (int) value);
			}
		}
		
		private static readonly int MovementNumber = Animator.StringToHash("MovementNumber");
	
		private void Awake()
		{
			_animator = GetComponent<Animator>();
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_rigidbody = GetComponent<Rigidbody2D>();
		}

		private void Start()
		{
			_playerMovement = new CharacterMovement();
			_playerMovement.Enable();
			_playerMovement.Movement.Movement1.canceled += context => { IntMovementNumber = 0;};
			_playerMovement.Movement.Movement1.started += context => { IntMovementNumber = AmongUsEnums.Movement.Moving; };
		}

		private void Update()
		{
			var direction = _playerMovement.Movement.Movement1.ReadValue<Vector2>();
			var vec = new Vector2(direction.x, direction.y);
			_spriteRenderer.flipX = direction.x < 0;
			_rigidbody.velocity = vec.MultiplyVector2(_speed);
		}
		
		private void Move(Vector2 direction)
		{
		}

		
	}
}


using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
	public float walkingSpeed = 10f;
	public float jumpForce = 6f;
	public GameObject feetLocation;

	private Animator _animator;
	private Rigidbody2D _rigidbody2D;

	private float _direction = 1;
	private float _horizontalVelocity = 0;
	private bool _isJumping = false;
	
	void Awake () 
	{
		this._animator = GetComponent<Animator>();
		this._rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
	{
		UpdateMovementStatesFromInput();
		ApplyVelocity();
		UpdateCharacterAnimation();
		HandleJump();
	}

	private void UpdateMovementStatesFromInput()
	{
		float horizontalAxis = Input.GetAxisRaw("Horizontal");
		UpdateDirectionFromHorizontalAxis(horizontalAxis);
		UpdateHorizontalVelocity(horizontalAxis);
	}

	private void UpdateDirectionFromHorizontalAxis(float horizontalAxis)
	{
		if (horizontalAxis == 0)
		{
			return; // Keep facing the same direction
		}

		_direction = horizontalAxis > 0 ? 1 : -1;
	}

	private void UpdateHorizontalVelocity(float horizontalAxis)
	{
		_horizontalVelocity =  walkingSpeed * horizontalAxis;
	}
	
	private void ApplyVelocity()
	{
		_rigidbody2D.velocity = new Vector2(_horizontalVelocity, this._rigidbody2D.velocity.y);
	}

	private void HandleJump()
	{
		if (IsOnTheGround())
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{	
				_rigidbody2D.velocity = new Vector2(this._rigidbody2D.velocity.x, jumpForce);
				_isJumping = true;
			}
			else
			{
				_isJumping = false;
			}
		}
	}

	private bool IsOnTheGround()
	{
		int platformLayerMask = LayerMask.GetMask("Platforms");
		Collider2D collision = Physics2D.OverlapCircle(feetLocation.transform.position, 0.02f, platformLayerMask);

		return collision != null;
	}

	private void OnDrawGizmos()
	{
		UnityEditor.Handles.color = Color.red;
		UnityEditor.Handles.DrawWireDisc(feetLocation.transform.position, Vector3.back, 0.02f);
	}

	private void UpdateCharacterAnimation()
	{
		EPlayerAnimations animation = GetAnimationTypeFromMovementStates();
		ChangeAnimationState(animation);
	}

	private EPlayerAnimations GetAnimationTypeFromMovementStates()
	{
		if (_isJumping)
		{
			return EPlayerAnimations.Jump;
		} 
		
		if (_horizontalVelocity != 0)
		{
			return EPlayerAnimations.Walk;
		}
		
		return EPlayerAnimations.Idle;
	}
	
	private void ChangeAnimationState(EPlayerAnimations value) 
	{
		//this._animator.SetInteger("AnimationState", (int)value);
	}
}
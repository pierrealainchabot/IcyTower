using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
	public float walkingSpeed = 10f;
	public float jumpForce = 6f;
	public GameObject feetLocation;
	public float isOnGroundCheckRadius = 0.05f;
	public float longJumpMaxTimeSec = 1f;

	private Animator _animator;
	private Rigidbody2D _rigidbody2D;

	private float _direction = 1;
	private float _horizontalVelocity = 0;
	private bool _isJumping = false;
	private float _elapsedLongJumpTime = 0;
	private bool _holdingJumpButton = false;
	private bool _longJumpingAllowed = false;

	void Awake () 
	{
		this._animator = GetComponent<Animator>();
		this._rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void OnBecameInvisible()
	{
		if (Camera.main != null)
		{
			bool bottomExit = Camera.main.WorldToScreenPoint(transform.position).y < 0;
			if (bottomExit)
			{
				GameManager.Instance.EndGame();	
			}	
		}
	}

	void Update ()
	{
		UpdateMovementStatesFromInput();
		ApplyVelocity();
		HandleJump();
		UpdatePlatformParenting(GetPlatformPlayerStadingOn());
		UpdateCharacterAnimation();
		
	}

	private void UpdateMovementStatesFromInput()
	{
		float horizontalAxis = Input.GetAxis("Horizontal");
		UpdateDirectionFromHorizontalAxis(horizontalAxis);
		UpdateHorizontalVelocity(horizontalAxis);
	}

	private void UpdateDirectionFromHorizontalAxis(float horizontalAxis)
	{
		if (horizontalAxis == 0)
		{
			return; // Keep facing the same direction
		}

		var currentDirection = horizontalAxis > 0 ? 1 : -1;
		if (_direction != currentDirection)
		{
			_direction = currentDirection;
			ChangeDirection();
		}
	}

	private void ChangeDirection()
	{
		Vector3 directionChanger = transform.localScale;
		directionChanger.x *= -1;
		transform.localScale = directionChanger;
	}

	private void UpdateHorizontalVelocity(float horizontalAxis)
	{
		_horizontalVelocity = walkingSpeed * horizontalAxis;
	}
	
	private void ApplyVelocity()
	{
		_rigidbody2D.velocity = new Vector2(_horizontalVelocity, this._rigidbody2D.velocity.y);
	}

	private void HandleJump()
	{
		
		if (IsOnTheGround() && !_holdingJumpButton)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{	
				_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
				_isJumping = true;
				_holdingJumpButton = true;
				_longJumpingAllowed = true;
			}
			else
			{
				_isJumping = false;
				_longJumpingAllowed = false;
				_elapsedLongJumpTime = 0;
			}
		}
		else if (_longJumpingAllowed && _holdingJumpButton)
		{
			_elapsedLongJumpTime += Time.deltaTime;
			if (_elapsedLongJumpTime < longJumpMaxTimeSec)
			{
				_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
			}
			else
			{
				_longJumpingAllowed = false;
			}
		}

		if (Input.GetKeyUp(KeyCode.Space))
		{
			_longJumpingAllowed = false;
			_holdingJumpButton = false;
		}
	}

	private GameObject GetPlatformPlayerStadingOn()
	{
		int platformLayerMask = LayerMask.GetMask("Platforms");
		Collider2D collision = Physics2D.OverlapCircle(feetLocation.transform.position, isOnGroundCheckRadius, platformLayerMask);
		return collision != null ? collision.gameObject : null;
	}

	private void UpdatePlatformParenting(GameObject platformPlayerStandingOn)
	{
		this.transform.parent = platformPlayerStandingOn != null ? platformPlayerStandingOn.transform : null;
	}

	private bool IsOnTheGround()
	{
		return this.transform.parent != null;
	}

	private void OnDrawGizmos()
	{
		UnityEditor.Handles.color = Color.red;
		UnityEditor.Handles.DrawWireDisc(feetLocation.transform.position, Vector3.back, isOnGroundCheckRadius);
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
			return EPlayerAnimations.Run;
		}
		
		return EPlayerAnimations.Idle;
	}
	
	private void ChangeAnimationState(EPlayerAnimations value) 
	{
		this._animator.SetInteger("AnimationState", (int)value);
	}
}
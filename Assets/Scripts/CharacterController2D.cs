using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharacterController2D : MonoBehaviour
{

	[SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private LayerMask m_WhatIsWall;                            // A mask determining what is wall to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_WallCheck;
	private float coyoteTimer;
	public float coyoteFrames;

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	public bool m_Grounded;            // Whether or not the player is grounded.
	private Rigidbody2D m_Rigidbody2D;
	private BoxCollider2D m_BoxCollider2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 velocity = Vector3.zero;
	private float limitFallSpeed = 25f; // Limit fall speed
	

	public bool canDoubleJump = true; //If player can double jump
	[SerializeField] private float m_DashForce = 25f;
	private bool canDash = true;
	private bool isDashing = false; //If player is dashing
	public bool m_IsWall = false; //If there is a wall in front of the player
	private bool isWallSliding = false; //If player is sliding in a wall
	private bool oldWallSlidding = false; //If player is sliding in a wall in the previous frame
	private float prevVelocityX = 0f;
	private bool canCheck = false; //For check if player is wallsliding

	public float life = 10f; //Life of the player
	public bool invincible = false; //If player can die
	private bool canMove = true; //If player can move

	private Animator animator;
	public ParticleSystem particleJumpUp; //Trail particles
	public ParticleSystem particleJumpDown; //Explosion particles

	private float jumpWallStartX = 0;
	private float jumpWallDistX = 0; //Distance between player and wall
	private bool limitVelOnWallJump = false; //For limit wall jump distance with low fps

	[Header("Events")]
	[Space]

	public UnityEvent OnFallEvent;
	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		m_BoxCollider2D = GetComponent<BoxCollider2D>();
		animator = GetComponent<Animator>();

		if (OnFallEvent == null)
			OnFallEvent = new UnityEvent();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}


	private void Update()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = IsGrounded();

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		/*Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
				m_Grounded = true;
				
			if (!wasGrounded)
			{
				OnLandEvent.Invoke();
				if (!m_IsWall && !isDashing)
					particleJumpDown.Play();
				canDoubleJump = true;
				if (m_Rigidbody2D.velocity.y < 0f)
					limitVelOnWallJump = false;
			}
		}*/

		m_IsWall = false;

		if (!m_Grounded)
		{
			m_IsWall = IsWall();
			/*OnFallEvent.Invoke();
			Collider2D[] collidersWall = Physics2D.OverlapCircleAll(m_WallCheck.position, k_GroundedRadius, m_WhatIsGround);
			for (int i = 0; i < collidersWall.Length; i++)
			{
				if (collidersWall[i].gameObject != null)
				{
					isDashing = false;
					m_IsWall = true;
				}
			}
			prevVelocityX = m_Rigidbody2D.velocity.x;*/
		}

        //Coyote Time
        if (m_Grounded)
        {
			coyoteTimer = 0;
        }
        else
        {
			coyoteTimer++;
        }

		Debug.LogWarning(coyoteTimer);

		if (limitVelOnWallJump)
		{
			if (m_Rigidbody2D.velocity.y < -0.5f)
				limitVelOnWallJump = false;
			jumpWallDistX = (jumpWallStartX - transform.position.x) * transform.localScale.x;
			if (jumpWallDistX < -0.5f && jumpWallDistX > -1f)
			{
				canMove = true;
			}
			else if (jumpWallDistX < -1f && jumpWallDistX >= -2f)
			{
				canMove = true;
				m_Rigidbody2D.velocity = new Vector2(10f * transform.localScale.x, m_Rigidbody2D.velocity.y);
			}
			else if (jumpWallDistX < -2f)
			{
				limitVelOnWallJump = false;
				m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
			}
			else if (jumpWallDistX > 0)
			{
				limitVelOnWallJump = false;
				m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
			}
		}
	}


	public void Move(float move, bool jump, bool dash, float lastDirection)
	{
		if (canMove)
		{
			if (dash && canDash && !isWallSliding)
			{
				//m_Rigidbody2D.AddForce(new Vector2(transform.localScale.x * m_DashForce, 0f));
				StartCoroutine(DashCooldown());
			}
			
			if (isDashing)
			{
                if (lastDirection > 0)
                {
					m_Rigidbody2D.velocity = Vector2.right * m_DashForce;
                }
                if (lastDirection < 0 )
                {
					m_Rigidbody2D.velocity = Vector2.left * m_DashForce;
                }
				//m_Rigidbody2D.velocity = new Vector2(transform.localScale.x * m_DashForce, 0);
				/*if (isDashing)
				{
					m_Rigidbody2D.velocity = new Vector2(transform.right.x, 0) * Mathf.Sign(move) * m_DashForce;
				}*/
			}
			//only control the player if grounded or airControl is turned on
			else if (m_Grounded || m_AirControl)
			{
				if (m_Rigidbody2D.velocity.y < -limitFallSpeed)
					m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, -limitFallSpeed);
				// Move the character by finding the target velocity
				Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
				// And then smoothing it out and applying it to the character
				m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

				// If the input is moving the player right and the player is facing left...
				if (move > 0 && !m_FacingRight && !isWallSliding)
				{
					// ... flip the player.
					Flip();
				}
				// Otherwise if the input is moving the player left and the player is facing right...
				else if (move < 0 && m_FacingRight && !isWallSliding)
				{
					// ... flip the player.
					Flip();
				}
			}
			// If the player should jump...
			if ((IsGrounded() && jump) || (coyoteTimer < coyoteFrames && jump))
			{
				// Add a vertical force to the player.
				animator.SetBool("IsJumping", true);
				animator.SetBool("JumpUp", true);
				m_Grounded = false;
				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
				canDoubleJump = true;
				particleJumpDown.Play();
				particleJumpUp.Play();
			}
			else if (!IsGrounded() && jump && canDoubleJump && !isWallSliding)
			{
				canDoubleJump = false;
				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce / 1.2f));
				animator.SetBool("IsDoubleJumping", true);
			}

			else if (m_IsWall && !m_Grounded)
			{
				if (!oldWallSlidding && m_Rigidbody2D.velocity.y < 0 || isDashing)
				{
					isWallSliding = true;
					//m_WallCheck.localPosition = new Vector3(-m_WallCheck.localPosition.x, m_WallCheck.localPosition.y, 0);
					Flip();
					StartCoroutine(WaitToCheck(0.1f));
					//canDoubleJump = true;
					animator.SetBool("IsWallSliding", true);
				}
				isDashing = false;

				if (isWallSliding)
				{
					if (move * transform.localScale.x > 0.1f)
					{
						
						StartCoroutine(WaitToEndSliding());
					}
					else
					{
						oldWallSlidding = true;
						m_Rigidbody2D.velocity = new Vector2(-transform.localScale.x * 2, -2);
					}
				}

				if (jump && isWallSliding)
				{
					animator.SetBool("IsJumping", true);
					animator.SetBool("JumpUp", true);
					m_Rigidbody2D.velocity = new Vector2(0f, 0f);
					m_Rigidbody2D.AddForce(new Vector2(transform.localScale.x * m_JumpForce * 4.0f, 305));
					jumpWallStartX = transform.position.x;
					limitVelOnWallJump = true;
					//canDoubleJump = true;
					isWallSliding = false;
					animator.SetBool("IsWallSliding", false);
					oldWallSlidding = false;
					//dm_WallCheck.localPosition = new Vector3(Mathf.Abs(m_WallCheck.localPosition.x), m_WallCheck.localPosition.y, 0);
					canMove = false;
				}
				else if (dash && canDash)
				{
					isWallSliding = false;
					animator.SetBool("IsWallSliding", false);
					oldWallSlidding = false;
					
					//m_WallCheck.localPosition = new Vector3(Mathf.Abs(m_WallCheck.localPosition.x), m_WallCheck.localPosition.y, 0);
					canDoubleJump = true;
					StartCoroutine(DashCooldown());
				}
			}
			else if (isWallSliding && !m_IsWall && canCheck)
			{
				isWallSliding = false;
				animator.SetBool("IsWallSliding", false);
				oldWallSlidding = false;
				//m_WallCheck.localPosition = new Vector3(Mathf.Abs(m_WallCheck.localPosition.x), m_WallCheck.localPosition.y, 0);
				canDoubleJump = false;
			}
		}
	}


	public void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		
	}

	public void ApplyDamage(float damage, Vector3 position)
	{
		if (!invincible)
		{
			animator.SetBool("Hit", true);
			life -= damage;
			Vector2 damageDir = Vector3.Normalize(transform.position - position) * 40f;
			m_Rigidbody2D.velocity = Vector2.zero;
			m_Rigidbody2D.AddForce(damageDir * 10);
			if (life <= 0)
			{
				StartCoroutine(WaitToDead());
			}
			else
			{
				StartCoroutine(Stun(0.25f));
				StartCoroutine(MakeInvincible(1f));
			}
		}
	}

	private bool IsGrounded()
    {
        if (m_Rigidbody2D.velocity.y <= 0)
        {
				float extraHeight = .02f;
				//RaycastHit2D raycastHit = Physics2D.Raycast(m_BoxCollider2D.bounds.center, Vector2.down, m_BoxCollider2D.bounds.extents.y + extraHeight, m_WhatIsGround);
				RaycastHit2D raycastHit = Physics2D.BoxCast(m_BoxCollider2D.bounds.center, m_BoxCollider2D.bounds.size, 0f, Vector2.down, extraHeight, m_WhatIsGround);

				Color rayColor;
				if (raycastHit.collider != null)
				{
					rayColor = Color.green;
				}
				else
				{
					rayColor = Color.red;
				}
				//Debug.DrawRay(m_BoxCollider2D.bounds.center, Vector2.down * (m_BoxCollider2D.bounds.extents.y + extraHeight), rayColor);
				//Debug.DrawRay(m_BoxCollider2D.bounds.center - new Vector3(m_BoxCollider2D.bounds.extents.x, m_BoxCollider2D.bounds.extents.y), Vector2.right * (m_BoxCollider2D.bounds.extents.y), rayColor);

				//Debug.Log(raycastHit.collider);
				return raycastHit.collider != null;
        }
        else
        {
			return false;
        }
		
    }

	private bool IsWall()
    {
		float extraHeight = .02f;
		float extraHeight2 = .02f;
		RaycastHit2D raycastHit = Physics2D.Raycast(m_BoxCollider2D.bounds.center, Vector2.right, m_BoxCollider2D.bounds.extents.x + extraHeight, m_WhatIsWall);
		RaycastHit2D raycastHit2 = Physics2D.Raycast(m_BoxCollider2D.bounds.center, Vector2.left, m_BoxCollider2D.bounds.extents.x + extraHeight2, m_WhatIsWall);
		Color rayColor;
		if (raycastHit.collider != null || raycastHit2.collider != null)
		{
			rayColor = Color.green;
		}
		else
		{
			rayColor = Color.red;
		}
		//Debug.DrawRay(m_BoxCollider2D.bounds.center, Vector2.right * (m_BoxCollider2D.bounds.extents.x + extraHeight), rayColor);
		//Debug.DrawRay(m_BoxCollider2D.bounds.center, Vector2.left * (m_BoxCollider2D.bounds.extents.x + extraHeight2), rayColor);
		//Debug.Log(raycastHit.collider);
		return raycastHit.collider != null || raycastHit2.collider != null;
	}

	IEnumerator DashCooldown()
	{
		animator.SetBool("IsDashing", true);
		float gravity = m_Rigidbody2D.gravityScale;
		m_Rigidbody2D.gravityScale = 0;
		isDashing = true;
		canDash = false;
		yield return new WaitForSeconds(0.1f);
		m_Rigidbody2D.gravityScale = gravity;
		isDashing = false;
		yield return new WaitUntil(() => m_Grounded);
		canDash = true;
	}

	IEnumerator Stun(float time)
	{
		canMove = false;
		yield return new WaitForSeconds(time);
		canMove = true;
	}
	IEnumerator MakeInvincible(float time)
	{
		invincible = true;
		yield return new WaitForSeconds(time);
		invincible = false;
	}
	IEnumerator WaitToMove(float time)
	{
		canMove = false;
		yield return new WaitForSeconds(time);
		canMove = true;
	}

	IEnumerator WaitToCheck(float time)
	{
		canCheck = false;
		yield return new WaitForSeconds(time);
		canCheck = true;
	}

	IEnumerator WaitToEndSliding()
	{
		yield return new WaitForSeconds(0.1f);
		//canDoubleJump = true;
		isWallSliding = false;
		animator.SetBool("IsWallSliding", false);
		oldWallSlidding = false;
		//m_WallCheck.localPosition = new Vector3(Mathf.Abs(m_WallCheck.localPosition.x), m_WallCheck.localPosition.y, 0);
	}

	IEnumerator WaitToDead()
	{
		animator.SetBool("IsDead", true);
		canMove = false;
		invincible = true;
		GetComponent<Attack>().enabled = false;
		yield return new WaitForSeconds(0.4f);
		m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
		yield return new WaitForSeconds(1.1f);
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRigidBody;
    public InputAction playerControls;
    private LayerMask groundMask;
    private LayerMask wallMask;
    private PlayerInputActions playerInputActions;
    private BoxCollider2D playerCollider;
    private PlayerStats playerStats;
    private PlayerAnimator playerAnimator;


    private float playerLook = 0f;
    private float playerface = 1f;
    private bool isAirborne = false;
    public float xAxis;
    private PlayerAudio playerAudio;
    private bool canDash = true;
    private float dashCount = 0;
    private bool wallJumping;
    public float xWallForce = 50f;
    public float WallJumpSpeedY = 50f;
    private float wallSlidingSpeed = 25f;
    private float maxYVelocity = -200;
    public float WallJumpTime = 0.05f;
    [SerializeField] private float dashPower;
    private float dashTime = 0.2f;
    private float dashCd = 1;
    public float jumpSpeed = 50;
    public int extrajumps = 0;
    public int totalJumpsAllowed = 0;
    private float parryMovementPenalty = 0.3f; 

    // Start is called before the first frame update

    private void Awake()
    {
        groundMask = LayerMask.GetMask("Ground");
        wallMask = LayerMask.GetMask("Wall");
        playerAudio = GetComponent<PlayerAudio>();
        playerInputActions = new PlayerInputActions();
        playerAnimator = GetComponent<PlayerAnimator>();
        playerCollider = GetComponent<BoxCollider2D>();


    }

    private void OnEnable()
    {

        playerControls = playerInputActions.Player.Move;
        playerControls.Enable();
        playerInputActions.Player.Dash.Enable();
        playerInputActions.Player.Jump.canceled += jumpCancel;
        playerInputActions.Player.Jump.performed += jump;
        playerInputActions.Player.Jump.Enable();
    }


    private void OnDisable()
    {
        playerControls.Disable();
        playerInputActions.Player.Jump.Disable();
        playerInputActions.Player.Dash.Disable();
    }

    void Start()
    {
  
        WallJumpTime = 0.1f;
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();

    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenuUIController.isPaused) return;
            playerAnimator.ReadInMovement(Math.Abs(xAxis), playerRigidBody.velocity.y, playerStats.isDashing, isGrounded());
        if (playerStats.isDashing || wallJumping  )
        {
            return;
        }
        if (DialogueManager.GetInstance().isPlaying || playerStats.isStaggered) {
            playerRigidBody.velocity = Vector2.zero;
            return;
        }
        UpdatePlayer();
        getInputs();
        flip(xAxis);
        walk(xAxis, playerStats.playerMovementSpeed);
        StartCoroutine(dash());

    }
    void UpdatePlayer()
    {
        if (xAxis < 0 || xAxis > 0)
        {
            playerLook = xAxis;
        }
        if (playerRigidBody.velocity.y < maxYVelocity)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, maxYVelocity);
        }

        if (wallSliding())
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, Math.Clamp(playerRigidBody.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        if (isGrounded() || wallSliding())
        {
            extrajumps = totalJumpsAllowed;
        }
        if (isGrounded())
        {
            playerAudio.StartStoppableAudio();
            playerStats.lastPosition = transform.position;
            isAirborne = false;
        }
        else {
            StartCoroutine("coyoteFrames");
            playerAudio.ToggleOffStoppableAudio();
        }


    }
    private IEnumerator coyoteFrames() {
        yield return  new WaitForSeconds(0.075f);
        isAirborne = true;

    }
    private void jumpCancel(InputAction.CallbackContext obj)
    {
        if (playerRigidBody.velocity.y > 0)
        {
            playerRigidBody.velocity = Vector2.up * 0.5f;
        }
    }
 
    void walk(float moveDirection, float movementSpeed)
    {
        if (moveDirection != 0)
        {
            playerAudio.PlaySoundNoOverlap("PlayerMove");
        }
        if (playerStats.Parrying || playerStats.LongParrying)
        {
            playerRigidBody.velocity = new Vector2(moveDirection * movementSpeed*parryMovementPenalty, playerRigidBody.velocity.y);
        }
        else
        {
            playerRigidBody.velocity = new Vector2(moveDirection * movementSpeed, playerRigidBody.velocity.y);
        }
    }

    void flip(float moveDirection)
    {
        if (moveDirection < 0 && playerface >0 )
        {
            playerface = moveDirection;
            transform.GetChild(0).localScale = new Vector2(-1 * Math.Abs(transform.GetChild(0).localScale.x), transform.GetChild(0).localScale.y);
            transform.GetChild(0).position = transform.GetChild(0).position + Vector3.left;

        }
        if (moveDirection > 0 && playerface <0 )
        {
            playerface = moveDirection;
            transform.GetChild(0).localScale = new Vector2(1 * Math.Abs( transform.GetChild(0).localScale.x), transform.GetChild(0).localScale.y);
            transform.GetChild(0).position = transform.GetChild(0).position + Vector3.right;
        }
    }

    private void jump(InputAction.CallbackContext obj)
    {
        bool jumpSuccessful = false;
        if (wallSliding() && !playerStats.isDashing)
        {
            jumpSuccessful = true;
            wallJumping = true;
            Invoke("WallJumpReset", WallJumpTime);
            if (xAxis == playerLook)
            {
                playerRigidBody.velocity = new Vector2(-xAxis * xWallForce, WallJumpSpeedY);
            }
            else if (xAxis == 0) {
                flip(-playerLook);
                playerRigidBody.velocity = new Vector2(-playerLook * xWallForce, WallJumpSpeedY);
            }
            else { playerRigidBody.velocity = new Vector2(xAxis * xWallForce, WallJumpSpeedY); }

        }
        else if (!isAirborne && !playerStats.isDashing)
        {
            jumpSuccessful = true;
            playerRigidBody.velocity = Vector2.up * jumpSpeed;
        }
        else if (extrajumps > 0 && !playerStats.isDashing)
        {
            jumpSuccessful = true;
            extrajumps--;
            playerRigidBody.velocity = Vector2.up * jumpSpeed;
        }
        if (jumpSuccessful)
        {
            playerAudio.PlaySound("PlayerJump");
        }

    }
    private void WallJumpReset()
    {
        wallJumping = false;
    }
    private IEnumerator dash()
    {
      
        
        if (isGrounded() || wallSliding())
        {
            dashCount = 0;
        }
        if (playerInputActions.Player.Dash.WasPressedThisFrame() && canDash && dashCount == 0)
        {
            playerAudio.StopAudio();
            playerAudio.PlaySound("PlayerDash");
            float originalGravity = playerRigidBody.gravityScale;
            playerStats.isDashing = true;
            dashCount += 1;
            playerRigidBody.gravityScale = 0;
            playerRigidBody.velocity = new Vector2(transform.GetChild(0).localScale.x * dashPower, 0);
            yield return new WaitForSeconds(dashTime);
            playerRigidBody.gravityScale = originalGravity;
            playerStats.isDashing = false;
            yield return new WaitForSeconds(dashCd);
            canDash = true;
        }
    }

    void getInputs()
    {
        xAxis = playerControls.ReadValue<Vector2>().x;
    }
    private bool isGrounded()
    {
        
        RaycastHit2D collision = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, .1f, groundMask);    
        RaycastHit2D collision2 = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, .1f, wallMask);
        return collision.collider != null || collision2.collider != null;
    }
    private bool isTouchingFront()
    {
        RaycastHit2D collision = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.right * playerLook, .1f, wallMask);
        return collision.collider != null;
    }
    private bool wallSliding()
    {
        return isTouchingFront() && !isGrounded() ? true : false;
    }

}

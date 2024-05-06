using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    private Rigidbody2D _body;
    private Animator _anim;
    private CapsuleCollider2D _capsuleCollider;
    private float _wallJumpCooldown;
    private float _horizontalInput;
    
    [Header("Coyote Time")]
    public float coyoteTime; 
    private float _coyoteCounter; 

    [Header("Multiple Jumps")]
    public int extraJumps;
    private int _jumpCounter;
    
    [Header("Wall Jumping")]
    public float wallJumpX; 
    public float wallJumpY; 
    
  
    private static readonly int Run = Animator.StringToHash("run");
    private static readonly int Grounded = Animator.StringToHash("grounded");

    public PlayerMovement(float wallJumpCooldown)
    {
        _wallJumpCooldown = wallJumpCooldown;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponentInParent<Animator>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();

    }

    
    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");

        // Flip when player moves left or right
        if (Mathf.Abs(_horizontalInput) > 0.01f) // Check if there is significant horizontal input
        {
            var localScale = transform.localScale;
            localScale = new Vector3(Mathf.Sign(_horizontalInput) * Mathf.Abs(localScale.x), localScale.y, localScale.z);
            transform.localScale = localScale;
        }

        // Set animation parameters for condition
        _anim.SetBool(Run, _horizontalInput != 0);
        _anim.SetBool(Grounded, IsGrounded());

        // Handle regular movement
        if (!OnWall() || IsGrounded())
        {
            _body.velocity = new Vector3(_horizontalInput * speed, _body.velocity.y, 0);
            _body.gravityScale = 3; // Ensure gravity is normal during regular movement
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        //Adjustable jump height
        if (Input.GetKeyUp(KeyCode.Space) && _body.velocity.y > 0)
            _body.velocity = new Vector2(_body.velocity.x, _body.velocity.y / 2);

        if (OnWall())
        {
            _body.gravityScale = 0;
            _body.velocity = Vector2.zero;
        }
        else
        {
            _body.gravityScale = 3;
            _body.velocity = new Vector2(_horizontalInput * speed, _body.velocity.y);

            if (IsGrounded())
            {
                _coyoteCounter = coyoteTime; //Reset coyote counter when on the ground
                _jumpCounter = extraJumps; //Reset jump counter to extra jump value
            }
            else
                _coyoteCounter -= Time.deltaTime; //Start decreasing coyote counter when not on the ground
        }
     
    }
    
    // Handles player jumping
    private void Jump()
    {
        if (_coyoteCounter <= 0 && !OnWall() && _jumpCounter <= 0)
        {
            return;
        }
        if (OnWall())
            WallJump();
        else
        {
            if (IsGrounded())
                _body.velocity = new Vector2(_body.velocity.x, jumpPower);
            else
            {
                //If not on the ground and coyote counter bigger than 0 do a normal jump
                if (_coyoteCounter > 0)
                    _body.velocity = new Vector2(_body.velocity.x, jumpPower);
                else
                {
                    if (_jumpCounter > 0) //If we have extra jumps then jump and decrease the jump counter
                    {
                        _body.velocity = new Vector2(_body.velocity.x, jumpPower);
                        _jumpCounter--;
                    }
                }
            }

            //Reset coyote counter to 0 to avoid double jumps
            _coyoteCounter = 0;
        }
    }
    
    // Checks if the player is grounded
    private bool IsGrounded()
    {
        var bounds = _capsuleCollider.bounds;
        float castDistance = 0.1f; // Small distance below the collider
        // CapsuleCast requires direction to be explicitly defined, for a vertical capsule use CapsuleDirection2D.Vertical
        RaycastHit2D raycastHit = Physics2D.CapsuleCast(bounds.center, bounds.size, CapsuleDirection2D.Vertical, 0, Vector2.down, castDistance, groundLayer);
        return raycastHit.collider != null;
    }
    
    private void WallJump()
    {
        _body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        _wallJumpCooldown = 0;
    }


    
    // Checks if the player is touching a wall
    private bool OnWall()
    {
        var bounds = _capsuleCollider.bounds;
        var transform1 = transform;
        Vector2 direction = transform1.right * Mathf.Sign(transform1.localScale.x); // Adjust based on player facing
        float castDistance = 0.1f; // Small distance to detect walls
        RaycastHit2D raycastHit = Physics2D.CapsuleCast(bounds.center, bounds.size, CapsuleDirection2D.Vertical, 0, direction, castDistance, wallLayer);
        return raycastHit.collider != null;
    }

    
    // Determines if the player can attack
    public bool CanAttack()
    {
        return _horizontalInput == 0 && IsGrounded() && !OnWall();
    }
    
}
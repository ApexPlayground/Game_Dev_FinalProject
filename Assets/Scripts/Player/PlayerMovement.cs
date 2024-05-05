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
    private static readonly int Jump1 = Animator.StringToHash("jump");
    private static readonly int Run = Animator.StringToHash("run");
    private static readonly int Grounded = Animator.StringToHash("grounded");

    // Start is called before the first frame update
    void Start()
    {
        
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponentInParent<Animator>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();

    }

    // Update is called once per frame
    private void FixedUpdate()
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

        // Handle wall interaction
        if (OnWall() && !IsGrounded())
        {
            if (_wallJumpCooldown > 0.2f)
            {
                _body.gravityScale = 0;
                _body.velocity = Vector2.zero;
            }
            else
            {
                _wallJumpCooldown += Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
        else
        {
            _wallJumpCooldown += Time.deltaTime;
        }
        
     
    }
    
    // Handles player jumping
    private void Jump()
    {
        if (IsGrounded())
        {
            _body.velocity = new Vector2(_body.velocity.x, jumpPower);
            _anim.SetTrigger(Jump1);
        }else if (OnWall() && !IsGrounded())
        {
            if (_horizontalInput == 0)
            {
                var localScale = transform.localScale;
                _body.velocity = new Vector2( -Mathf.Sign(localScale.x) * 10, 0);
                localScale = new Vector3(Mathf.Sign(_horizontalInput) * Mathf.Abs( localScale.x),  localScale.y,  localScale.z);
                transform.localScale = localScale;
            }
            else
            {
                
                _body.velocity = new Vector2( -Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            _wallJumpCooldown = 0;
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

    
    // Checks if the player is touching a wall
    private bool OnWall()
    {
        var bounds = _capsuleCollider.bounds;
        Vector2 direction = transform.right * Mathf.Sign(transform.localScale.x); // Adjust based on player facing
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
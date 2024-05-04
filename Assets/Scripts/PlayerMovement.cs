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
    private BoxCollider2D _boxCollider;
    private float _wallJumpCooldown;
  

    private float _horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponentInParent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    private void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        
       


        //flip when player moves left or right 
        if (Mathf.Abs(horizontalInput) > 0.01f) // Check if there is significant horizontal input
        {
            var localScale = transform.localScale;
            localScale = new Vector3(Mathf.Sign(horizontalInput) * Mathf.Abs(localScale.x), localScale.y, localScale.z);
            transform.localScale = localScale;
        }

      
        
        _anim.SetBool("run", horizontalInput != 0);
        _anim.SetBool("grounded", IsGrounded());

        //wall jump logic
        if (_wallJumpCooldown < 0.2f)
        {
            _body.velocity = new Vector3(horizontalInput * speed, _body.velocity.y, 0);

            if (OnWall() && !IsGrounded())
            {
                _body.gravityScale = 0;
                _body.velocity = Vector2.zero;
            }
            else
            {
              //  _body.gravityScale = 3;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            _wallJumpCooldown += Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            _body.velocity = new Vector2(_body.velocity.x, jumpPower);
            _anim.SetTrigger("Jump");
        }else if (OnWall() && !IsGrounded())
        {
            
        }
       
       
    }
    
    private bool IsGrounded()
    {
        var bounds = _boxCollider.bounds;
        RaycastHit2D rayCastHit = Physics2D.BoxCast(bounds.center, bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return rayCastHit.collider != null;
    }
    private bool OnWall()
    {
        var bounds = _boxCollider.bounds;
        RaycastHit2D rayCastHit = Physics2D.BoxCast(bounds.center, bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return rayCastHit.collider != null;
    }
    
}
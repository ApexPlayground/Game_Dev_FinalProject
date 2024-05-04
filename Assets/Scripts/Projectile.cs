using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;  // Projectile speed

    private float _direction;  // Movement direction
    private bool _hit;  // Indicates if hit occurred
    private float _lifetime;  // Time active

    private Animator _anim;  // Animator for effects
    private BoxCollider2D _boxCollider;  // Collision detection

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (_hit) return;  // Stop movement if hit

        float movementSpeed = speed * Time.deltaTime * _direction;
        transform.Translate(movementSpeed, 0, 0);  // Move projectile

        _lifetime += Time.deltaTime;
        if (_lifetime > 5) gameObject.SetActive(false);  // Deactivate after 5 seconds
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _hit = true;
        _boxCollider.enabled = false;
        _anim.SetTrigger("explode");  // Trigger explosion animation
    }

    public void SetDirection(float direction)
    {
        _lifetime = 0;  // Reset lifetime
        _direction = direction;  // Set movement direction
        gameObject.SetActive(true);
        _hit = false;  // Reset hit status
        _boxCollider.enabled = true;  // Enable collider

        // Flip scale if direction differs from current
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != Mathf.Sign(direction))
        {
            localScaleX *= -1;  // Flip horizontal scale
        }
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }



    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
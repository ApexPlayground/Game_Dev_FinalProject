using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;  // Projectile speed

    private float _direction;  // Movement direction
    private bool _hit;  // Indicates if hit occurred
    private float _lifetime;  // fireball lifetime active

    private Animator _anim;  // Animator for effects
    private BoxCollider2D _boxCollider;  // Collision detection
    private static readonly int Explode = Animator.StringToHash("explode");

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
        _anim.SetTrigger(Explode);  // Trigger explosion animation
        
        if (collision.CompareTag("Enemy"))
            collision.GetComponent<Health.Health>().TakeDamage(1);
    }

    public void SetDirection(float direction)
    {
        _lifetime = 0;  // Reset lifetime
        _direction = direction;  // Set movement direction
        gameObject.SetActive(true);
        _hit = false;  // Reset hit status
        _boxCollider.enabled = true;  // Enable collider


        var localScale = transform.localScale;
        float localScaleX = Mathf.Abs(localScale.x);  // Use the absolute value to handle scale uniformly
        localScale = new Vector3(Mathf.Sign(direction) * localScaleX, localScale.y, localScale.z);
        transform.localScale = localScale;
    }




    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
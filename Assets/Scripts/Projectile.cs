using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private AudioSource _bubblePop;

    [SerializeField]
    private Rigidbody2D _rb2d;

    [SerializeField]
    private int _maxHits;

    private bool _rotateTowardsDirection = false;
    public bool RotateTowardsDirection
    {
        get => _rotateTowardsDirection;
        set => _rotateTowardsDirection = value;
    }

    private Launcher _launcher;
    public Launcher Launcher
    {
        get => _launcher;
        set => _launcher = value;
    }

    private Vector2 _initialSpeed;

    public void Setup(Launcher launcher)
    {
        _rotateTowardsDirection = true;
        _launcher = launcher;
    }

    private void Update()
    {
        if(_initialSpeed == Vector2.zero)
        {
            _initialSpeed = _rb2d.linearVelocity;
        }

        if (_rotateTowardsDirection)
        {
            float angle = Mathf.Atan2(_rb2d.linearVelocityY, _rb2d.linearVelocityX) * Mathf.Rad2Deg + 90;
            _rb2d.rotation = angle; // Set rotation
        }
    }

    private void ChangeDirection(Collision2D collision)
    {
        Vector2 normal = collision.contacts[0].normal; // Normal de la superficie

        // Calcular el nuevo vector reflejado
        Vector2 reflectedVelocity = Vector2.Reflect(_initialSpeed, normal);
        _initialSpeed = reflectedVelocity;

        // Aplicar la nueva dirección al Rigidbody
        _rb2d.linearVelocity = reflectedVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
            _launcher.ProjectileDestroyed();
        }

        ChangeDirection(collision);

        _maxHits -= 1;
        if( _maxHits <= 0)
        {
            Destroy(gameObject);
            _launcher.ProjectileDestroyed();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bubble"))
        {
            _bubblePop.Play();
            collision.GetComponent<Bubble>().ExplodeBubble();
        }
    }
}

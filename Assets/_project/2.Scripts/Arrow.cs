using UnityEngine;

public class Arrow : Projectile
{
    [SerializeField] private Rigidbody2D _rigid;
    [SerializeField] private float _speed = 1.0f;
    public override void Init()
    {

    }

    public void Shoot(Vector2 _direction)
    {
        _rigid.velocity = _direction * _speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            if (collision.TryGetComponent<IDamageable>(out IDamageable _damageable))
            {
                _damageable.OnDamage(100);
            }
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
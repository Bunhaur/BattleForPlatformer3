using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent (typeof(EnemyAnimation))]
public class Enemy : MonoBehaviour
{
    protected EnemyHealth _health;
    private EnemyAnimation _animation;

    [field: SerializeField] public float Damage { get; private set; } = 15;

    private void Awake()
    {
        _health = GetComponent<EnemyHealth>();
        _animation = GetComponent<EnemyAnimation>();
    }

    private void OnEnable()
    {
        _health.Dead += Die;
    }

    private void OnDisable()
    {
        _health.Dead -= Die;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.TryGetComponent(out Player player))
        {
            _animation.PlayAnimationHit();
            _health.ChangeValue(-player.Damage);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
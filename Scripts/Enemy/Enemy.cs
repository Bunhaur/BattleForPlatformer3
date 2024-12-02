using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent (typeof(EnemyAnimation))]
public class Enemy : MonoBehaviour
{
    protected EnemyHealth Health;
    private EnemyAnimation _animation;

    [field: SerializeField] public float Damage { get; private set; } = 15;

    private void Awake()
    {
        Health = GetComponent<EnemyHealth>();
        _animation = GetComponent<EnemyAnimation>();
    }

    private void OnEnable()
    {
        Health.Dead += Die;
    }

    private void OnDisable()
    {
        Health.Dead -= Die;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.TryGetComponent(out Player player))
        {
            _animation.PlayAnimationHit();
            Health.Hit(player.Damage);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(InputService))]
[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour
{
    private PlayerMovement _movementController;
    private Jumper _jumpController;
    private InputService _inputService;
    private PlayerHealth _health;
    private float _horizontal;

    [field: SerializeField] public float Damage { get; private set; } = 15;

    private void Awake()
    {
        _movementController = GetComponent<PlayerMovement>();
        _jumpController = GetComponent<Jumper>();
        _inputService = GetComponent<InputService>();
        _health = GetComponent<PlayerHealth>();
    }

    private void OnEnable()
    {
        _health.Dead += Die;
    }

    private void OnDisable()
    {
        _health.Dead -= Die;
    }

    private void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
        Flip();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HealthBox healthbox))
            TakeHealthBox(healthbox);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Enemy enemy))
            TakeHit(enemy);
    }

    private void Move()
    {
        _horizontal = _inputService.GetHorizontal();
        _movementController.Move(_horizontal);
    }

    private void TakeHit(Enemy enemy)
    {
        _jumpController.Jump();
        _health.Hit(enemy.Damage);
    }

    private void Flip()
    {
        if (_horizontal != 0)
            _movementController.FlipSprite(_horizontal);
    }

    private void TakeHealthBox(HealthBox healthBox)
    {
        _health.Heal(healthBox.HealthRecovery);
        healthBox.Remove();
    }

    private void Jump()
    {
        if (_inputService.IsPushJump())
            _jumpController.Jump();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
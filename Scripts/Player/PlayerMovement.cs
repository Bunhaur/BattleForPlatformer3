using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private const float SpriteRotationY = 180f;

    [SerializeField] private float _speed = 5f;

    private Rigidbody2D _rigidbody;
    private Vector2 _velocity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(float horizontal)
    {
        _velocity = _rigidbody.velocity;
        _velocity.x = horizontal * _speed;
        _rigidbody.velocity = _velocity;
    }

    public void FlipSprite(float horizontal)
    {
        if (horizontal < 0)
            transform.rotation = Quaternion.Euler(0, SpriteRotationY, 0);
        else if (horizontal > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    private const float RotationY = 180f;

    [SerializeField] private Vector3[] _way;
    [SerializeField] private float _speed;
    [SerializeField] private Player _player;

    private float _findDistance = 5;
    private Vector3 _point;
    private int _index;

    private void Awake()
    {
        _point = _way[_index];
        Flip();
    }

    private void Update()
    {
        SetTarget();
        Move();
    }

    private bool IsFindPlayer()
    {
        if (_player != null && Vector2.Distance(transform.position, _player.transform.position) < _findDistance)
            return true;

        return false;
    }

    private void SetTarget()
    {
        if (IsFindPlayer())
            _point = _player.transform.position;
        else
            _point = _way[_index];

        if (transform.position == _point)
            _index = ++_index % _way.Length;
    }

    private void Move()
    {
        Flip();
        transform.position = Vector2.MoveTowards(transform.position, _point, _speed * Time.deltaTime);
    }

    private void Flip()
    {
        if (transform.position.x < _point.x)
            transform.rotation = Quaternion.Euler(0, RotationY, 0);
        else
            transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        float size = .5f;

        foreach (Vector3 point in _way)
            Gizmos.DrawSphere(point, size);
    }
}

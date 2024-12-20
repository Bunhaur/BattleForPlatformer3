using Unity.VisualScripting;
using UnityEngine;

public class UIElementFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _offsetY;

    private Vector2 _offsetPosition;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (_target != null)
            transform.position = GetTargetPosition() + _offsetPosition;
        else
            Destroy(gameObject);
    }

    private Vector2 GetTargetPosition()
    {
        return _target.position;
    }

    private void Init()
    {
        _offsetPosition = new Vector2(0, _offsetY);
    }
}
using UnityEngine;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private SmoothIndicator _smoothIndicator;

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        _health.Changed += _smoothIndicator.Show;
    }

    private void OnDisable()
    {
        _health.Changed -= _smoothIndicator.Show;
    }

    private void Init()
    {
        _smoothIndicator.Show(_health.CurrentValue);
    }
}
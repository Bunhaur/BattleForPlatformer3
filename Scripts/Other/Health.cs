using System;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [field: SerializeField] public float MaxValue { get; private set; } = 100;
    [field: SerializeField] public float CurrentValue { get; private set; } = 85;

    protected float _minValue;
    protected Coroutine _timeoutDamageWork;

    public event Action Dead;
    public event Action<float> Changed;

    public void ChangeValue(float value)
    {
        CurrentValue = Mathf.Clamp(CurrentValue += value, _minValue, MaxValue);

        Changed?.Invoke(CurrentValue);

        if (CurrentValue == _minValue)
            Dead?.Invoke();
    }
}
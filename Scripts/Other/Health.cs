using System;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    protected float MinValue;
    protected Coroutine TimeoutDamageWork;

    public event Action Dead;
    public event Action<float> Changed;

    [field: SerializeField] public float MaxValue { get; private set; } = 100;
    [field: SerializeField] public float CurrentValue { get; private set; } = 85;

    public void Heal(float value)
    {
        ChangeValue(value);
    }

    public void Hit(float value)
    {
        ChangeValue(-value);

        if (CurrentValue == MinValue)
            Dead?.Invoke();
    }

    private void ChangeValue(float value)
    {
        CurrentValue = Mathf.Clamp(CurrentValue += value, MinValue, MaxValue);
        Changed?.Invoke(CurrentValue);
    }
}
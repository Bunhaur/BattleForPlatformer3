using UnityEngine;

public class HealthBox : MonoBehaviour
{
    [field: SerializeField] public int HealthRecovery { get; private set; } = 50;

    public void Remove()
    {
        Destroy(gameObject);
    }
}
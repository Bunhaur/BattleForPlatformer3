using UnityEngine;

public class HealthBox : MonoBehaviour
{
    public readonly int HealthRecovery = 50;

    public void Remove()
    {
        Destroy(gameObject);
    }
}
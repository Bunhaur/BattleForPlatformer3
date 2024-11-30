using UnityEngine;

public class InputService : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Jump = nameof(Jump);

    public float GetHorizontal()
    {
        return Input.GetAxisRaw(Horizontal);
    }

    public bool IsPushJump() 
    { 
        return Input.GetButtonDown(Jump);
    }
}
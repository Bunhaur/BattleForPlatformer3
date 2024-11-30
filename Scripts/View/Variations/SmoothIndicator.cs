using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class SmoothIndicator : MonoBehaviour, IShowable
{
    private Slider _slider;
    private Coroutine _increaseValueJob;
    private float _speedChange = 50f;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public void Show(float value)
    {
        if (_increaseValueJob != null)
            StopCoroutine(_increaseValueJob);

        _increaseValueJob = StartCoroutine(IncreaseValue(value));
    }

    private IEnumerator IncreaseValue(float value)
    {
        while (_slider.value != value)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, value, _speedChange * Time.deltaTime);

            yield return null;
        }

        _increaseValueJob = null;
    }
}
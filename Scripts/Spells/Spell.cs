using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public abstract class Spell : MonoBehaviour
{
    [SerializeField] protected PlayerHealth _playerHealth;
    [SerializeField] protected Button _button;
    [SerializeField] protected float _cooldown;
    [SerializeField] protected float _actionTime;

    public event Action Ended;

    private Image _image;
    private Coroutine _fillImageWork;
    private float _imageMaxFill = 1;

    protected virtual void Awake()
    {
        Init();
    }

    protected virtual void OnEnable()
    {
        _button.onClick.AddListener(Activate);
        Ended += Diactivate;
    }

    protected virtual void OnDisable()
    {
        _button.onClick.RemoveListener(Activate);
        Ended -= Diactivate;
    }

    private IEnumerator StartFillImageLogic()
    {
        yield return _fillImageWork = StartCoroutine(FillPicture(0, _actionTime));

        Ended?.Invoke();

        yield return _fillImageWork = StartCoroutine(FillPicture(_imageMaxFill, _cooldown));

        Diactivate();
    }

    private IEnumerator FillPicture(float target, float time)
    {
        while (_image.fillAmount != target)
        {
            _image.fillAmount = SmoothValueChange(target, time);

            yield return null;
        }
    }
    private float SmoothValueChange(float target, float time)
    {
        return Mathf.MoveTowards(_image.fillAmount, target, Time.deltaTime / time);
    }

    protected virtual void Init()
    {
        _image = GetComponent<Image>();
        _image.fillMethod = Image.FillMethod.Radial360;
    }

    protected virtual void Activate()
    {
        _button.interactable = false;
        _fillImageWork = StartCoroutine(StartFillImageLogic());
    }

    protected virtual void Diactivate()
    {
        _button.interactable = true;
    }
}
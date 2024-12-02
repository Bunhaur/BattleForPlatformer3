using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public abstract class Spell : MonoBehaviour
{
    [SerializeField] protected PlayerHealth PlayerHealth;
    [SerializeField] protected Button Button;
    [SerializeField] protected float Cooldown = 4;
    [SerializeField] protected float ActionTime = 6;

    private Image _image;
    private Coroutine _fillImageWork;
    private float _imageMaxFill = 1;

    public event Action Ended;

    protected virtual void Awake()
    {
        Init();
    }

    protected virtual void OnEnable()
    {
        Button.onClick.AddListener(Activate);
        Ended += Diactivate;
    }

    protected virtual void OnDisable()
    {
        Button.onClick.RemoveListener(Activate);
        Ended -= Diactivate;
    }

    protected virtual void Init()
    {
        _image = GetComponent<Image>();
        _image.fillMethod = Image.FillMethod.Radial360;
    }

    protected virtual void Activate()
    {
        Button.interactable = false;
        _fillImageWork = StartCoroutine(StartFillImageLogic());
    }

    protected virtual void Diactivate()
    {
        Button.interactable = true;
    }

    private IEnumerator StartFillImageLogic()
    {
        yield return StartCoroutine(FillPicture(0, ActionTime));

        Ended?.Invoke();

        yield return StartCoroutine(FillPicture(_imageMaxFill, Cooldown));

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
}
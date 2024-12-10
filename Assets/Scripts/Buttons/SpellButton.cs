using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class SpellButton : MonoBehaviour
{
    [SerializeField] private float _actionTime;
    [SerializeField] private float _cooldown;

    private float _minImageFillValue = 0;
    private float _maxImageFillValue = 1;

    private Image _image;
    private Button _button;
    private Coroutine StartFillImageLogicWork;

    public event Action ActionEnded;

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        _button?.onClick.AddListener(Disable);
        ActionEnded += Enable;
    }

    private void OnDisable()
    {
        _button?.onClick.RemoveListener(Disable);
        ActionEnded -= Enable;
    }

    public Button GetButton()
    {
        return _button;
    }

    private void Init()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();

        _image.fillMethod = Image.FillMethod.Radial360;
    }

    private void Disable()
    {
        _button.interactable = false;

        StartFillImageLogicWork = StartCoroutine(StartFillImageLogic());
    }

    private void Enable()
    {
        _button.interactable = true;
    }

    private IEnumerator StartFillImageLogic()
    {
        yield return FillImage(_minImageFillValue, _actionTime);

        ActionEnded?.Invoke();

        yield return FillImage(_maxImageFillValue, _cooldown);
    }

    private IEnumerator FillImage(float target, float time)
    {
        while (_image.fillAmount != target)
        {
            _image.fillAmount = Mathf.MoveTowards(_image.fillAmount, target, Time.deltaTime / time);

            yield return null;
        }
    }
}
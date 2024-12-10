using UnityEngine;
using UnityEngine.UI;

public class AuraView : MonoBehaviour
{
    [SerializeField] private SpellButton _spellButton;
    [SerializeField] private Aura _aura;

    private Button _button;

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Activate);
        _spellButton.ActionEnded += Diactivate;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Activate);
        _spellButton.ActionEnded -= Diactivate;
    }

    private void Activate()
    {
        _aura.gameObject.SetActive(true);
    }

    private void Diactivate()
    {
        if (_aura != null)
            _aura.gameObject.SetActive(false);
    }

    private void Init()
    {
        _button = _spellButton.GetButton();
    }
}
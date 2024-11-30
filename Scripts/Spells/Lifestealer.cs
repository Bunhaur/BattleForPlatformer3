using System.Collections;
using UnityEngine;

public class Lifestealer : Spell
{
    [SerializeField] private float _lifeStealValue;
    [SerializeField] private float _delayNextLifeSteal = .5f;
    [SerializeField] private SpelllAura _aura;

    private WaitForSeconds _delay;
    private Coroutine _stealWork;
    private bool _canDamage;

    protected override void OnEnable()
    {
        base.OnEnable();

        Ended += DisableAura;
        _aura.Triggered += StartLogic;
        _button.onClick.AddListener(EnableAura);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        Ended -= DisableAura;
        _button.onClick.RemoveListener(EnableAura);
    }

    private void EnableAura()
    {
        _aura.gameObject.SetActive(true);
    }

    private void DisableAura()
    {
        _aura.gameObject.SetActive(false);
    }

    private void StartLogic(EnemyHealth enemyHealth)
    {
        if (_canDamage)
        {
            _canDamage = false;
            _stealWork = StartCoroutine(Steal(enemyHealth));
        }
    }

    private IEnumerator Steal(EnemyHealth enemyHealth)
    {
        enemyHealth.ChangeValue(-_lifeStealValue);
        _playerHealth.ChangeValue(_lifeStealValue);

        yield return _delay;

        _canDamage = true;
    }

    protected override void Init()
    {
        base.Init();
        
        DisableAura();

        _delay = new WaitForSeconds(_delayNextLifeSteal);
        _canDamage = true;
    }
}

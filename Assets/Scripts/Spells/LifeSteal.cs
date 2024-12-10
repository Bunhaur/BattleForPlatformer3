using System.Collections;
using UnityEngine;

public class LifeSteal : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private float _stealValue;

    private WaitForSeconds _wait;
    private Coroutine _stealWork;

    private bool _canDamage;
    private float _delayNextLifeSteal = .5f;

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        _canDamage = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyHealth enemyHealth) && _canDamage)
        {
            _canDamage = false;

            _stealWork = StartCoroutine(Steal(enemyHealth));
        }
    }

    private void Init()
    {
        _wait = new WaitForSeconds(_delayNextLifeSteal);
    }
    private IEnumerator Steal(EnemyHealth enemyHealth)
    {
        if (enemyHealth.CurrentValue < _stealValue)
            _playerHealth.Heal(enemyHealth.CurrentValue);
        else
            _playerHealth.Heal(_stealValue);

        enemyHealth.Hit(_stealValue);

        yield return _wait;

        _canDamage = true;
    }
}
using System.Collections;
using UnityEngine;

public class SpawnerHealthBox : MonoBehaviour
{
    [SerializeField] private HealthBox _prefab;
    [SerializeField] private Vector2 _spawnAreaMin;
    [SerializeField] private Vector2 _spawnAreaMax;
    [SerializeField] private float _cooldown;

    private void Start()
    {
        StartCoroutine(CreateHealthBoxes());
    }

    private IEnumerator CreateHealthBoxes()
    {
        Vector3 randomPosition;
        float radius = 3f;
        var time = new WaitForSeconds(_cooldown);

        while (enabled)
        {
            while (Physics2D.OverlapCircle(GetRandomSpawnPoint(out randomPosition), radius) != null) ;

            Instantiate(_prefab, randomPosition, Quaternion.identity);

            yield return time;
        }
    }

    private Vector3 GetRandomSpawnPoint(out Vector3 position)
    {
        float randomPositionX;
        float randomPositionY;
        Vector2 spawnPosition;

        randomPositionX = Random.Range(_spawnAreaMin.x, _spawnAreaMax.x);
        randomPositionY = Random.Range(_spawnAreaMin.y, _spawnAreaMax.y);
        spawnPosition = new Vector2(randomPositionX, randomPositionY);
        position = spawnPosition;

        return spawnPosition;
    }
}

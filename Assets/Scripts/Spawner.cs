using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Finish _finish;
    [SerializeReference] private float _timeSpawn;
    private float _timeAfterLastSpawn;
 
    private void Start()
    {
        _timeAfterLastSpawn = _timeSpawn;
    }

    void Update()
    {
        _timeAfterLastSpawn -= Time.deltaTime;
        if (_player.IsDied || _finish.IsFinished)
        {
            return;
        }
        else
        {
            if(_timeAfterLastSpawn <= 0)
            {
                InstantiateEnemy();
                _timeAfterLastSpawn = _timeSpawn;
            }
        }
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_enemyPrefab, _spawnPoint.position, _spawnPoint.rotation);
        enemy.Init(_player);
    }
}

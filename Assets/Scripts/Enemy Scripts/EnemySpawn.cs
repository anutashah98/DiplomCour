using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform[] _spawnPosition;
    [SerializeField] private int _enemySpawnLimit = 10;
    [SerializeField] private float _minSpawnTime = 2f, _maxSpawnTime = 5f;
   
    public static EnemySpawn _instance;

    private GameObject _newEnemy;
   [SerializeField] private List<GameObject> _spawnEnemies = new List<GameObject>();

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    private void Start()
    {
        Invoke("SpawnEnemy", Random.Range(_minSpawnTime, _maxSpawnTime));
    }

    private void SpawnEnemy()
    {
        Invoke("SpawnEnemy", Random.Range(_minSpawnTime, _maxSpawnTime));
        
        if (_spawnEnemies.Count == _enemySpawnLimit)
            return;

        _newEnemy = Instantiate(_enemyPrefab, _spawnPosition[Random.Range(0, _spawnPosition.Length)].position,
            Quaternion.identity);
        
        _spawnEnemies.Add(_newEnemy);
        
        
    }

    public void EnemyDied(GameObject enemy)
    {
        _spawnEnemies.Remove(enemy);
    }
}

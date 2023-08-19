using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _healthEnemy = 100f;
    [SerializeField] private Slider _enemyHealth;

    private Enemy _enemyScript;

    private void Awake()
    {
        _enemyScript = GetComponent<Enemy>();
    }

    public void TakeDamage(float damageAmount)
    {
        if (_healthEnemy <= 0)
            return;

        _healthEnemy -= damageAmount;

        if (_healthEnemy <= 0f)
        {
            _healthEnemy = 0;
            _enemyScript.EnemyDied();
            
            EnemySpawn._instance.EnemyDied(gameObject);
            
            GameplayController._instance.RestartGame();
        }

        _enemyHealth.value = _healthEnemy;
    }
}

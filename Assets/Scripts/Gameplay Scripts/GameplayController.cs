using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static GameplayController _instance;

    [SerializeField] private TextMeshProUGUI _enemyKillCountTxt;

    private int _enemyKilledCount;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public void EnemyKilled()
    {
        _enemyKilledCount++;
        _enemyKillCountTxt.text = "Enemy Killed: " + _enemyKilledCount;
    }

    public void RestartGame()
    {
        Invoke("Restart", 3f);
    }

    private void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
    }
}

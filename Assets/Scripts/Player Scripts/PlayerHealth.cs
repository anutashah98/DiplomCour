using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _healthPlayer = 100f;
    [SerializeField] private Slider _playerHealthSlider;

    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    public void TakeDamage(float damageAmount)
    {
        if (_healthPlayer <= 0)
            return;

        _healthPlayer -= damageAmount;

        if (_healthPlayer <= 0)
        {
            _playerMovement.PlayerDied();
            
            GameplayController._instance.RestartGame();//Игрок ничего не должен знать про геймплей контроллер, это не его ответственность, статика плохо
        }

        _playerHealthSlider.value = _healthPlayer;
    }
}

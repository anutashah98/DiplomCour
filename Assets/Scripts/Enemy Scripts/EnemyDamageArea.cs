using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageArea : MonoBehaviour
{
    [SerializeField] private float _deactivateWaitTime = 0.1f;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _damageAmount = 5f;

    private float _deactivateTimer;
    private bool _canDealDamage;

    private PlayerHealth _playerHealth;

    private void Awake()
    {
        _playerHealth = GameObject.FindWithTag(TagManager.PLAYER_TAG).GetComponent<PlayerHealth>();
        //Можно заменить на 
        
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, 1f, _playerLayer))
        {
            if (_canDealDamage)
            {
                _canDealDamage = false;
                _playerHealth.TakeDamage(_damageAmount);
            }
        }
        DeactivateDamageArea();
    }

    private void DeactivateDamageArea()
    {
        if (Time.time > _deactivateTimer)
            gameObject.SetActive(false);
    }

    public void ResetDeactivatedTimer()
    {
        _canDealDamage = true;
        _deactivateTimer = Time.time + _deactivateWaitTime;
    }
}

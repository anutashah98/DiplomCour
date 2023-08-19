using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _stoppingDistance = 1.5f;
    [SerializeField] private float _attackWaitTime = 2.5f;
    [SerializeField] private float _attackFinishedWaitTime = 0.5f;
    [SerializeField] private EnemyDamageArea _enemyDamageArea;
    [SerializeField] private RectTransform _healthBarTransform;

    private Transform _playerTarget;
    private Vector3 _tempScale;
    private float _attackTimer;
    private float _attackFinishedTimer;
    private bool _enemyDied;
    private Vector3 _helthBarTempScale;

    private PlayerAnimation _enemyAnimation;

    private void Awake()
    {
        _enemyAnimation = GetComponent<PlayerAnimation>();
        _playerTarget = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;
        
    }

    private void Update()
    {
        if(_enemyDied)
            return;
        
        SearchForPlayer();
    }

    private void SearchForPlayer()
    {
        if (!_playerTarget)
            return;

        if (Vector3.Distance(transform.position, _playerTarget.position) > _stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, _playerTarget.position,
                _moveSpeed * Time.deltaTime);

            _enemyAnimation.PlayAnimation(TagManager.WALK_ANIMATION_NAME);
            
            HandleFacingDirection();
        }
        else
        {
            CheckIfAttackFinished();
            Attack();
        }
            
    }

    private void HandleFacingDirection()
    {
        _tempScale = transform.localScale;

        if (transform.position.x > _playerTarget.position.x)
            _tempScale.x = Mathf.Abs(_tempScale.x);
        else
            _tempScale.x = -Mathf.Abs(_tempScale.x);

        transform.localScale = _tempScale;

        if (transform.localScale.x > 0f)
            _helthBarTempScale.x = Mathf.Abs(_helthBarTempScale.x);
        else
            _helthBarTempScale.x = -Mathf.Abs(_helthBarTempScale.x);

        _healthBarTransform.localScale = _helthBarTempScale;
    }

    private void CheckIfAttackFinished()
    {
        if (Time.time > _attackFinishedTimer)
            _enemyAnimation.PlayAnimation(TagManager.IDLE_ANIMATION_NAME);
    }

    private void Attack()
    {
        if (Time.time > _attackTimer)
        {
            _attackFinishedTimer = Time.time + _attackFinishedWaitTime;
            _attackTimer = Time.time + _attackWaitTime;
            
            _enemyAnimation.PlayAnimation(TagManager.ATTACK_ANIMATION_NAME);
        }
    }

    private void EnemyAttacked()
    {
        _enemyDamageArea.gameObject.SetActive(true);
        _enemyDamageArea.ResetDeactivatedTimer();
    }

    public void EnemyDied()
    {
        _enemyDied = true;
        _enemyAnimation.PlayAnimation(TagManager.DEATH_ANIMATION_NAME);
        Invoke("DestroyEnemyAfterDelay", 1.5f);
    }

    private void DestroyEnemyAfterDelay()
    {
        Destroy(gameObject);
    }
}

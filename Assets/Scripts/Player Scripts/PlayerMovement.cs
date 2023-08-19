using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3.5f;
    [SerializeField]
    private float _minBound_X = -71f, _maxBound_X = 71f, _minBound_Y = -3.3f, _maxBound_Y = 0f; //крайние точки при ходьбе
    [SerializeField] private float _shootWaitTime = 0.5f;
    [SerializeField] private float _moveWaitTime = 0.3f;

    private Vector3 _tempPos;

    private float _xAxis, _yAxis;
    private float _waitBeforeShooting;
    private float _waitBeforMoving;
    private bool _canMove = true;
    private bool _playerDied;

    private PlayerAnimation _playerAnimations;
    private PlayerShootingManager _playerShootingManager;

    private void Awake()
    {
        _playerAnimations = GetComponent<PlayerAnimation>();
        _playerShootingManager = GetComponent<PlayerShootingManager>();
    }

    void Update()
    {
        if (_playerDied)
            return;
        
        HandleMovement();
        HandleAnimation();
        HandleFacingDirection();
        
        HandleShooting();
        CheckIfCanMove();
    }

    void HandleMovement()
    {
        _xAxis = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);
        _yAxis = Input.GetAxisRaw(TagManager.VERTICAL_AXIS);

        if (!_canMove)
            return;
        
        _tempPos = transform.position;

        _tempPos.x += _xAxis * _moveSpeed * Time.deltaTime;
        _tempPos.y += _yAxis * _moveSpeed * Time.deltaTime;

        if (_tempPos.x < _minBound_X)
            _tempPos.x = _minBound_X;
        if (_tempPos.x > _maxBound_X)
            _tempPos.x = _maxBound_X;
        if (_tempPos.y < _minBound_Y)
            _tempPos.y = _minBound_Y;
        if (_tempPos.y > _maxBound_Y)
            _tempPos.y = _maxBound_Y;

        transform.position = _tempPos;
    }

    void HandleAnimation()
    {
        if (!_canMove)
            return;
        
        if (Mathf.Abs(_xAxis) > 0 || Mathf.Abs(_yAxis) > 0)
            _playerAnimations.PlayAnimation(TagManager.WALK_ANIMATION_NAME);
        else
            _playerAnimations.PlayAnimation(TagManager.IDLE_ANIMATION_NAME);
    }

    void HandleFacingDirection()
    {
        if (_xAxis > 0)
            _playerAnimations.SetFacingDirection(true);
        else if (_xAxis < 0)
            _playerAnimations.SetFacingDirection(false);
        
        
    }

    void StopMovement()
    {
        _canMove = false;
        _waitBeforMoving = Time.time + _moveWaitTime;
    }

    void Shoot()
    {
        _waitBeforeShooting = Time.time + _shootWaitTime;
        StopMovement();
        _playerAnimations.PlayAnimation(TagManager.SHOOT_ANIMATION_NAME);
        
        _playerShootingManager.Shoot(transform.localScale.x);
        
    }



    void CheckIfCanMove()
    {
        if (Time.time > _waitBeforMoving)
            _canMove = true;
    }

    void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Time.time > _waitBeforeShooting)
                Shoot();
        }
    }

    public void PlayerDied()
    {
        _playerDied = true;
        
        _playerAnimations.PlayAnimation(TagManager.DEATH_ANIMATION_NAME);
        Invoke("DestroyPlayerAfterDelay", 2f);
    }

    private void DestroyPlayerAfterDelay()
    {
        Destroy(gameObject);
    }
}    
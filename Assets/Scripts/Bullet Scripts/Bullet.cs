using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 15f;
    [SerializeField] private float _damageAmount = 35f;
    
    private Vector3 _moveVector = Vector3.zero;
    private Vector3 _tempScale;

    private void Update()
    {
        MoveBullet();
    }

    private void MoveBullet()
    {
        _moveVector.x = _moveSpeed * Time.deltaTime;
        transform.position += _moveVector;
    }

    public void SetNegativeSpeed()
    {
        _moveSpeed *= -1f;

        _tempScale = transform.localScale;
        _tempScale.x = -_tempScale.x;
        transform.localScale = _tempScale;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.ENEMY_TAG))
        {
            
        }
    }
}

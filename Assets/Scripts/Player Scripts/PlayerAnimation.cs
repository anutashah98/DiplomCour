using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;

    private Vector3 _tempScale;

    private int _currentAnimation;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void PlayAnimation(string animationName)
    {
        if (_currentAnimation == Animator.StringToHash(animationName))
            return;

        _anim.Play(animationName);

        _currentAnimation = Animator.StringToHash(animationName);
    }

    public void SetFacingDirection(bool faceRight)
    {
        _tempScale = transform.localScale;
        
        //пишется в одну строчку
        //так: _tempScale.x = faceRight ? 1f : -1f;
        if (faceRight)
            _tempScale.x = 1f;
        else
            _tempScale.x = -1f;

        transform.localScale = _tempScale;
    }
}

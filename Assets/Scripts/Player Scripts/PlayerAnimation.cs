using System;
using System.Collections;
using System.Collections.Generic;
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
        
        if (faceRight)
            _tempScale.x = 1f;
        else
            _tempScale.x = -1f;

        transform.localScale = _tempScale;
    }
}

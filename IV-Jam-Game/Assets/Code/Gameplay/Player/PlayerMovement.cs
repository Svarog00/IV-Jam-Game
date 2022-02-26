using Assets.Code.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement
{
    private const string IsRunningAnimation = "IsRunning";

    private Rigidbody2D _rb2;
    private Transform _playerTransform;
    private Animator _animator;

    private Vector3 _targetPosition;
    private Vector3 _movementVector;

    private float _speed;
    private bool _isMoving;
    private bool _faceRight = false;

    public PlayerMovement(Rigidbody2D rb2, Transform transform, float speed, Animator animator)
    {
        _rb2 = rb2;
        _playerTransform = transform;
        _speed = speed;
        _animator = animator;
    }

    public void SetPoint(Vector3 point)
    {
        _targetPosition = point;
        _isMoving = true;
        _animator.SetBool(IsRunningAnimation, _isMoving);
        _movementVector = GetMovementVector();
        SetDirectionSprite();
    }

    public void Move()
    {
        var movementVector = Vector3.Distance(_playerTransform.position, _targetPosition);
        if (_isMoving)
        {
            _rb2.MovePosition(_rb2.position + (Vector2)_movementVector.normalized * _speed * Time.deltaTime);
            if(Vector3.Distance(_playerTransform.position, _targetPosition) <= 0.1f)
            {
                _isMoving = false;
                _animator.SetBool(IsRunningAnimation, _isMoving);
            }
        }
    }

    private Vector2 GetMovementVector()
    {
        return _targetPosition - _playerTransform.position;
    }

    private void SetDirectionSprite()
    {
        if (_movementVector.normalized.x > 0 && _faceRight == true)
        {
            Flip();
        }
        if (_movementVector.normalized.x < 0 && _faceRight == false)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _faceRight = !_faceRight;
        Vector3 Scaler = _playerTransform.localScale;
        Scaler.x *= -1;
        _playerTransform.localScale = Scaler;
    }

}

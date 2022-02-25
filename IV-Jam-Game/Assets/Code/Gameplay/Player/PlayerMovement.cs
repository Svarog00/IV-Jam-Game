using Assets.Code.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement
{
    private Rigidbody2D _rb2;
    private Transform _playerTransform;

    private Vector3 _targetPosition;
    private Vector3 _movementVector;

    private float _speed;
    private bool _isMoving;

    public PlayerMovement(Rigidbody2D rb2, Transform transform, float speed)
    {
        _rb2 = rb2;
        _playerTransform = transform;
        _speed = speed;
    }

    public void SetPoint(Vector3 point)
    {
        _targetPosition = point;
        _isMoving = true;
        _movementVector = GetMovementVector();
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
            }
        }
    }

    private Vector2 GetMovementVector()
    {
        return _targetPosition - _playerTransform.position;
    }
}

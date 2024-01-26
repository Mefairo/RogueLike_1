using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IMoveable
{
    [SerializeField] private float _moveSpeed;

    private Vector3 _moveInput;
    private Vector2 _moveVelocity;
    private Rigidbody2D _rbPlayer;
    private bool _facingRight = true;

    public event Action<float> OnChangeMoveSpeed;

    public Rigidbody2D RBPlayer => _rbPlayer;

    public float MoveSpeed
    {
        get => _moveSpeed;
        set
        {
            _moveSpeed = value;
            OnChangeMoveSpeed?.Invoke(value);
        }
    }

    private void Awake()
    {
        _rbPlayer = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();

        CheckFlip();
    }
    private void FixedUpdate()
    {
        _rbPlayer.MovePosition(_rbPlayer.position + _moveVelocity * Time.deltaTime);
    }
    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    public void Move()
    {
        _moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _moveVelocity = _moveInput.normalized * _moveSpeed;
    }

    private void CheckFlip()
    {
        if (!_facingRight && _moveInput.x > 0)
            Flip();

        else if (_facingRight && _moveInput.x < 0)
            Flip();
    }
}

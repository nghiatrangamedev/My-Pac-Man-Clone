using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float _speed = 10.0f;
    float _horizontalInput;
    float _verticalInput;

    Rigidbody2D _playerRb;

    // Start is called before the first frame update
    void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    void PlayerInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    void PlayerMovement()
    {
        if (_horizontalInput != 0)
        {
            _playerRb.MovePosition(transform.position + transform.right * _speed * _horizontalInput * Time.deltaTime);
        }

        if (_verticalInput != 0)
        {
            _playerRb.MovePosition(transform.position + transform.up * _speed * _verticalInput * Time.deltaTime);
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float _speed = 10.0f;
    float _horizontalInput;
    float _verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
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
            transform.Translate(Vector2.right * _horizontalInput * _speed * Time.deltaTime);
        }

        if (_verticalInput != 0)
        {
            transform.Translate(Vector2.up * _verticalInput * _speed * Time.deltaTime);
        }
    }


}

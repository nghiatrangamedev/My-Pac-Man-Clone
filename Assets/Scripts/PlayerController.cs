using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float _speed = 10.0f;
    float _horizontalInput;
    float _verticalInput;

    bool _isPowerup = false;
    public bool IsPowerUp

    {
        get { return _isPowerup; }
        private set
        {
            _isPowerup = value;
        }
    }

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

    IEnumerator TurnOffPowerUp()
    {
        yield return new WaitForSeconds(5);
        _isPowerup = false;
        Debug.Log("Power up is over");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LeftTeleport")
        {
            transform.position = new Vector3(15, transform.position.y, transform.position.z);
        }

        else if (collision.gameObject.tag == "RightTeleport")
        {
            transform.position = new Vector3(-15, transform.position.y, transform.position.z);
        }

        else if (collision.gameObject.tag == "PowerUp")
        {
            Destroy(collision.gameObject);
            _isPowerup = true;
            Debug.Log("Now you can eat the enemy ! ^.^ ");
            StartCoroutine(TurnOffPowerUp());
        }
    }


}

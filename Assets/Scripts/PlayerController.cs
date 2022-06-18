using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] AudioClip _chompSound;

    AudioSource _playerAudio;
    Animator _playerAnimator;

    float _speed = 10.0f;
    float _horizontalInput;
    float _verticalInput;

    bool _isPowerup = false;
    bool _isDeath = false;

    bool _isChompSoundPlayed = false;

    bool _isFaceRight = true;
    bool _isFaceLeft = false;
    bool _isFaceUp = false;
    bool _isFaceDown = false;


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
        _playerAudio = GetComponent<AudioSource>();
        _playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManager.IsStarting)
        {
            ActiveEatAnimation();
            if (!_isDeath)
            {
                PlayerInput();
                RotatePlayer();
            }
            
        }
        
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
            CheckChompSound();
            _playerRb.MovePosition(transform.position + Vector3.right * _speed * _horizontalInput * Time.deltaTime);
        }

        if (_verticalInput != 0)
        {
            CheckChompSound();
            _playerRb.MovePosition(transform.position + Vector3.up * _speed * _verticalInput * Time.deltaTime);
        }
    }

    void RotatePlayer()
    {
        if (_verticalInput > 0 && !_isFaceUp)
        {
            _isFaceUp = true;
            transform.rotation = Quaternion.Euler(0, 0, 90);

            _isFaceDown = false;
            _isFaceLeft = false;
            _isFaceRight = false;
        }

        else if (_verticalInput < 0 && !_isFaceLeft)
        {
            _isFaceDown = true;
            transform.rotation = Quaternion.Euler(0, 0, -90);

            _isFaceUp = false;
            _isFaceLeft = false;
            _isFaceRight = false;
        }

        else if (_horizontalInput > 0 && !_isFaceRight)
        {
            _isFaceRight = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);

            _isFaceUp = false;
            _isFaceDown = false;
            _isFaceLeft = false;
        }

        else if (_horizontalInput < 0 && !_isFaceLeft)
        {
            _isFaceLeft = true;
            transform.rotation = Quaternion.Euler(0, 180, 0);

            _isFaceUp = false;
            _isFaceDown = false;
            _isFaceRight = false;
        }
    }

    void CheckChompSound()
    {
        if (!_isChompSoundPlayed)
        {
            _playerAudio.PlayOneShot(_chompSound);
            _isChompSoundPlayed = true;
            StartCoroutine(ChompSoundTime());
        }
        
    }

    void ActiveEatAnimation()
    {
        if (_horizontalInput != 0 || _verticalInput != 0)
        {
            _playerAnimator.SetFloat("_speed", 1);
        }

        else
        {
            _playerAnimator.SetFloat("_speed", 0);
        }
    }

    IEnumerator TurnOffPowerUp()
    {
        yield return new WaitForSeconds(5);
        _isPowerup = false;
        Debug.Log("Power up is over");
    }

    IEnumerator ChompSoundTime()
    {
        yield return new WaitForSeconds(1);
        _isChompSoundPlayed = false;
    }

    IEnumerator WaitDeathAnimatorEnd()
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!IsPowerUp && !_isDeath)
            {
                _isDeath = true;
                _playerAnimator.SetBool("_isDeath", true);
                StartCoroutine(WaitDeathAnimatorEnd());
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] Transform _playerPostion;
    [SerializeField] Transform _ghostPlace;
    [SerializeField] PlayerController _playerController;
    [SerializeField] AudioClip _beEatenSound;
    [SerializeField] AudioClip _deathSound;

    AudioSource _enemyAudio;
    Rigidbody2D _enemyRb;
    SpriteRenderer _enemySpriteRenderer;
    Animator _enemyAnimator;

    float _speed = 10.0f;

    bool _isMoveUp = false;
    bool _isMoveDown = false;
    bool _isMoveRight = false;
    bool _isMoveLeft = false;

    bool _isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        _enemyRb = GetComponent<Rigidbody2D>();
        _enemySpriteRenderer = GetComponent<SpriteRenderer>();
        _enemyAudio = GetComponent<AudioSource>();
        _enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (_gameManager.IsStarting)
        {
            ChangeColor();

            if (!_isDead)
            {
                AliveMovement();
            }

            else if (_isDead)
            {
                DeadMovement();
            }
        }
       
    }

    void AliveMovement()
    {
        _speed = 10.0f;

        if (!_isMoveUp && ! _isMoveDown && !_isMoveLeft && !_isMoveRight)
        {
            Vector3 distance = _playerPostion.position - transform.position;
            _enemyRb.MovePosition(transform.position + distance.normalized * _speed * Time.deltaTime);
        }

        else
        {
            if (_isMoveDown)
            {
                _enemyRb.MovePosition(transform.position + Vector3.down * _speed * Time.deltaTime);
            }

            else if (_isMoveUp)
            {
                _enemyRb.MovePosition(transform.position + Vector3.up * _speed * Time.deltaTime);
            }

            else if (_isMoveLeft)
            {
                _enemyRb.MovePosition(transform.position + Vector3.left * _speed * Time.deltaTime);
            }

            else
            {
                _enemyRb.MovePosition(transform.position + Vector3.right * _speed * Time.deltaTime);
            }
        }
    }

    void DeadMovement()
    {
        _speed = 20.0f;

        if (!_isMoveUp && !_isMoveDown && !_isMoveLeft && !_isMoveRight)
        {
            Vector3 distance = _ghostPlace.position - transform.position;
            _enemyRb.MovePosition(transform.position + distance.normalized * _speed * Time.deltaTime);
        }

        else
        {
            if (_isMoveDown)
            {
                _enemyRb.MovePosition(transform.position + Vector3.down * _speed * Time.deltaTime);
            }

            else if (_isMoveUp)
            {
                _enemyRb.MovePosition(transform.position + Vector3.up * _speed * Time.deltaTime);
            }

            else if (_isMoveLeft)
            {
                _enemyRb.MovePosition(transform.position + Vector3.left * _speed * Time.deltaTime);
            }

            else
            {
                _enemyRb.MovePosition(transform.position + Vector3.right * _speed * Time.deltaTime);
            }
        }
    }

    void ChangeColor()
    {
        if (_playerController.IsPowerUp && !_isDead)
        {
            _enemySpriteRenderer.color = Color.cyan;
        }

        else if (!_playerController.IsPowerUp && !_isDead)
        {
            _enemySpriteRenderer.color = Color.red;
        }
    }

    void ActiveAnimation()
    {
        _enemyAnimator.SetBool("_isMoving", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            if (_playerController.IsPowerUp)
            {
                _enemyAudio.PlayOneShot(_beEatenSound);
                _isDead = true;
                _enemySpriteRenderer.color = Color.gray;
            }

            else
            {
                _enemyAudio.PlayOneShot(_deathSound);
                Debug.Log("Game Over");
            }

        }


        if (collision.gameObject.tag == "MoveUp")
        {
            _isMoveUp = true;
            _enemyRb.MovePosition(transform.position + Vector3.up * Time.deltaTime);
            Debug.Log("Move Up");
        }

        else if (collision.gameObject.tag == "MoveDown")
        {
            _isMoveDown = true;
            _enemyRb.MovePosition(transform.position + Vector3.down * Time.deltaTime);
            Debug.Log("Move Down");
        }

        else if (collision.gameObject.tag == "MoveLeft")
        {
            _isMoveLeft = true;
            _enemyRb.MovePosition(transform.position + Vector3.left * Time.deltaTime);
            Debug.Log("Move Left");
        }

        else if (collision.gameObject.tag == "MoveRight")
        {
            _isMoveRight = true;
            _enemyRb.MovePosition(transform.position + Vector3.right * Time.deltaTime);
            Debug.Log("Move Right");
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MoveUp")
        {
            _isMoveUp = false;
        }

        else if (collision.gameObject.tag == "MoveDown")
        {
            _isMoveDown = false;
        }

        else if (collision.gameObject.tag == "MoveLeft")
        {
            _isMoveLeft = false;
        }

        else if (collision.gameObject.tag == "MoveRight")
        {
            _isMoveRight = false;
        }
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

        else if (collision.gameObject.tag == "GhostPlace")
        {
            if (_isDead)
            {
                _isDead = false;
                _enemySpriteRenderer.color = Color.red;
            }
        }
    }
}

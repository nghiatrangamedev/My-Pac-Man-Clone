using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform _playerPostion;
    [SerializeField] PlayerController _playerController;

    Rigidbody2D _enemyRb;
    SpriteRenderer _enemySpriteRenderer;
   
    Vector3 _startPos;

    float _speed = 10.0f;

    bool _isCollideWithWall = false;
    bool _isCollideWithTopWall = false;
    bool _isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
        _enemyRb = GetComponent<Rigidbody2D>();
        _enemySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
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

    void AliveMovement()
    {
        if (!_isCollideWithWall && !_isCollideWithTopWall)
        {
            Vector3 distance = _playerPostion.position - transform.position;
            _enemyRb.MovePosition(transform.position + distance.normalized * _speed * Time.deltaTime);
        }

        else
        {
            if (_isCollideWithTopWall)
            {
                _enemyRb.MovePosition(transform.position + Vector3.down * _speed * Time.deltaTime);
            }

            else
            {
                _enemyRb.MovePosition(transform.position + Vector3.up * _speed * Time.deltaTime);
            }

        }
    }

    void DeadMovement()
    {
        if (!_isCollideWithWall && !_isCollideWithTopWall)
        {
            Vector3 distance = _startPos - transform.position;
            _enemyRb.MovePosition(transform.position + distance.normalized * _speed * Time.deltaTime);
        }

        else
        {
            if (_isCollideWithTopWall)
            {
                _enemyRb.MovePosition(transform.position + Vector3.down * _speed * Time.deltaTime);
            }

            else
            {
                _enemyRb.MovePosition(transform.position + Vector3.up * _speed * Time.deltaTime);
            }
        }
    }

    void ChangeColor()
    {
        if (_playerController.IsPowerUp && !_isDead)
        {
            _enemySpriteRenderer.color = Color.blue;
        }

        else if (!_playerController.IsPowerUp && !_isDead)
        {
            _enemySpriteRenderer.color = Color.red;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            if (_playerController.IsPowerUp)
            {
                _isDead = true;
                _enemySpriteRenderer.color = Color.gray;
            }

            else
            {
                Debug.Log("Game Over");
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }

        }


        if (collision.gameObject.tag == "Wall")
        {
            _isCollideWithWall = true;
            _enemyRb.MovePosition(transform.position + Vector3.up * Time.deltaTime);
            Debug.Log("Collide with wall");
        }

        else if (collision.gameObject.tag == "TopWall")
        {
            _isCollideWithTopWall = true;
            _enemyRb.MovePosition(transform.position + Vector3.down * Time.deltaTime);
            Debug.Log("Collide with top wall");
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            _isCollideWithWall = false;
            Debug.Log("Not collide with wall");
        }

        else if (collision.gameObject.tag == "TopWall")
        {
            _isCollideWithTopWall = false;
            Debug.Log("Not collide with top wall");
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

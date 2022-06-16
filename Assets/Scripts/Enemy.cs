using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform playerPostion;
    Rigidbody2D _enemyRb;

    float _speed = 10.0f;
    bool _isCollideWithWall = false;
    bool _isCollideWithTopWall = false;
    // Start is called before the first frame update
    void Start()
    {
        _enemyRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }

    void EnemyMovement()
    {
        if (!_isCollideWithWall && !_isCollideWithTopWall)
        {
            Vector3 distance = playerPostion.position - transform.position;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Game Over");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        else if (collision.gameObject.tag == "Wall")
        {
            _isCollideWithWall=true;
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    GameManager _gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        _gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _gameManagerScript.Score += 10;
            Destroy(gameObject);
        }
    }
}

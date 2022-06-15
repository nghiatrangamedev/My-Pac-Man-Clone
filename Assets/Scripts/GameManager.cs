using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject[] _foods;
    // Start is called before the first frame update
    void Start()
    {
        _foods = GameObject.FindGameObjectsWithTag("Food");
    }

    // Update is called once per frame
    void Update()
    {
        CheckFood();
    }

    void CheckFood()
    {
        _foods = GameObject.FindGameObjectsWithTag("Food");
        if (_foods.Length < 1)
        {
            Debug.Log("You win");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    AudioSource _audio;
    GameObject[] _foods;

    bool _isStarting = false;

    public bool IsStarting
    {
        get { return _isStarting; }

        private set
        {
            _isStarting = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
        _audio.Play();
        StartCoroutine(WaitOpeningEnd());
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

    IEnumerator WaitOpeningEnd()
    {
        yield return new WaitForSeconds(3);
        IsStarting = true;
    }
}

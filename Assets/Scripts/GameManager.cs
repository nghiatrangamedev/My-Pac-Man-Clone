using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText;

    [SerializeField] GameObject _gameStartText;
    [SerializeField] GameObject _gameOverText;
    [SerializeField] GameObject _winText;
    [SerializeField] GameObject _enemy;
    [SerializeField] GameObject _player;

    [SerializeField] AudioClip _beginningSound;
    [SerializeField] AudioClip _winningSound;

    AudioSource _enemyAudio;
    GameObject[] _foods;

    float _score = 0;

    bool _isStartAnimationPlayed = false;
    bool _isWinningSoundPlayed = false;

    public float Score
    {
        get { return _score; }
        set
        {
            _score = value;
        }
    }

    bool _isStarting = false;

    public bool IsStarting
    {
        get { return _isStarting; }

        set
        {
            _isStarting = value;
        }
    }

    bool _isGameOver = false;
    public bool IsGameOver
    {
        get { return _isGameOver; }

        set
        {
            _isGameOver = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _enemyAudio = GetComponent<AudioSource>();
        _enemyAudio.PlayOneShot(_beginningSound);
        StartCoroutine(WaitOpeningEnd());
        _foods = GameObject.FindGameObjectsWithTag("Food");
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isStarting)
        {
            GameStartAnimation();
        }

        DisplayScore();
        CheckFood();

        if (_isGameOver)
        {
            StartCoroutine(DisplayGameOver());
        }
    }

    void CheckFood()
    {
        _foods = GameObject.FindGameObjectsWithTag("Food");
        if (_foods.Length < 1)
        {
            PlayWinningSound();
            _winText.SetActive(true);
            Destroy(_player);
            Destroy(_enemy);
        }
    }

    void DisplayScore()
    {
        _scoreText.SetText("Score: " + Score);
    }

    void PlayWinningSound()
    {
        if (!_isWinningSoundPlayed)
        {
            _isWinningSoundPlayed = true;
            _enemyAudio.PlayOneShot(_winningSound);
        }
        
    }

    void GameStartAnimation()
    {
        if (!_isStartAnimationPlayed)
        {
            _isStartAnimationPlayed = true;
            _gameStartText.SetActive(true);
            StartCoroutine(GameStartAnimationTimeRate());
        }
    }

    IEnumerator GameStartAnimationTimeRate()
    {
        yield return new WaitForSeconds(0.4f);
        _gameStartText.SetActive(false);
        _isStartAnimationPlayed = false;

    }

    IEnumerator WaitOpeningEnd()
    {
        yield return new WaitForSeconds(3);
        IsStarting = true;
    }

    IEnumerator DisplayGameOver()
    {
        yield return new WaitForSeconds(1);
        _gameOverText.SetActive(true);
    }
}

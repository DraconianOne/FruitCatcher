using UnityEngine;
using System.Collections;
using System;

public class GameController : MonoBehaviour
{

    public event Action GameOver;
    public event Action<int> LifeLost;
    public event Action Restart;

    private static GameController _instance;
    public static GameController Instance { get { return _instance; }}
    
    private int lives = 3;
    private int score = 0;
    private bool isGameOver = false;
    private bool waitForRestart = false;
    
    public int Score { get { return score; } }

    [SerializeField] private float baseThrowDelay = 3.5f;
    
    [SerializeField] private float minThrowDelay = 0.5f;
    [SerializeField] private float restartDelay = 2f;
    //private WaitForSeconds throwDelayWait = new WaitForSeconds(2);
    private float _restartWait = 0f;
    private float _currentThrowDelay;
    private float _delayStep = 0.5f;
    private float _intervalTimer = 0f;
    private float _intervalDuration = 10f;
    private float _intervalMin = 5f;
    private float _challengeDuration = 10f;
    //private float _challengeTimer = 0f;
    private float _challengeIncrement = 5f;
    private bool _inChallengeState = false;
    
    [SerializeField] private Thrower _thrower;

    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartGame();
    }

    void FixedUpdate()
    {
        if (_restartWait > 0f) { _restartWait -= Time.deltaTime; }
        else if (waitForRestart)
        {
            Debug.Log("About to restart");
            RestartGame();
        }
        else
        {
            GameUpdate();
        }
    }

    void GameUpdate()
    {
        if(_intervalTimer > 0f) { _intervalTimer -= Time.deltaTime; }
        else if(_inChallengeState)
        {
            //Go to next phase
            Debug.Log("Finishing challenge state");
            _inChallengeState = false;
            _intervalDuration = Mathf.Max(_intervalDuration - 1, _intervalMin);
            _challengeDuration += _challengeIncrement;
            _currentThrowDelay = baseThrowDelay;
            _intervalTimer = _intervalDuration;
        }
        else
        {
            _currentThrowDelay -= _delayStep; //Add validation to make sure doesn't go under 0.5?
            Debug.Log("Lowering delay: " + _currentThrowDelay);
            if (Mathf.Approximately(_currentThrowDelay, minThrowDelay)) //??? Right to use approximately here?
            {
                Debug.Log("In Challenge State");
                _inChallengeState = true;
                _intervalTimer = _challengeDuration;
            }
            else
            {
                _intervalTimer = _intervalDuration;
            }
        }
    }
    
    private void StartGame()
    {
        score = 0;
        lives = 3;
        
        //TODO Wait until everything is loaded

        _intervalTimer = _intervalDuration;
        _currentThrowDelay = baseThrowDelay;
        
        //TODO Add countdown to start
        //Start Spawning balls
        
        StartCoroutine(ThrowBalls());

    }

    private void RestartGame()
    {
        Debug.Log("Restart");
        Restart?.Invoke();
        waitForRestart = false;
        Debug.Log("Restart - StartCoRoutine");
        //StartCoroutine(ThrowBalls());
    }

    private IEnumerator ThrowBalls()
    {
        Debug.Log($"Coroutine - throwBalls {isGameOver}, {waitForRestart}");
        //WaitForSeconds wait = new WaitForSeconds(GetThrowDelay());
        //while (!isGameOver && !waitForRestart)
        while(true)
        {
           Debug.Log("in coroutine");
           if(!isGameOver && !waitForRestart){
               Debug.Log("Throwing in coroutine");
                _thrower.ThrowItem(score);
           }
           yield return new WaitForSeconds(GetThrowDelay());
        }
    }

    private float GetThrowDelay()
    {
        
        Debug.Log("Current throw delay: "+_currentThrowDelay);
        if (waitForRestart) return 1f;
        return _currentThrowDelay;
    }
    

    public void LoseLife()
    {
        Debug.Log("Life Losst");
        //StopAllCoroutines();
        waitForRestart = true;
        lives--;
        //UIController.Instance.LoseLife(lives);
        LifeLost?.Invoke(lives);
        //Invoke Life Lost state
        
        isGameOver = lives <= 0;
        if (isGameOver)
        {
            GameOver?.Invoke();
            EndGame();
        } else {
            _restartWait = restartDelay;
        }
    }


    public void EndGame()
    {
        StopAllCoroutines();
        _thrower.StopThrowing();
        //Add event here
        //UIController.Instance.EndGame();
       
    }

    public void UpdateScore(int n)
    {
        score += n;
        UIController.Instance.UpdateScore(score);
    }
    
}

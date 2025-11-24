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

    [SerializeField] private float baseThrowDelay = 5f;
    [SerializeField] private float minThrowDelay = 0.5f;
    [SerializeField] private float restartDelay = 2f;
    private WaitForSeconds throwDelayWait = new WaitForSeconds(2);
    private float _restartWait = 0f;
    
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
    }
    
    private void StartGame()
    {
        score = 0;
        lives = 3;
        
        //TODO Wait until everything is loaded
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
        WaitForSeconds wait = new WaitForSeconds(GetThrowDelay());
        //while (!isGameOver && !waitForRestart)
        while(true)
        {
           Debug.Log("in coroutine");
           if(!isGameOver && !waitForRestart){
               Debug.Log("Throwing in coroutine");
                _thrower.ThrowItem(score);
           }
           yield return wait;
        }
    }

    private float GetThrowDelay()
    {
        if (waitForRestart) return 1f;
        return baseThrowDelay;
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

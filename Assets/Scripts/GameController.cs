using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

   

    private static GameController _instance;
    public static GameController Instance { get { return _instance; }}
    
    private int lives = 3;
    private int score = 0;
    private bool isGameOver = false;

    [SerializeField] private float throwDelay = 2f; 
    private WaitForSeconds throwDelayWait = new WaitForSeconds(2);
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

    private void StartGame()
    {
        score = 0;
        lives = 3;
        
        //TODO Wait until everything is loaded
        //TODO Add countdown to start
        //Start Spawning balls
        
        StartCoroutine(ThrowBalls());

    }

    private IEnumerator ThrowBalls()
    {
        WaitForSeconds wait = new WaitForSeconds(throwDelay);
        while (!isGameOver)
        {
            _thrower.ThrowItem();
            yield return wait;
        }
        
    }

    public void LoseLife()
    {
        lives--;
        UIController.Instance.LoseLife(lives);
        //Invoke Life Lost state
        
        isGameOver = (lives <= 0);
        if (isGameOver) EndGame();
    }

    public void EndGame()
    {
        StopAllCoroutines();
        _thrower.StopThrowing();
        //Add event here
        UIController.Instance.EndGame();
    }

    public void UpdateScore(int n)
    {
        score += n;
        UIController.Instance.UpdateScore(score);
    }
    
}

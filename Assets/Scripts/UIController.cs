using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    
    private static UIController _instance;
    public static UIController Instance { get { return _instance; }}
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private GameObject[] lives; 
    /*
   [SerializeField] private Image[] lives;
   [SerializeField] private TextMeshProUGUI scoreText;
   [SerializeField] private TextMeshProUGUI gameOverText;
   */
    
    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    private void Start()
    {
        GameController.Instance.LifeLost += OnLoseLife;
        GameController.Instance.GameOver += EndGame;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString("D4");
    }
    
    public void OnLoseLife(int i)
    {
        lives[i].SetActive(false);
    }

    public void EndGame()
    {
        gameOverText.SetActive(true);
    }
    
    /*
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        gameOverText.gameObject.SetActive(false);
        foreach (Image life in lives)
            life.gameObject.SetActive(true);
        
    }

    public void LoseLife(int i)
    {
        //lives[i].gameObject.SetActive(false);
    }

    public void EndGame()
    {
        //gameOverText.gameObject.SetActive(true);
    }
    */
    
}

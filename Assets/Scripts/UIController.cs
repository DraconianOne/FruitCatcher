using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image[] lives;
    [SerializeField] private TextMeshProUGUI gameOverText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverText.gameObject.SetActive(false);
        foreach (Image life in lives)
            life.gameObject.SetActive(true);
    }

    public void LoseLife(int i)
    {
        lives[i].gameObject.SetActive(false);
    }

    public void EndGame()
    {
        gameOverText.gameObject.SetActive(true);
    }
    
}

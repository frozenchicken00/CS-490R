using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> cardPrefab;
    public int score;
    private int bestScore;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText; 
    public bool isGameActive;
    public Button restartButton;
    public GameObject titleScreen;
    public TextMeshProUGUI easyBestScoreText;
    public TextMeshProUGUI mediumBestScoreText;
    public TextMeshProUGUI hardBestScoreText;
    private float currentVisibilityDuration = 30.0f;
    // Start is called before the first frame update
    void Start()
    {

        bestScore = PlayerPrefs.GetInt("BestScore_" + SceneManager.GetActiveScene().name, 0);

        // Initialize the score
        score = 0;
        UpdateScoreText();
    }
    

    IEnumerator SpawnTarget() {
        while (isGameActive) {
            yield return new WaitForSeconds(currentVisibilityDuration);
            int index = Random.Range(0, cardPrefab.Count);
            Instantiate(cardPrefab[index]);
        }
    }

    public void UpdateScore(int scoreToAdd) {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
        if (score > bestScore)
        {
            bestScore = score;

            // Save the new best score to PlayerPrefs
            PlayerPrefs.SetInt("BestScore_" + SceneManager.GetActiveScene().name, bestScore);
            PlayerPrefs.Save();
        }
        if (score < 0)
        {
            GameOver();
        }
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score + "\nBest: " + bestScore;
    }

    public void GameOver() {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty) {
        isGameActive = true;
        score = 0;
        currentVisibilityDuration = difficulty;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private Renderer cardRenderer;
    private float visibilityDuration = 1f; // Duration for which the card is visible
    private float visibilityTimer;
    private bool isVisible = true;
    private GameManager gameManager;
    public int pointValue;

    // Start is called before the first frame update
    void Start()
    {
        visibilityTimer = visibilityDuration;
        cardRenderer = GetComponent<Renderer>();
        transform.position = RandomSpawnPos();
        SetCardVisibility(isVisible);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    private void SetCardVisibility(bool visible)
    {
        if (cardRenderer != null)
        {
            cardRenderer.enabled = visible; // Enable or disable the card's renderer
        }
    }
    private void OnMouseDown() {
        if(gameManager.isGameActive) {
            if(isVisible) {
                Destroy(gameObject);
                gameManager.UpdateScore(pointValue);
                CheckScore();
            }
        }
    }
    private void CheckScore()
    {
        if (gameManager.score < 0)
        {
            gameManager.GameOver();
        }
    }

    Vector3 RandomSpawnPos() {
    Vector3[] spawnPositions = new Vector3[] {
        new Vector3(-3.5f, 8.2f, 0f),
        new Vector3(0f, 8.2f, 0f),
        new Vector3(3.5f, 8.2f, 0f),
        new Vector3(-3.5f, 4.8f, 0f),
        new Vector3(0f, 4.8f, 0f),
        new Vector3(3.5f, 4.8f, 0f),
        new Vector3(-3.5f, 1.4f, 0f),
        new Vector3(0f, 1.4f, 0f),
        new Vector3(3.5f, 1.4f, 0f)
    };

    int randomIndex = Random.Range(0, spawnPositions.Length);
    return spawnPositions[randomIndex];
    }

    // Update is called once per frame
    void Update()
    {
        visibilityTimer -= Time.deltaTime;

        if (visibilityTimer <= 0f)
        {
            isVisible = !isVisible; // Toggle visibility
            SetCardVisibility(isVisible);
            visibilityTimer = visibilityDuration; // Reset the timer
            if (!isVisible)
            {
                Destroy(gameObject);
            }
        }
    }
}

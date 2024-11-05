using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour // INHERITANCE (if GameManager extends from a parent class like MonoBehaviour)
{
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Button startButton; // Reference to the start button
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI scoreText;
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private float spawnRange = 9;
    private int score = 0;
    private int highScore;
    private int waveNumber = 1;
    private bool isGameOver = false;
    private bool isGameStarted = false;

    void Start()
    {
        // Only load high score at the start; game setup will happen in StartGame
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore;
        
        // Ensure game objects like start button are enabled at the start
        startButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isGameStarted || isGameOver) return; // Stop updating if game hasn't started or is over

        int enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn) // ABSTRACTION (method abstracts details of spawning enemy wave)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition() // ABSTRACTION (method abstracts spawn position logic)
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(spawnPosX, 0, spawnPosZ);
    }

    public void UpdateScore(int scoreToAdd)
    {
        if (isGameOver) return; // Stop score updates if game is over

        score += scoreToAdd;
        
        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = "High Score: " + highScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        isGameStarted = true;
        score = 0;
        waveNumber = 1;
        
        UpdateScore(0);
        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        
        startButton.gameObject.SetActive(false); // Hide the start button
    }
}

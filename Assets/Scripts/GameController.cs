using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public int hazardCount;
    public Vector3 spawnValues;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    private int score;
    public Text scoreText;

    private bool restart;
    public Text restartText;

    private bool gameOver;
    public Text gameOverText;


    private void Start()
    {
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
    }

    private void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {

                var hazard = hazards[Random.Range(0, hazards.Length)];
                var spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                var spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }

            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' to Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddPointsToScore(int points)
    {
        score += points;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}

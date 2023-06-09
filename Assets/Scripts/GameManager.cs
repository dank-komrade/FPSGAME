using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int enemiesAlive = 0;
    public int round = 0;

    public GameObject[] spawnPoints;
    public GameObject enemyPrefab;
    public Text waveNumber;
    public Text roundsSurvived;
    public GameObject endScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemiesAlive == 0)
        {
            round++;
            NextWave(round);
            waveNumber.text = "Wave: " + round.ToString();
        }
    }

    public void NextWave(int round)
    {
        for (var x = 0; x < round; x++)
        {
            GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject enemySpawned = Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
            enemySpawned.GetComponent<EnemyManager>().gameManager = GetComponent<GameManager>();
            enemiesAlive++;
        }
        
    }

    public void Restart()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene(0);
    }

    public void EndGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        endScreen.SetActive(true);
        roundsSurvived.text = round.ToString();
        Debug.Log("gaemeover");

        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public GameObject startMenu;
    public GameObject gameOverMenu;
    public Text enemiesDefeatedText;
    public Text survivalTimeText;
    EnemySpawner enemySpawner;
    int survivalMinutes, survivalSeconds; //Survival time
    int enemiesDefeated;
    internal HealthIndicator healthIndicator;
    public AudioSource soundtrack;

    // Use this for initialization
    void Start () {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        healthIndicator = FindObjectOfType<HealthIndicator>();
        startMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        healthIndicator.gameObject.SetActive(false);
	}

    public void startGame()
    {
        startMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        GameManager.resetGame();
        GameManager.gameState = GameManager.GAMESTATE.PLAYING;
        healthIndicator.gameObject.SetActive(true);
        healthIndicator.resetHealth();
        enemySpawner.spawnWave();
    }

    public void gameOver()
    {
        gameOverMenu.SetActive(true);
        healthIndicator.gameObject.SetActive(false);
        GameManager.gameState = GameManager.GAMESTATE.GAMEOVER;
        GameManager.getGameStats(out survivalMinutes, out survivalSeconds, out enemiesDefeated);
        
        //Print Stats
        if (survivalSeconds < 10)
        {
            survivalTimeText.text = survivalMinutes + ":" + '0' + survivalSeconds;
        }
        else
        {
            survivalTimeText.text = survivalMinutes + ":" + survivalSeconds;
        }
        enemiesDefeatedText.text = enemiesDefeated.ToString();
    }

    // Update is called once per frame
    void Update () {
		if (GameManager.gameState == GameManager.GAMESTATE.READY || GameManager.gameState == GameManager.GAMESTATE.GAMEOVER)
        {
            soundtrack.volume = 0.5f;
        }
        else //gameState = READY
        {
            soundtrack.volume = 0.25f;
        }
	}
}

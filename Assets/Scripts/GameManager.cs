using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager {
    public enum GAMESTATE { READY, PLAYING, GAMEOVER }
    public static GAMESTATE gameState = GAMESTATE.READY;

    static float timeSurvived = 0;
    static int enemiesDefeated = 0;

    public static void addTime(float time)
    {
        timeSurvived += time;
    }

    public static void defeatedEnemy()
    {
        enemiesDefeated++;
    }

    public static void getGameStats(out int survivedMinutes, out int survivedSeconds, out int defeatedEnemies)
    {
        survivedMinutes = (int)Mathf.Floor(timeSurvived / 60); //Minutes
        survivedSeconds = Mathf.RoundToInt(timeSurvived - (60 * survivedMinutes)); //Seconds
        defeatedEnemies = enemiesDefeated;
    }

    public static void resetGame()
    {
        timeSurvived = 0;
        enemiesDefeated = 0;
    }
}

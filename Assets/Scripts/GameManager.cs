using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int _highestPlatformReached = 0;

    public void EndGame()
    {
        ResetLevel();
    }

    private void ResetLevel()
    {
        // TODO : Modal here, Reset or GoBackToMainMenu
        Debug.Log("Your highest floor was : " + _highestPlatformReached);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ResetStatistics()
    {
        _highestPlatformReached = 0;
    }
}

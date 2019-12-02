using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    
    public GameObject gameOverEmpty;
    public GameObject stageClearEmpty;
    
    public void GameOver()
    {
        gameOverEmpty.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StageClear()
    {
        stageClearEmpty.gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    
    public GameObject gameOverEmpty;
    // Start is called before the first frame update
    void Awake()
    {
        gameOverEmpty = GameObject.FindWithTag("GameOverEmpty");
    }
    
    void Start()
    {
        gameOverEmpty.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        gameOverEmpty.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

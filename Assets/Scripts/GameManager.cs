using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    static GameManager instance;
    public TextMeshProUGUI timeScore;
    public GameObject gameOverPanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeScore.text = Time.timeSinceLevelLoad.ToString("00");
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    public static void GameOver(bool dead)
    {
        if (dead)
        {
            instance.gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }


}

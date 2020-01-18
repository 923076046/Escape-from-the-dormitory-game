using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject menu;
    public bool gamePaused = false;
    public GameObject background;
    public void gamePause()
    {
        menu.SetActive(true);
        gamePaused = true;
        Time.timeScale = 0;
    }

    public void gameContinue()
    {
        menu.SetActive(false);
        gamePaused = false;
        Time.timeScale = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject tmp = Instantiate(background);
        gameContinue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menu.activeInHierarchy)
            {
                gamePause();
            }
            else
            {
                gameContinue();
            }
        }

    }

    public void Succeed(string achievement)
    {
        string A_info = PlayerPrefs.GetString(achievement);
        if (A_info!="Succeed")
        {
            PlayerPrefs.SetString("New achievement", achievement);
        }
        SceneManager.LoadScene("Succeed");
    }

    public void Succeed()
    {
        SceneManager.LoadScene("Succeed");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}

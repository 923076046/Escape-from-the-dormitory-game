using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : UnityEngine.MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("roll");
    }

    public void Achievement()
    {
        //SceneManager.LoadScene("roll");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAchievement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string A_info = PlayerPrefs.GetString("New achievement");
        string ifSucceed = PlayerPrefs.GetString(A_info);
        if (A_info!="Succeed")
        {
            GameObject.Find("Achieve").SetActive(false);
        }
        PlayerPrefs.SetString(A_info, "Succeed");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

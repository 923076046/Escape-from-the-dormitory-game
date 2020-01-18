using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchieveController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string achievement = "Achievement";
        for (int i = 1; i <= 5; i++)
        {
            string str = achievement + i;
            string A_info = PlayerPrefs.GetString(str);
            if (A_info == "Succeed")
            {
                GameObject.Find(str).transform.Find("Text").GetComponent<Text>().color = new Color32(50, 221, 50,255);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}

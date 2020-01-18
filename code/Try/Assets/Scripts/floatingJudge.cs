using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingJudge : MonoBehaviour
{
    public bool isFloating = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "Cat" && other.name != "MenuBox")
        { 
            isFloating = true;
         }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name != "Cat" && other.name != "MenuBox")
        {
            isFloating = false;
        }
    }
}

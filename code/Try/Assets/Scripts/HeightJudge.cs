using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightJudge : MonoBehaviour
{
    public bool stepOnSth=false;
    private float highHeight = 0;
    private float lowHeight = 0;
    private float fallHeight = 0;
    public float dieHight;
    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stepOnSth)
        {
            highHeight = transform.position.y;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!stepOnSth)
        {
            stepOnSth = true;
            lowHeight = transform.position.y;
            if (highHeight-lowHeight>=dieHight)
            {
                gameController.GameOver();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        stepOnSth = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (stepOnSth)
        {
            stepOnSth = false;
        }
    }
}

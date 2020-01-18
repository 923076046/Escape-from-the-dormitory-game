using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape2 : MonoBehaviour
{
    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            gameController.Succeed();
        }
    }
}

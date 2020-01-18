using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    private playerController player;
    private CatMove cat;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
        cat = GameObject.Find("Cat").GetComponent<CatMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cat.mount)
        {
            animator.SetBool("Mount", true);
        }
        else
        {
            animator.SetBool("Mount", false);
        }

        if (player.isCarryItem)
        {
            animator.SetBool("Carry item", true);
        }
        else
        {
            animator.SetBool("Carry item", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Jumping", true);
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetBool("Running", true);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            animator.SetBool("Running", false);
        }


        
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            animator.SetBool("Backward", true);
        }
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            animator.SetBool("Backward", false);
        }


        
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            animator.SetBool("Left", true);
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            animator.SetBool("Left", false);
        }



        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            animator.SetBool("Right", true);
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            animator.SetBool("Right", false);
        }


        
    }
}

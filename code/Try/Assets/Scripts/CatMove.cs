using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMove : MonoBehaviour
{
    public int catState = -1;
    private bool moveToPoint1 = false;
    private Vector3 loadPoint1;
    private Vector3 loadPoint2;
    private Vector3 loadPoint3;
    private Vector3 loadPoint4;
    private Vector3 loadPoint5;
    private bool startMove = true;
    private Vector3 direction;
    private int moveTo;
    private int moveState = 0;
    public bool floating = false;
    public bool mount = false;
    public bool haveFood = false;
    public bool haveWater = false;
    private GameObject player;
    private GameController gameController;
    private bool atPoint6 = false;
    public GameObject drink;
    public GameObject eat;
    public GameObject catVoice;
    private bool drinked=false;
    private bool eaten=false;
    public GameObject levelup;
    // Start is called before the first frame update

    void Start()
    {
        player = GameObject.Find("Player");
        loadPoint1 = GameObject.Find("LoadPoint1").transform.position;
        loadPoint2 = GameObject.Find("LoadPoint2").transform.position;
        loadPoint3 = GameObject.Find("LoadPoint3").transform.position;
        loadPoint4 = GameObject.Find("LoadPoint4").transform.position;
        loadPoint5 = GameObject.Find("LoadPoint5").transform.position;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public void Mount()
    {
        mount = true;
        player.transform.SetParent(transform);
        player.transform.position = transform.position + new Vector3(0, 0.4f, -0.2f);
    }

    private bool MoveTo(Vector3 loadPoint)
    {
        float temp = loadPoint.y;
        loadPoint.y = gameObject.transform.position.y;
        transform.LookAt(loadPoint);
        loadPoint.y = temp;
        transform.Rotate(0, 180, 0);
        transform.position = Vector3.MoveTowards(transform.position, loadPoint, Time.deltaTime * 2);
        if (transform.position==loadPoint)
        {
            startMove = false;
            return true;
        }
        return false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gameController.gamePaused)
        {
            if (catState == 0)
            {
                if (!moveToPoint1)
                {
                    GetComponent<Item>().behaviour2 = "";
                    if (MoveTo(loadPoint1))
                    {
                        moveToPoint1 = true;
                    }
                }
                else
                {
                    if (MoveTo(loadPoint3))
                    {
                        catState = 1;
                        mount = false;
                        player.transform.parent = null;
                        GameObject tmp = Instantiate(catVoice);
                        Destroy(tmp, 2);
                    }
                }

            }
            if (catState == 1)
            {
                int shouldMove = Random.Range((int)1, (int)500);
                int moveSpecial = Random.Range((int)1, (int)500);
                if (!startMove && moveSpecial == 1)
                {
                    moveTo = Random.Range((int)2, (int)5);
                    startMove = true;
                    moveState = 1;
                }
                if (!startMove && shouldMove == 1)
                {
                    startMove = true;
                    direction = new Vector3(Random.Range(-1, 1), transform.position.y, Random.Range(-1, 1));
                    moveState = 0;
                }
                if (startMove)
                {
                    if (moveState == 1)
                    {
                        switch (moveTo)
                        {
                            case 2:
                                MoveTo(loadPoint2); break;
                            case 3:
                                MoveTo(loadPoint3); break;
                            case 4:
                                MoveTo(loadPoint4); break;
                        }
                    }
                    else if (moveState == 0)
                    {
                        MoveTo(direction);
                    }
                }
            }
            if (catState == 2)
            {
                if (MoveTo(loadPoint5))
                {
                    catState = 3;
                    GameObject tmp = Instantiate(catVoice);
                    Destroy(tmp, 2);
                }
            }
            if (catState==3 && haveWater && !drinked)
            {
                drinked = true;
                GameObject tmp = Instantiate(drink);
                Destroy(tmp, 1);
            }
            if (catState == 3 && haveFood && !eaten)
            {
                eaten = true;
                GameObject tmp = Instantiate(eat);
                Destroy(tmp, 1);
            }
            if (catState == 3 && haveWater && haveFood)
            {
                levelUp();
                catState = 4;
                GameObject tmp = Instantiate(levelup);
                Destroy(tmp, 5);
            }
            /*
            if (catState == 5)
            {
                if (MoveTo(loadPoint6))
                {
                    atPoint6 = true;
                }
                if (atPoint6)
                {
                    transform.GetComponent<Rigidbody>().isKinematic = true;
                    if (MoveTo(loadPoint7))
                    {
                        catState = 6;
                    }
                }
            }*/
        }
    }

    private void levelUp()
    {
        transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        transform.position = new Vector3(transform.position.x, transform.position.y + transform.localScale.y * 0.5f, transform.position.z);
        transform.localScale*=1.5f;
        transform.Rotate(0, 180, 0);
        GetComponent<Item>().itemName = "强力猫";
        GetComponent<Item>().behaviour2 = "骑猫";
        transform.Find("MenuBox").GetComponent<BoxCollider>().size *= 1.5f;
    }

    private void OnTriggerEnter(Collider other)
    {
    }
}

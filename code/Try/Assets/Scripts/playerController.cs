using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : UnityEngine.MonoBehaviour 
{
    private playerController playerInformation;
    private CatMove cat;
    public GameObject carryItem;
    public bool isCarryItem = false;
    private CharacterController controller;
    public GameObject sight;
    public float speed;
    public float RotationSpeed;
    private float moveH;
    private float moveV;
    private bool showed = false;
    private UIController UI;
    public GameObject pillowNew;
    public float gravity;
    private Vector3 moveDirection = Vector3.zero;
    public GameObject food;
    public GameObject water;
    public GameObject mixture;
    public GameObject catHair;
    public float jumpSpeed;
    private GameController gameController;
    public bool powerful = false;
    public bool flexible = false;
    public GameObject Mushroom;
    private bool findMushroom = false;
    private bool big = false;
    public GameObject jumpAudio;
    public GameObject putWater;
    public GameObject drink;
    public GameObject crash;
    public GameObject rua;
    public GameObject openDoor;
    public GameObject pick;
    public GameObject block;
    public GameObject mushroom;
    private bool callYan = false;
    // Use this for initialization
    void Start () 
    {
        carryItem = null;
        controller = this.GetComponent<CharacterController>();
        UI = GameObject.Find("/OperationUI").GetComponent<UIController>();
        playerInformation = gameObject.GetComponent<playerController>();
        cat = GameObject.FindGameObjectWithTag("Cat").GetComponent<CatMove>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        //时刻受重力作用
        moveDirection.y -= gravity * Time.deltaTime;
        
        
        controller.Move(moveDirection * Time.deltaTime);

        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");
        
        if (gameController.gamePaused)
        {
            moveH = 0;
            moveV = 0;
        }
        //判断玩家是否在地上，系统自带isGrounded
        if (controller.isGrounded)
        {
            moveDirection.y = 0;
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed;
                GameObject tmp = Instantiate(jumpAudio);
                Destroy(tmp, 1);
            }
        }
        controller.Move(moveDirection * Time.deltaTime);
        //角色模型方向跟随相机转动，sight是传入的相机对象，判断两者方向是否一致
        if (Mathf.Abs(transform.rotation.y) != Mathf.Abs(sight.transform.rotation.y))
        {
            transform.eulerAngles = new Vector3(0, sight.transform.eulerAngles.y, 0);
        }
        //利用系统提供的人物模型类CharacterController移动，在start()中controller = this.GetComponent<CharacterController>()
        controller.Move(moveH * new Vector3(transform.right.x, 0, transform.right.z) * speed);
        controller.Move(moveV * new Vector3(transform.forward.x, 0, transform.forward.z) * speed);
        //UI设置
        if (Input.GetMouseButtonDown(1) && UI.canRelease)
        {
            Item item = playerInformation.carryItem.GetComponent<Item>();
            Release(item);
            UI.getItem(item);
            UI.showItem(item);
        }
    }


    private void PickUp(GameObject MeetItem)
    {
        //判断状态是否已经捡起物体
        if (!playerInformation.isCarryItem)
        {
            UI.carryState = true;
            //获得遇到物体中的组件item（每个可捡起的物体都有一个组件Script，包含是否被捡起及UI信息，下文图片实例）
            Item item = MeetItem.GetComponent<Item>();
            //调整方向
            gameObject.transform.LookAt(MeetItem.transform.Find("MenuBox"));
            //设置将要捡起的物体的父对象为player，并且用GetComponent<Rigidbody>().constraints冻结物体的位移，并更新状态
            MeetItem.transform.SetParent(gameObject.transform);
            playerInformation.isCarryItem = true;
            playerInformation.carryItem = MeetItem;
            item.isCarryed = true;
            MeetItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            UI.hideAll();
        }

    }

    private void Release(Item item)
    {
        //同理，捡起反之
        item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        UI.carryState = false;
        item.transform.parent = null;
        UI.hideAll();
        playerInformation.isCarryItem = false;
        playerInformation.carryItem = null;
        item.isCarryed = false;
    }

    public void MeetEvent(GameObject MeetItem)
    {
        //获得遇到物体中的组件item，同上
        Item item = MeetItem.GetComponent<Item>();
        UI.getItem(item);
        UI.showAll();
        //一系列的状态判断
        if (playerInformation.isCarryItem)
        {
            Item carryItem = playerInformation.carryItem.GetComponent<Item>();
            if (carryItem.itemName == "枕头")
            {
                if (item.itemName == "下方")
                {
                    UI.changeBehaviour2("扔下去");
                }
            }
            if (carryItem.itemName == "一袋猫粮")
            {
                if (item.itemName == "猫碗")
                {
                    UI.changeBehaviour2("加猫粮");
                }
                /*
                if (item.itemName == "储物架")
                {
                    UI.changeBehaviour2("扔猫粮");
                }*/
            }
            if (carryItem.itemName == "矿泉水瓶")
            {
                if (item.itemName=="猫碗")
                {
                    UI.changeBehaviour2("加水");
                }
            }
            if (carryItem.itemName == "神奇药水")
            {
                if (item.itemName == "猫碗")
                {
                    UI.changeBehaviour2("加入");
                }
            }

            if (carryItem.itemName == "可乐")
            {
                if (item.itemName == "猫碗")
                {
                    UI.changeBehaviour2("加入");
                }
            }

            if (carryItem.itemName =="猫毛")
            {
                if (item.itemName == "猫碗")
                {
                    UI.changeBehaviour2("加入");
                }
            }

            if (carryItem.itemName == "晾衣杆")
            {
                if (item.itemName == "寝室门")
                {
                    UI.changeBehaviour2("开锁");
                }
            }

            if (carryItem.itemName == "钥匙")
            {
                if (item.itemName == "寝室门")
                {
                    UI.changeBehaviour2("踢出去");
                }
            }
        }
        else
        {
            if (item.itemName == "寝室门")
            {
                if (powerful)
                {
                    UI.changeBehaviour2("踢飞");
                    item.description = "我感觉我能把门踢飞！";
                }
                if (big)
                {
                    UI.changeBehaviour2("打开");
                    item.description = "现在我可以直接开门了";
                }
            }
        }
    }
    //右键点击触发事件同理
    public void RightClickEvent(GameObject MeetItem)
    {
        Item item = MeetItem.GetComponent<Item>();
        if (playerInformation.isCarryItem)
        {
            Item carryItem = playerInformation.carryItem.GetComponent<Item>();
            if (carryItem.itemName == "枕头")
            {
                if (item.itemName == "下方")
                {
                    Release(carryItem);
                    carryItem.isCarryed = true;
                    pillowNew.SetActive(true);
                    GameObject.Find("Pillow").SetActive(false);
                }
            }

            if (carryItem.itemName == "一袋猫粮")
            {
                if (item.itemName == "猫碗")
                {
                    Bowl bowl = item.GetComponent<Bowl>();
                    Debug.Log("Bowl" + bowl.isFilled);
                    if (bowl.isFilled)
                    {
                        UI.setTalk("碗装满了");
                        UI.showTalk();
                    }
                    else
                    {
                        if (cat.haveFood)
                        {
                            UI.setTalk("别喂太多");
                            UI.showTalk();
                        }
                        else
                        {
                            bowl.isFilled = true;
                            cat.haveFood = true;
                            Instantiate(food, item.transform.Find("FoodPlace").transform);
                            item.description = "小猫饿坏了，它大概会飞奔过来";
                            UI.setTalk("小猫饿坏了，它大概会飞奔过来");
                            UI.showTalk();
                            cat.catState = 2;
                        }
                    }
                }
                /*
                if (item.itemName=="储物架")
                {
                    Instantiate(food, item.transform.Find("FoodPlace").transform);
                    cat.catState = 5;
                }*/
            }

            if (carryItem.itemName == "矿泉水瓶")
            {
                if (item.itemName=="猫碗")
                {
                    Bowl bowl = item.GetComponent<Bowl>();
                    if (bowl.isFilled)
                    {
                        UI.setTalk("碗装满了");
                        UI.showTalk();
                    }
                    else
                    {
                        if (cat.haveWater)
                        {
                            UI.setTalk("别喂太多");
                            UI.showTalk();
                        }
                        else
                        {
                            cat.haveWater = true;
                            bowl.isFilled = true;
                            Instantiate(water, item.transform.Find("WaterPlace").transform);
                            item.description = "里面有水了，小猫应该很快就会过来喝";
                            UI.setTalk("里面有水了，小猫应该很快就会过来喝");
                            UI.showTalk();
                            cat.catState = 2;
                            GameObject tmp = Instantiate(putWater);
                            Destroy(tmp, 1);
                        }
                    }
                }
            }

            if (carryItem.itemName == "神奇药水")
            {
                if (item.itemName == "猫碗")
                {
                    Bowl bowl = item.GetComponent<Bowl>();
                    bool b = !bowl.isFilled || bowl.cola || bowl.catHair;
                    if (!b)
                    {
                        UI.setTalk("呃……");
                        UI.showTalk();
                    }
                    else
                    {
                        bowl.isFilled = true;
                        Instantiate(mixture, item.transform.Find("WaterPlace").transform);
                        bowl.potion = true;
                        item.description = "这碗大概要扔掉……";
                        UI.setTalk("加进了神奇药水");
                        UI.showTalk();
                        GameObject tmp = Instantiate(putWater);
                        Destroy(tmp, 1);
                    }
                }
                if (item.itemName=="可乐" || item.itemName=="猫毛")
                {
                    UI.setTalk("我需要一个容器来混合药剂");
                    UI.showTalk();
                }
            }

            if (carryItem.itemName == "可乐")
            {
                if (item.itemName == "猫碗")
                {
                    Bowl bowl = item.GetComponent<Bowl>();
                    if (bowl.isFilled && !bowl.potion)
                    {
                        UI.setTalk("呃……");
                        UI.showTalk();

                    }
                    else
                    {
                        if (bowl.effective || bowl.catHair)
                        {
                            UI.setTalk("配方上不是这么写的……");
                            UI.showTalk();
                        }
                        else
                        {
                            bowl.isFilled = true;
                            Instantiate(mixture, item.transform.Find("WaterPlace").transform);
                            bowl.cola = true;
                            item.description = "得洗碗了";
                            UI.setTalk("加进了可乐");
                            UI.showTalk();
                            GameObject tmp = Instantiate(putWater);
                            Destroy(tmp, 1);
                        }
                    }
                }
                if (item.itemName == "神奇药水")
                {
                    UI.setTalk("我需要一个容器来混合药剂");
                    UI.showTalk();
                }
            }

            if (carryItem.itemName == "猫毛")
            {
                if (item.itemName == "猫碗")
                {
                    Bowl bowl = item.GetComponent<Bowl>();
                    if (bowl.effective || bowl.cola)
                    {
                        UI.setTalk("配方上不是这么写的……");
                        UI.showTalk();
                    }
                    else
                    {
                        bowl.catHair = true;
                        Instantiate(catHair, item.transform.Find("WaterPlace").transform);
                        item.description = "猫碗里当然会有猫毛";
                        UI.setTalk("加进了猫毛");
                        UI.showTalk();
                    }
                }
                if (item.itemName == "神奇药水")
                {
                    UI.setTalk("我需要一个容器来混合药剂");
                    UI.showTalk();
                }
            }

            if (carryItem.itemName == "晾衣杆")
            {
                if (item.itemName == "寝室门")
                {
                    if (!flexible)
                    {
                        UI.setTalk("我不够灵活，晾衣杆太难操控了");
                        UI.showTalk();
                    }
                    else
                    {
                        item.transform.RotateAround(item.transform.parent.transform.position, item.transform.up, 45);
                        UI.setTalk("门开了！");
                        UI.showTalk();
                        PlayerPrefs.SetString("New achievement", "Achievement3");
                        GameObject tmp = Instantiate(openDoor);
                        Destroy(tmp, 1);
                    }
                }
            }

            if (carryItem.itemName == "钥匙")
            {
                if (item.itemName == "寝室门")
                {
                    if (callYan)
                    {
                        gameController.Succeed("Achievement5");
                    }
                    else
                    {
                        UI.setTalk("放假了楼道里应该不会有人经过了吧……");
                        UI.showTalk();
                    }
                }
            }
        }
        else
        {
            if (item.behaviour2=="捡起")
            {
                PickUp(MeetItem);
                GameObject tmp = Instantiate(pick);
                Destroy(tmp, 1);
            }

            if (item.itemName == "猫" || item.itemName=="强力猫")
            {
                if (cat.catState == -1)
                {
                    cat.catState = 0;
                    cat.Mount();
                }
                if (cat.catState == 4)
                { 
                    cat.Mount();
                    transform.parent = null;
                    cat.transform.rotation = transform.rotation;
                    cat.transform.forward = -cat.transform.forward;
                    transform.GetComponent<CharacterController>().height = 5;
                    speed = 0.1f;
                    cat.transform.SetParent(transform);
                    jumpSpeed = 6f;
                    CameraController camera = GameObject.Find("Main Camera").GetComponent<CameraController>();
                    camera.Distance = camera.MaxDistance;
                }
            }

            if (item.itemName=="垃圾桶")
            {
                GameObject.Find("Trash").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                GameObject.Find("Cola").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                GameObject.Find("Bottle").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                GameObject tmp = Instantiate(crash);
                Destroy(tmp, 1);
            }

            if (item.itemName=="厕所门")
            {
                item.transform.RotateAround(item.transform.parent.transform.position, item.transform.up, -22.5f);
            }

            if (item.itemName=="柜子")
            {
                item.transform.RotateAround(item.transform.parent.transform.position, item.transform.up, 45);
            }

            if (item.itemName=="大力药水")
            {
                powerful = true;
                UI.setTalk("我感觉全身都充满了力量！");
                UI.showTalk();
                GameObject tmp = Instantiate(drink);
                Destroy(tmp, 1);
                tmp = Instantiate(rua);
                Destroy(tmp, 1);
            }

            if (item.itemName == "灵巧药水")
            {
                flexible = true;
                UI.setTalk("我感觉身体变得协调！");
                UI.showTalk();
                GameObject tmp = Instantiate(drink);
                Destroy(tmp, 1);
                tmp = Instantiate(rua);
                Destroy(tmp, 1);
            }

            if (item.itemName=="寝室门")
            {
                if (powerful)
                {
                    item.transform.GetComponent<Rigidbody>().isKinematic = false;
                    item.transform.GetComponent<Rigidbody>().AddForce(new Vector3(10000, 0, 10000));
                    GameObject tmp = Instantiate(crash);
                    Destroy(tmp, 1);
                    PlayerPrefs.SetString("New achievement", "Achievement2");
                }
                if (big)
                {
                    item.transform.RotateAround(item.transform.parent.transform.position, item.transform.up, 45);
                    UI.setTalk("门开了！");
                    UI.showTalk();
                    PlayerPrefs.SetString("New achievement", "Achievement4");
                    GameObject tmp = Instantiate(openDoor);
                    Destroy(tmp, 1);
                }
            }

            if (item.itemName=="手机")
            {
                UI.setTalk("老颜：我马上来救你，记得把钥匙弄到外面！");
                UI.showTalk();
                callYan = true;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag=="MenuBox")
        {
            GameObject item = other.transform.parent.gameObject;
            Item itemInformation = item.GetComponent<Item>();
            if (!itemInformation.isCarryed)
            {
                if (!itemInformation.isCarryed && Input.GetMouseButtonDown(0) && !showed)
                {
                    UI.setTalk(itemInformation);
                    UI.showTalk();
                    showed = true;
                }

                if (Input.GetMouseButtonDown(1))
                {
                    RightClickEvent(item);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MenuBox")
        {
            GameObject item = other.transform.parent.gameObject;
            Item itemInformation = item.GetComponent<Item>();
            if (!itemInformation.isCarryed)
            {
                MeetEvent(item);
            }
        }
        else if (other.name == "MarioTrigger")
        {
            if (!findMushroom)
            {
                findMushroom = true;
                Instantiate(Mushroom, new Vector3(other.transform.position.x + (float)0.11, other.transform.position.y + (float)0.02, other.transform.position.z - (float)0.285), new Quaternion(0, 0, 0, 0));
            }
            GameObject tmp = Instantiate(block);
            Destroy(tmp, 1);
        }
        else if (other.name == "Mushroom")
        {
            Destroy(other.transform.parent.gameObject);
            transform.localScale = new Vector3(2, 2, 2);
            speed *= 2;
            big = true;
            GameObject tmp = Instantiate(mushroom);
            Destroy(tmp, 1);
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "MenuBox")
        {
            GameObject item = other.transform.parent.gameObject;
            Item itemInformation = item.GetComponent<Item>();
            if (!itemInformation.isCarryed)
            {
                UI.hideAll();
                showed = false;
            }
        }
    }
}

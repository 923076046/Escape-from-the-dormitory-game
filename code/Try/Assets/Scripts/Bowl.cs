using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    public bool isFilled = false;
    public bool potion = false;
    public bool cola = false;
    public bool catHair = false;
    public bool effective = false;
    public GameObject makePotion;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (potion && catHair && !effective)
        {
            Item item = transform.GetComponent<Item>();
            effective = true;
            transform.GetComponent<Item>().itemName = "灵巧药水";
            item.behaviour2 = "喝";
            item.description = "颜氏秘传灵活药水，喝下之后手眼协调";
            GameObject tmp = Instantiate(makePotion);
            Destroy(tmp, 1);
        }
        if (potion && cola && !effective)
        {
            Item item = transform.GetComponent<Item>();
            effective = true;
            transform.GetComponent<Item>().itemName = "大力药水";
            item.behaviour2 = "喝";
            item.description = "颜氏秘传大力药水，喝下之后重拳出击";
            GameObject tmp = Instantiate(makePotion);
            Destroy(tmp, 1);
        }
    }
}

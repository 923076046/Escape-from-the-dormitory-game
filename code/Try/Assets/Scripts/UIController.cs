using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public bool carryState = false;
    public bool canRelease = false;
    private Text itemName;
    private Text behaviour1;
    private Text behaviour2;
    private Text talk;
    // Start is called before the first frame update
    void Start()
    {
        itemName = GameObject.Find("ItemName").GetComponent<Text>();
        behaviour1 = GameObject.Find("Behaviour1").GetComponent<Text>();
        behaviour2 = GameObject.Find("Behaviour2").GetComponent<Text>();
        talk = GameObject.Find("Talk").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (behaviour2.text!="放下")
        {
            canRelease = false;
        }
        else
        {
            canRelease = true;
        }
    }
    public void Fade(string name, int fade)
    {
        Component[] comps = GameObject.Find(name).GetComponentsInChildren<Component>();
        foreach (Component c in comps)
        {
            if (c is Graphic)
            {
                (c as Graphic).CrossFadeAlpha(fade, 0.25f, true);
            }

        }
    }

    public void changeBehaviour2(string newBehaviour)
    {
        behaviour2.text = newBehaviour;
    }

    public void hideAll()
    {
        hideTalk();
        hideItem();
    }

    public void showItem(Item item)
    {
        Fade("Part0", 1);
        if (!item.behaviour1.Equals(""))
        {
            Fade("Part1", 1);
        }
        if (!item.behaviour2.Equals(""))
        {
            Fade("Part2", 1);
        }
    }

    public void showAll()
    {
        Fade("Part0", 1);
        Fade("Part1", 1);
        Fade("Part2", 1);
    }
    public void getItem(Item item)
    {
        itemName.text = item.itemName;
        behaviour1.text = item.behaviour1;
        behaviour2.text = item.behaviour2;
    }

    public void hideItem()
    {
        Fade("Part0", 0);
        Fade("Part1", 0);
        Fade("Part2", 0);

        if (carryState)
        {
            changeBehaviour2("放下");
            Fade("Part2", 1);
        }
    }

    public void setTalk(string s)
    {
        talk.text = s;
    }

    public void setTalk(Item item)
    {
        talk.text = item.description;
    }

    public void showTalk()
    {
        Fade("TalkPart", 1);
    }

    public void hideTalk()
    {
        Fade("TalkPart", 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultText : UnityEngine.MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Component[] comps = GameObject.Find("/OperationUI").GetComponentsInChildren<Component>();
        foreach (Component c in comps)
        {
            if (c is Graphic)
            {
                (c as Graphic).CrossFadeAlpha(0, 0, true);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTag : MonoBehaviour
{
    public GameObject target;
    public bool clearTag;
    public string newTag;
    public void Change()
    {
        if (clearTag)
            target.tag = "Untagged";

        else
            target.tag = newTag;
    }
}

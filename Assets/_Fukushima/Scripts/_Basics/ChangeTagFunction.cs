using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTagFunction : MonoBehaviour
{
    public void changeTag(string newTag)
    {
        gameObject.tag = newTag;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisionWith : MonoBehaviour
{
    [SerializeField] GameObject objectToIgnore;
    void Start()
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), objectToIgnore.GetComponent<Collider>());
    }
}

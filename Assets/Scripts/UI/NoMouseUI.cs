using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMouseUI : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        Vector2 posicao = Input.mousePosition;
        transform.position = posicao;
    }
}

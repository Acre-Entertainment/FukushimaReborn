using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WterBox : MonoBehaviour
{
    public string tagName;

    public bool canReturn;
    public bool automaticShift;

    public float timeToSink;
    public float timeSinking;
    public float sinkSpeed;

    public bool canInteract = true;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(automaticShift == true)
        {
            StartCoroutine(repeat());
        }
    }

    IEnumerator repeat()
    {
        while(true)
        {
            yield return goDown();
            yield return goUp();
        }
    }
    IEnumerator activated()
    {
        canInteract = false;
        yield return goDown();
        if(canReturn == true)
        {
            yield return goUp();
            canInteract = true;
        }
    }
    IEnumerator goDown()
    {
        yield return new WaitForSeconds(timeToSink);
        rb.velocity = new Vector3(0, -sinkSpeed, 0);
        yield return new WaitForSeconds(timeSinking);
        rb.velocity = new Vector3(0, 0, 0);
    }
    IEnumerator goUp()
    {
        yield return new WaitForSeconds(timeToSink);
        rb.velocity = new Vector3(0, sinkSpeed, 0);
        yield return new WaitForSeconds(timeSinking);
        rb.velocity = new Vector3(0, 0, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == tagName && canInteract == true)
        {
            StartCoroutine(activated());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightScript : MonoBehaviour
{
    public float setTimeUntilGameOver;
    public float currentTimeUntilGameOver;
    public float playerHeight;
    public bool isSeeingPlayer;
    public GameObject hittingThing;
    Ray ray;
    RaycastHit hit;
    Vector3 origin, target, direction;

    void Start()
    {
        currentTimeUntilGameOver = setTimeUntilGameOver;
    }
    void Update()
    {
        if(isSeeingPlayer == true)
        {
            currentTimeUntilGameOver = currentTimeUntilGameOver - 1 * Time.deltaTime;
            if(currentTimeUntilGameOver <= 0)
            {
                GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameOver>().EndGame();
            }
        }
    }
    public void onGameOver()
    {

    }
    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            target = other.transform.position + new Vector3(0, playerHeight, 0);
            origin = gameObject.transform.position;
            direction = target - origin;
            ray = new Ray(origin, direction);
            Debug.DrawRay(origin, direction, Color.green);
            if(Physics.Raycast(ray, out hit))
            {
                hittingThing = hit.collider.gameObject;
                if(hittingThing.tag == "Player")
                {
                    isSeeingPlayer = true;
                }
                else
                {
                    isSeeingPlayer = false;
                }
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            isSeeingPlayer = false;
            currentTimeUntilGameOver = setTimeUntilGameOver;
        }
    }
}

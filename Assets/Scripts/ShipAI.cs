using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAI : MonoBehaviour
{

    bool solved;
    int difficulty;
    float speed;
    List<int> flagsRequired;
    Transform destination;
    GameObject shipEater;

    // Use this for initialization
    void Start()
    {
        flagsRequired = new List<int>();
        solved = false;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destination.position, step);
        if (this.gameObject.GetComponent<Collider>().bounds.Intersects(shipEater.GetComponent<Collider>().bounds))
        {
            Destroy(this.gameObject);
            GameObject.Find("DefaultGamemode").GetComponent<Gamemode>().gameOver();
        }
    }

    public void Initialize(int newDifficulty, float newSpeed, Transform newDestination)
    {
        difficulty = newDifficulty;
        speed = newSpeed;
        destination = newDestination;
        transform.LookAt(destination);
        shipEater = GameObject.Find("ShipEater");
    }

    public void setDifficulty(int newDifficulty)
    {
        difficulty = newDifficulty;
    }


    void RemoveFlag()
    {
        //Remove the head of the required flags
        flagsRequired.RemoveAt(0);

        //If there's no flags left then you're solved
        if (flagsRequired.Count == 0)
        {
            solved = true;
        }
    }


}

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
    Transform lighthouse;
    Transform exit;
    GameObject shipEater;

    // Use this for initialization
    void Start()
    {
        flagsRequired = new List<int>();
        flagsRequired.Add(1);
        solved = false;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        //make sure the boat is looking at the destination
        if (Vector3.Dot((destination.position - transform.position).normalized, transform.forward) < 0.9) {
            Vector3 targetDir = destination.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        } else
        {
            //move the boat by a step
            transform.position += transform.forward * step;
            Debug.Log(Vector3.Distance(transform.position, destination.position));

            //check for gameover
            if (this.gameObject.GetComponent<Collider>().bounds.Intersects(shipEater.GetComponent<Collider>().bounds))
            {
                Destroy(this.gameObject);
                GameObject.Find("DefaultGamemode").GetComponent<Gamemode>().gameOver();
            } else if (Vector3.Distance(transform.position, destination.position) <= 12 && flagsRequired.Count > 0)
            {
                RemoveFlag();
            }
        }

    }

    public void Initialize(int newDifficulty, float newSpeed, Transform lighthouse, Transform exit)
    {
        difficulty = newDifficulty;
        speed = newSpeed;
        this.lighthouse = lighthouse;
        this.exit = exit;
        destination = this.lighthouse;
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
            destination = exit;
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAI : MonoBehaviour
{

    bool solved, rotated;
    int difficulty;
    float speed;
    List<int> flagsRequired;
    GameObject shipEater;

    // Use this for initialization
    void Start()
    {
        flagsRequired = new List<int>();
        flagsRequired.Add(1);
        solved = false;
        rotated = false;
    }

    // Update is called once per frame
    void Update()
    {
        //rotate the ship by 45 degrees if the puzzle for it has been solved
        if (solved == true && rotated == false) {
            StartCoroutine(RotateMe(Vector3.up * 45, 1.5f));
            rotated = true;
        } else {
            //move the boat by a step
            float step = speed * Time.deltaTime;
            transform.position += transform.forward * step;

            //check for gameover
            if (this.gameObject.GetComponent<Collider>().bounds.Intersects(shipEater.GetComponent<Collider>().bounds)) {
                Destroy(this.gameObject);
                GameObject.Find("DefaultGamemode").GetComponent<Gamemode>().gameOver();
            }
        }

    }

    //rotate the ship by "byAngles" degrees in "inTime" seconds
    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime) {
            transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }
    }

    public void Initialize(int newDifficulty, float newSpeed, Transform lighthouse)
    {
        difficulty = newDifficulty;
        speed = newSpeed;
        transform.LookAt(lighthouse);
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
        if (flagsRequired.Count == 0) {
            solved = true;
        }
    }


}

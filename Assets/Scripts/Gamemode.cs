using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemode : MonoBehaviour
{

    [SerializeField]
    int score; //score for the player
    bool isPlaying; //whether we're playing or not
    int difficulty; //ramp up as game goes on?
    float speed; //change according to ship when we introduce types of ship?
    [SerializeField]
    GameObject shipPrefab;

    //Spawn points for ships
    [SerializeField]
    Transform Spawn1;
    [SerializeField]
    Transform Spawn2;
    [SerializeField]
    Transform Spawn3;



    // Use this for initialization
    void Start()
    {
        difficulty = 0;
        speed = 1;
        score = 0;
        isPlaying = true;
        SpawnShip();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Spawn a single ship
    void SpawnShip()
    {
        GameObject go = (GameObject)Instantiate(shipPrefab, Spawn1.position, transform.rotation);
        go.GetComponent<ShipAI>().Initialize(difficulty, speed, this.gameObject.GetComponent<Transform>());
    }

    public void gameOver()
    {
        isPlaying = false;

    }
}

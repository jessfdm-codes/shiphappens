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
        SpawnShipEater();
        SpawnShip(Spawn1, Spawn2);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnShipEater()
    {
        //CapsuleCollider ShipEaterClone = new CapsuleCollider();
        GameObject ShipEater = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        ShipEater.name = "ShipEater";
        ShipEater.transform.position = new Vector3(0, 5, -1);
        ShipEater.GetComponent<CapsuleCollider>().height = 15;
        ShipEater.GetComponent<CapsuleCollider>().radius = 2.5f;
        Destroy(ShipEater.GetComponent<MeshFilter>());
        
    }

    //Spawn a single ship
    void SpawnShip(Transform spawn1, Transform spawn2)
    {
        GameObject go = (GameObject)Instantiate(shipPrefab, spawn1.position, transform.rotation);
        go.GetComponent<ShipAI>().Initialize(difficulty, speed, this.gameObject.GetComponent<Transform>(), spawn2.transform);
    }

    public void gameOver()
    {
        isPlaying = false;

    }
}

using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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

    [SerializeField]
    private SemaphoreGesture currentGesture;
    [SerializeField]
    private string currLeftHandPostion;
    [SerializeField]
    private string currRightHandPostion;

    // Use this for initialization
    void Start()
    {
        Debug.Log(SemaphoreGenerator.loadExistentSemaphoreGuestures());
        difficulty = 0;
        speed = 1;
        score = 0;
        isPlaying = true;
        SpawnShipEater();
        SpawnShip(Spawn1);
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
    void SpawnShip(Transform spawn1)
    {
        GameObject go = (GameObject)Instantiate(shipPrefab, spawn1.position, transform.rotation);
        go.GetComponent<ShipAI>().Initialize(difficulty, speed, this.gameObject.GetComponent<Transform>());
    }

    public void gameOver()
    {
        isPlaying = false;

    }


    /*
     * Gesture Recog
     */
     private void RecalculateCurrentGesture()
    {
        if (currLeftHandPostion != null && currRightHandPostion != null)
        {
            currentGesture = new SemaphoreGesture(currLeftHandPostion, currRightHandPostion);
        }
    }

    public void UpdateLeftHandPosition(string newPos)
    {
        if (Regex.IsMatch(newPos, "$g[0-2][0-2]^"))
        {
            currLeftHandPostion = newPos;
        }
        RecalculateCurrentGesture();
    }

    public void UpdateRightHandPosition(string newPos)
    {
        if (Regex.IsMatch(newPos, "$g[0-2][0-2]^")){
            currRightHandPostion = newPos;
        }
        RecalculateCurrentGesture();
    }
}

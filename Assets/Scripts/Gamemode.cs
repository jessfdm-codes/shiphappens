using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemode : MonoBehaviour {

    [SerializeField]
    int score; //score for the player
    bool isPlaying; //whether we're playing or not
    int difficulty; //ramp up as game goes on?
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
    void Start () {
		difficulty = 0;
		score = 0;
		isPlaying = false;
		SpawnShip();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	//Spawn a single ship
	void SpawnShip () {
		GameObject go = Instantiate(shipPrefab, transform.position, transform.rotation);
		go.GetComponent<ShipAI>().Initialize(difficulty);
	}
}

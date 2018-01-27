using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAI : MonoBehaviour {

	bool solved;
	int difficulty;
	List<int> flagsRequired;
	Transform destination;

	// Use this for initialization
	void Start () {
		flagsRequired = new List<int>();
		solved = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void Initialize(int newDifficulty) {
		difficulty = newDifficulty;
	}
	
	public void setDifficulty(int newDifficulty) {
		difficulty = newDifficulty;
	}
	
	
	void RemoveFlag() {
		//Remove the head of the required flags
		flagsRequired.RemoveAt(0);
		
		//If there's no flags left then you're solved
		if(flagsRequired.Count == 0) {
			solved = true;
		}
	}
	
	
}

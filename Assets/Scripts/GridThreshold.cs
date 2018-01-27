using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridThreshold : MonoBehaviour {

    [SerializeField]
    Gamemode controller;

	// Use this for initialization
	void Start () {
        controller = GameObject.Find("DefaultGamemode").GetComponent<Gamemode>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collide");
        if (other.gameObject.name == "Controller (left)")
        {
            Debug.Log("Left: " + gameObject.name);
            controller.UpdateLeftHandPosition(this.name);
        }
        else if (other.gameObject.name == "Controller (right)")
        {
            Debug.Log("Right: " + gameObject.name);
            controller.UpdateRightHandPosition(this.name);
        }
    }
}

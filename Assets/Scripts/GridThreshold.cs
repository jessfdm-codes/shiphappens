using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridThreshold : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collide");
        if (other.gameObject.name == "Controller (left)")
            Debug.Log("Left: " + gameObject.name);
        else if (other.gameObject.name == "Controller (right)")
            Debug.Log("Right: " + gameObject.name);
    }
}

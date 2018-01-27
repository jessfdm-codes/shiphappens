using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pos : MonoBehaviour {

    public GameObject rel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("x:" + (this.transform.position.x - rel.transform.position.x) + ", y:" + (this.transform.position.y - rel.transform.position.y));
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        if (this.transform.position.y < 0)
            Destroy(this);
	}
}

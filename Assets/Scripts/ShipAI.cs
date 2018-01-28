using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAI : MonoBehaviour
{

    bool solved, rotated;
    float speed;
	List<SemaphoreGestureTarget> flagsRequired;
    GameObject shipEater;

    // Use this for initialization
    void Start()
    {
        solved = false;
    }

    // Update is called once per frame
    void Update()
    {
        //rotate the ship by 45 degrees if the puzzle for it has been solved
        if (solved == true) {
            StartCoroutine(RotateMe(Vector3.right * 45, 1.5f));
            Destroy(this.gameObject);
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

	public void Initialize(float newSpeed, Transform destination, List<SemaphoreGestureTarget> requiredFlags)
    {
		flagsRequired = requiredFlags;
        speed = newSpeed;
        transform.LookAt(destination);
        shipEater = GameObject.Find("ShipEater");
        UpdateFlag();
    }


	public bool ReceiveGesture(SemaphoreGesture sg) {
		if (sg.Equals (flagsRequired[0])) {
			ResolveGesture ();
		}

		return solved;
	}

    private void UpdateFlag()
    {
        this.transform.Find("Icon").GetComponent<SpriteRenderer>().sprite = flagsRequired[0].GetIcon();
    }

    void ResolveGesture()
    {
        //Remove the head of the required flags
        flagsRequired.RemoveAt(0);

        //If there's no flags left then you're solved
		if (flagsRequired.Count == 0) {
			solved = true;
			GetComponentInChildren<SpriteRenderer> ().enabled = false;
		} else {
			UpdateFlag ();
		}
    }


}

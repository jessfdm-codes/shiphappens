using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorKnock : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(ChangeScene("Level", Color.clear, Color.black, 2.5f));
    }

    IEnumerator FadeScreen(Color from, Color to, float inTime)
    {
        SteamVR_Fade.Start(from, 0f);
        SteamVR_Fade.Start(to, inTime);

        yield return new WaitForSeconds(inTime);
    }

    //fade the screen then change the scene
    IEnumerator ChangeScene(string scene, Color from, Color to, float inTime)
    {
        yield return StartCoroutine(FadeScreen(from, to, inTime));
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}

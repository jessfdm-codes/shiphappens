using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour {

    [SerializeField]
    private Text txt;
    [SerializeField]
    private Gamemode controller;

	// Use this for initialization
	void Start () {
        controller = GameObject.Find("DefaultGamemode").GetComponent<Gamemode>();
        txt = this.GetComponent<Text>();
        txt.text = "0";
	}
	
	// Update is called once per frame
	void Update () {
        txt.text = controller.GetScore().ToString();
	}
}

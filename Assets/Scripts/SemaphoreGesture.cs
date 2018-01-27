using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SemaphoreGesture {

	private string name;
	private string left;
	private string right;
	private string posesFileName = "Poses.json";

	public SemaphoreGesture(string left, string right) {
		this.left = left;
		this.right = right;
	}

	public SemaphoreGesture (string name) {
		this.name = name;
	}

	public void SetHandPosFromName() {
		switch (name) {
		case "UU":
			this.left = "g10";
			right = "g10";
			break;
		case "U":
			left = "g00";
			right = "g20";
			break;
		case "N":
			left = "g02";
			right = "g22";
			break;
		case "P":
			left = "g10";
			right = "g01";

		}
	}

	private void LoadPoses() {
		string filepath = Path.Combine (Application.streamingAssetsPath, posesFileName);

		if (File.Equals (filepath)) {
			string dataAsJson = File.ReadAllText (filepath);
		}
	}


	public bool Equals(SemaphoreGesture sg) {
		if (sg.left == this.left && sg.right == this.right) {
			return true;
		} else {
			return false;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemaphoreGesture {

	private string left;
	private string right;

	public SemaphoreGesture(string newLeft, string newRight) {
		left = newLeft;
		right = newRight;
	}

	public bool Equals(SemaphoreGesture sg) {
		if (sg.left == this.left && sg.right == this.right) {
			return true;
		} else {
			return false;
		}
	}
}

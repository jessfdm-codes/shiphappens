using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SemaphoreGesture {

	private string left;
	private string right;

	public SemaphoreGesture(string left, string right) {
		this.left = left;
		this.right = right;
	}

    public bool Equals(SemaphoreGesture sg) {
		if (sg.left == this.left && sg.right == this.right) {
			return true;
		} else {
			return false;
		}
	}

    override
    public string ToString()
    {
        return "" + left + right;
    }
}

public class SemaphoreGestureTarget : SemaphoreGesture {

    private Sprite Icon;

    public Sprite GetIcon()
    {
        return Icon;
    }

    public SemaphoreGestureTarget(string left, string right) : base(left, right)
    {
        Icon = Resources.Load<Sprite>("Semaphore Sprites/" + left + right);
        if (Icon == null)
        {
            throw new System.IO.FileNotFoundException("The Semaphore Sprit '" + left + right + "' does not exist in the resources folder.");
        }
    }

}

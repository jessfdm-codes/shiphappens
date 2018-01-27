using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemaphoreGrid : MonoBehaviour {

    [SerializeField]
    private GameObject thresholdPrefab;
    private List<GameObject> thresholds;
    private const float ylock = 90.0f;

    // Use this for initialization
    void Start () {

        thresholds = new List<GameObject>();


        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                float newX = 0.0f, newY = 0.0f;
                float width = 0.0f, height = 0.0f;

                switch (i)
                {
                    case 0:
                        newX = -0.8f;
                        width = 1.0f;
                        break;
                    case 1:
                        newX = 0.0f;
                        width = 0.6f;
                        break;
                    case 2:
                        newX = 0.8f;
                        width = 1.0f;
                        break;
                }
                switch (j)
                {
                    case 0:
                        newY = 0.71f;
                        height = 1.0f;
                        break;
                    case 1:
                        newY = -0.09f;
                        height = 0.6f;
                        break;
                    case 2:
                        newY = -0.89f;
                        height = 1.0f;
                        break;
                }

                Debug.Log(newX);
                Debug.Log(newY);
                Debug.Log(width);
                Debug.Log(height);
                Debug.Log("=-=-=-=-=-=-=-=-=-=-=");

                GameObject go = Instantiate(thresholdPrefab, this.transform);
                go.transform.localPosition = new Vector3(newX, newY, 0.0f);
                go.transform.localScale = new Vector3(width, height, 100.0f);
                go.name = "g" + i + j;
                thresholds.Add(go);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(0.0f, ylock, 0.0f);
	}
}

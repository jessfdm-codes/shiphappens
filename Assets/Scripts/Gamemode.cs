using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Gamemode : MonoBehaviour
{

    [SerializeField]
    int score; //score for the player
    bool isPlaying; //whether we're playing or not
    int difficulty; //ramp up as game goes on?
    float speed; //change according to ship when we introduce types of ship?
    [SerializeField]
    GameObject shipPrefab;
    [SerializeField]
    GameObject lighthousePrefab;

	List<GameObject> boats;

    //Spawn points for ships
    [SerializeField]
    Transform Spawn1;
    [SerializeField]
    Transform Spawn2;
    [SerializeField]
    Transform Spawn3;
	[SerializeField]
	int spawnRate;

	List<SemaphoreGestureTarget> availableGestures;

    [SerializeField]
    private SemaphoreGesture currentGesture;
    [SerializeField]
    private string currLeftHandPostion;
    [SerializeField]
    private string currRightHandPostion;

    // Use this for initialization
    void Start()
    {
        availableGestures = SemaphoreGenerator.loadExistentSemaphoreGuestures();
        difficulty = 0;
        speed = 1;
        score = 0;
		spawnRate = 5;
        isPlaying = true;
		boats = new List<GameObject> ();
        SpawnLighthouse();
        SpawnShipEater();
		StartCoroutine (SpawnCooldown());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnLighthouse()
    {
        GameObject lightHouse = (GameObject)Instantiate(lighthousePrefab, new Vector3(-2.7f, 2.2f, 4.5f), transform.rotation);
        lightHouse.transform.Rotate(new Vector3(-1, 0, 1));
    }

    void SpawnShipEater()
    {
        //CapsuleCollider ShipEaterClone = new CapsuleCollider();
        GameObject ShipEater = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ShipEater.name = "ShipEater";
        ShipEater.transform.position = new Vector3(-1.35f, 2, 2.4f);
        ShipEater.GetComponent<BoxCollider>().size = new Vector3(12.2f, 8, 13);
        ShipEater.GetComponent<BoxCollider>().isTrigger = true;
        Destroy(ShipEater.GetComponent<MeshFilter>());
        
    }

	List<SemaphoreGestureTarget> GenerateFlags() {
		int rand = Random.Range (0, availableGestures.Count);
		List<SemaphoreGestureTarget> newList = new List<SemaphoreGestureTarget> ();
		newList.Add(availableGestures[rand]);

		return newList;


	}

    //Spawn a single ship
    void SpawnShip(Transform spawn1)
    {
        GameObject go = (GameObject)Instantiate(shipPrefab, spawn1.position, transform.rotation);
		boats.Add (go);

		go.GetComponent<ShipAI>().Initialize(speed, this.gameObject.GetComponent<Transform>(), GenerateFlags() );
    }

    public void gameOver()
    {
        isPlaying = false;

    }

	IEnumerator SpawnCooldown() {
		while (true) {
			var chance = Random.Range (0.0f, 1.0f);

			if (chance < 0.33f) {
				SpawnShip (Spawn1);
			} else if (chance < 0.66f) {
				SpawnShip (Spawn2);
			} else {
				SpawnShip (Spawn3);
			}

			yield return new WaitForSeconds (spawnRate);
		}
	
	}

	void BroadcastGesture() {
		foreach (GameObject b in boats) {
			if (b.GetComponent<ShipAI> ().ReceiveGesture (currentGesture)) {
				boats.Remove (b);
			}
		}
	
	}

    /*
     * Gesture Recog
     */
     private void RecalculateCurrentGesture()
    {
        if (currLeftHandPostion != null && currRightHandPostion != null)
        {
            currentGesture = new SemaphoreGesture(currLeftHandPostion, currRightHandPostion);

        }
    }

    public void UpdateLeftHandPosition(string newPos)
    {
        if (Regex.IsMatch(newPos, "$g[0-2][0-2]^"))
        {
            currLeftHandPostion = newPos;
        }
        RecalculateCurrentGesture();
    }

    public void UpdateRightHandPosition(string newPos)
    {
        if (Regex.IsMatch(newPos, "$g[0-2][0-2]^")){
            currRightHandPostion = newPos;
        }
        RecalculateCurrentGesture();
    }
}

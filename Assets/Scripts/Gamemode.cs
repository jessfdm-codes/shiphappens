using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    UnityEngine.UI.Text onScreenText;

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
        onScreenText.fontSize = 49;
        onScreenText.alignment = TextAnchor.UpperLeft;
        onScreenText.text = "";
		spawnRate = 2;
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
        GameObject lightHouse = (GameObject)Instantiate(lighthousePrefab, new Vector3(-2.72f, 1.77f, 4.32f), transform.rotation);
        lightHouse.transform.Rotate(new Vector3(0.102f, 0.368f, 0.613f));
    }

    void SpawnShipEater()
    {
        //CapsuleCollider ShipEaterClone = new CapsuleCollider();
        GameObject ShipEater = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ShipEater.name = "ShipEater";
        ShipEater.transform.position = new Vector3(-0.4f, 2, 1.5f);
        ShipEater.GetComponent<BoxCollider>().size = new Vector3(16.3f, 8, 18);
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
        
        //Freeze all objects
        foreach (GameObject ship in GameObject.FindGameObjectsWithTag("Ship"))
        {
            ship.GetComponent<ShipAI>().enabled = false;
        }
        GameObject radio = GameObject.Find("Radio");
        if(radio != null)
        {
            AudioSource audio = radio.GetComponent<AudioSource>();
            audio.volume = 0.2f;
        }

        onScreenText.fontSize = 10;
        onScreenText.alignment = TextAnchor.UpperCenter;
        onScreenText.text = "Oh no, a ship hit the rocks!\nGame over!\nScore: " + score;

        StartCoroutine(ChangeScene("Menu", Color.clear, Color.black, 6f));
    }

    //fade screen from "from" to "to" in "inTime" seconds
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

    /*
     * Gesture Recog
     */
     private void RecalculateCurrentGesture()
    {
        if (currLeftHandPostion != null && currRightHandPostion != null)
        {
            currentGesture = new SemaphoreGesture(currLeftHandPostion, currRightHandPostion);

        }

        Debug.Log("New player pose ::: [Left: " + currLeftHandPostion + "], [Right: " + currRightHandPostion +"]");
        BroadcastGesture();
    }

    public void UpdateLeftHandPosition(string newPos)
    {
        if (Regex.IsMatch(newPos, "^g[0-2][0-2]$"))
        {
            currLeftHandPostion = newPos;
        }
        RecalculateCurrentGesture();
    }

    public void UpdateRightHandPosition(string newPos)
    {
        if (Regex.IsMatch(newPos, "^g[0-2][0-2]$")){
            currRightHandPostion = newPos;
        }
        RecalculateCurrentGesture();
    }

    private void BroadcastGesture()
    {
        foreach (GameObject b in new List<GameObject>(boats))
        {
            try
            {
                if (b.GetComponent<ShipAI>().ReceiveGesture(currentGesture))
                {
                    boats.Remove(b);
                }
            } catch (MissingReferenceException mse)
            {}
            
        }

    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void IncrementScore()
    {
        score++;
        onScreenText.text = "Score: " + score;
    }

    public int GetScore()
    {
        return score;
    }
}

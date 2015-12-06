using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class gameController : MonoBehaviour
{
	public bool debugMatch = false;
	int gameStep;
	internal static int gameRunning;
	public GameObject cupGroup;
	public GameObject stand;
	public Text txtScoreYellow;
	public Text txtScoreGreen;
	public Text txtTimer;
	public Text txtTimerCounter;
	public Material yellow;
	public Material green;
	public GameObject scoreZone;
	Vector3[] cupLocation;
	Vector3[] standLocation;
	float startCounterVal;
	float counter;

	public GameObject canvasHelp;
	public GameObject canvasStart;

	internal standController[] lstStand;
	internal glassController[] lstGlass;

	// Use this for initialization
	void Start()
	{
		cupLocation = new Vector3[]{
			new Vector3(0.83f, 0f, 0.91f),
			new Vector3(0.83f, 0f, 2.09f),
			new Vector3(1.65f, 0f, 1.5f),
			new Vector3(1.75f, 0f, 0.25f),
			new Vector3(1.75f, 0f, 2.75f)
		};
		standLocation = new Vector3[]{
			new Vector3(0.2f, 0f, 0.09f),
			new Vector3(0.1f, 0f, 0.85f),
			new Vector3(0.2f, 0f, 0.85f),
			new Vector3(1.355f, 0f, 0.87f),
			new Vector3(1.4f, 0f, 1.3f),
			new Vector3(1.75f, 0f, 1.1f),
			new Vector3(1.75f, 0f, 0.09f),
			new Vector3(1.85f, 0f, 0.09f),
		};

		lstGlass = new glassController[cupLocation.Length];
		GameObject tmpObj;
		for (int i = 0; i < cupLocation.Length; i++)
		{
			tmpObj = (GameObject)Instantiate(cupGroup, cupLocation [i], new Quaternion(0f, 0f, 0f, 1f));
			lstGlass[i] = tmpObj.GetComponentInChildren<glassController>();
		}

		lstStand = new standController[standLocation.Length * 2];
		Quaternion q = Quaternion.Euler(270f, 0f, 0f);
		for (int i = 0; i < standLocation.Length; i++)
		{
			tmpObj = (GameObject)Instantiate(stand, standLocation [i], q);
			tmpObj.GetComponent<Renderer>().material = yellow;
			standController st = tmpObj.GetComponent<standController>();
			st.player = enPlayer.yellow;
			lstStand[i] = st;
		}

		for (int i = 0; i < standLocation.Length; i++)
		{
			tmpObj = (GameObject)Instantiate(stand, new Vector3(standLocation [i].x, standLocation [i].y, 3f - standLocation [i].z), q);
			tmpObj.GetComponent<Renderer>().material = green;
			standController st = tmpObj.GetComponent<standController>();
			st.player = enPlayer.green;
			lstStand[i + standLocation.Length] = st;
		}

		if(debugMatch)
		{
			gameStep = 2;
			gameRunning = 1;
			canvasHelp.SetActive(false);
			canvasStart.SetActive(false);
			txtTimerCounter.enabled = false;
			counter = 900;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(gVar.keyHelp))
		{
			canvasHelp.SetActive(!canvasHelp.activeSelf);
		}else if(Input.GetKeyDown(gVar.keyExit) || Input.GetKeyDown(gVar.keyCancel))
		{
			Application.Quit();
		}

		switch (gameStep)
		{
			case 0:
				gameRunning = 0;
				if (Input.GetKeyDown(gVar.keyStart))
				{
					
					scoreCounterCinemaCentral[] sc = scoreZone.GetComponentsInChildren<scoreCounterCinemaCentral>();
					sc[0].UpdateScore();
					sc[1].UpdateScore();
					canvasStart.SetActive(false);
					txtTimerCounter.enabled = true;
					startCounterVal = 2f;
					gameStep++;
				}
				break;
			case 1:
				startCounterVal -= Time.deltaTime;
				
				txtTimerCounter.text = Mathf.Ceil(startCounterVal).ToString("0");
				
				if (startCounterVal < 0f)
				{
					gameStep++;
					gameRunning = 1;
					canvasHelp.SetActive(false);
					txtTimerCounter.enabled = false;
					counter = 90;
				}
				break;
			case 2:
				if (Input.GetKeyDown(gVar.keyStart))
					Application.LoadLevel(Application.loadedLevel);
				
				txtTimer.text = ((int)(counter / 60)).ToString("0") + ":" + (counter % 60).ToString("00");
				
				counter -= Time.deltaTime;
				
				if (counter < 0)
				{
					gameRunning = 0;
					gameStep++;
					canvasHelp.SetActive(true);
					canvasStart.SetActive(true);
				}
				break;
			case 3:
				if (Input.GetKeyDown(gVar.keyStart))
					Application.LoadLevel(Application.loadedLevel);
				break;
			default:
				break;
		}
	}
}

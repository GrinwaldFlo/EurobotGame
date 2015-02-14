using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class scoreCount : MonoBehaviour
{
	public Text txtScoreYellow;
	public Text txtScoreGreen;
	public GameObject table;
	int[] score;
	internal static int[] scoreCinemaCentral;
	internal static int[] scoreCinemaLath;
	internal static int[] scoreBuildingZone;
	internal static int[] scorePenality;
	internal static int[] scorePenalityInc;
	internal static int[] scoreClap;
	internal static int[] scoreStairs;

	internal const int scorePopCorn = 1;
	internal const int scoreStand = 2;
	internal const int scoreLightBulb = 3;
	internal const int scoreOneClap = 3;
	internal const int scoreOnePenalty = 5;
	internal const int scoreOneStep = 2;
	internal const int scoreAllStep = 4;
	internal const int scoreRobotOnStairs = 15;

	scoreClap[] scoreClapLst;
	gameController gameCtrl;

	// Use this for initialization
	void Start()
	{
		score = new int[2];
		scoreCinemaLath = new int[2];
		scoreCinemaCentral = new int[2];
		scoreBuildingZone = new int[2];
		scorePenality = new int[2];
		scorePenalityInc = new int[2];
		scoreClap = new int[2];
		scoreStairs = new int[2];


		scoreClapLst = table.GetComponentsInChildren<scoreClap>();
		gameCtrl = table.GetComponent<gameController>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (score == null)
			score = new int[2];
		if (scoreCinemaLath == null)
			scoreCinemaLath = new int[2];
		if (scoreCinemaCentral == null)
			scoreCinemaCentral = new int[2];
		if (scoreBuildingZone == null)
			scoreBuildingZone = new int[2];
		if (scorePenality == null)
			scorePenality = new int[2];
		if (scorePenalityInc == null)
			scorePenalityInc = new int[2];
		if (scoreClap == null)
			scoreClap = new int[2];
		if (scoreStairs == null)
			scoreStairs = new int[2];

		// Count clap
		scoreClap[0] = 0;
		scoreClap[1] = 0;
		for (int i = 0; i < scoreClapLst.Length; i++)
		{
			if(scoreClapLst[i].transform.eulerAngles.x < 10f)
				scoreClap[(int)scoreClapLst[i].player] += scoreOneClap;
		}
		// Count penalties
		scorePenality[0] = 0;
		scorePenality[1] = 0;
		for (int i = 0; i < gameCtrl.lstGlass.Length; i++)
		{
			if(gameCtrl.lstGlass[i].isPenalty)
			{
				scorePenality[(int)gameCtrl.lstGlass[i].playerPenalty] += scoreOnePenalty;
			}
		}
		for (int i = 0; i < gameCtrl.lstStand.Length; i++)
		{
			if(gameCtrl.lstStand[i].isPenalty)
			{
				scorePenality[(int)gameCtrl.lstStand[i].playerPenalty] += scoreOnePenalty;
			}
		}
		scorePenality[0] += scorePenalityInc[0];
		scorePenality[1] += scorePenalityInc[1];
		// Final sum
		for (int i = 0; i < 2; i++)
		{
			score[i] = scoreCinemaLath[i] + scoreCinemaCentral[i] + scoreBuildingZone[i] + scoreClap[i] + scoreStairs[i] - scorePenality[i];
		}


		if(scorePenality[1] == 0)
			txtScoreGreen.text = score[1].ToString();
		else
			txtScoreGreen.text = score[1].ToString() + "(-" + scorePenality[1] + ")";

		if(scorePenality[0] == 0)
			txtScoreYellow.text = score[0].ToString();
		else
			txtScoreYellow.text = score[0].ToString() + "(-" + scorePenality[0] + ")";
	}
}

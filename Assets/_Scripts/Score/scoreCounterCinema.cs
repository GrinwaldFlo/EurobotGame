using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class scoreCounterCinema : MonoBehaviour
{
	public enPlayer player;

	List<GameObject> lstObj;

	// Use this for initialization
	void Start()
	{
		lstObj = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update()
	{
		int tmp = 0;

		for (int i = 0; i < lstObj.Count; i++)
		{
			if(lstObj[i].tag == gVar.tagPopCorn)
			{
				tmp += scoreCount.scorePopCorn;
			}
		}
		if(scoreCount.scoreCinemaLath != null)
			scoreCount.scoreCinemaLath[(int)player] = tmp;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == gVar.tagPopCorn)
		{
			lstObj.Add(other.gameObject);
		}
		else if(other.gameObject.tag == gVar.tagGlassOut)
		{
			other.gameObject.GetComponent<glassController>().isInZone = true;
			other.gameObject.GetComponent<glassController>().isInPlayerZone = player;
		}
	}

	void OnTriggerExit(Collider other)
	{
		lstObj.Remove(other.gameObject);

		if(other.gameObject.tag == gVar.tagGlassOut)
		{
			other.gameObject.GetComponent<glassController>().isInZone = false;
		}
	}
}

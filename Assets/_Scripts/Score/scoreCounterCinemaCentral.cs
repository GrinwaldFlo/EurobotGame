using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scoreCounterCinemaCentral : MonoBehaviour
{

	public enPlayer player;
	List<GameObject> lstObj;
	
	// Use this for initialization
	void Start()
	{
		lstObj = new List<GameObject>();
	}
	
	internal void UpdateScore()
	{
		int tmp = 0;
		
		for (int i = 0; i < lstObj.Count; i++)
		{
			if (lstObj [i].tag == gVar.tagPopCorn)
			{
				tmp += scoreCount.scorePopCorn;
			} else if (lstObj [i].tag == gVar.tagStand)
			{
				standController tmpStand = lstObj [i].GetComponent<standController>();
				if (tmpStand.player == player && tmpStand.isUp())
				{
					tmpStand.isCounting = true;
					tmp += scoreCount.scoreStand;
				}
				else
				{
					tmpStand.isCounting = false;
				}
			} else if (lstObj [i].tag == gVar.tagLightBulb)
			{
				tmp += ((int)(lstObj [i].transform.position.y / gVar.standHeight)) * scoreCount.scoreLightBulb;
			}
		}
		
		scoreCount.scoreCinemaCentral [(int)player] = tmp;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == gVar.tagPopCorn || other.gameObject.tag == gVar.tagStand || other.gameObject.tag == gVar.tagLightBulb)
		{
			if (!lstObj.Contains(other.gameObject))
				lstObj.Add(other.gameObject);
		}
		else if(other.gameObject.tag == gVar.tagGlassOut)
		{
			other.gameObject.GetComponent<glassController>().isInZone = true;
			other.gameObject.GetComponent<glassController>().isInPlayerZone = player;
		}
		UpdateScore();
	}
	
	void OnTriggerExit(Collider other)
	{
		lstObj.Remove(other.gameObject);
		UpdateScore();

		if(other.gameObject.tag == gVar.tagGlassOut)
		{
			other.gameObject.GetComponent<glassController>().isInZone = false;
		}
		else if(other.gameObject.tag == gVar.tagStand)
		{
			other.gameObject.GetComponent<standController>().isCounting = false;
		}
	}
}

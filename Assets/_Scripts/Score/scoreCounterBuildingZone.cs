using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scoreCounterBuildingZone : MonoBehaviour
{
	List<GameObject> lstObj;
	
	// Use this for initialization
	void Start()
	{
		lstObj = new List<GameObject>();
	}
	
	void UpdateScore()
	{
		int[] tmp = new int[2];

		for (int i = 0; i < lstObj.Count; i++)
		{
			if (lstObj [i].tag == gVar.tagStand)
			{
				standController tmpStand = lstObj [i].GetComponent<standController>();

				if(tmpStand.isUp())
				{
					tmpStand.isCounting = true;
					tmp[(int)tmpStand.player] += scoreCount.scoreStand;

					if(tmpStand.curObj != null)
					{
						tmp[(int)tmpStand.player] += ((int)(tmpStand.curObj.transform.position.y / gVar.standHeight)) * scoreCount.scoreLightBulb;
					}
				}
				else
				{
					tmpStand.isCounting = false;
				}
			}
		}
		
		scoreCount.scoreBuildingZone [0] = tmp[0];
		scoreCount.scoreBuildingZone [1] = tmp[1];
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == gVar.tagStand)
		{
			if (!lstObj.Contains(other.gameObject))
				lstObj.Add(other.gameObject);
		}
		UpdateScore();
	}
	
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == gVar.tagStand)
		{
			standController tmpStand = other.gameObject.GetComponent<standController>();
			tmpStand.isCounting = false;
		}
		lstObj.Remove(other.gameObject);
		UpdateScore();
	}
}

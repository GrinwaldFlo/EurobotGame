using UnityEngine;
using System.Collections;

public class scoreStairs : MonoBehaviour 
{
	public enPlayer player;


	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == gVar.tagPlayer)
		{
			robotControl r = other.gameObject.GetComponentInParent<robotControl>();
			if(r.player == player)
			{
				scoreCount.scoreStairs[(int)player] = scoreCount.scoreRobotOnStairs;
			}
		}
	
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == gVar.tagPlayer)
		{
			robotControl r = other.gameObject.GetComponentInParent<robotControl>();
			if(r.player == player)
			{
				scoreCount.scoreStairs[(int)player] = 0;
			}
		}
	}
}

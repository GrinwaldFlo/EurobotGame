using UnityEngine;
using System.Collections;

public class actionPutPopcorn : MonoBehaviour 
{
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == gVar.tagPlayer && other.gameObject.name == "RobotActionDeposePopcornCollider")
		{
			robotControl r = other.gameObject.GetComponentInParent<robotControl>();
			r.release = enRelease.popcorn;
		}
	}

	void OnTriggerExit(Collider other)
	{

		if(other.gameObject.tag == gVar.tagPlayer && other.gameObject.name == "RobotActionDeposePopcornCollider")
		{
			robotControl r = other.gameObject.GetComponentInParent<robotControl>();
			r.release = enRelease.front;
		}
	}
}

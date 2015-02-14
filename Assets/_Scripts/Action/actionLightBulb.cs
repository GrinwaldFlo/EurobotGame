using UnityEngine;
using System.Collections;

public class actionLightBulb : MonoBehaviour 
{
	robotControl robot;

	void Start()
	{
		robot = this.GetComponentInParent<robotControl>();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == gVar.tagLightBulb)
		{
			robot.action = enAction.grabLightBulb;
		}
	}
}

using UnityEngine;
using System.Collections;

public class robotCollision : MonoBehaviour
{
	robotControl robot;
	float lastCollisionTime;

	// Use this for initialization
	void Start()
	{
		robot = this.GetComponentInParent<robotControl>();
		lastCollisionTime = Time.time;
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == gVar.tagStand)
		{
			standController tmpStand = col.gameObject.GetComponent<standController>();
			if(tmpStand.isCounting && tmpStand.player != robot.player)
			{
				tmpStand.isPenalty = true;
				tmpStand.playerPenalty = robot.player;
			}
		}
		else if(col.gameObject.tag == gVar.tagPlayer)
		{
			float s1 = robot.GetComponent<Rigidbody>().velocity.sqrMagnitude;
			float s2 = col.gameObject.GetComponent<Rigidbody>().velocity.sqrMagnitude;

			if(s1 < gVar.minSpeedChoc && s2 < gVar.minSpeedChoc)
			{
				return;
			}

			if(s1 > s2)
			{
				if(Time.time - lastCollisionTime > gVar.timeBetweenChoc)
				{
					scoreCount.scorePenalityInc[(int)robot.player] += scoreCount.scoreOnePenalty;
					lastCollisionTime = Time.time;
				}
			}
		}
		else if(col.gameObject.tag == gVar.tagGlass)
		{
			Vector3 curA = col.rigidbody.rotation.eulerAngles;
			glassController glCtrl = col.gameObject.GetComponentInChildren<glassController>();

			if (glCtrl.isInPlayerZone != robot.player && glCtrl.isInZone && (curA.x > 350f || curA.x < 10f ) && (curA.z > 350f || curA.z < 10f ))
			{
				glCtrl.isPenalty = true;
				glCtrl.playerPenalty = robot.player;
			}
		}
	}
}

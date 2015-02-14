using UnityEngine;
using System.Collections;

public class robotClaper : MonoBehaviour
{
	robotControl robot;
	bool on;
	JointSpring spr;

	// Use this for initialization
	void Start()
	{
		robot = this.GetComponentInParent<robotControl>();
		on = false;
		spr.spring = 10;
		spr.damper = 1;
	}
	
	// Update is called once per frame
	void Update()
	{
		if(robot.action == enAction.clap && Input.GetKeyDown(gVar.keyAction1[(int)robot.player]))
		{
			on = !on;
		}

		if(robot.action != enAction.clap)
			on = false;
	}

	void FixedUpdate()
	{
		if(on)
		{
			spr.targetPosition = 90f;
		}
		else
		{	
			spr.targetPosition = 0f;
		}
		hingeJoint.spring = spr;
	}
}

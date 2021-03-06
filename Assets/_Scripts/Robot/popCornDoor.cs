﻿using UnityEngine;
using System.Collections;

public class popCornDoor : MonoBehaviour 
{
	robotControl robot;
	bool on;
	JointSpring spr;
	
	// Use this for initialization
	void Start()
	{
		robot = this.GetComponentInParent<robotControl>();
		on = false;
		spr.spring = GetComponent<HingeJoint>().spring.spring;
		spr.damper = GetComponent<HingeJoint>().spring.damper;
	}
	
	// Update is called once per frame
	void Update()
	{
		if(robot.release == enRelease.popcorn && Input.GetKeyDown(gVar.keyRelease[(int)robot.player]))
		{
			on = !on;
		}
		
		if(robot.release != enRelease.popcorn)
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
		GetComponent<HingeJoint>().spring = spr;
	}
}

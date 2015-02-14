﻿using UnityEngine;
using System.Collections;

public class actionClap : MonoBehaviour 
{

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == gVar.tagPlayer)
		{
			robotControl r = other.gameObject.GetComponentInParent<robotControl>();
			r.action = enAction.clap;
		}
	}
}

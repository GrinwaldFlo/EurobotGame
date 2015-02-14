using UnityEngine;
using System.Collections;

public class popCornTaker : MonoBehaviour
{
	public Animator animator;
	robotControl robot;

	// Use this for initialization
	void Start()
	{
		robot = this.GetComponentInParent<robotControl>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (robot.action == enAction.grabPopCorn && Input.GetKeyDown(gVar.keyAction1[(int)robot.player]))
		{
			if(robot.hasCup)
				animator.SetTrigger("getPopCorn");
			else
				animator.SetTrigger("getPopCorn2");
		}
	}

}

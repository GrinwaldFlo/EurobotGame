using UnityEngine;
using System.Collections;

public class scoreClap : MonoBehaviour
{
	internal enPlayer player;

	// Use this for initialization
	void Start()
	{
		if(GetComponent<Renderer>().material.name.ToUpper().Contains("YELLOW"))
			player = enPlayer.yellow;
		else
			player = enPlayer.green;
	}
	

}

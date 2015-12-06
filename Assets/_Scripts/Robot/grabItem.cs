using UnityEngine;
using System.Collections;

public class grabItem : MonoBehaviour
{

	public Collider holdingColider;
	public GameObject grabbed;

	// object Holded
	GameObject[] objHolded;
	Vector3 holdingPositionOriginal;
	Vector3 holdingPositionActual;
	Vector3 holdingPositionFinal;
	float holdingLerp;
	float holdingLerpSpeed = 4f;
	//Quaternion holdingRotation;
	int holdingType;
	Vector3 holdingObjHeight;
	float holdNoObjTime;
	robotControl robot;

	// Use this for initialization
	void Start()
	{
		holdingPositionFinal = new Vector3(0.14f, 0.002f, 0.0f);
		objHolded = new GameObject[10];
		holdingObjHeight = new Vector3(0f, 0.07f, 0f);
		holdingType = -1;
		robot = this.GetComponentInParent<robotControl>();
	}

	void FixedUpdate()
	{
		HoldGlass();
		ReleaseGlass();
		
		if (holdNoObjTime > 0)
			holdNoObjTime -= Time.deltaTime;
	}

	void ReleaseGlass()
	{
		if (objHolded == null)
			return;
		
		if (robot.release == enRelease.front && Input.GetKeyDown(gVar.keyRelease[(int)robot.player]))
		{
			holdingType = -1;
			holdingColider.enabled = false;
			holdNoObjTime = 1f;
			robot.hasCup = false;
			
			for (int i = 0; i < objHolded.Length; i++)
			{
				if (objHolded [i] != null)
				{
					objHolded [i].GetComponent<Rigidbody>().isKinematic = false;			
					objHolded [i].transform.parent = null;
					objHolded [i] = null;
				}
			}
		}
	}
	
	void HoldGlass()
	{
		if (holdingType == -1)
			return;
		
		for (int i = 0; i < objHolded.Length; i++)
		{
			if (objHolded [i] != null)
			{
				if (holdingLerp < 1.0f)
				{
					if (i == 0)
					{
						holdingPositionActual = Vector3.Lerp(holdingPositionOriginal, holdingPositionFinal + holdingObjHeight * i, holdingLerp);
					} else
					{
						holdingPositionActual = Vector3.Lerp(holdingPositionFinal + holdingObjHeight * (i - 1), holdingPositionFinal + holdingObjHeight * i, holdingLerp);
					}
					objHolded [i].transform.localPosition = holdingPositionActual;
					//objHolded [i].rigidbody.rotation = holdingRotation;
				} else
				{
//					objHolded [i].rigidbody.isKinematic = true;
//					holdingPositionActual = holdingPositionFinal + holdingObjHeight * i;
				}
			}
		}
		
		if (holdingLerp < 1f)
			holdingLerp += holdingLerpSpeed * Time.deltaTime;
		else
		{
			// Faire montes les pieds ?
			if (holdingType == 1 && objHolded [0] != null)
			{
				for (int i = objHolded.Length -2; i >= 0; i--)
				{
					objHolded [i + 1] = objHolded [i];
				}
				objHolded [0] = null;
				holdingLerp = 0;
			}
		}
		
		holdingColider.enabled = objHolded [0] != null && holdingLerp >= 1f;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<Rigidbody>() == null || holdNoObjTime > 0f || other.gameObject.tag == gVar.tagScore)
			return;
		
		Vector3 curA = other.gameObject.GetComponent<Rigidbody>().rotation.eulerAngles;
		
		if (other.gameObject.tag == gVar.tagGlass && holdingType == -1 && genFunc.inRange(curA.x, 0f, 1f) && genFunc.inRange(curA.z, 0f, 1f))
		{
			objHolded [0] = other.gameObject;
			objHolded [0].transform.parent = grabbed.transform;
			objHolded [0].GetComponent<Rigidbody>().isKinematic = true;
			
			holdingPositionOriginal = other.transform.localPosition;
			holdingLerp = 0;
			//holdingRotation = new Quaternion(0f, 1f, 0f, 0f);
			holdingType = 0;
			robot.hasCup = true;
		} else if (other.gameObject.tag == gVar.tagStand && (holdingType == -1 || holdingType == 1) && objHolded [0] == null && genFunc.inRange(curA.x, 270f, 1f))
		{
			objHolded [0] = other.gameObject;
			objHolded [0].transform.parent = grabbed.transform;
			objHolded [0].GetComponent<Rigidbody>().isKinematic = true;
			
			holdingPositionOriginal = other.transform.localPosition;
			holdingLerp = 0;
			//holdingRotation = Quaternion.Euler(270f, 0f, 0f);
			holdingType = 1;
		}
	}
}

using UnityEngine;
using System.Collections;

public class standController : MonoBehaviour
{
	public Collider lightBulbCollider;
	internal GameObject curObj;
	internal enPlayer player;
	internal bool isCounting;
	internal bool isPenalty;
	internal enPlayer playerPenalty;

	void FixedUpdate()
	{
		if(curObj != null)
		{
			curObj.transform.position = transform.TransformPoint(0f, 0f, 0.11f); //Vector3.Lerp(curObj.transform.position, transform.TransformPoint(0f, 0f, 0.11f), 0.5f);

			Vector3 curA = gameObject.GetComponent<Rigidbody>().rotation.eulerAngles;
			if (!(genFunc.inRange(curA.x, 270f, 20f)))
			{
				curObj.GetComponent<Rigidbody>().isKinematic = false;
				curObj = null;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == gVar.tagScore)
			return;

		// Check stand is straight
		Vector3 curA = gameObject.GetComponent<Rigidbody>().rotation.eulerAngles;

		if (!(genFunc.inRange(curA.x, 270f, 1f)))
			return;

		if (curObj == null && other.gameObject.tag == "LightBulb")
		{
			curObj = other.gameObject;
			curObj.GetComponent<Rigidbody>().isKinematic = true;
		}
	}

	internal bool isUp()
	{
		return genFunc.inRange(transform.rotation.eulerAngles.x, 270f, 1f);
	}
}

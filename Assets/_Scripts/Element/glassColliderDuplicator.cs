using UnityEngine;
using System.Collections;

public class glassColliderDuplicator : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		GameObject coll = transform.GetChild(1).gameObject;
		int nb = 15;
		float step = 360f / (nb+1);
		for (int i = 0; i < nb; i++)
		{
			GameObject newColl = Instantiate(coll);
			newColl.transform.SetParent(this.transform, false);
			newColl.transform.localRotation = newColl.transform.localRotation;
			newColl.transform.Rotate(new Vector3(0f, step * (i+1), 0f), Space.World);
				
				//Rotate(0f, step * (i + 1) , 0f);
		}
	}



}

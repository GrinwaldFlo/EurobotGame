using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class helpController : MonoBehaviour
{
	public Material highlightMaterial;
	public Canvas mainUI;

	public GameObject claps;
	public GameObject stairs;
	public GameObject popcorns;
	public GameObject spots;

	// Use this for initialization
	void Start()
	{
		disableAll();
		claps.SetActive(true);
	}
	
	// Update is called once per frame
	void Update()
	{
		float shininess = Mathf.Abs(Mathf.Sin(Time.time * 3f) * 0.03f);
		highlightMaterial.SetFloat("_Shininess", shininess);
		//Debug.Log(highlightMaterial.GetFloat("_Shininess"));
	}

	void disableAll()
	{
		claps.SetActive(false);
		stairs.SetActive(false);
		popcorns.SetActive(false);
		spots.SetActive(false);
	}

	public void butBack()
	{
		mainUI.enabled = true;
		this.gameObject.SetActive(false);
	}

	public void butClaps()
	{
		disableAll();
		claps.SetActive(true);
	}

	public void butStairs()
	{
		disableAll();
		stairs.SetActive(true);
	}

	public void butPopcorns()
	{
		disableAll();
		popcorns.SetActive(true);
	}

	public void butSpots()
	{
		disableAll();
		spots.SetActive(true);
	}
}

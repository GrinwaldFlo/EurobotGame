using UnityEngine;
using System.Collections;

public class ventouse : MonoBehaviour
{
	public Collider takeCollider;
	public Collider releaseCollider;

	GameObject curObj;
	enAction action;
	enAction oldAction;
	float[] xPos = new float[]{0.05f, 0.13f, 0.195f, 0.225f};
	float yPos = 0.25f;
	float speed = 3f;
	float lerp;
	int step = 0;
	Vector3 origine;
	Vector3 destination;
	bool hitSomething;
	robotControl robot;

	enum enAction
	{
		inside = 0,
		release = 1,
		takeNear = 2,
		takeFar = 3
	}

	// Use this for initialization
	void Start()
	{
		robot = this.GetComponentInParent<robotControl>();
		action = enAction.inside;
		oldAction = enAction.release;
		origine = transform.localPosition;
		destination = new Vector3(transform.localPosition.x, yPos, 0f);
		lerp = 0f;
	}
    
	// Update is called once per frame
	void Update()
	{
		if(gameController.gameRunning == 0)
			return;

		if (Input.GetKeyDown(gVar.keyDeposeLightBulb[(int)robot.player]))
		{
			action = enAction.release;
		} else if (robot.action == global::enAction.grabLightBulb && Input.GetKeyDown(gVar.keyAction1[(int)robot.player]))
		{
			action = enAction.takeNear;
		}
		//TODO Implémenter press long
		/* else if (Input.GetButton(gVar.keyGrab + gVar.P2(player)))
		{
			action = enAction.takeFar;
		}*/
	}

	void FixedUpdate()
	{
		if (action != oldAction)
		{
			step = 0;
			origine = transform.localPosition;
			oldAction = action;
			takeCollider.enabled = false;
			releaseCollider.enabled = false;
			lerp = 0f;
			destination = new Vector3(transform.localPosition.x, yPos, 0f);
			hitSomething = false;
		}

		switch (action)
		{
			case enAction.inside:
				MoveInside();
				break;
			case enAction.takeFar:
			case enAction.takeNear:
				TakeObj();
				break;
		case enAction.release:
				ReleaseObj();
				break;
			default:
				break;
		}

		if (lerp <= 1f)
			lerp += speed * Time.deltaTime;

		if (curObj != null)
		{
			curObj.transform.position = new Vector3(transform.position.x, transform.position.y - 0.03f, transform.position.z);
		}
	}

	void TakeObj()
	{
		switch (step)
		{
		// Go up
			case 0:
			// Go up
				if (lerp > 1f || Mathf.Approximately(transform.localPosition.y, yPos))
				{
					step++;
					origine = transform.localPosition;
					destination = new Vector3(xPos [(int)action], yPos, 0f);
					lerp = 0f;
				}
				transform.localPosition = Vector3.Lerp(origine, destination, lerp);
				break;
			case 1:
			// Go in xPos
				if (lerp > 1f)
				{
					step++;
					takeCollider.enabled = true;
					origine = transform.localPosition;
					destination = new Vector3(xPos [(int)action], 0.05f, 0f);
					lerp = 0f;
				}
				transform.localPosition = Vector3.Lerp(origine, destination, lerp);
				break;
			case 2:
			// Get object
				if (lerp > 1f || hitSomething)
				{
					action = enAction.inside;
				}
				transform.localPosition = Vector3.Lerp(origine, destination, lerp);
				break;
			default:
				break;
		}
	}

	void ReleaseObj()
	{
		switch (step)
		{
		// Go up
			case 0:
			// Go up
				if (lerp > 1f || Mathf.Approximately(transform.localPosition.y, yPos))
				{
					step++;
					origine = transform.localPosition;
					destination = new Vector3(xPos [(int)action], yPos, 0f);
					lerp = 0f;
				}
				transform.localPosition = Vector3.Lerp(origine, destination, lerp);
				break;
			case 1:
			// Go in xPos
				if (lerp > 1f)
				{
					step++;
					releaseCollider.enabled = true;
					origine = transform.localPosition;
					destination = new Vector3(xPos [(int)action], 0.07f, 0f);
					lerp = 0f;
				}
				transform.localPosition = Vector3.Lerp(origine, destination, lerp);
				break;
			case 2:
			// Get object
				if (lerp > 1f || hitSomething)
				{
					action = enAction.inside;
				}
				transform.localPosition = Vector3.Lerp(origine, destination, lerp);
				break;
			default:
				break;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == gVar.tagScore)
			return;

		hitSomething = true;


		if (curObj == null && other.gameObject.tag == "LightBulb")
		{
			curObj = other.gameObject;
			curObj.GetComponent<Rigidbody>().isKinematic = true;
		}
		else if(curObj != null)
		{
			curObj.GetComponent<Rigidbody>().isKinematic = false;
			curObj = null;
		}

		takeCollider.enabled = false;
		releaseCollider.enabled = false;
	}

	void MoveInside()
	{
		switch (step)
		{
		// Go up
			case 0:
			// Check if we are up
				if (lerp > 1f)
				{
					step++;
					origine = transform.localPosition;
					destination = new Vector3(xPos [(int)action], yPos, 0f);
					lerp = 0f;
				}
				transform.localPosition = Vector3.Lerp(origine, destination, lerp);
				break;
			case 1:
				if (lerp > 1f)
				{
					step++;
				}
				transform.localPosition = Vector3.Lerp(origine, destination, lerp);
				break;
			case 2:
			// Dodo
			default:
				break;
		}
	}




}

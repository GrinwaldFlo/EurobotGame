using UnityEngine;
using System.Collections;

public class robotControl : MonoBehaviour
{
	public float acceleration;
	public float ratioRotation = 0.4f;
	public enPlayer player;

	public WheelCollider wheelL;
	public WheelCollider wheelR;

	internal enAction action;
	internal enRelease release;
	internal bool hasCup;

	// Motion
	public float[] speed = new float[]{1000, 350};
	int curSpeed = 0;
	Vector3 movement;
	Vector3 movementRobot;
	Vector3 movementSpeed;



	// Use this for initialization
	void Start()
	{
		action = enAction.clap;
	}

	// Update is called once per frame
	void Update()
	{
		if (gameController.gameRunning == 0)
			return;

		if (Input.GetKeyUp(gVar.keySpeedSlow [(int)player]))
		{
			curSpeed = curSpeed == 0 ? 1 : 0;
		}
	}

	void FixedUpdate()
	{
		rigidbody.centerOfMass = new Vector3(0f, 0.01f, 0f);

		if (gameController.gameRunning == 0)
		{
			wheelL.brakeTorque = 10000f;
			wheelR.brakeTorque = 10000f;
			return;
		}
		else
		{
			wheelL.brakeTorque = 0f;
			wheelR.brakeTorque = 0f;
		}

		Move();
	}

	float h, v;
	void Move()
	{
		if (Input.GetKey(gVar.keyUp [(int)player]))
			v = 1f;
		else if (Input.GetKey(gVar.keyDown [(int)player]))
			v = -1f;
		else
			v = 0f;
		if (Input.GetKey(gVar.keyLeft [(int)player]))
			h = -1f;
		else if (Input.GetKey(gVar.keyRight [(int)player]))
			h = 1f;
		else
			h = 0f;

		if(h != 0f)
		{
			if(Mathf.Abs(wheelL.rpm) > speed[curSpeed] * ratioRotation ||
			   Mathf.Abs(wheelR.rpm) > speed[curSpeed] * ratioRotation)
			{
				wheelL.motorTorque = 0f;
				wheelR.motorTorque = 0f;
			}
			else
			{
				wheelL.motorTorque = h * acceleration;
				wheelR.motorTorque = -h * acceleration;
			}
		}
		else if( v!= 0f)
		{
			if(Mathf.Abs(wheelL.rpm) > speed[curSpeed])
			{
				wheelL.motorTorque = 0f;
				wheelR.motorTorque = 0f;
			}
			else
			{
				wheelL.motorTorque = v * acceleration;
				wheelR.motorTorque = v * acceleration;
			}
		}
		else
		{
			wheelL.brakeTorque = 300f;
			wheelR.brakeTorque = 300f;
			wheelL.motorTorque = 0f;
			wheelR.motorTorque = 0f;
		}

		/*movement = new Vector3(v, 0.0f, 0.0f);
		
		movementRobot = transform.TransformVector(movement);
		movementSpeed = movementRobot * speed [curSpeed] * Time.deltaTime;
		rigidbody.AddForce(movementSpeed, ForceMode.Force);
		rigidbody.AddTorque(transform.up * h * speed [curSpeed] * p * Time.deltaTime, ForceMode.Force);*/
	}


}

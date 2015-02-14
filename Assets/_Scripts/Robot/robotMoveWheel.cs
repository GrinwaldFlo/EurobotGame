using UnityEngine;
using System.Collections;

public class robotMove : MonoBehaviour
{
	public float speed = 6f;
	public float speedRotation = 6f;
	public float maxSpeed = 0.5f;
	public float breakForce = 0.1f;
	public float breakStop = 50f;

	public WheelCollider wheelFL;
	public WheelCollider wheelFR;
	public WheelCollider wheelBL;
	public WheelCollider wheelBR;

	public float wheelRpm;

	public float[] torques;
	public float[] brakes;

	Vector3 movement;
	public float maxSpeedMe;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void Awake ()
	{
	}

	void FixedUpdate ()
	{
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		
		Move (h, v);

		//Animating(h, v);
	}

	void Move (float h, float v)
	{
		//maxSpeedMe = transform.InverseTransformDirection(rigidbody.velocity).z;
		maxSpeedMe = Mathf.Max(new float[]{Mathf.Abs(wheelFL.rpm), Mathf.Abs(wheelFR.rpm), Mathf.Abs(wheelBL.rpm),Mathf.Abs(wheelBR.rpm) });

		wheelFL.brakeTorque = 0;
		wheelFR.brakeTorque = 0;
		wheelBL.brakeTorque = 0;
		wheelBR.brakeTorque = 0;

		wheelFL.motorTorque = v * speed;
		wheelFR.motorTorque = v * speed;
		wheelBL.motorTorque = v * speed;
		wheelBR.motorTorque = v * speed;
		wheelFL.motorTorque += h * speedRotation;
		wheelFR.motorTorque -= h * speedRotation;
		wheelBL.motorTorque += h * speedRotation;
		wheelBR.motorTorque -= h * speedRotation;

		if(maxSpeedMe > maxSpeed)
		{
			wheelFL.motorTorque = 0;
			wheelFR.motorTorque = 0;
			wheelBL.motorTorque = 0;
			wheelBR.motorTorque = 0;
			wheelFL.brakeTorque = breakForce * maxSpeedMe;
			wheelFR.brakeTorque = breakForce * maxSpeedMe;
			wheelBL.brakeTorque = breakForce * maxSpeedMe;
			wheelBR.brakeTorque = breakForce * maxSpeedMe;
		}

		if(v == 0 && h == 0)
		{
			wheelFL.motorTorque = 0;
			wheelFR.motorTorque = 0;
			wheelBL.motorTorque = 0;
			wheelBR.motorTorque = 0;
			wheelFL.brakeTorque = breakStop;
			wheelFR.brakeTorque = breakStop;
			wheelBL.brakeTorque = breakStop;
			wheelBR.brakeTorque = breakStop;
		}

		torques = new float[]{wheelFL.motorTorque, wheelFR.motorTorque, wheelBL.motorTorque, wheelBR.motorTorque};
		brakes = new float[]{wheelFL.brakeTorque, wheelFR.brakeTorque, wheelBL.brakeTorque, wheelBR.brakeTorque};
		//Debug.Log("H"+ h + " V" + v + " sX" + playerRigidBody.velocity.x.ToString("0.00") + " sY" + playerRigidBody.velocity.y.ToString("0.00") + " sZ" + playerRigidBody.velocity.z.ToString("0.00"));
	//	movement.Set (0.0f, 0.0f, v);
	//	movement = movement.normalized * speed * Time.deltaTime;
		
	//	playerRigidBody.MovePosition (transform.position + movement);
	}
	
}

using UnityEngine;
using System.Collections;

public class WheelTest : MonoBehaviour
{

	public float speed = 6f;
	public float speedRotation = 6f;
	public float maxSpeed = 0.5f;
	public float breakForce = 0.1f;
	public float breakStop = 50f;

	public GameObject wheelFL;
	public GameObject wheelFR;
	public GameObject wheelBL;
	public GameObject wheelBR;

	public float wheelRpm;

	public float[] torques;
	public float[] brakes;

	public float maxSpeedMe;

	private WheelCollider[] wheels;
	
	// Use this for initialization
	void Start()
	{
		wheels = new WheelCollider[4];
		wheels[0] = wheelFL.GetComponent<WheelCollider>();
		wheels[1] = wheelFR.GetComponent<WheelCollider>();
		wheels[2] = wheelBL.GetComponent<WheelCollider>();
		wheels[3] = wheelBR.GetComponent<WheelCollider>();
	}

	void FixedUpdate()
	{
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		Move(h, v);

		//Animating(h, v);
	}

	void Move(float h, float v)
	{
		for (int i = 0; i < wheels.Length; i++)
		{
			wheels[i].motorTorque = speed * v;
		}
	}
}

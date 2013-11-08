using UnityEngine;
using System.Collections;

public class FrigateMovement : ShipMovement {
	
	AccountSession AS;
	
	float currentThrust;
	float currentSendTimer = 0;
	
	Vector3 vel;
	Vector3 storedForce;
	Quaternion rot;
	
	
	float currentHorizontalForce = 0;
	
	Vector2 MouseInput;
	float HorizontalInput;
	float VerticalInput;

	// Use this for initialization
	void Start () {
		
		MouseInput = Vector2.zero;
		HorizontalInput = 0;
		VerticalInput = 0;
		
		/*
		AS = GameObject.FindGameObjectWithTag("AccountSession").GetComponent<AccountSession>();
		
		
		if (!networkView.isOwner)
		{
			
			Destroy(this);	
			
		}
		*/
		
	}
	
	public override void ProvideInput(Vector2 _MouseInput,float _HorizontalInput,float _VerticalInput)
	{
	
		MouseInput = _MouseInput;
		HorizontalInput = _HorizontalInput;
		VerticalInput = _VerticalInput;

	}
	
	
	// Update is called once per frame
	void Update () {
		
		
		Vector3 relativeVector;
		relativeVector = new Vector3(MouseInput.x,0,MouseInput.y);
	
		float inputValue = VerticalInput;
		
		if(inputValue < -0.25f)
		{
			inputValue = -0.25f;
		}
		
		vel = (transform.forward * acceleration) * inputValue;
			
		rot = Quaternion.Lerp(rot,Quaternion.LookRotation(relativeVector,transform.up),Time.deltaTime * rotationSpeed);
			
		float angle = Quaternion.Angle(transform.rotation,Quaternion.LookRotation(relativeVector,transform.up));
		
		angle = angle / 60;	
			
		if(angle > 1)
			angle = 1;
		
		if(Vector3.Dot(transform.right,relativeVector) < 0)
		{
			angle = -angle;
		}
	
		if(rigidbody.velocity.magnitude < maxSpeed)
		{
			rigidbody.AddForce(vel * Time.deltaTime,ForceMode.Acceleration);
		}
		
		audio.pitch = rigidbody.velocity.magnitude / maxSpeed;
	
		transform.rotation = rot;
		
		transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,60 * -angle);
		
		
		if(currentSendTimer > 0.02f)
		{
			Vector2 Pos = new Vector2(transform.position.x,transform.position.z);
			Vector2 Vel = new Vector2(rigidbody.velocity.x,rigidbody.velocity.z);
			short Rot = (short)transform.localEulerAngles.y;
			networkView.RPC("SendInput",uLink.RPCMode.Server,MouseInput,Pos,Vel,Rot);
			currentSendTimer = 0;
		}
		else
		{
			currentSendTimer += Time.deltaTime;
		}

	}
	
	void LateUpdate () {
		
		if (networkView.isOwner)
			Camera.main.transform.position = transform.position + (Vector3.up * 25);		
	
	}

}

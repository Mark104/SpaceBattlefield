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

	// Use this for initialization
	void Start () {
		
		AS = GameObject.FindGameObjectWithTag("AccountSession").GetComponent<AccountSession>();
		
		
		if (!networkView.isOwner)
		{
			
			Destroy(this);	
			
		}
		
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 relativeVector;
		
		if(AS.isBot)
		{
			relativeVector = transform.forward;
		}
		else
		{
			relativeVector = new Vector3(Input.mousePosition.x - (Screen.width * 0.5f),0,Input.mousePosition.y - (Screen.height * 0.5f));
			relativeVector.Normalize();
		}
		
		float inputValue = Input.GetAxis("Vertical");
		
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
			rigidbody.AddForce(vel,ForceMode.Acceleration);
		}
		
		audio.pitch = rigidbody.velocity.magnitude / maxSpeed;
		
		if(currentSendTimer > 0.05f)
		{
			networkView.RPC("InputRecived",uLink.RPCMode.Server,rigidbody.velocity,transform.position,rot);
			storedForce = Vector3.zero;
			currentSendTimer = 0;
			
		}
		else
		{
			currentSendTimer += Time.deltaTime;	
		}
		
		transform.rotation = rot;
		
		transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,60 * -angle);

	}
	
	void LateUpdate () {
		
		if (networkView.isOwner)
			Camera.main.transform.position = transform.position + (Vector3.up * 20);
		
	
	}

}

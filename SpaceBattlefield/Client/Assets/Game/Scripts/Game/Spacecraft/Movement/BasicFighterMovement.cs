using UnityEngine;
using System.Collections;

public class BasicFighterMovement : uLink.MonoBehaviour {
	
	AccountSession AS;
	
	float currentThrust;
	float currentSendTimer = 0;
	
	Vector3 vel;
	Vector3 storedForce;
	Quaternion rot;

	// Use this for initialization
	void Start () {
		
		AS = GameObject.FindGameObjectWithTag("AccountSession").GetComponent<AccountSession>();
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(AS.isBot)
		{
			if(currentThrust <3)
			{
				currentThrust += 1;
			}
		}
		else
		{
			currentThrust += Input.GetAxis("Vertical");
		}
		
		
		
		
		if (currentThrust < 5)
		{
			
			
			
			if(currentThrust < 0)
			{
				currentThrust = 0;
			}
		}
		else
		{
			
			currentThrust = 5;
		}
		
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
		
		
		
		vel = Vector3.Lerp(vel,relativeVector * currentThrust,Time.deltaTime * 2);
		
		storedForce += vel;
		
		rot = Quaternion.Lerp(rot,Quaternion.LookRotation(relativeVector,transform.up),Time.deltaTime * 3);
		
		rigidbody.AddForce(vel);
		
		if(currentSendTimer > 0.1f)
		{
			networkView.RPC("InputRecived",uLink.RPCMode.Server,storedForce,rot);
			storedForce = Vector3.zero;
			currentSendTimer = 0;
		}
		else
		{
			currentSendTimer += Time.deltaTime;	
		}
		
		
		transform.rotation = rot;
		
		
		
	
	}
	
	void LateUpdate () {
		
		
		Camera.main.transform.position = transform.position + (Vector3.up * 20);
		
	
	}
	
	
	void FixedUpdate () {
		
		
		rigidbody.AddForce((transform.forward * currentThrust) * Time.deltaTime,ForceMode.Force);
		
		
		
	}
	
	void uLink_OnNetworkInstantiate(byte _Team) {
		
		switch(_Team)
		{
			case 1:
			
				print ("I'm red");
			
				rot = Quaternion.Euler(0,90,0);
				
				break;
			case 2:
			
				print ("I'm green");
			
				rot = Quaternion.Euler(0,-90,0);
				
				break;
			case 3:
				
				print ("I'm blue");
			
				rot = Quaternion.Euler(0,0,0);
				
				break;
		}
		
	}
}

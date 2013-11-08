using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProxyInterface : InputInterface {
	
	Quaternion destRotation;
	Vector3 lerpPosition;
	float lerpStrength = 0;
	
	// Use this for initialization
	void Start () {
		
		Destroy(SM);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		
		transform.rotation = Quaternion.Lerp(transform.rotation,destRotation,0.2f);
		
	
	}
	
	[RPC]
	void RecieveInput(Vector2 _MouseInput, Vector2 _Pos, Vector2 _Vel,short _Rot,uLink.NetworkMessageInfo _Info)
	{
		GotTick(_Pos,_Vel,_Rot,_Info.timestamp);
	}
	
	void GotTick(Vector2 _Pos,Vector2 _Vel,short _Rot,double _TimeStamp)
	{
		double timeDifference = uLink.Network.time - _TimeStamp;
		Vector2 extrapolatedPosition = _Pos + new Vector2((float)(_Vel.x * timeDifference),(float)(_Vel.y * timeDifference));
		
		float dis = Vector2.Distance(_Pos,new Vector2(transform.position.x,transform.position.z));
		
		if(dis > 2)
		{
			print ("Hit thresshold");
			transform.position = new Vector3(extrapolatedPosition.x,0,extrapolatedPosition.y);
		}
		else
		{
			lerpPosition = new Vector3(extrapolatedPosition.x,0,extrapolatedPosition.y);	
			lerpStrength = dis / 2;
			transform.position = Vector3.Lerp(transform.position,lerpPosition,lerpStrength);
			
		}
		
		Vector3 difference = new Vector3(_Vel.x,0,_Vel.y) -	rigidbody.velocity;	
		difference *= (float)timeDifference;
		
		rigidbody.velocity = new Vector3(_Vel.x,0,_Vel.y) + difference;
		
		float rotDiff = _Rot - transform.eulerAngles.y; 
		rotDiff *= (float)timeDifference;
		
		destRotation =  Quaternion.Euler(transform.eulerAngles.x,_Rot ,transform.eulerAngles.z);
		//transform.rotation = destRotation;
		
	}
	
}

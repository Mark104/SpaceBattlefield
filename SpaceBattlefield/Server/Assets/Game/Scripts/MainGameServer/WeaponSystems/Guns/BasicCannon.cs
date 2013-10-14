using UnityEngine;
using System.Collections;


public class BasicCannon : Armament {
	
	float damage;
	float fireRate = 0.2f;
	float nextFire = 0;
	float turnRate = 1;
	int turnLimit = 30;
	int projectileSpeed = 20;
	
	float turretOffset = 1;
	
	GameObject bulletType;
	
	
	public override void Start () {
	
		base.Start();
		
	}
	
	public override void UpdateFacing (Vector3 _Pos) {	
		
		
		Vector3 relativePos = _Pos - transform.position;
		
		float currentAngle = Vector3.Angle(transform.parent.forward,relativePos);
		
		if(currentAngle != 0)
		{
	
			
			if(currentAngle <  turnLimit)
			{
		        Quaternion rotation = Quaternion.LookRotation(relativePos,transform.parent.up);
			
				float frameAngle = Vector3.Angle(transform.forward,relativePos);
				
				if(frameAngle != 0)
				{
					float frameStep = turnRate / frameAngle;
		
					if(frameStep > 1)
					{
						transform.rotation = rotation;
					}
					else
					{
						transform.rotation = Quaternion.Slerp(transform.rotation,rotation,frameStep);
					}
				}
			
			}
			else
			{
				Quaternion rotation = transform.parent.rotation;	
				
				float frameAngle = Vector3.Angle(transform.forward,transform.parent.forward);
				
				if(frameAngle != 0)
				{
					float frameStep = turnRate / frameAngle;
					
					
					
					if(frameStep > 1)
					{
						transform.rotation = rotation;
					}
					else
					{
						transform.rotation = Quaternion.Slerp(transform.rotation,rotation,frameStep);
					}
					
				}
			}
			
			
		}
			
	}
	
	public override string Status () {
		
		
		
		
		return "test";
		
	}

	
	
	
}

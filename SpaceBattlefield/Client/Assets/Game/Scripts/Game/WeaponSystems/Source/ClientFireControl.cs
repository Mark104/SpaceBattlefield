using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClientFireControl : uLink.MonoBehaviour {
		
	protected Dictionary<byte,Armament> armamentList = new Dictionary<byte, Armament>();
	
	float lastX;
	
	float lastY;
	
	AccountSession AS;
	
	ShipEntity SE;
	
	public void Awake ()
	{
		AS = GameObject.FindGameObjectWithTag("AccountSession").GetComponent<AccountSession>();
		SE = GetComponent<ShipEntity>();
	}
	
	
	public void LinkGun (Armament _InLink)
	{
		armamentList.Add((byte)_InLink.id,_InLink);
		print ("Linked");
	}
	
	public void SendGunMessage(int _ID,short _Rotation,Vector3 _Position,GameObject projectile,int _Speed)
	{
		Vector2 vec2PositionConversion = new Vector2(_Position.x,_Position.z);
		networkView.RPC("GunFired",uLink.RPCMode.Server,(byte)_ID,_Rotation,vec2PositionConversion);
		
		
		GameObject tmpObj =	(GameObject)Instantiate(projectile,_Position,Quaternion.Euler(0,_Rotation,0));
		ClientBullet cb = tmpObj.GetComponent<ClientBullet>();
		cb.speed = _Speed;
		

		
		Physics.IgnoreCollision(this.collider,tmpObj.collider);
		
		cb.team = SE.shipTeam;
		
		cb.tag = "Bullet";
		
		
	}
	
	
	
	public virtual void PrimaryInput()
	{}
	
	public virtual void SecondaryInput()
	{}
	
	public virtual void MousePositionUpdate(Vector3 WOrldSpacePosition)
	{}
	
	void Update () {
		
		if(networkView.isOwner)
		{
		
			if(Input.mousePosition.x != lastX)
			{
				if(Input.mousePosition.y != lastY)
				{
					Vector3 pointerLocation = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,Camera.main.camera.nearClipPlane));
					
					MousePositionUpdate(pointerLocation);
				}
			}
			
			lastX = Input.mousePosition.x;
			lastY = Input.mousePosition.y;
			
			if(Input.GetButtonDown("PrimaryWeapon"))
			{
				PrimaryInput();
			}
			
			if(Input.GetButtonDown("SecondaryWeapon"))
			{
				SecondaryInput();
			}
		}
	}
	
	[RPC]
	public void SpawnBullet(byte _ID,short _Rotation,Vector2 _Position)
	{
		armamentList[_ID].ProxyFire(_Rotation,_Position,SE.shipTeam);
	}

}

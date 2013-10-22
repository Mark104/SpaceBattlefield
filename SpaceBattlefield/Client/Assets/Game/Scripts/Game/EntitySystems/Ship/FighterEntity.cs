using UnityEngine;
using System.Collections;

public class FighterEntity : ShipEntity {

	void OnTriggerEnter(Collider _Collision)
	{

		if(_Collision.collider.CompareTag("Bullet")) // did we get hit by a bullet?	
		{
			if(networkView.isOwner)
			{
				if(shipTeam != _Collision.collider.GetComponent<ClientBullet>().team) // was that bulllet from another team?
				{
					health -= _Collision.collider.GetComponent<ClientBullet>().damage;
					
					print ("Lost health " + health);
					
					if(health <= 0)
					{
						print ("Death message sent");
						networkView.RPC("Destroyed",uLink.RPCMode.Server);
						Destroy(gameObject);	
					}
				}
			}
			
			Destroy(_Collision.collider.gameObject);
		}
		
	}

}

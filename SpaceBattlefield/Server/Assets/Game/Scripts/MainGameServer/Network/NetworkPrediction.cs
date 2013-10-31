using UnityEngine;
using System.Collections;

public class NetworkPrediction : uLink.MonoBehaviour {

	void uLink_OnSerializeNetworkView(uLink.BitStream stream, uLink.NetworkMessageInfo info)
	{
		if (stream.isWriting)
		{
			// Send information to all proxies and the owner (clients)
			// This code is executed on the creator (server) 
			stream.Write(new Vector2(transform.position.x,transform.position.z));
			stream.Write(new Vector2(rigidbody.velocity.x,rigidbody.velocity.z));
			stream.Write(rigidbody.rotation); 
		}
		else
		{
		
		}
	}
	
	
	[RPC]
	public void InputRecived(Vector3 velocity,Vector3 _Position,Quaternion rot,uLink.NetworkMessageInfo info)
	{
		rigidbody.velocity = velocity;
		transform.position = _Position;
		transform.rotation = rot;
	}
}

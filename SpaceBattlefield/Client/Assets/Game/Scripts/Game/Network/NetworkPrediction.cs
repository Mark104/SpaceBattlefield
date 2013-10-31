using UnityEngine;
using System.Collections;

public class NetworkPrediction : uLink.MonoBehaviour {
	
	public float thresshold;
	
	

	void uLink_OnSerializeNetworkView(uLink.BitStream stream, uLink.NetworkMessageInfo info)
	{
		if (stream.isWriting)
		{
			
		}
		else
		{
			// Update the proxy state when statesync arrives.
			Vector2 pos = stream.Read<Vector2>();
			Vector2 vel = stream.Read<Vector2>();
			Quaternion rot = stream.Read<Quaternion>();

			GotTick(pos, vel, rot, info.timestamp);
		}
	}
	

	void GotTick(Vector2 _Pos,Vector2 _Vel,Quaternion _Rot,double _TimeStamp)
	{
		if(networkView.isOwner)
		{
		}
		else
		{
			if(Vector2.Distance(_Pos,new Vector2(transform.position.x,transform.position.z)) > thresshold)
			{
				print ("Hit thresshold");
				transform.position = new Vector3(_Pos.x,0,_Pos.y);
			}
			else
			{
				double timeDifference = uLink.Network.time - _TimeStamp;
				print ("Attempting to extrapolate with " + timeDifference);

				Vector2 extrapolatedPosition = _Pos + new Vector2((float)(_Vel.x * timeDifference),(float)(_Vel.y * timeDifference));
				
				float extrapolationAmount = Vector2.Distance(extrapolatedPosition,new Vector2(transform.position.x,transform.position.z));
				
				print ("Distance to extrapolated point is " + extrapolationAmount);
				transform.position = Vector3.Lerp(transform.position,new Vector3(extrapolatedPosition.x,0,extrapolatedPosition.y),Time.deltaTime * 4);
			}
			
			rigidbody.velocity = new Vector3(_Vel.x,0,_Vel.y);
			transform.rotation = _Rot;
		}
	}
	
	
}

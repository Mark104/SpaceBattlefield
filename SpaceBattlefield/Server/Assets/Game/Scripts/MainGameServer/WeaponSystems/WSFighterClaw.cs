using UnityEngine;
using System.Collections;

public class WSFighterClaw : ServerFireController {
	
	[RPC]
	public void GunFired(byte _ID,short _Rotation,Vector2 _Position)
	{
		networkView.RPC("SpawnBullet",uLink.RPCMode.OthersExceptOwner,_ID,_Rotation,_Position);
	}

}

using UnityEngine;
using System.Collections;

public class FighterEntity : ShipEntity {

	[RPC]
	void Destroyed()
	{
		uLink.Network.Destroy(this.gameObject);
		
		
	}

}

using UnityEngine;
using System.Collections;

public class InputChecker : uLink.MonoBehaviour {

	
	[RPC]
	void SendInput(Vector2 _MouseInput,Vector2 _Pos, Vector2 _Vel,short _Rot)
	{
		networkView.RPC("RecieveInput",uLink.RPCMode.OthersExceptOwner,_MouseInput,_Pos,_Vel,_Rot);
				
	}
}

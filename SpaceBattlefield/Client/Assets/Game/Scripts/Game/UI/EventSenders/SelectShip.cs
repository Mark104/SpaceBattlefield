using UnityEngine;
using System.Collections;

public class SelectShip : MonoBehaviour {

	public byte shipID;

	public void OnClick()
	{
		
		GameObject.FindGameObjectWithTag("GameController").GetComponent<GC_InGameController>().AttemptShipSelection(shipID);
		
	}
}

using UnityEngine;
using System.Collections;

public class JoinTeam : MonoBehaviour {
	
	public byte teamID;


	public void OnClick()
	{
		
			
		GameObject.FindGameObjectWithTag("GameController").GetComponent<GC_InGameController>().JoinAttempt(teamID);

	}
}

using UnityEngine;
using System.Collections;

public class JoinTeam : MonoBehaviour {
	
	public byte teamId;


	public void OnClick()
	{
		
			
		GameObject.FindGameObjectWithTag("GameController").GetComponent<GC_InGameController>().JoinAttempt(teamId);

	}
}

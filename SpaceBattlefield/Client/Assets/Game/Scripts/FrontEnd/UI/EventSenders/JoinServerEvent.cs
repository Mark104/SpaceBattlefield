using UnityEngine;
using System.Collections;

public class JoinServerEvent : MonoBehaviour {

	public void OnClick()
	{
		
		GameObject.FindGameObjectWithTag("GameController").GetComponent<GC_FrontEnd>().JoinServer();
	
	}
}

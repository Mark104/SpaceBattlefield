using UnityEngine;
using System.Collections;

public class FrontUIManager : MonoBehaviour {
	
	public PnlLogin _PnlLogin;
	
	public void LogedIn()
	{
		_PnlLogin.ChangeHideState();
		
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

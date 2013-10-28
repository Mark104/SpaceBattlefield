using UnityEngine;
using System.Collections;

public class FrontUIManager : MonoBehaviour {
	
	public PnlLogin _PnlLogin;
	public PnlTop _PnlTop;
	public GameObject _PnlBack;
	
	public void LogedIn()
	{
		_PnlLogin.ChangeHideState();
		_PnlTop.ChangeHideState();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

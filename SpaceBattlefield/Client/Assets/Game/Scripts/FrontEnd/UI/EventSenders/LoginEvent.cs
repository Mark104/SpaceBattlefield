using UnityEngine;
using System.Collections;

public class LoginEvent : MonoBehaviour {
	
	public UILabel username;
	public UILabel password;

	public void OnClick()
	{
		
		GameObject.FindGameObjectWithTag("GameController").GetComponent<GC_FrontEnd>().AttemptLogin(username.text,password.text);
	
	}
}

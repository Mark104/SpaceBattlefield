using UnityEngine;
using uLobby;
using uGameDB;
using System.Collections;

public class GC_FrontEnd : uLink.MonoBehaviour {
	
	bool isLogedIn = false;
	
	AccountSession AS;
	
	FrontUIManager UI;
	
	

	// Use this for initialization
	void Start () {
		
		UI = GameObject.FindGameObjectWithTag("UIManager").GetComponent<FrontUIManager>();
		
		
		Lobby.AddListener(this);
		
		GameObject actSessionObj = GameObject.FindGameObjectWithTag("AccountSession");
		
		if(actSessionObj == null)
		{
			GameObject tmpObj = Instantiate(new GameObject()) as GameObject;
			
			tmpObj.tag = "AccountSession";
			tmpObj.name = "AccountSession";
				
			AS = tmpObj.AddComponent<AccountSession>();
			AS.EnteredWaitingRoom(this);
			
			uLink.NetworkViewID tmpId = new uLink.NetworkViewID(0);
			
			tmpObj.AddComponent<uLink.NetworkView>().SetManualViewID(1);
		}
		else
		{
			AS.GetComponent<AccountSession>().EnteredWaitingRoom(this);
				
			SkipLoginPhase();
			
			
		}
		
		
		
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void AttemptLogin(string _Name,string _Password)
	{
		print ("Attempting login with " + _Name + _Password);
		
		AS.Login(_Name,_Password);
	}
	
	public void Register(string _Name,string _Password)
	{
		
		AS.Register(_Name,_Password);
		
		
	}
	
	public void LogedIn()
	{
		isLogedIn = true;
		
		UI.LogedIn();
		
	}
	
	void SkipLoginPhase()
	{
		
		
		
	}
	
}

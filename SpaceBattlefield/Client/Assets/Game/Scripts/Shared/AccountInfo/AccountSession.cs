using UnityEngine;
using uLink;
using uLobby;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AccountSession : uLink.MonoBehaviour {
	
	public GC_FrontEnd FrontEndGC;
	
	private GameObject loadingScreen;
	
	string serverIp;
	
	int serverPort;
	
	Account myAccount;
	
	public string _AccountName;
	
	public uLink.BitStream shipCode;

	// Use this for initialization
	void Start () {
		
		DontDestroyOnLoad(this);
		
		
		AccountManager.OnAccountRegistered += OnAccountRegistered;
		AccountManager.OnRegisterFailed += OnRegisterFailed;
		AccountManager.OnAccountLoggedIn += OnAccountLoggedIn;
		AccountManager.OnAccountLoggedOut += OnAccoutLogedOut;
		AccountManager.OnLogInFailed += OnAccountLoginFail;
		
		Application.runInBackground = true;
		Lobby.AddListener(this);
		
		Lobby.ConnectAsClient("ec2-54-229-103-211.eu-west-1.compute.amazonaws.com",7050);
		print ("Connecting");
		
		//Lobby.ConnectAsClient("192.168.0.3",7050);
		
	}
	
	public void EnteredWaitingRoom (GC_FrontEnd _GC_FrontEnd)
	{
		FrontEndGC = _GC_FrontEnd;
		
	}
	public void Login (string _Username, string _Password)
	{
		AccountManager.LogIn(_Username,_Password);
		_AccountName = _Username;
	}
	
	public void Register (string _Username, string _Password)
	{
		AccountManager.RegisterAccount(_Username,_Password);
	}
	
	public void JoinServer(string _Host,int _Port)
	{
		//uLink.Network.Connect("25.150.103.245",_Port);
		uLink.Network.Connect("192.168.0.3",_Port);
	}

	
	private void uLobby_OnConnected()
	{
		print ("Connected to MasterServer");
	}
	
	private void OnAccountLoggedIn(Account _Account)
	{
		FrontEndGC.LogedIn();
		
		myAccount = _Account;
	}
	
	private void OnAccoutLogedOut(Account _Account)
	{
		
	}
	
	private void OnAccountLoginFail(string test,uLobby.AccountError _Error)
	{

	}
	
	private void OnAccountRegistered(Account _Account)
	{
		
	}

	
	private void OnRegisterFailed(string _Failure,uLobby.AccountError _Error)
	{
		
	}
	
	
	IEnumerator  LoadLevelInBackground()
	{
		loadingScreen = Instantiate(Resources.Load("LoadingScreen")) as GameObject;
		
		DontDestroyOnLoad(loadingScreen);
		
		yield return Application.LoadLevelAsync(1);
		
		Destroy(loadingScreen);
	}
	
	void uLink_OnConnectedToServer()
	{
			
		StartCoroutine(LoadLevelInBackground());
	}
	
	void uLink_OnDisconnectedFromServer(uLink.NetworkDisconnection mode)
	{
	

	}
	
	

	
	
}

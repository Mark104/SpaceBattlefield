using UnityEngine;
using uLobby;
using uGameDB;
using System.Collections;
using System.Collections.Generic;

public class GC_FrontEnd : uLink.MonoBehaviour {
	
	bool isLogedIn = false;
	
	AccountSession AS;
	
	FrontUIManager UI;
	
	IEnumerable<ServerInfo> servers;
	
	enum GameState
	{
		WAITINGFORPLAYERS,
		STARTING,
		PLAYING,
		RESULTS,
		IDLE
	} GameState	currentServerState = GameState.WAITINGFORPLAYERS;
		
		
	

	// Use this for initialization
	void Start () {
		
		UI = GameObject.FindGameObjectWithTag("UIManager").GetComponent<FrontUIManager>();
		
			
		uLobby.ServerRegistry.OnServerDataUpdated += OnServerDataUpdated;
		
		
		Lobby.AddListener(this);
		
		GameObject actSessionObj = GameObject.FindGameObjectWithTag("AccountSession");
		
		if(actSessionObj == null)
		{
			GameObject tmpObj = Instantiate(new GameObject()) as GameObject;
			
			tmpObj.tag = "AccountSession";
			tmpObj.name = "AccountSession";
				
			AS = tmpObj.AddComponent<AccountSession>();
			AS.EnteredWaitingRoom(this);
			
			uLink.NetworkViewID tmpId = new uLink.NetworkViewID(1);
			
			tmpObj.AddComponent<uLink.NetworkView>().SetManualViewID(1);
		}
		else
		{
			AS = actSessionObj.GetComponent<AccountSession>();
				
			UI._PnlLogin.SkipPanel();
			UI._PnlTop.SkipPanel();
			
		}
		
		
		
	
	}
	
	
	
	void OnDestroy() {
		
		Lobby.RemoveListener(this);
		uLobby.ServerRegistry.OnServerDataUpdated -= OnServerDataUpdated;
		
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
	
	void OnServerDataUpdated (ServerInfo _Server)
	{

		uLink.BitStream stream =_Server.data.ReadBitStream();
		
		short playerCount = stream.ReadInt16(); // Read the current player count

		byte gameState = stream.ReadByte(); // Read the current game state
		
		ServerStateChanged((GameState)gameState);
		
		print("Data updated with " + ((GameState)gameState).ToString());
	}
	
	void ServerStateChanged(GameState _NewState)
	{
		if(_NewState == GameState.PLAYING)
		{
			
			UI._PnlTop.SetMainTxt("Match in progress");
		}
		else if (_NewState == GameState.RESULTS)
		{
			UI._PnlTop.SetJoinButtonState(false);
			UI._PnlTop.SetMainTxt("Match ending in");
		}
		else if (_NewState == GameState.STARTING)
		{
			UI._PnlTop.SetJoinButtonState(true);
			UI._PnlTop.SetMainTxt("Match starting in");
		}
		else if (_NewState == GameState.WAITINGFORPLAYERS)
		{
			
			UI._PnlTop.SetMainTxt("Match waiting for players");
		}
		else if (_NewState == GameState.IDLE)
		{
			
			UI._PnlTop.SetMainTxt("Next round begins in");	
		}
		
		currentServerState = _NewState;
		
	}
	
	public void JoinServer()
	{
		
		
		
		servers = ServerRegistry.GetServers();
		
		foreach(ServerInfo _Info in servers)
		{
			print ("Attempting Connect with" + _Info.host + " on port " + _Info.port);
			AS.JoinServer(_Info.host,_Info.port);
		}
		
	}
	
}

using UnityEngine;
using uLink;
using uLobby;
using System.Collections.Generic;
using System.Collections;

public struct NetworkProfile {
	
	public uLink.NetworkPlayer player;
	public string name;
	public byte team;
	
}


public class GC_MainGameServer : uLink.MonoBehaviour {
	
	
	//Initilised values ----------------------------------------
	
	short minimumPlayers = 2;
	short startGameTime = 20; // In seconds
	short roundTime = 10; // In seconds
	short resultsTime = 2; // In seconds
	short idleTIme = 6; // In seconds
	
	
	public GameObject shipPlacement;
	
	string ServerName = "GameServer"; 
	
	//----------------------------------------------------------
	
	enum GameState
	{
		WAITINGFORPLAYERS,
		STARTING,
		PLAYING,
		RESULTS,
		IDLE
	} GameState	currentGameState = GameState.WAITINGFORPLAYERS;
	
	short playerCount = 0;
	
	float currentGameTimer = 0; // In seconds
	
	float currentStartingTimer = 0; // in seconds
	
	float currenResultsTimer = 0; // In seconds
	
	bool serverDetailsNeedUpdating = false;
	
	float serverUpdateInterval = 2;
	
	//Revised code above this
	
	double timeTillStart;
	
	Dictionary<int,NetworkProfile> unassignedPlayers = new Dictionary<int, NetworkProfile>();
	
	Dictionary<int,NetworkProfile> redTeam = new Dictionary<int, NetworkProfile>();
	
	Dictionary<int,NetworkProfile> blueTeam = new Dictionary<int, NetworkProfile>();
	
	Dictionary<int,NetworkProfile> greenTeam = new Dictionary<int, NetworkProfile>();
	
	Dictionary<int,NetworkProfile> playerList = new Dictionary<int, NetworkProfile>();
	
	Dictionary<int,Vector3> playerSpawnBuffer = new Dictionary<int, Vector3>();
	
	List<GameObject> placements = new List<GameObject>();
	
	
	
	void UpdateServerListing () {
		
		
	}
	
	void ClearTeamsAndRestart()
	{
		List<string> unassignedList = new List<string>();
		List<string> redList = new List<string>();
		List<string> blueList = new List<string>();
		List<string> greenList = new List<string>();
		
		List<int> keys = new List<int>(playerList.Keys);
		
		foreach(int id in keys)
		{
			NetworkProfile tmpProf  = playerList[id];
			tmpProf.team = 0;
			unassignedList.Add(tmpProf.name);
			playerList[id] = tmpProf;
			
			
		}
		
		networkView.RPC("ServerTeams",uLink.RPCMode.Others,1,unassignedList.ToArray(),redList.ToArray(),blueList.ToArray(),greenList.ToArray());
	}
	
	// Use this for initialization
	void Start () {
		
		Application.runInBackground = true;
		
		Lobby.AddListener(this);
	
		uLobby.LobbyConnectionError handle = Lobby.ConnectAsServer("ec2-54-229-103-211.eu-west-1.compute.amazonaws.com",7050);
		SendDebugInfo("MasterServer connect state " + handle.ToString());
	
		playerCount = 3;
	}
	
	void OnDestroy() {
		
		Lobby.RPC("RemoveServerFromDebug",LobbyPeer.lobby,networkView.owner.id);
		
	}
	
	void SwitchState(GameState _SwitchedState)
	{
		if(_SwitchedState == GameState.WAITINGFORPLAYERS) // We must be restarting the game
		{
			
		}
		else if (_SwitchedState == GameState.STARTING) // We must be starting
		{
			currentGameTimer = (short)startGameTime;
			
		}
		else if (_SwitchedState == GameState.PLAYING) // we must have started
		{
			currentGameTimer = (short)roundTime;
			SpawnShips();
			
			foreach(GameObject _Ship in placements)
			{
				
				uLink.Network.Destroy(	_Ship);
				
			}
			
			placements.Clear();
			//currentGameTimer *= 60;
			
		}
		else if (_SwitchedState == GameState.RESULTS) // we must have ended game, show results
		{
			currentGameTimer = (short)resultsTime;
			
		}
		else if (_SwitchedState == GameState.IDLE) // Match is over, server going into idle
		{
			currentGameTimer = (short)idleTIme;
			
		}
		
		currentGameState = _SwitchedState;
		

		networkView.RPC("ServerStateChanged",uLink.RPCMode.Others,(byte)currentGameState);
		
		
		serverDetailsNeedUpdating = true;
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if(currentGameState == GameState.PLAYING)
		{
			if(currentGameTimer <= 0)
			{
				SwitchState(GameState.RESULTS);
			}
			else
			{
				currentGameTimer -= Time.deltaTime;
			}
		}
		else if (currentGameState == GameState.IDLE)
		{
			if(currentGameTimer <= 0)
			{
				SwitchState(GameState.STARTING);
			}
			else
			{
				currentGameTimer -= Time.deltaTime;
			}
		}
		else if (currentGameState == GameState.STARTING)
		{
			if(currentGameTimer <= 0)
			{
				SwitchState(GameState.PLAYING);
			}
			else
			{
				currentGameTimer -= Time.deltaTime;
			}
		}
		else if (currentGameState == GameState.RESULTS)
		{
			if(currentGameTimer <= 0)
			{
				if(playerCount > minimumPlayers)
				{
					ClearTeamsAndRestart();
					SwitchState(GameState.IDLE);
				}
				else
				{
					SwitchState(GameState.WAITINGFORPLAYERS);
				}
			}
			else
			{
				currentGameTimer -= Time.deltaTime;
			}
			
		}
		else if (currentGameState == GameState.WAITINGFORPLAYERS)
		{
			if(playerCount >= minimumPlayers)
			{
				SwitchState(GameState.STARTING);
			}
		}
		
		if(serverDetailsNeedUpdating)
		{
			if(serverUpdateInterval <= 0)
			{
				//ServerRegistry.UpdateServerData(playerCount,(int)currentGameState);
				ServerRegistry.UpdateServerData(playerCount,(byte)currentGameState);
				serverUpdateInterval = 2;
			
			}
			else
			{
				serverUpdateInterval -= Time.deltaTime;
			}

		}
		

	
	}
	
	public void RespawnPlayer(uLink.NetworkPlayer player)
	{
		
		GameObject tmp = uLink.Network.Instantiate(player,"ClientUnpacker","ClientUnpacker","ServerUnpacker",new Vector3(0,0,-250),Quaternion.identity,5);
		tmp.name = "" + player.id;
		
	}
	
	public void SendDebugInfo(string _Message)
	{
		//print ("Sent Message");
		//Lobby.RPC("PassOnDebugInfo",LobbyPeer.lobby,_Message,networkView.owner.id);
	}
	
	void SpawnShips()
	{
		foreach(KeyValuePair<int,Vector3> _value in playerSpawnBuffer)
		{
			int tempKey = _value.Key;
			Quaternion rotation = Quaternion.identity;
			if(playerList[tempKey].team == 1)
			{
				rotation = Quaternion.Euler(new Vector3(0,90,0));
			}
			else if (playerList[tempKey].team == 2)
			{
				rotation = Quaternion.Euler(new Vector3(0,-90,0));
			}
			else if (playerList[tempKey].team == 3)
			{
				rotation = Quaternion.Euler(new Vector3(0,0,0));
			}
			uLink.Network.Instantiate(playerList[tempKey].player,"otherShip","ownerShip","serverShip",_value.Value,rotation,0,playerList[tempKey].team);
			
		}
		
		playerSpawnBuffer.Clear();
	}
	
	
	[RPC]
	void SpawnShip(Vector3 _Position,uLink.NetworkMessageInfo _Info)
	{
		
		if(currentGameState == GameState.STARTING)
		{
			playerSpawnBuffer.Add(_Info.sender.id,_Position);
			
			Quaternion rotation = Quaternion.identity;
			if(playerList[_Info.sender.id].team == 1)
			{
				rotation = Quaternion.Euler(new Vector3(0,90,0));
			}
			else if (playerList[_Info.sender.id].team == 2)
			{
				rotation = Quaternion.Euler(new Vector3(0,-90,0));
			}
			else if (playerList[_Info.sender.id].team == 3)
			{
				rotation = Quaternion.Euler(new Vector3(0,0,0));
			}
			
			GameObject tmp = uLink.Network.Instantiate(networkView.owner,"shipPlacement","shipPlacement","shipPlacement",_Position,rotation,0);
			placements.Add(tmp);
			
		}
		else if (currentGameState == GameState.PLAYING)
		{
			Quaternion rotation = Quaternion.identity;
			if(playerList[_Info.sender.id].team == 1)
			{
				rotation = Quaternion.Euler(new Vector3(0,90,0));
			}
			else if (playerList[_Info.sender.id].team == 2)
			{
				rotation = Quaternion.Euler(new Vector3(0,-90,0));
			}
			else if (playerList[_Info.sender.id].team == 3)
			{
				rotation = Quaternion.Euler(new Vector3(0,0,0));
			}
			uLink.Network.Instantiate(_Info.sender,"otherShip","ownerShip","serverShip",_Position,rotation,0,playerList[_Info.sender.id].team);
		}
	}
	
	
	[RPC]
	void HostGame()
	{

		SendDebugInfo("Game Started");
			
		networkView.RPC("StartGame",uLink.RPCMode.Others);
		
		timeTillStart = uLink.Network.time + 5;
	

	}
	
	[RPC]	
	void UserConnected (string _Name,uLink.NetworkMessageInfo _Info)
	{	
		
		print ("User" + _Name + " connected");
		NetworkProfile tempProf = new NetworkProfile();

		tempProf.name = _Name;
		
		tempProf.player = _Info.sender;
		
		tempProf.team = 0;
		
		playerList.Add(_Info.sender.id,tempProf);
		
		List<string> unassignedList = new List<string>();
		List<string> redList = new List<string>();
		List<string> blueList = new List<string>();
		List<string> greenList = new List<string>();
		
		foreach(KeyValuePair<int,NetworkProfile> key in playerList)
		{
			switch(key.Value.team)
			{
				case 0 :
					
					unassignedList.Add(key.Value.name);
				
					break;
					
				case 1 :
				
					redList.Add(key.Value.name);
				
					break;
					
				case 2 :
					
					blueList.Add(key.Value.name);
				
					break;
					
				case 3 :
				
					greenList.Add(key.Value.name);
					
					break;
			}
			
		}
		
		networkView.RPC("ServerStatus",_Info.sender,(short)currentGameState,currentGameTimer,startGameTime,roundTime,resultsTime);
		
		networkView.RPC("ServerTeams",_Info.sender,1,unassignedList.ToArray(),redList.ToArray(),blueList.ToArray(),greenList.ToArray());
	}
	
	[RPC]
	void AddToRed (uLink.NetworkMessageInfo _Info)
	{
		byte currentTeam = 1;
		
		NetworkProfile tmpProf = playerList[_Info.sender.id];
		
		byte lastTeam = tmpProf.team;
		
		tmpProf.team = 1;
		
		playerList[_Info.sender.id] = tmpProf;
		
		networkView.RPC("TeamSet",_Info.sender,currentTeam);
	
		//networkView.RPC("UpdatePlayerList",uLink.RPCMode.Others,currentTeam,playerList[_Info.sender.id].name,lastTeam);
		
		SendDebugInfo(playerList[_Info.sender.id].name + " added to red, moved from " + lastTeam);
	}
	
	[RPC]
	void AddToBlue (uLink.NetworkMessageInfo _Info)
	{
		byte currentTeam = 2;
		
		NetworkProfile tmpProf = playerList[_Info.sender.id];
		
		byte lastTeam = tmpProf.team;
		
		tmpProf.team = 2;
		
		playerList[_Info.sender.id] = tmpProf;
		
		networkView.RPC("TeamSet",_Info.sender,currentTeam);
	
		//networkView.RPC("UpdatePlayerList",uLink.RPCMode.Others,currentTeam,playerList[_Info.sender.id].name,lastTeam);
		
		SendDebugInfo(playerList[_Info.sender.id].name + " added to red, moved from " + lastTeam);
	}
	
	[RPC]
	void AddToGreen (uLink.NetworkMessageInfo _Info)
	{
		byte currentTeam = 3;
		
		NetworkProfile tmpProf = playerList[_Info.sender.id];
		
		byte lastTeam = tmpProf.team;
		
		tmpProf.team = 3;
		
		playerList[_Info.sender.id] = tmpProf;
		
		networkView.RPC("TeamSet",_Info.sender,currentTeam);
	
		//networkView.RPC("UpdatePlayerList",uLink.RPCMode.Others,currentTeam,playerList[_Info.sender.id].name,lastTeam);
		
		SendDebugInfo(playerList[_Info.sender.id].name + " added to red, moved from " + lastTeam);
	}
	
	void uLink_OnPlayerConnected(uLink.NetworkPlayer player) {
		
		playerCount++; //Player count goes up by one
		
		print ("Player " + player + " Connceted");
			
		serverDetailsNeedUpdating = true;
	}
	
	void uLink_OnPlayerDisconnected(uLink.NetworkPlayer player) {
		
		playerCount--; //Player count goes down by one
		
		playerList.Remove(player.id);
		
		networkView.RPC("RemovePlayerFromList",uLink.RPCMode.Others,player.id);
		
		serverDetailsNeedUpdating = true;
	}
	
	void uLink_OnConnectedToServer()
	{
		
		
	}
	private void uLobby_OnConnected()
	{
		print ("Connected to masterserver");
	
		ServerRegistry.AddServer("25.150.103.245",6050,ServerName,playerCount,(short)currentGameState);//,playerCount,(int)currentGameState);
		//ServerRegistry.AddServer(6050,ServerName,playerCount,(int)currentGameState);//,playerCount,(int)currentGameState);
	
	}
	
	private void uLobby_OnServerAdded(ServerInfo server)
	{
		SendDebugInfo("Server Connected " + server.data.ToString());
	}
}

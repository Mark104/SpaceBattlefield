using UnityEngine;
using System.Collections;

public class GC_InGameController : uLink.MonoBehaviour {
	
	AccountSession AS;
	
	GameUIManager UI;
	
	double currentTimer;
	
	short startingTime;
	short roundTime;
	short resultsTime;
	
	enum GameState
	{
		WAITINGFORPLAYERS,
		STARTING,
		PLAYING,
		RESULTS,
		IDLE
	} GameState	currentServerState = GameState.WAITINGFORPLAYERS;
	
	enum PlayerState
	{
		TEAMSELECT,
		TEAMASSIGNED,
		AWAITINGDEPLOYMENT,
		DEPLOYED,
	} PlayerState	currentPlayerState = PlayerState.TEAMSELECT;
	

	byte playerTeam = 4; // 0 is unassigned / 1 is red / 2 is green / 3 is red // 4 is noteam
	
	
	void PlayerStateChange(PlayerState _NewState)
	{
		if(_NewState == PlayerState.TEAMSELECT)
		{
		
			
		}
		else if (_NewState == PlayerState.TEAMASSIGNED)
		{
			
			
		}
		else if (_NewState == PlayerState.AWAITINGDEPLOYMENT)
		{
			
		}
		else if (_NewState == PlayerState.DEPLOYED)
		{
			
			
		}
		currentPlayerState = _NewState;
		
	}
	
	void StateChanged(GameState _NewState)
	{
		if(_NewState == GameState.PLAYING)
		{
		
			
		}
		else if (_NewState == GameState.RESULTS)
		{
			
			
		}
		else if (_NewState == GameState.STARTING)
		{
			
		}
		else if (_NewState == GameState.WAITINGFORPLAYERS)
		{
			
			
		}
		else if (_NewState == GameState.IDLE)
		{
			uLink.Network.Disconnect();
			Application.LoadLevel(0);
				
		}
		
		currentServerState = _NewState;	
		
		print ("Server changed to " + currentServerState);
	}
	
	void Awake () {
		
		
		AS = GameObject.FindGameObjectWithTag("AccountSession").GetComponent<AccountSession>();
		UI = GameObject.FindGameObjectWithTag("UIManager").GetComponent<GameUIManager>();
	}
	
	void Start () {
			
		networkView.RPC("UserConnected",uLink.RPCMode.Server,AS._AccountName);
		
	}
	
	public void JoinAttempt (byte _ID)
	{
		if(_ID == 0) // is red
		{
			networkView.RPC("AddToRed",uLink.RPCMode.Server);
		}
		else if (_ID == 1) // is blue
		{
			networkView.RPC("AddToBlue",uLink.RPCMode.Server);
		}
		else if (_ID == 2) // is green
		{
			networkView.RPC("AddToGreen",uLink.RPCMode.Server);
		}
	}
	
	[RPC]
	void TeamSet(byte _ID)
	{
		playerTeam = _ID;
		PlayerStateChange(PlayerState.TEAMASSIGNED);
	}
	
	[RPC]
	void ServerStatus (short _ServerState,float _CurrentTime,short _StartingTime,short _RoundTime,short _ResultsTime,uLink.NetworkMessageInfo _Info)
	{
		//print (_info.timestamp);
	
		currentTimer = (double)_CurrentTime + (uLink.Network.time - _Info.timestamp);
		
		//print ("Got unassigned players " + _UnassignedPlayers[0]);
	
		
		startingTime = _StartingTime;
		roundTime = _RoundTime;
		resultsTime = _ResultsTime;
		
		
		StateChanged((GameState)_ServerState);
	
	}
	
	[RPC]
	void ServerStateChanged (byte _ServerState,uLink.NetworkMessageInfo _Info)
	{
		print ("Got server changed rpc");
		StateChanged((GameState)_ServerState);
		
		if(currentServerState == GameState.STARTING)
		{
			//UI._GameUIPanel.GameStatus.text = "Till match starts";
			currentTimer = startingTime - (uLink.Network.time - _Info.timestamp);
		}
		else if (currentServerState == GameState.PLAYING)
		{
			//UI._GameUIPanel.GameStatus.text = "Till match ends";
			currentTimer = roundTime - (uLink.Network.time - _Info.timestamp);
		}
		else if (currentServerState == GameState.RESULTS)
		{
			//UI._GameUIPanel.GameStatus.text = "Till next round";
			currentTimer = resultsTime - (uLink.Network.time - _Info.timestamp);
		}
		
		
	}
	
	
	
}

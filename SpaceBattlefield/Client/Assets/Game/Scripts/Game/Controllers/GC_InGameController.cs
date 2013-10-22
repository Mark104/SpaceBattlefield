using UnityEngine;
using System.Collections;

public class GC_InGameController : uLink.MonoBehaviour {
	
	AccountSession AS;
	
	GameUIManager UI;
	
	double currentTimer;
	
	short startingTime;
	short roundTime;
	short resultsTime;
	
	private GameObject placement;
	
	bool setupFinished = false;
	
	float botReactionTimer = 0;
	
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
		SHIPASSIGNMENT,
		AWAITINGDEPLOYMENT,
		DEPLOYED,
	} PlayerState	currentPlayerState = PlayerState.TEAMSELECT;
	

	byte playerTeam = 4; // 0 is unassigned / 1 is red / 2 is green / 3 is red // 4 is noteam
	
	public void Respawn()
	{
		
		print ("respawning!");
		
		PlayerStateChange(PlayerState.AWAITINGDEPLOYMENT);
		
		if(playerTeam == 1)
		{
			Camera.main.transform.position = new Vector3(-50,20,0); // move to red spawn
			
		}
		else if (playerTeam == 2)
		{
			Camera.main.transform.position = new Vector3(50,20,0); // move to green spawn
			
		}
		else if (playerTeam == 3)
		{
			
			Camera.main.transform.position = new Vector3(0,20,-50); // move to blue spawn
			
		}
	}
	
	public void AttemptShipSelection(byte _ID)
	{
		networkView.RPC("ShipSelection",uLink.RPCMode.Server,_ID);
		UI._DevShipPicker.ChangeHideState();
	}
	
	public void Initalize()
	{
		print ("Sending connect message with name " + AS._AccountName);
		networkView.RPC("UserConnected",uLink.RPCMode.Server,AS._AccountName);
		setupFinished = true;
		
		print ("Called");
		botReactionTimer= 0;
	}
	
	void PlayerStateChange(PlayerState _NewState)
	{
		if(_NewState == PlayerState.TEAMSELECT)
		{
		
			
		}
		else if (_NewState == PlayerState.TEAMASSIGNED)
		{
			
			
		}
		else if (_NewState == PlayerState.SHIPASSIGNMENT)
		{
			UI._DevShipPicker.ChangeHideState();
		}
		else if (_NewState == PlayerState.AWAITINGDEPLOYMENT)
		{
			
		}
		else if (_NewState == PlayerState.DEPLOYED)
		{
			
			
		}
		
		print ("State set to " + _NewState);
		currentPlayerState = _NewState;
		
	}
	
	void StateChanged(GameState _NewState)
	{
		if(_NewState == GameState.PLAYING)
		{
			UI._GameUI.txtStatus.text = "Till match ends";
			
		}
		else if (_NewState == GameState.RESULTS)
		{
			UI._GameUI.txtStatus.text = "Till match closes";
			
		}
		else if (_NewState == GameState.STARTING)
		{
			UI._GameUI.txtStatus.text = "Till match starts";
			
			
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
			
		
		
	}
	
	void Update () {
		
		botReactionTimer+= Time.deltaTime;
		
		if(!setupFinished)
		{
			
		}
		else
		{
			if(AS.isBot && botReactionTimer > 2)
			{
				if(currentPlayerState == PlayerState.TEAMSELECT)
				{
					
					JoinAttempt((byte)Random.Range(1,3));
					botReactionTimer = 0;
					
				}	
			}
		}
		
		if(currentPlayerState == PlayerState.AWAITINGDEPLOYMENT)
		{
			if(AS.isBot)
			{
				RaycastHit hit;
				
				Ray ray = Camera.main.camera.ScreenPointToRay(new Vector3(Screen.width / 2 + Random.Range(-50,50), Screen.height / 2 + Random.Range(-50,50), 0));
				
				if(Physics.Raycast(ray, out hit))
				{
					if (hit.collider.gameObject.CompareTag("DeploymentArea"))
					{
						Vector3 twoPointVector = new Vector3(hit.point.x,0,hit.point.z);
						networkView.RPC("SpawnShip",uLink.RPCMode.Server,twoPointVector);
						PlayerStateChange(PlayerState.DEPLOYED);
					}
	
				}
				
			}
		
			if(Input.GetMouseButtonDown(0))
			{
				RaycastHit hit;
				Ray ray = Camera.main.camera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
				
				Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
				
				if(Physics.Raycast(ray, out hit))
				{
					if (hit.collider.gameObject.CompareTag("DeploymentArea"))
					{
						Vector3 twoPointVector = new Vector3(hit.point.x,0,hit.point.z);
						networkView.RPC("SpawnShip",uLink.RPCMode.Server,twoPointVector);
						PlayerStateChange(PlayerState.DEPLOYED);
					}
	
				}
			}
		}
		
		currentTimer-= Time.deltaTime;
		
		if(currentTimer < 0)
			currentTimer =0;
		
		UI._GameUI.txtTimer.text = "" + (float)currentTimer;

	}
	
	public void JoinAttempt (byte _ID)
	{
		if(currentPlayerState == PlayerState.TEAMSELECT)
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
	}
	
	[RPC]
	void ShipSet(byte _ID)
	{
		print ("Ship set at " + _ID);
		if(_ID == 0)
		{
			
		}else if (_ID == 1)
		{
			placement = Resources.Load("PlacementObj/Fighters/claw") as GameObject;
			
			
		}else if (_ID == 2)
		{
			
			
		}else if (_ID == 3)
		{
			
			
		}else if (_ID == 4)
		{
			
		}else if (_ID == 5)
		{
			
			
		}else if (_ID == 6)
		{
			
			
		}else if (_ID == 7)
		{
			
		}
		else if (_ID == 8)
		{
			placement = Resources.Load("PlacementObj/Battleships/victus") as GameObject;
			
		}
		
		PlayerStateChange(PlayerState.AWAITINGDEPLOYMENT);
	}
	
	[RPC]
	void TeamSet(byte _ID)
	{
		
		AS.currentTeam = _ID;
		
		print ("Team set to " + _ID);
		playerTeam = _ID;
		PlayerStateChange(PlayerState.SHIPASSIGNMENT);
		
		UI._GameTeams.ChangeHideState();
		
		if(playerTeam == 1)
		{
			Camera.main.transform.position = new Vector3(-50,20,0); // move to red spawn
			
		}
		else if (playerTeam == 2)
		{
			Camera.main.transform.position = new Vector3(50,20,0); // move to green spawn
			
		}
		else if (playerTeam == 3)
		{
			
			Camera.main.transform.position = new Vector3(0,20,50); // move to blue spawn
			
		}
		
		
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
		print ("Got server changed rpc " + _ServerState);
		StateChanged((GameState)_ServerState);
		
		if(currentServerState == GameState.STARTING)
		{
			currentTimer = startingTime - (uLink.Network.time - _Info.timestamp);
		}
		else if (currentServerState == GameState.PLAYING)
		{
			currentTimer = roundTime - (uLink.Network.time - _Info.timestamp);
		}
		else if (currentServerState == GameState.RESULTS)
		{
			UI._GameTeams.ChangeHideState();
			currentTimer = resultsTime - (uLink.Network.time - _Info.timestamp);
		}
		
		
	}
	
	
	
}

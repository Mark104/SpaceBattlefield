using UnityEngine;
using System.Collections;

public class GameUIManager : MonoBehaviour {
	
	public pnlGameUI _GameUI;
	public pnlGameInfo _GameInfo;
	public pnlShipSelection _ShipSelection;
	
	public GameObject _Background;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	public void EnterGameUIMode() {
		
		print ("Destroying background");
		_GameInfo.ChangeHideState();
		_ShipSelection.ChangeHideState();
		Destroy(_Background);
		
	}
}

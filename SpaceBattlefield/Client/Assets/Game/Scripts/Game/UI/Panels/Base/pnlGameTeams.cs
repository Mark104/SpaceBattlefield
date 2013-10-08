using UnityEngine;
using System.Collections;

public class pnlGameTeams : UIPanelController {

	void Awake () {
		
		hidePosition = new Vector3(0,-1400,0);
		showPosition = new Vector3(0,0,0);
		currentlyHidden = false;
		
	}
	
}

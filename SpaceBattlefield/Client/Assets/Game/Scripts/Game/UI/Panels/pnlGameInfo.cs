using UnityEngine;
using System.Collections;

public class pnlGameInfo : UIPanelController {

	void Awake () {
		
		hidePosition = new Vector3(-1000,0,0);
		showPosition = new Vector3(-20,0,0);
		currentlyHidden = true;
		
	}
}

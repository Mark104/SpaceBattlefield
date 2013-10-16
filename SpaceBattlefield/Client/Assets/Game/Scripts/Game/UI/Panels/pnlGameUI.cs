using UnityEngine;
using System.Collections;

public class pnlGameUI : UIPanelController {
	
	public UILabel txtStatus;
	public UILabel txtTimer;

	void Awake () {
		
		hidePosition = new Vector3(0,200,0);
		showPosition = new Vector3(0,0,0);
		currentlyHidden = false;
		
	}
}

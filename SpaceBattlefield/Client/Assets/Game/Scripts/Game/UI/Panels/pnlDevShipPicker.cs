using UnityEngine;
using System.Collections;

public class pnlDevShipPicker : UIPanelController {

	void Awake () {
		
		hidePosition = new Vector3(-1200,0,0);
		showPosition = new Vector3(-800,0,0);
		currentlyHidden = true;
		
	}
}

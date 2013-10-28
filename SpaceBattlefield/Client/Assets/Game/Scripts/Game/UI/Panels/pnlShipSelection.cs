using UnityEngine;
using System.Collections;

public class pnlShipSelection : UIPanelController {
	
	public UILabel TeamTitle;
	public UITiledSprite TeamBack;

	void Awake () {
		
		hidePosition = new Vector3(600,0,0);
		showPosition = new Vector3(0,0,0);
		currentlyHidden = true;
		
	}
	
	
	public void TeamIs(byte _Team)
	{
		Color teamBackCol = Color.white;
		if(_Team == 1)
		{
			TeamTitle.text = "You are RED";
			teamBackCol = Color.red;
			teamBackCol.a = 32;
		}else if (_Team == 2)
		{
			TeamTitle.text = "You are BLUE";
			teamBackCol = Color.blue;
			teamBackCol.a = 32;
		}else if (_Team == 3)
		{
			TeamTitle.text = "You are GREEN";
			teamBackCol = Color.green;
			teamBackCol.a = 32;
		}
		
		TeamBack.color = teamBackCol;
	}
}

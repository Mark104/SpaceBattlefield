using UnityEngine;
using System.Collections;

public class PnlTop : UIPanelController {
	
	
	public UILabel _TxtTimer;
	
	public UILabel _TxtPlyrCount;

	void Awake () {
		
		hidePosition = new Vector3(0,20,0);
		showPosition = new Vector3(0,-300,0);
		currentlyHidden = true;
		
	}
	
	public void SetMainTxt(string _Txt)
	{
			
		transform.Find("txtMain").GetComponent<UILabel>().text = _Txt;
	}
	
	public void SetJoinButtonState(bool _State)
	{
		if(_State)
		{
			
			
			transform.Find("btnJoin").gameObject.collider.enabled = true;
			
			
			transform.Find("btnJoin/Label").GetComponent<UILabel>().color = Color.white;
			
		}
		else
		{
			transform.Find("btnJoin").gameObject.collider.enabled = false;
			
			
			transform.Find("btnJoin/Label").GetComponent<UILabel>().color = Color.gray;
			
		}
			
	}
}

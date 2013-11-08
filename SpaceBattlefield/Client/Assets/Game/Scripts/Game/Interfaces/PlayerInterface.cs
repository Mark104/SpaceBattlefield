using UnityEngine;
using System.Collections;

public class PlayerInterface : InputInterface {

	// Use this for initialization
	void Awake () {
		
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(SM == null)
		{
			SM = GetComponent<ShipMovement>();	
		}
		
		if(IsActive)
		{
			Vector2 mouseDirection = new Vector2(Input.mousePosition.x - (Screen.width * 0.5f),Input.mousePosition.y - (Screen.height * 0.5f));
			mouseDirection.Normalize();
			SM.ProvideInput(mouseDirection,Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
		}
		
	}
}

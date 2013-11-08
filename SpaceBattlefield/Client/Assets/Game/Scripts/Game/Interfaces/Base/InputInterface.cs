using UnityEngine;
using System.Collections;

public class InputInterface : MonoBehaviour {
	
	protected ShipMovement SM;
	
	public bool IsActive = true;

	// Use this for initialization
	void Awake () {
	
		SM = GetComponent<ShipMovement>();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

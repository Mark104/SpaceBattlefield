using UnityEngine;
using System.Collections;

public class UIUpdater : MonoBehaviour {
	
	UIWidget myself;

	// Use this for initialization
	void Start () {
		
		myself = GetComponent<UIWidget>();
	
	}
	
	// Update is called once per frame
	void Update () {
		
		myself.MarkAsChanged();
	
	}
}

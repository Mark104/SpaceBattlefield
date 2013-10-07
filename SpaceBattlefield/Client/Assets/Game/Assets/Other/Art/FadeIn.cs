using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour {
	
	BloomAndLensFlares BALF;

	// Use this for initialization
	void Start () {
		
		
		Application.runInBackground = true;
		BALF = GetComponent<BloomAndLensFlares>();
	
	}
	
	// Update is called once per frame
	void Update () {
		
	
		BALF.bloomThreshhold =  Mathf.Lerp(0,0.2f,Time.time * 0.5f);
		BALF.bloomIntensity =  Mathf.Lerp(5,0.25f,Time.time * 0.5f);

	
	}
}

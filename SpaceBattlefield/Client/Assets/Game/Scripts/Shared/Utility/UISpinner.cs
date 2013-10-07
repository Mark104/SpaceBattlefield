using UnityEngine;
using System.Collections;

public class UISpinner : MonoBehaviour {
	
	public float animationSpeed = 1;

	// Use this for initialization
	void Start () {
		
		animation[animation.clip.name].speed = animationSpeed;
	
	}
	
}

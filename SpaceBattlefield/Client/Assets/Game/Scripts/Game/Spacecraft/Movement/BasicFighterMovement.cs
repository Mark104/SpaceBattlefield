using UnityEngine;
using System.Collections;

public class BasicFighterMovement : MonoBehaviour {
	
	
	float currentThrust;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if (currentThrust < 10)
		{
			
			currentThrust += Input.GetAxis("Vertical");
			
			if(currentThrust < 0)
			{
				currentThrust = 0;
			}
		}
		
		Vector3 relativeVector = new Vector3(Input.mousePosition.x - (Screen.width * 0.5f),0,Input.mousePosition.y - (Screen.height * 0.5f));
		relativeVector.Normalize();

		rigidbody.AddForce((transform.forward * currentThrust) * Time.deltaTime,ForceMode.Acceleration);
		
		transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(relativeVector,transform.up),Time.deltaTime);
	
	}
	
	void LateUpdate () {
		
		
		
		Camera.main.transform.position = transform.position + (Vector3.up * 8);
		
		
		
		
		
		
	}
}

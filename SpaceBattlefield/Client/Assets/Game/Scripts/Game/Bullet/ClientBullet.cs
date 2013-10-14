using UnityEngine;
using System.Collections;

public class ClientBullet : MonoBehaviour {
	
	public float life = 7;
	public int speed;
	float currentLife = 0;
	public byte team;

	// Use this for initialization
	void Start () {
	
		transform.rigidbody.velocity = transform.forward * speed;
	}
	
	// Update is called once per frame
	void Update () {
		
		currentLife += Time.deltaTime;
		
		if(currentLife > life)
		{
			Destroy(gameObject);	
		}
	
	}
	
}

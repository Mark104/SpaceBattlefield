using UnityEngine;
using System.Collections;

public class Armament : uLink.MonoBehaviour {
	
	public ServerFireController SFC;
	
	public int id = 0;
	
	public virtual void UpdateFacing (Vector3 _Pos) {	
		
			
	}
	
	public virtual void SpawnBullet (Vector2 _Direction,Vector2 _Position) {
		
		
		
	}
	
	public virtual string Status () {
			
		return "test";
		
	}

	// Use this for initialization
	public virtual void Start () {
		
		transform.root.gameObject.SendMessage("LinkGun",this);
		SFC = transform.root.GetComponent<ServerFireController>();
		
	}

}

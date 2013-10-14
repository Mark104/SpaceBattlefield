using UnityEngine;
using System.Collections;

public class Armament : MonoBehaviour {
	
	public ClientFireControl CFC;
	
	public int id = 0;
	
	public virtual bool Fire () {	
		
		return false;
	}
	
	public virtual void ProxyFire (short _Rotation,Vector2 _Position,byte _Team) {
	}
	
	public virtual void UpdateFacing (Vector3 _Pos) {	
		
			
	}
	
	public virtual string Status () {
			
		return "test";
		
	}

	// Use this for initialization
	public virtual void Start () {
		
		transform.root.gameObject.SendMessage("LinkGun",this);
		CFC = transform.root.GetComponent<ClientFireControl>();
		
	}

}

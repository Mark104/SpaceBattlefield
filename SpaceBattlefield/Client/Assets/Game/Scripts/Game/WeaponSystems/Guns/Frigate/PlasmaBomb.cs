using UnityEngine;
using System.Collections;

public class PlasmaBomb : Armament {
	
	public override void Start () {
	
		damage = 10;
		
		base.Start();
		
	}

	public override bool Fire () {
	
		return	base.Fire();
		
	}
	
	public override void ProxyFire (short _Rotation,Vector2 _Position,byte _Team) {
		
		
		base.ProxyFire(_Rotation,_Position,_Team);
		
	}
	
	
	public override string Status () {
		
		
		return "test";
		
	}
}

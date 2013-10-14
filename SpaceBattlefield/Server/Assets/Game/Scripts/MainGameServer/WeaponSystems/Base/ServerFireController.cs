using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ServerFireController : uLink.MonoBehaviour {
	
	protected Dictionary<byte,Armament> armamentList = new Dictionary<byte, Armament>();
	
	public void LinkGun (Armament _InLink)
	{
		armamentList.Add((byte)_InLink.id,_InLink);
		print ("Linked");
	}
	
}

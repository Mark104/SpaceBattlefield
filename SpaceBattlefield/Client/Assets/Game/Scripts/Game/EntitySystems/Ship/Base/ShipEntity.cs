using UnityEngine;
using uLink;
using System.Collections;

public class ShipEntity : uLink.MonoBehaviour {

	protected short health = 10;
	protected short maxHealth = 10;
	
	public byte shipTeam;
	
	
	void Awake (){
		
	}
	
	void uLink_OnNetworkInstantiate(uLink.NetworkMessageInfo info) {
		
		uLink.BitStream tmpStream = info.networkView.initialData.ReadBitStream();
		
		shipTeam = tmpStream.ReadByte();
		
		gameObject.name = "" + shipTeam;
		
		string playerName = tmpStream.ReadString();
		
		if(!networkView.isOwner)
		{
			GameObject tmp = Instantiate(Resources.Load("Overlay")) as GameObject;
			
	
			tmp.transform.Find("PlayerName").GetComponent<TextMesh>().text = playerName;
			tmp.transform.parent = gameObject.transform;
			tmp.transform.localPosition = Vector3.zero;
			
		}
	}
	
	void OnDestroy() {
		
		print ("I be dead laddie, respawning!");
		GameObject.FindGameObjectWithTag("GameController").GetComponent<GC_InGameController>().Respawn();
		
		
	}
	
	

}

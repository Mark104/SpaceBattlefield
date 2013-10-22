using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WSFrigateBomber : ClientFireControl {

	public override void PrimaryInput()
	{
		if(armamentList[0].Fire())
		{
			
			
		}
		if(armamentList[1].Fire())
		{
			
	
		}
	}
	
	public override void SecondaryInput()
	{
		if(armamentList[2].Fire())
		{
			
	
		}
	}
	
	public override void MousePositionUpdate(Vector3 WOrldSpacePosition)
	{
		
		
		
	}
}

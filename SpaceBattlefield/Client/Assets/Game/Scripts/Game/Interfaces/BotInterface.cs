using UnityEngine;
using System.Collections;

public class BotInterface : InputInterface {
	
	Vector2 startingScoutPos = Vector2.zero;
	
	enum StrategyState
	{
		SCOUTING,
		FIGHTING,
		HUTNING,
		RETREATING
	} StrategyState currentStrategyState;
	
	enum TacticalState
	{
		ATTACKRUN,
		VERINGOFF,
		DODGING
	} TacticalState currentTacticalState;
	
	enum Personality
	{
		AGGRESSIVE,
		DEFENSIVE
	} Personality botPersonality;
	
	GameObject target;

	// Use this for initialization
	void Start () {
		
		botPersonality = Personality.AGGRESSIVE;
		BoxCollider tmpCol = gameObject.AddComponent<BoxCollider>();
		tmpCol.size = new Vector3(80,40,80);
		tmpCol.isTrigger = true;
		
		SM.DisableVisualRotation();
		
		currentStrategyState = StrategyState.SCOUTING;
	
	}
	
	void OnTriggerEnter(Collider other) {
		
		ShipEntity tmpEntity = other.GetComponent<ShipEntity>();
		
		if(tmpEntity != null)
		{
			//if (tmpEntity.shipTeam != GetComponent<ShipEntity>().shipTeam)
			//{
				if(target == null)
				{
					target = other.collider.gameObject;
					currentStrategyState = StrategyState.FIGHTING;
					print ("AFTER HIM");
				}
			//}
		}
		
	}
	
	void CloseToDest()
	{
		if(currentStrategyState == StrategyState.FIGHTING)
		{
			
			
		} else if (currentStrategyState == StrategyState.SCOUTING)
		{
			startingScoutPos = new Vector2(Random.Range(-50,50),Random.Range(-50,50));
			
			
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector2 targetDirection = Vector2.zero;
		
		if(currentStrategyState == StrategyState.FIGHTING)
		{
			targetDirection = new Vector2(target.transform.position.x - transform.position.x,target.transform.position.z - transform.position.z);
			
			
		}else if (currentStrategyState == StrategyState.SCOUTING)
		{
			targetDirection = new Vector2(startingScoutPos.x - transform.position.x,startingScoutPos.y - transform.position.z);
			
		}
		
		if(targetDirection.magnitude < 4)
		{
			CloseToDest();
			
		}
		else
		{
			targetDirection.Normalize();
		}
		
		SM.ProvideInput(targetDirection,1,1);
	}
}

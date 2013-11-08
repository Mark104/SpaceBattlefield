using UnityEngine;
using System.Collections;

public class ShipMovement : uLink.MonoBehaviour {

	public float acceleration = 0;
	public float maxSpeed = 0;
	public float rotationSpeed = 0;
	public float cameraDistance = 0;
	
	protected bool visualRotationAllowed = true;
	
	
	public virtual void ProvideInput(Vector2 _MouseInput,float _HorizontalInput,float _VerticalInput)
	{
		
		
		
	}
	
	public void DisableVisualRotation()
	{
		
		visualRotationAllowed = false;
	}
	
}

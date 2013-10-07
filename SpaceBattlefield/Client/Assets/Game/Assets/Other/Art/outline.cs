using UnityEngine;
using System.Collections;

public class outline : MonoBehaviour {
    void Update()
    {
		if(Camera.mainCamera!=null)
		{
       		//transform.LookAt(Camera.mainCamera.transform.position);
			
			transform.rotation = Camera.mainCamera.transform.rotation;
			float tmp = (Vector3.Distance(transform.position,Camera.mainCamera.transform.position)) * 0.03f;
			transform.localScale = new Vector3(tmp,tmp,tmp);
		
		}
	}
}

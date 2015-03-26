using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour 
{
    public Transform TargetMaker;
	
	// Update is called once per frame
	void Update () 
    {
        int button = 0;
        if(Input.GetMouseButtonDown(button))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray.origin,ray.direction,out hitInfo))
            {
                Vector3 targetPosition = hitInfo.point;
                TargetMaker.position = targetPosition;
            }
        }
	}

}

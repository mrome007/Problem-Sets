using UnityEngine;
using System.Collections;

public class PlayerTank : MonoBehaviour 
{
    public Transform TargetTransform;
    private float movementSpeed, rotSpeed;

	// Use this for initialization
	void Start ()
    {
        movementSpeed = 5.0f;
        rotSpeed = 2.0f;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    //stop once you've reached the target.
        if(Vector3.Distance(transform.position, TargetTransform.position) < 5.0f)
        {
            return;
        }

        //calculate direction from current position to target position.
        Vector3 tarPos = TargetTransform.position;
        tarPos.y = transform.position.y;
        Vector3 dirRot = tarPos - transform.position;

        //build a quaternion for this new rotation using look rotation.
        Quaternion tarRot = Quaternion.LookRotation(dirRot);

        //move and rotate the tank.
        transform.rotation = Quaternion.Slerp(transform.rotation, tarRot, rotSpeed * Time.deltaTime);
        transform.Translate(new Vector3(0f, 0f, movementSpeed * Time.deltaTime));
	}
}

using UnityEngine;
using System.Collections;

public class PlayerTankController : MonoBehaviour 
{
    public GameObject Bullet;

    private Transform turret;
    private Transform bulletSpawnPoint;
    private float curSpeed, targetSpeed, rotSpeed;
    private float turretRotSpeed = 10.0f;
    private float maxForwardSpeed = 20.0f;
    private float maxBackwardSpeed = -20.0f;

    //Bullet shooting rate
    protected float shootRate = 0.5f;
    protected float elapsedTime;

	// Use this for initialization
	void Start () 
    {
	    //Tank Settings
        elapsedTime = 0.0f;
        rotSpeed = 150.0f;
        turret = gameObject.transform.GetChild(0).transform;
        bulletSpawnPoint = turret.GetChild(0).transform;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    UpdateWeapon();
        UpdateControl();
	}

    void UpdateWeapon()
    {
        if(Input.GetMouseButton(0))
        {
            elapsedTime += Time.deltaTime;
           // Debug.Log(elapsedTime);
            if(elapsedTime >= shootRate)
            {
                elapsedTime = 0.0f;
                Instantiate(Bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            }
        }
    }

    void UpdateControl()
    {
        Plane playerPlane = new Plane(Vector3.up,
            transform.position + new Vector3(0.0f, 0.0f, 0.0f));

        //generates a ray from the cursor position.
        Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);

        //determine the point where the cursor ray intersects with the plane.
        float hitdist = 0.0f;

        //if the ray is parallel to the plane, raycast will return false.
        if(playerPlane.Raycast(raycast,out hitdist))
        {
            //get the point along the ray that hits the calculated distance.
            Vector3 rayHitPoint = raycast.GetPoint(hitdist);

            Quaternion targetRotatoin = Quaternion.LookRotation(rayHitPoint - transform.position);
            turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation,
                targetRotatoin, Time.deltaTime * turretRotSpeed);
        }

        if(Input.GetKey(KeyCode.W))
        {
            targetSpeed = maxForwardSpeed;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            targetSpeed = maxBackwardSpeed;
        }
        else
        {
            targetSpeed = 0.0f;
        }

        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0.0f, -rotSpeed * Time.deltaTime, 0.0f);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0.0f, rotSpeed * Time.deltaTime, 0.0f);
        }

        //determine current speed.
        curSpeed = Mathf.Lerp(curSpeed, targetSpeed, 7.0f * Time.deltaTime);
        transform.Translate(Vector3.forward * Time.deltaTime * curSpeed);
    }
}

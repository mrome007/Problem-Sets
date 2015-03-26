using UnityEngine;
using System.Collections;

public class Perspective : Sense
{
    public int FieldOfView = 35;
    public int ViewDistance = 100;

    private Transform playerTrans;
    private Vector3 rayDirection;

    protected override void Initialize()
    {
        //find player trans
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void UpdateSense()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= DetectionRate)
        {
            DetectAspect();
        }
    }
    

    //Detect perspective field of view for the AI Character
    void DetectAspect()
    {
        RaycastHit hit;
        //Direction from current position to player position
        rayDirection = playerTrans.position - transform.position;
        //Check the angle between the AI character's forward
        //vector and the direction vector between player and AI
        if ((Vector3.Angle(rayDirection, transform.forward)) < FieldOfView)
        {
            // Detect if player is within the field of view
            if (Physics.Raycast(transform.position, rayDirection, out hit, ViewDistance))
            {
                Aspect aspect = hit.collider.GetComponent<Aspect>();
                if (aspect != null)
                {
                    //Check the aspect
                    if (aspect.AspectName == AspectName)
                    {
                        print("Enemy Detected");
                        Wander.tarPos = hit.collider.gameObject.transform.position;
                    }
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if (!BDebug || playerTrans == null) return;
        Debug.DrawLine(transform.position, playerTrans.position, Color.
        red);
        Vector3 frontRayPoint = transform.position +
        (transform.forward * ViewDistance);
        //Approximate perspective visualization
        Vector3 leftRayPoint = frontRayPoint;
        leftRayPoint.x += FieldOfView * 0.5f;
        Vector3 rightRayPoint = frontRayPoint;
        rightRayPoint.x -= FieldOfView * 0.5f;
        Debug.DrawLine(transform.position, frontRayPoint, Color.green);
        Debug.DrawLine(transform.position, leftRayPoint, Color.green);
        Debug.DrawLine(transform.position, rightRayPoint, Color.green);
    }

}

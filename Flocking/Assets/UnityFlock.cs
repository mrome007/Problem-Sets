using UnityEngine;
using System.Collections;

public class UnityFlock : MonoBehaviour 
{
    public float MinSpeed = 2.0f;
    public float TurnSpeed = 1.0f;
    public float RandomFreq = 2.0f;
    public float RandomForce = 2.0f;

    public float Gravity = 2.0f;

    //alignment variables.
    public float ToOriginForce = 5.0f;
    public float ToOriginRange = 10.0f;

    //separation variables.
    public float AvoidanceRadius = 5.0f;
    public float AvoidanceForcee = 2.0f;

    //cohesion variables.
    public float FollowVelocity = 1.2f;
    public float FollowRadius = 4.0f;

    private Transform origin;
    private Vector3 velocity;
    private Vector3 normalizedVelocity;
    private Vector3 randomPush;
    private Vector3 originPush;
    private Transform[] objects;
    private UnityFlock[] otherFlocks;
    private Transform transformComponent;


	// Use this for initialization
	void Start () 
    {
        RandomFreq = 1.0f / RandomFreq;

        //assign parent to origin.
        origin = transform.parent;

        //flock transform.
        transformComponent = transform;

        Component[] tempFlocks = null;

        if(transform.parent)
        {
            tempFlocks = transform.parent.GetComponentsInChildren<UnityFlock>();
        }

        objects = new Transform[tempFlocks.Length];
        otherFlocks = new UnityFlock[tempFlocks.Length];

        for(int i = 0; i < tempFlocks.Length; i++)
        {
            objects[i] = tempFlocks[i].transform;
            otherFlocks[i] = (UnityFlock)tempFlocks[i];
        }

        transform.parent = null;
        StartCoroutine(UpdateRandom());
	}
	
	// Update is called once per frame
	void Update () 
    {
        float speed = velocity.magnitude;
        Vector3 avgVelocity = Vector3.zero;
        Vector3 avgPosition = Vector3.zero;
        float count = 0;
        float f = 0.0f;
        float d = 0.0f;
        Vector3 myPosition = transformComponent.position;
        Vector3 forceV;
        Vector3 toAvg;
        Vector3 wantedVel;

        for(int i = 0; i < objects.Length; i++)
        {
            Transform transform = objects[i];
            if(transform != transformComponent)
            {
                Vector3 otherPosition = transform.position;

                //average positio to calculate cohesion.
                avgPosition += otherPosition;
                count++;

                //directional vector from other flock to this flock.
                forceV = myPosition - otherPosition;
                d = forceV.magnitude;

                //Add push value if the magnitude, the length of the
                //vector, is less than followRadius to the leader
                if (d < FollowRadius)
                {
                    //calculate the velocity, the speed of the object, based
                    //on the avoidance distance between flocks if the
                    //current magnitude is less than the specified
                    //avoidance radius
                    if (d < AvoidanceRadius)
                    {
                        f = 1.0f - (d / AvoidanceRadius);
                        if (d > 0)
                        {
                            avgVelocity += (forceV / d) * f * AvoidanceForcee;
                        }
                    }
                    //just keep the current distance with the leader
                    f = d / FollowRadius;
                    UnityFlock otherSealgull = otherFlocks[i];
                    //we normalize the otherSealgull velocity vector to get
                    //the direction of movement, then we set a new velocity
                    avgVelocity += otherSealgull.normalizedVelocity * f * FollowVelocity;
                }
            }
        }

        if(count > 0)
        {
            avgVelocity /= count;
            toAvg = (avgPosition / count) - myPosition;
        }
        else
        {
            toAvg = Vector3.zero;
        }

        forceV = origin.position - myPosition;
        d = forceV.magnitude;
        f = d / ToOriginRange;

        if(d > 0)
        {
            originPush = (forceV / d) * f * ToOriginForce;
        }
        
        if(speed < MinSpeed && speed > 0)
        {
            velocity = (velocity / speed) * MinSpeed;
        }

        wantedVel = velocity;

        //Calculate final velocity
        wantedVel -= wantedVel * Time.deltaTime;
        wantedVel += randomPush * Time.deltaTime;
        wantedVel += originPush * Time.deltaTime;
        wantedVel += avgVelocity * Time.deltaTime;
        wantedVel += toAvg.normalized * Gravity * Time.deltaTime;
        //Final Velocity to rotate the flock into
        velocity = Vector3.RotateTowards(velocity, wantedVel, TurnSpeed * Time.deltaTime, 100.00f);
        transformComponent.rotation =
        Quaternion.LookRotation(velocity);
        //Move the flock based on the calculated velocity
        transformComponent.Translate(velocity * Time.deltaTime, Space.World);
        //normalise the velocity
        normalizedVelocity = velocity.normalized;
	}

    IEnumerator UpdateRandom()
    {
        while (true)
        {
            randomPush = Random.insideUnitSphere * RandomForce; //random vector3, within a sphere's radius.
            yield return new WaitForSeconds(RandomFreq + Random.Range(-RandomFreq/2.0f, RandomFreq/2.0f));
        }
    }
}

using UnityEngine;
using System.Collections;

public class UnityFlockController : MonoBehaviour 
{
    public Vector3 Offset;
    public Vector3 Bound;
    public float Speed = 5.0f;

    private Vector3 initialPosition;
    private Vector3 nextMovementPoint;


	// Use this for initialization
	void Start () 
    {
        initialPosition = transform.position;
        CalculateNextPoint();
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(nextMovementPoint - transform.position),
            1.0f * Time.deltaTime);
        if(Vector3.Distance(transform.position,nextMovementPoint) <= 10.0f)
        {
            CalculateNextPoint();
        }
	}

    void CalculateNextPoint()
    {
        float posX = Random.Range(initialPosition.x - Bound.x, initialPosition.x + Bound.x);
        float posY = Random.Range(initialPosition.y - Bound.y, initialPosition.y + Bound.y);
        float posZ = Random.Range(initialPosition.z - Bound.z, initialPosition.z + Bound.z);

        nextMovementPoint = initialPosition + new Vector3(posX, posY, posZ);
    }
}

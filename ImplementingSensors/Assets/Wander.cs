using UnityEngine;
using System.Collections;

public class Wander : MonoBehaviour
{
    public static Vector3 tarPos;
    private float movementSpeed = 2.5f;
    private float rotSpeed = 2.0f;
    private float minX, maxX, minZ, maxZ;
	// Use this for initialization
	void Start () 
    {
        minX = -35.0f;
        maxX = 35.0f;
        minZ = -35.0f;
        maxZ = 35.0f;

        GetNextPosition();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(Vector3.Distance(tarPos,transform.position) <= 5.0f)
        {
            GetNextPosition();
        }

        Quaternion tarRot = Quaternion.LookRotation(tarPos - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, tarRot, rotSpeed * Time.deltaTime);
        transform.Translate(new Vector3(0f, 0f, movementSpeed * Time.deltaTime));
	}

    void GetNextPosition()
    {
        tarPos = new Vector3(Random.Range(minX, maxX), -3.5f, Random.Range(minZ, maxZ));
    }
}

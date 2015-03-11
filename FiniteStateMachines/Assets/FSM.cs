using UnityEngine;
using System.Collections;

public class FSM : MonoBehaviour
{
    //Player Transform
    protected Transform playerTransform;

    //next destination position of the NPC tank.
    protected Vector3 destPos;

    //list of points for patrolling.
    protected GameObject[] pointList;

    //bullet shooting rate
    protected float shootRate;
    protected float elapsedTime;

    //Tank Turret
    public Transform turret { get; set; }
    public Transform bulletSpawnPoint { get; set; }

    protected virtual void Initialize() { }
    protected virtual void FSMUpdate() { }
    protected virtual void FSMFixedUpdate() { }
    
	// Use this for initialization
	void Start () 
    {
        Initialize();
	}
	
	// Update is called once per frame
	void Update () 
    {
        FSMUpdate();
	}

    void FixedUpdate()
    {
        FSMFixedUpdate();
    }
}

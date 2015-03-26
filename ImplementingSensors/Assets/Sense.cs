using UnityEngine;
using System.Collections;

public class Sense : MonoBehaviour 
{
    public bool BDebug = true;
    public Aspect.aspect AspectName = Aspect.aspect.Enemy;
    public float DetectionRate = 1.0f;

    protected float elapsedTime = 0.0f;
    protected virtual void Initialize() { }
    protected virtual void UpdateSense() { }

	// Use this for initialization
	void Start () 
    {
        elapsedTime = 0.0f;
        Initialize();
	}
	
	// Update is called once per frame
	void Update () 
    {
        UpdateSense();
	}
}

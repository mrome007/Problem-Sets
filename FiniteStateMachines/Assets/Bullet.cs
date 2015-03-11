using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
    public GameObject Explosion;
    public float Speed = 65.0f;
    public float LifeTime = 3.0f;
    public int Damage = 50;


	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, LifeTime);
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.position += transform.forward * Speed * Time.deltaTime;
	}

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Instantiate(Explosion, contact.point, Quaternion.identity);
        Destroy(gameObject);
    }
}

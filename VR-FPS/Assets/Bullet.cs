using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	public float speed = 1f;
	public Vector3 direction;
	private float lifeSpan = 2f;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += direction * speed * Time.deltaTime;
		lifeSpan -= Time.deltaTime;
		if (lifeSpan <= 0)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Enemy")
		{
			Destroy(collider.gameObject);
			Destroy(gameObject);
		}
	}
}

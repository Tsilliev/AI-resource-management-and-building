using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("tranCube"))
		{
		Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
		}
	}
	
	
	void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("tranCube"))
		{
		Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
		}
	}
}

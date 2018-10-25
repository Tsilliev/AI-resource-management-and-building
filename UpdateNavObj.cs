using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class UpdateNavObj : MonoBehaviour {
	
	Collider collider1;
	Collider collider2;
	Collider colliderClone;
	GameObject cubeClone;
	
	void Start () {
		//MAKE THE BLOCK BELOW THIS ONE AN OBSTACLE SO THE AI AVOIDS IT
		//MAKE THE BLOCK 2 BLOCKS BELOW THIS ONE UNWALKABLE TO MAKE ONE BLOCK EMPTY AREAS UNWALKABLE
		 Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);

		for (int i = 0; i < hitColliders.Length; ++i)
		{

			if (hitColliders[i].name.Contains("Block") && transform.position == new Vector3(hitColliders[i].transform.position.x, hitColliders[i].transform.position.y + 1, hitColliders[i].transform.position.z)
				|| hitColliders[i].name.Contains("Block") && transform.position == new Vector3(hitColliders[i].transform.position.x, hitColliders[i].transform.position.y + 2, hitColliders[i].transform.position.z))
			{
				
				hitColliders[i].gameObject.layer = LayerMask.NameToLayer("Foundation");
				collider2 = hitColliders[i].GetComponent<BoxCollider>();
				collider2.isTrigger = false;
				Bounds bz = collider2.bounds;
				Pathfinding.GraphUpdateObject guo2 = new Pathfinding.GraphUpdateObject(bz);	
				AstarPath.active.UpdateGraphs (guo2);
			}
			
		}
		
		//UPDATE GRID ON THIS OBJECT
	collider1 = GetComponent<BoxCollider>();
	collider1.isTrigger = false;
	Bounds b = collider1.bounds;
	Pathfinding.GraphUpdateObject guo = new Pathfinding.GraphUpdateObject(b);
	//guo.setWalkability = true;	
	
	AstarPath.active.UpdateGraphs (guo);
	
	
	}
	
	/*
	void OnTriggerEnter(Collider other)
    {
       print("colliding with: " + other.gameObject.name);
        if (other.gameObject.name.Contains("Terrain") && this.gameObject.name.Contains("Block"))
        {
		 print("colliding with terrain, initiating obstacle creation");
		cubeClone = Instantiate(this.gameObject, new Vector3(Mathf.Round(transform.position.x), transform.position.y - 0.2f, Mathf.Round(transform.position.z)), Quaternion.identity) as GameObject;
		cubeClone.name = "foundation";
		cubeClone.layer = LayerMask.NameToLayer("Foundation");	
		colliderClone = GetComponent<BoxCollider>();
		//colliderClone.isTrigger = false;
		Bounds bc = colliderClone.bounds;
		Pathfinding.GraphUpdateObject guoc = new Pathfinding.GraphUpdateObject(bc);	
	
		AstarPath.active.UpdateGraphs (guoc);	  
		}
    }
	*/
}
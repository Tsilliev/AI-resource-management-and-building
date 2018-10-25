using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class job_woodCutting : MonoBehaviour {
	
	public AudioClip treeFallClip;
	NavMeshAgent agent;
    public AudioClip[] audioSources;
	public float NearestTreeRadius;
	
	GameObject nearestObj;
	//ASSIGN WOODCUTTER
	GameObject woodcutter;
	//ASSIGN DROP POINT
	public GameObject dropPoint;
	GameObject gameManager;
	
	Stock stock;
	CharInventory charInventory;
	float TreeHP;
	
	bool goingToTree;
	bool cuttingDownTree;
	bool WoodCutting;
	bool idle;
	bool invoked;

	// Use this for initialization
	void Start () {
		woodcutter = this.gameObject;
		gameManager = GameObject.Find("Gamemanager");
		agent = woodcutter.GetComponent<NavMeshAgent>();
		stock = gameManager.GetComponent<Stock>();
		charInventory = woodcutter.GetComponent<CharInventory>();
		NearestTreeRadius = 500f;
		//charInventory.job_woodcutting = true;
		WoodCutting = false;
		//FindNearestTree();
	}
	
	// Update is called once per frame
	void Update () {
	if(WoodCutting == true)
	{
		
		if( idle == true && invoked == false)
		{
		print("woodcutter idle state - repeat find nearest tree each 10 sec");
		InvokeRepeating("FindNearestTree", 1, 10.0F);
		invoked = true;
		}
		
		if(goingToTree == true && cuttingDownTree == false)
		{
		float distance = Vector3.Distance (woodcutter.transform.position, nearestObj.transform.position);
			if(distance <= 2.0f )
			{
			agent.isStopped = true;
			goingToTree = false;
			CuttingTreeF();
			}
		}
		else if (goingToTree == false && cuttingDownTree == false)
		{
			if(charInventory.woodAmount < 15)
			{
			FindNearestTree();
			}
			else if(charInventory.woodAmount >= 15)
			{
			agent.isStopped = false;
			agent.destination = dropPoint.transform.position;	
			float distance = Vector3.Distance (woodcutter.transform.position, dropPoint.transform.position);
	
				if(distance <= 2.0f )
				{
				agent.isStopped = true;
				stock.mat_wood += charInventory.woodAmount;
				charInventory.woodAmount = 0;
				cuttingDownTree = false;
				FindNearestTree();
				}
			}
		}
		if(cuttingDownTree == true )
		{
			TreeHP = nearestObj.GetComponent<TreeStats>().TreeHP;
			if(TreeHP < 0)
			{
			
			CancelInvoke();
			AudioSource audio = woodcutter.GetComponent<AudioSource>();
			audio.clip = treeFallClip;
			audio.Play();
		
			charInventory.woodAmount += nearestObj.GetComponent<TreeStats>().WoodAmount;
			StartCoroutine(TreeRegrow(nearestObj));
			cuttingDownTree = false;
			}
			
				
		}
		
	
	}
	
	}
	
	
	public void ToggleWoodCutting()
	{
	WoodCutting = !WoodCutting;	
	CancelInvoke();
	agent.isStopped = true;
	goingToTree = false;
	cuttingDownTree = false;
	idle = true;
	invoked = false;	
	}
	
	public void CuttingTreeF()
	{

		TreeHP = nearestObj.GetComponent<TreeStats>().TreeHP;
		//TreeWoodAmount = nearestObj.GetComponent<TreeStats>().WoodAmount;
		InvokeRepeating("AttackTree", 0, 2.0F);
		cuttingDownTree = true;	
	
		
	}
	
	
	
	public void AttackTree()
	{
	AudioSource audio = woodcutter.GetComponent<AudioSource>();
	audio.clip = audioSources[Random.Range(0, audioSources.Length)];
	audio.Play();
    nearestObj.GetComponent<TreeStats>().TreeHP -=1;   
		
		
	}

	
	public void FindNearestTree()
	{
	
     // The minimum distance we are looking at lateron
    float minDistance = float.MaxValue;
 
    // Get the collisions
    Collider[] hitColliders = Physics.OverlapSphere(transform.position, NearestTreeRadius);
    // could also be done using
    // for (int i = 0; i < hitColliders.Length; ++i)
    int i = 0;
    while (i < hitColliders.Length)
    {
		
        // Check, if the collision object has the correct tag
        if (hitColliders[i].tag.Equals("Tree"))
        {
		
            // Get the position of the collider we are looking at
            Vector3 possiblePosition = hitColliders[i].transform.position;
 
            // Get the distance between us and the collider
            //float currDistance = Vector3.Distance(center, possiblePosition);
			float currDistance = (transform.position - possiblePosition).sqrMagnitude;
            // If the distance is smaller than the one before...
            if (currDistance < minDistance)
            {
                // Assign gameobject
                nearestObj = hitColliders[i].gameObject;
                // Set our compare-value to that one
                minDistance = currDistance;
				
            }
        }
        i++;
    }
	 //agent.SetDestination(nearestObj.transform.position);
	
	agent.destination = nearestObj.transform.position;
	agent.isStopped = false;
	goingToTree = true;
	//return nearestObj;
	}
	
	public IEnumerator TreeRegrow(GameObject tree)
	{
	tree.SetActive(false);
	yield return new WaitForSeconds(Random.Range(240,600));
	tree.SetActive(true);
	tree.GetComponent<TreeStats>().RandomizeTreeStats();
	
	}
	
	
	
	
  }

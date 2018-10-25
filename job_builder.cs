using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

public class job_builder : MonoBehaviour {
	public AudioClip[] audioSources;
	NavMeshAgent agent;

	
	public bool job_building;
	bool goingToBlock;
	bool idle;
	bool invoked;
	bool building;
	bool compared;
	bool stuck;
	bool foundYB;
	public float buildRange;
	Material mat_wood;
	GameObject nearestBlock;
	GameObject gameManager;
	Seeker seeker;
	AIPath ai;
	Path path;
	double compareDist;
	//private NavMeshPath path;
	public List<GameObject> BanBlocksList = new List<GameObject>();
	GM gm;
	
	// Use this for initialization
	void Start () {
		seeker = GetComponent<Seeker>();
		ai = GetComponent<AIPath>();
	job_building = false;
	idle = true;
	buildRange = 500f;	
	//agent = GetComponent<NavMeshAgent>();
	mat_wood = Resources.Load("Mats/mat_woodblock") as Material;
	gameManager = GameObject.Find("Gamemanager");
	//path = new NavMeshPath();	
	gm = gameManager.GetComponent<GM>();
	}
	

	public void ToggleBuilding()
	{
	job_building = !job_building;	
	CancelInvoke();
	//agent.isStopped = true;
	building = false;
	goingToBlock = false;
	idle = true;
	invoked = false;
	nearestBlock = null;
	if(BanBlocksList.Count > 0)
		{
				for (int i = 0; i < BanBlocksList.Count; ++i)
				{
				BanBlocksList[i].GetComponent<BlockStats>().banned = false;
				}
				BanBlocksList.Clear();
		}
	}
	
	void Update () {
		
	if(job_building == true)
	{

		if( idle == true && invoked == false)
		{
		print("idle state - repeat find nearest block each 10 sec");
		InvokeRepeating("GoToNextBlock", 1, 10.0F);
		invoked = true;
		
			if(BanBlocksList.Count > 0)
			{
				for (int i = 0; i < BanBlocksList.Count; ++i)
				{
				BanBlocksList[i].GetComponent<BlockStats>().banned = false;
				}
				BanBlocksList.Clear();
			}
			
		}
		else if(goingToBlock)
		{

			idle = false;
			invoked = false;
			
			if(ai.reachedEndOfPath)
			{
			
			
		/*
		double distance = Vector3.Distance (transform.position, nearestBlock.transform.position);
		double roundDist = System.Math.Round( distance, 2);
		
			 if(compared == false)
			 {
				 compareDist = roundDist;
				 compared = true;
			 }
			
			 timeout -= Time.deltaTime;
			if ( timeout < 0 && compareDist == roundDist )
			{
				if(nearestBlock != null)
				{
				BanBlocksList.Add(nearestBlock);
				nearestBlock.GetComponent<BlockStats>().banned = true;	
				}
			
			CancelInvoke("GoToNextBlock");
			print("Repathing - Stuck");	
			goingToBlock = false;
			nearestBlock = null;
			Invoke("GoToNextBlock", 0.3f);
			timeout = 6f;
			
			}
			else if (timeout < 0 && compareDist != roundDist)
			{
			compared = false;

			timeout = 6f;
			}
			
			if(roundDist <= 2f )
			{
			
			agent.isStopped = true;
			*/
			goingToBlock = false;
			building = true;
			InvokeRepeating("BuildBlock", 0, 1.0F);
			}
			
		}
		else if (goingToBlock == false  && building == true)
		{
			if(nearestBlock.GetComponent<BlockStats>().BlockHP >= 5)
			{
			for (int i = 0; i < BanBlocksList.Count; ++i)
			{
			BanBlocksList[i].GetComponent<BlockStats>().banned = false;
			}
			BanBlocksList.Clear();
			
			nearestBlock.name = "Wooden Block";
			nearestBlock.GetComponent<MeshRenderer> ().material = mat_wood;
			nearestBlock.AddComponent<UpdateNavObj>();
			
		
			
			CancelInvoke("BuildBlock");
			building = false;
			gm.ToDoBlocks.Remove(gm.ToDoBlocks[0]);
			Invoke("GoToNextBlock", 0.5f);
			
			}
		}
		
	}
	}

	
	
	
	public void BuildBlock()
	{
	AudioSource audio = GetComponent<AudioSource>();
	audio.clip = audioSources[Random.Range(0, audioSources.Length)];
	audio.Play();
    nearestBlock.GetComponent<BlockStats>().BlockHP +=1;   
		
		
	}
	public void GoToNextBlock()
	{

	if(gm.ToDoBlocks.Count > 0)
	{
	nearestBlock = gm.ToDoBlocks[0];
	seeker.StartPath (transform.position,nearestBlock.transform.position, OnPathComplete);

	}
	else
	{
	idle = true;
	}
		
	}
	
	public void OnPathComplete(Path p)
		{
			if(!p.error)
			{
				goingToBlock = true;
				//if(nearestBlock != null)
				//print("path complete for cube at loc: " + nearestBlock.transform.position);
			//	else
				//print("path complete");
			}
			else
			{
			print("invalid path!");
			}
		}
	
	
	/*
	public void FindNearestBlock()
	{
	nearestBlock = null;


    float minDistance = float.MaxValue;
	float lastYpos = float.MaxValue;
	
    Collider[] hitColliders = Physics.OverlapSphere(transform.position, buildRange);

		for (int i = 0; i < hitColliders.Length; ++i)
		{

			if (hitColliders[i].name.Contains("tran"))
			{
				Vector3 possiblePosition = hitColliders[i].transform.position;
				float currDistance = (transform.position - possiblePosition).sqrMagnitude;

				if (currDistance < minDistance && hitColliders[i].GetComponent<BlockStats>().banned == false)
				{
				
				nearestBlock = hitColliders[i].gameObject;
				
				minDistance = currDistance;
					for (int g = 0; g < hitColliders.Length; ++g)
					{
						if(nearestBlock.transform.position == new Vector3(hitColliders[g].transform.position.x, hitColliders[g].transform.position.y - 1, hitColliders[g].transform.position.z) || nearestBlock.transform.position == new Vector3(hitColliders[g].transform.position.x, hitColliders[g].transform.position.y - 2, hitColliders[g].transform.position.z))
						{
						nearestBlock = hitColliders[g].gameObject;
						print("assigned lower block");
						}
					}
				}
				
				
			}

		}
		if(nearestBlock != null && goingToBlock == false)
		{
			print ("going to nearest block: " + nearestBlock.transform.position);
			
			NavMesh.CalculatePath(transform.position,nearestBlock.transform.position, NavMesh.AllAreas, path);
		
			if (path.status == NavMeshPathStatus.PathComplete)
			{
				for (int i = 0; i < BanBlocksList.Count; ++i)
				{
				BanBlocksList[i].GetComponent<BlockStats>().banned = false;
				}
				BanBlocksList.Clear();
				
				agent.isStopped = false;
				agent.destination = nearestBlock.transform.position;
				goingToBlock = true;
				foundBB = true;
				idle = false;
				invoked = false;
			}
			else
			{
			print("path not valid, repath");
			BanBlocksList.Add(nearestBlock);
			nearestBlock.GetComponent<BlockStats>().banned = true;
			CancelInvoke("FindNearestBlock");
			FindNearestBlock();
			
			}

		}
		else
		{
		idle = true;
		}

		
	}
	*/
}

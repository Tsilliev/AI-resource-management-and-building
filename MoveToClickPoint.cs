 // MoveToClickPoint.cs
    using UnityEngine;
    using UnityEngine.AI;
	using Pathfinding;
    
    public class MoveToClickPoint :  MonoBehaviour {
      //  NavMeshAgent agent;
       // private NavMeshPath path;
		 Seeker seeker;
		 Path path;
        void Start() {
			seeker = GetComponent<Seeker>();
           // agent = GetComponent<NavMeshAgent>();
			// path = new NavMeshPath();
        }
        
        void Update() {
     
			if (Input.GetMouseButtonDown(2))
			{
                RaycastHit hit;
		
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 500)) 
				{ 
					seeker.StartPath (transform.position,hit.point, OnPathComplete);
					//FindPath(transform.position, hit.point);
					
					/*
					NavMesh.CalculatePath(transform.position,hit.point, NavMesh.AllAreas, path);
					
					 if (path.status == NavMeshPathStatus.PathComplete)
					print ("Path complete");
					else if (path.status == NavMeshPathStatus.PathInvalid)
					print ("Path invalid");
					else if (path.status == NavMeshPathStatus.PathPartial)
					print ("Path partial");
					
					
					agent.isStopped = false;
					agent.destination = hit.point;
					*/
                }
				
				
				
            }
			
			
		//	Move();
        }
		
		public void OnPathComplete(Path p)
		{
			if(!p.error)
			{
			print("path complete");
			}
			else
			{
			print("invalid path!");
			}
		}
		
		
		/*
		 public void Move()
		 {
			 if (Path.Count > 0)
			 {
			transform.position = Vector3.MoveTowards(transform.position, Path[0],
			Time.deltaTime * 3F);
				 if (Vector3.Distance(transform.position, Path[0]) < 1.1F)
				 {
				 Path.RemoveAt(0);
				 }
			 }
		 }
		 */
		

    }
	
		
		
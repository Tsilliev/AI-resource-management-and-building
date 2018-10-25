using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControls : MonoBehaviour {

    #region Bools
    bool buildMode;
    public bool dirLeft;
    public bool dirRight;
    public bool deleteTran;
    bool buildTran;
    bool boolRan;


    #endregion Bools

    #region GameObjects
    public GameObject tranCube;
    public GameObject blueprintCube;
    GameObject tranOrb;
	GameObject gameManager;
	GameObject cubeClone;
	GameObject tranStart;
	GM gm;


    #endregion GameObjects

    #region Vectors

    #endregion Vectors

    #region Floats and Ints
	int mCalcX;
	int mCalcY;
	int mCalcZ;
	int invmCalcX;
	int ix;

    #endregion Floats and Ints

    #region Lists
    public List<GameObject> tranDragList = new List<GameObject>();
    private float mEndPosX;
    private float mEndPosZ;
    private float difPosX;
    private float difPosZ;
    private float mStartPosX;
    private float mStartPosZ;
    private GameObject cubeClone2;

    #endregion Lists



    void Start () {
	tranOrb = GameObject.Find("tranOrb");
	tranOrb.SetActive(false);
	gameManager = GameObject.Find("Gamemanager");	
	gm = gameManager.GetComponent<GM>();
	
	}
	

	void Update ()
    {


            #region ButtonB       
        if (Input.GetKeyDown(KeyCode.B))
	{
	buildMode = !buildMode;
	boolRan = false;
	
	if(buildMode == true && boolRan == false)
	{
	blueprintCube.SetActive(true);
	boolRan = true;
	}
	else if(buildMode == false && boolRan == false)
	{
	blueprintCube.SetActive(false);
	boolRan = true;
	}

	
	}
        #endregion ButtonB

        if(buildMode == true)
	    {
            #region bluePrintCubeMoveOnTerrain
            RaycastHit hit;
		    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            

            if (Physics.Raycast(ray, out hit, 1000))
			{
			
			Vector3 gridPos = new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y), Mathf.Round(hit.point.z));
			Vector3 MyNormal = hit.normal;
			MyNormal = hit.transform.TransformDirection(MyNormal);	
			
			if(hit.collider.tag == "terrain")
			{
			blueprintCube.transform.position = gridPos;
			
			
			}
			else if(hit.collider.tag == "block")
			{
				
				if(MyNormal == hit.transform.up)
				 {
				blueprintCube.transform.position = new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y + 0.5f), Mathf.Round(hit.point.z));
				 }
				
				
				if(MyNormal == -hit.transform.up)
				 {
				blueprintCube.transform.position = new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y - 0.5f), Mathf.Round(hit.point.z));
				 }
				
				
				if(MyNormal == hit.transform.right)
				 {
				blueprintCube.transform.position = new Vector3(Mathf.Round(hit.point.x + 0.5f), Mathf.Round(hit.point.y), Mathf.Round(hit.point.z));
				 }
		
				 if(MyNormal == -hit.transform.right)
				 {
				blueprintCube.transform.position = new Vector3(Mathf.Round(hit.point.x - 0.5f), Mathf.Round(hit.point.y), Mathf.Round(hit.point.z));
				 }
				 
				 
				 if(MyNormal == hit.transform.forward)
				 {
				blueprintCube.transform.position = new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y), Mathf.Round(hit.point.z + 0.5f));
				 }
		
				 if(MyNormal == -hit.transform.forward)
				 {
				blueprintCube.transform.position = new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y), Mathf.Round(hit.point.z - 0.5f));
				 }
			}
			
			
			}

            #endregion bluePrintCubeMoveOnTerrain

            #region bluePrintCubeSpawnOnClick
            if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift)) 
		    {
			    tranOrb.SetActive(true);
			    tranOrb.transform.position = new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y), Mathf.Round(hit.point.z));
			    blueprintCube.SetActive(false);
			    mStartPosX = hit.point.x;
                mStartPosZ = hit.point.z;

			
			    if (Physics.Raycast(ray, out hit, 1000))
			    {
			    Vector3 gridPos = new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y), Mathf.Round(hit.point.z));
			    Vector3 MyNormal = hit.normal;
			    MyNormal = hit.transform.TransformDirection(MyNormal);	
			
						
				    if(!Input.GetKeyDown(KeyCode.LeftShift))
				    {
					    if(hit.collider.tag == "terrain")
					    {
					    cubeClone = Instantiate(tranCube, gridPos, Quaternion.identity) as GameObject;
					    gm.ToDoBlocks.Add(cubeClone);
					    tranStart = cubeClone;
					    }
					    else if(hit.collider.tag == "block")
					    {
						
						    if(MyNormal == hit.transform.up)
						     {
						    cubeClone = Instantiate(tranCube, new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y + 0.5f), Mathf.Round(hit.point.z)), Quaternion.identity) as GameObject;
						    gm.ToDoBlocks.Add(cubeClone);
						     }
						
						
						    if(MyNormal == -hit.transform.up)
						     {
						    cubeClone = Instantiate(tranCube, new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y - 0.5f), Mathf.Round(hit.point.z)), Quaternion.identity) as GameObject;
						    gm.ToDoBlocks.Add(cubeClone);
						     }
						
						
						    if(MyNormal == hit.transform.right)
						     {
						    cubeClone = Instantiate(tranCube, new Vector3(Mathf.Round(hit.point.x + 0.5f), Mathf.Round(hit.point.y), Mathf.Round(hit.point.z)), Quaternion.identity) as GameObject;
						    gm.ToDoBlocks.Add(cubeClone);
						     }
				
						     if(MyNormal == -hit.transform.right)
						     {
						    cubeClone = Instantiate(tranCube, new Vector3(Mathf.Round(hit.point.x - 0.5f), Mathf.Round(hit.point.y), Mathf.Round(hit.point.z)), Quaternion.identity) as GameObject;
						    gm.ToDoBlocks.Add(cubeClone);
						     }
						 
						 
						     if(MyNormal == hit.transform.forward)
						     {
						    cubeClone = Instantiate(tranCube, new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y), Mathf.Round(hit.point.z + 0.5f)), Quaternion.identity) as GameObject;
						    gm.ToDoBlocks.Add(cubeClone);
						     }
				
						     if(MyNormal == -hit.transform.forward)
						     {
						    cubeClone = Instantiate(tranCube, new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y), Mathf.Round(hit.point.z - 0.5f)), Quaternion.identity) as GameObject;
						    gm.ToDoBlocks.Add(cubeClone);
						     }
						 
					
					    }
				    }
				
			
			    }
    

		    }
            #endregion bluePrintCubeSpawnOnClick

            #region DeleteCube
            else if ( Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift) && hit.collider.name.Contains("tran") )
		    {
				gm.ToDoBlocks.Remove(hit.collider.gameObject);
				Destroy(hit.collider.gameObject);
				
		    }
            #endregion DeleteCube

            #region DragBlueprintCube
            else if (Input.GetMouseButton(0))
		    {

		        if (Physics.Raycast(ray, out hit, 1000))
			    {	
				
				    if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
				    {
					    float distance_to_screen = Camera.main.WorldToScreenPoint(tranOrb.transform.position).z;
					    Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen ));
					    tranOrb.transform.position = new Vector3( pos_move.x, tranOrb.transform.position.y, pos_move.z );
                        mEndPosX = tranOrb.transform.position.x;
                        mEndPosZ = tranOrb.transform.position.z;
					    difPosX = mEndPosX - mStartPosX;
                        difPosZ = mEndPosZ - mStartPosZ;
                        


                        int difTotal = ((int)difPosZ + 1) * (int)difPosX;
                        int posDifTotal = difTotal;
                        int difZ = (int)difPosZ;


                       
                        if (difZ < 0)
                        {
                            difZ *= -1;
                        }
                        if (difTotal < 0)
                        {
                            posDifTotal *= -1;
                        }
                        print("posDifTotal: " + posDifTotal);
                        // print("Z: " + difPosZ);
                        if (tranDragList.Count < (int)difPosX )
                        {
                            for (int i = tranDragList.Count; i < (int)difPosX; ++i)
                            {
                                ix = tranDragList.Count;

                                if (difPosX < 0)
                                {
                                    ix *= -1;
                                }
                                cubeClone = Instantiate(tranStart, new Vector3(tranStart.transform.position.x + ix, tranStart.transform.position.y, tranStart.transform.position.z + (int)difPosZ), Quaternion.identity) as GameObject;
                                cubeClone.name = "tranStart" + i;
                                tranDragList.Add(cubeClone);


                            }


                        }
                        if (tranDragList.Count < posDifTotal)
                        {
                            int tdc = tranDragList.Count;
                            for (int z = tranDragList.Count; z < posDifTotal; ++z)
                            {
                                print("tdc: " + tdc);
                                cubeClone2 = Instantiate(tranStart, new Vector3(tranDragList[tdc-1].transform.position.x, tranDragList[tdc - 1].transform.position.y, tranDragList[tdc - 1].transform.position.z + (int)difPosZ), Quaternion.identity) as GameObject;
                                cubeClone2.name = "tranStartZ" + z;
                                tranDragList.Add(cubeClone2);
                                tdc--;


                            }
                        }

                        if (tranDragList.Count > (int)difPosX)
                        {

                            for (int z = (int)difPosX; z < tranDragList.Count; ++z)
                            {

                                Destroy(tranDragList[tranDragList.Count - 1]);
                                tranDragList.Remove(tranDragList[tranDragList.Count - 1]);

                            }
                        }
					
				    }
			    }
			
		    }
		    else if (Input.GetMouseButtonUp(0))
		    {
		    blueprintCube.SetActive(true);
		    tranOrb.SetActive(false);
		    tranDragList.Clear();
		    }

            #endregion DragBlueprintCube
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

		public GameObject CamPosObj;
		bool RightMBDown;
		public float RotSpeed = 100;
		public float MoveSpeed = 50;
		public float minY = 0f;
		public float maxY = 1000f;
		public float ZoomSensitivity = 0.8f;
		void Start()
		{

		}

		void Update()
		{
			 
			
			if (Input.GetMouseButtonDown(1))
			{	
			RightMBDown = true;
			}
			else if (Input.GetMouseButtonUp(1))
			{	
			RightMBDown = false;
			}
			if(Input.GetKey(KeyCode.D))
			{
			transform.Translate(new Vector3(MoveSpeed * Time.deltaTime,0,0));
			}
			if(Input.GetKey(KeyCode.A))
			{
			transform.Translate(new Vector3(-MoveSpeed * Time.deltaTime,0,0));
			}
			if(Input.GetKey(KeyCode.S))
			{
			transform.Translate(new Vector3(0,0,-MoveSpeed * Time.deltaTime));
			}
			if(Input.GetKey(KeyCode.W))
			{
			transform.Translate(new Vector3(0,0, MoveSpeed * Time.deltaTime));
			}
		
			// LOOKING AROUND	
			if(RightMBDown == true )
			{	
				if( transform.eulerAngles.x >= 280 && transform.eulerAngles.x <= 360 || transform.eulerAngles.x >= 0 && transform.eulerAngles.x <= 80)
				{
					
				Vector3 rotation = Vector3.zero;
			    rotation += Vector3.left * Time.deltaTime * RotSpeed *  Input.GetAxis("Mouse Y");
                transform.Rotate(rotation, Space.Self);
 
                // Make sure z rotation stays locked
                rotation = transform.rotation.eulerAngles;
                rotation.z = 0;
                transform.rotation = Quaternion.Euler(rotation);
				}
				
				else if (transform.eulerAngles.x < 280 && transform.eulerAngles.x > 180 && Input.GetAxis("Mouse Y") < 0 || transform.eulerAngles.x > 80 && transform.eulerAngles.x < 180 && Input.GetAxis("Mouse Y") > 0 )
				{
					Vector3 rotation = Vector3.zero;
			   rotation += Vector3.left * Time.deltaTime * RotSpeed *  Input.GetAxis("Mouse Y");
                transform.Rotate(rotation, Space.Self);
 
                // Make sure z rotation stays locked
                rotation = transform.rotation.eulerAngles;
                rotation.z = 0;
                transform.rotation = Quaternion.Euler(rotation);
					
				}
				
				 if(Input.GetAxis("Mouse X") != 0 )
				{
					
				Vector3 rotation = Vector3.zero;
			    rotation += Vector3.up * Time.deltaTime * RotSpeed *  Input.GetAxis("Mouse X");
                transform.Rotate(rotation, Space.Self);
 
                // Make sure z rotation stays locked
                rotation = transform.rotation.eulerAngles;
                rotation.z = 0;
                transform.rotation = Quaternion.Euler(rotation);
				}
				
			}
				
			/*
		if(MiddleMBDown == true)
		{
			float xAxisValue = Input.GetAxis("Mouse X") *  Time.deltaTime * 4;
			float zAxisValue = Input.GetAxis("Mouse Y") *  Time.deltaTime * 4;
			
	
			CamPosObj.transform.Translate(new Vector3(xAxisValue, 0.0f, zAxisValue));

			//CamPosObj.transform.position = new Vector3(Mathf.Clamp(CamPosObj.transform.position.x, -ClampCamX, ClampCamX),CamPosObj.transform.position.y ,Mathf.Clamp(CamPosObj.transform.position.z, MinClampCamZ, MaxClampCamZ));
			 CamPosObj.transform.position = new Vector3(CamPosObj.transform.position.x,CamPosObj.transform.position.y ,CamPosObj.transform.position.z);
			 
			 transform.position = CamPosObj.transform.position;
			 
			 
		}
		// zooming
			if (Input.GetAxis("Mouse ScrollWheel") != 0f && CamPosObj.transform.position.y > minY &&  CamPosObj.transform.position.y < maxY) // forward
				{
				float YAxisValue = Input.GetAxis("Mouse ScrollWheel") * ZoomSensitivity ;
			
			CamPosObj.transform.Translate(new Vector3(0.0f, -YAxisValue , 0.0f));
		
			 transform.position = CamPosObj.transform.position;
				}
				else if (Input.GetAxis("Mouse ScrollWheel") < 0f && CamPosObj.transform.position.y <= minY || Input.GetAxis("Mouse ScrollWheel") > 0f && CamPosObj.transform.position.y >= maxY) 
				{
				float YAxisValue = Input.GetAxis("Mouse ScrollWheel") * ZoomSensitivity ;
			
			CamPosObj.transform.Translate(new Vector3(0.0f, -YAxisValue , 0.0f));
     
			 transform.position = CamPosObj.transform.position;
				}
			*/	
		}
	}


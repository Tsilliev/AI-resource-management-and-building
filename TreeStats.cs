using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStats : MonoBehaviour {


	public float TreeHP;
	public int WoodAmount;
	
	// Use this for initialization
	void Start () {
	TreeHP = Random.Range(7,15);
	if(TreeHP <= 10)
	{
		WoodAmount = Random.Range(6,7);		
	}
	else if(TreeHP > 10)
	{
		WoodAmount = Random.Range(7,8);
	}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void RandomizeTreeStats()
	{
	TreeHP = Random.Range(7,15);
	if(TreeHP <= 10)
	{
		WoodAmount = Random.Range(6,7);		
	}
	else if(TreeHP > 10)
	{
		WoodAmount = Random.Range(7,8);
	}
		
	}
}

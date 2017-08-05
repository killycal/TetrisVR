using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lines : MonoBehaviour {
	public GameObject[] lines;
	// Use this for initialization
	private bool[] adjust;
	private bool[] destroy;
	private int h;
	public int toDestroy;
	void Start () {
		adjust = new bool[h];
		destroy = new bool[h];
		for (int i=0; i<h; i++)
		{
			adjust [i] = false;
			destroy [i] = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public int getLineNo(string line){
		int no;
		line=line.Remove (0, 4);
		no = int.Parse (line);
		return no;
	}
	public bool getAdjust(int line){
		return adjust [line];
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lines : MonoBehaviour {
	public GameObject[] lines;
	// Use this for initialization
	public List<int> destroy = new List<int>();
	private int h;
	private int toDestroy;
	void Start () {
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
}

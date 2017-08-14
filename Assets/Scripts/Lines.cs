using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lines : MonoBehaviour {
	public GameObject[] lines;
	// Use this for initialization
	public List<int> destroy = new List<int>();
	private int h;
	public int totalDestroy=0;
	private bool flip=false;
	public float speed=2.0f;
	public int destruction=2;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (totalDestroy==10&&flip==false) {
			speed += .5f;
			flip = true;
		}
		else if (totalDestroy==20&&flip==true) {
			speed += .5f;
			flip = false;
		}
		else if (totalDestroy==30&&flip==false) {
			speed += .5f;
			flip = true;
		}
		else if (totalDestroy==40&&flip==false) {
			speed += .5f;
			flip = true;
		}
	}
	public int getLineNo(string line){
		int no;
		line=line.Remove (0, 4);
		no = int.Parse (line);
		return no;
	}
}

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
	private float spd = 5.0f;
	public int destruction=2;
	public int points=0;
	public int level=0;
	public int headshots = 0;
	private Vector3 pos;
	private Vector3 end;
	private float startTime;
	private float journeyLength;
	private GameObject world;
	private bool hit = false;
	public string death="Line23";
	private int deathno=23;
	private float ystart;
	void Start(){
		pos=this.transform.position;
		world = GameObject.Find ("Scene");
		ystart = pos.y;
	}
	// Update is called once per frame
	void FixedUpdate () {
		
		if (hit == true) {
			float distCovered = (Time.time - startTime) * spd;
			float fracJourney = distCovered / journeyLength;
			world.transform.position = Vector3.Lerp (pos, end, fracJourney);	
		}
		if (totalDestroy==5&&flip==false) {
			speed += .5f;
			flip = true;
			level = 1;
		}
		else if (totalDestroy==10&&flip==true) {
			speed += .5f;
			flip = false;
			level = 2;
		}
		else if (totalDestroy==15&&flip==false) {
			speed += .5f;
			flip = true;
			level = 3;
		}
		else if (totalDestroy==20&&flip==false) {
			speed += .5f;
			flip = true;
			level = 4;
		}
	}
	public int getLineNo(string line){
		int no;
		line=line.Remove (0, 4);
		no = int.Parse (line);
		return no;
	}
	public void up(){
		BroadcastMessage ("adjustUp",SendMessageOptions.DontRequireReceiver);
	}
	public void Hit(){
		headshots++;
		pos = world.transform.position;
		end.Set (pos.x, pos.y + -1f, pos.z);
		startTime = Time.time;
		journeyLength = Vector3.Distance (pos, end);
		deathno--;
		death = "Line" + deathno;
		hit = true;
	}
	public void ResetWorld(){
		pos = world.transform.position;
		end.Set (pos.x, pos.y+(float)headshots, pos.z);
		headshots = 0;
		startTime = Time.time;
		journeyLength = Vector3.Distance (pos, end);
		death = "Line23";
		deathno = 23;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {
	private bool orphan=false;
	private int num;
	private Vector3 pos;
	private Vector3 end;
	public float speed = 1.0f;
	private float startTime;
	private float journeyLength;
	private bool move=false;
	private float lineno=-1;
	private float exline=-1;
	// Use this for initialization
	void Start () {
		pos=this.transform.position;
		end = pos;
		end.Set (pos.x, pos.y-.1f, pos.z);
		startTime = Time.time;
		journeyLength = Vector3.Distance(pos, end);
	}
	

	void FixedUpdate () {
		if (!orphan) {
			if (!this.gameObject.GetComponentInParent<Movement> ().getMove ()) {
				this.gameObject.transform.parent = GameObject.Find("Cubes").transform;
				orphan = true;
				this.gameObject.name = "cube";
			}
		}
		if (move == true) {
			//this.gameObject.name = "Cube";
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			this.transform.position = Vector3.Lerp (pos, end, fracJourney);
		} 
		if ((this.gameObject.transform.position.y-end.y<.001)&&(move==true)){
			GameObject.Find ("Horizontal").GetComponent<Lines> ().toDestroy = 0;
			move=false;
		}
	}
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.name == "cube" || other.gameObject.name == "Plane")
			move = false;
	}
	void OnTriggerEnter(Collider other)
	{
		string line=other.gameObject.name.Remove (0, 4);
		float lastline = lineno;
		lineno = int.Parse (line);
	}
	public void setMove (bool moveit)
	{
		move=moveit;
	}
	private void adjust(float input)
	{
			exline = input;
			if (lineno > exline) { 
				move = true;
				pos = this.gameObject.transform.position;
				end.Set (pos.x, pos.y - .1f*GameObject.Find ("Horizontal").GetComponent<Lines> ().toDestroy, pos.z);
				startTime = Time.time;
				journeyLength = Vector3.Distance(pos, end);
			}
	}
}

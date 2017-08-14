using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
 	private Lines line;
	private int num;
	private Vector3 pos;
	private Vector3 end;
	private float speed = 2.0f;
	private float startTime;
	private float endTime;
	private float journeyLength;
	private bool move=true;
	private Component[] children;

	void Start () {
		pos=this.transform.position;
		end = pos;
		end.Set (pos.x, -10f, pos.z);
		startTime = Time.time;
		journeyLength = Vector3.Distance(pos, end);
		speed=GameObject.Find ("Horizontal").GetComponent<Lines> ().speed;
		if (this.gameObject.name == "Destruction Powerup(Clone)") {
			speed = speed * 1.5f;
		}
	}
	void FixedUpdate () {
		if (move==true)
		{
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			this.transform.position=Vector3.Lerp(pos,end,fracJourney);	
		}
		if (Time.time - startTime > 27 / speed) {
			DestroyObject (this.gameObject);
		}
	}
	void OnCollisionEnter(Collision other)
	{
		//if this is a 
		if ((this.gameObject.name=="Destruction Powerup(Clone)")&&(other.gameObject.name == "Bow Arrow" || other.gameObject.name == "FireSource" || other.gameObject.name == "Arrowhead collider")) {
			Destroy (this.gameObject);
			GameObject.Find ("Horizontal").GetComponent<Lines> ().destruction = 0;
		}
		else if (other.gameObject.name != "Bow Arrow" && other.gameObject.name != "FireSource" && other.gameObject.name != "Arrowhead collider" && other.gameObject.name !="Destruction Powerup(Clone)") {
			move = false;
			endTime = Time.time;
		}
		else {
			Vector3 impact = other.transform.position;
			children=this.gameObject.GetComponentsInChildren<Component>();
			float shortest=float.MaxValue;
			int childno=0;
			for (int i = 3; i < children.Length; i++) {
				float dist = Vector3.Distance (impact, children [i].gameObject.transform.position);
				if (dist < shortest&&children[i].gameObject.name=="Cube") {
					shortest = dist;
					childno = i;
				}
			}
			Destroy (children[childno].gameObject);
		}
	}
	public bool getMove()
	{
		return move;
	}
}
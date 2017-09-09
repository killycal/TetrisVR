using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMovement : MonoBehaviour {
	private Lines line;
	private int num;
	private Vector3 pos;
	private Vector3 end;
	private float speed = 2.0f;
	private float startTime;
	private float endTime;
	private float journeyLength;
	private bool move=true;
	private bool hit=false;
	private Component[] children;
	public GameObject effect; 
	private Color color;
	private bool broken=false;

	void Start () {
		pos=this.transform.position;
		end = pos;
		end.Set (pos.x, -10f, pos.z);
		startTime = Time.time;
		journeyLength = Vector3.Distance(pos, end);
		speed=GameObject.Find ("Horizontal").GetComponent<Lines> ().speed;
		color= new Color (this.gameObject.GetComponentInChildren<Renderer>().material.color.r,this.gameObject.GetComponentInChildren<Renderer>().material.color.g, this.gameObject.GetComponentInChildren<Renderer>().material.color.b,1);
	}
	void FixedUpdate () {
		if (move==true)
		{
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			this.transform.position=Vector3.Lerp(pos,end,fracJourney);	
		}
		if (Time.time - startTime > 70 / speed) {
			DestroyObject (this.gameObject);
		}
		/*if (Time.time - endTime > .05f&&hit==true) {
			Destroy (GameObject.Find("Arrow Scale Parent"));
			hit = false;
		}*/
	}
	void OnCollisionEnter(Collision other)
	{
		 if (other.gameObject.name == "Bow Arrow" || other.gameObject.name == "FireSource" || other.gameObject.name == "Arrowhead collider") {
			
			Vector3 impact = other.transform.position;
			children=this.gameObject.GetComponentsInChildren<Component>();
			float shortest=float.MaxValue;
			int childno=0;
			for (int i = 3; i < children.Length; i++) {
				float dist = Vector3.Distance (impact, children [i].gameObject.transform.position);
				if (dist < shortest&&children[i].gameObject.name=="NCube"||children[i].gameObject.name=="ZCube") {
					shortest = dist;
					childno = i;
				}

			}
			effect.GetComponent<ParticleSystem> ().startColor = color;

			if (children [childno].gameObject.name=="ZCube") {//if zcube is hit blow everything up
				BroadcastMessage ("destruct");
				BroadcastMessage ("playDestroy");
			}
			else
				children[childno].gameObject.GetComponent<NCube>().destruct();
			Instantiate (effect, children [childno].gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
			endTime = Time.time;
			hit = true;
		} else if (other.gameObject.name == "cube") {	//if Ncube hits cube
			Vector3 impact = other.transform.position;
			children=this.gameObject.GetComponentsInChildren<Component>();
			float shortest=float.MaxValue;
			int childno=0;
			int z = 0;
			for (int i = 3; i < children.Length; i++) {
				float dist = Vector3.Distance (impact, children [i].gameObject.transform.position);
				if (dist < shortest&&children[i].gameObject.name=="NCube") {
					shortest = dist;
					childno = i;
				}
				if (children [i].gameObject.name == "ZCube")
					z = i;
			}
			//effect.GetComponent<ParticleSystem> ().startColor = color;

			children[childno].gameObject.GetComponent<NCube>().destruct();
			if (broken == false) {
				children [z].gameObject.GetComponent<NCube> ().destruct ();
				Instantiate (effect, children [z].gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
			}
			broken = true;
			endTime = Time.time;
			Destroy (other.gameObject);
		}
		else {		//if NCube hits floor
			Vector3 impact = other.transform.position;
			children=this.gameObject.GetComponentsInChildren<Component>();
			float shortest=float.MaxValue;
			int childno=0;
			int z = 0;
			for (int i = 3; i < children.Length; i++) {
				float dist = Vector3.Distance (impact, children [i].gameObject.transform.position);
				if (dist < shortest&&children[i].gameObject.name=="NCube"||children[i].gameObject.name=="ZCube") {
					shortest = dist;
					childno = i;
					if (children [i].gameObject.name == "ZCube")
						z = i;
				}

					
			}
			//effect.GetComponent<ParticleSystem> ().startColor = color;
			if (broken == false) {
				children [z].gameObject.GetComponent<NCube> ().destruct ();
				Instantiate (effect, children [z].gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
			}
			broken = true;
			children[childno].gameObject.GetComponent<NCube>().destruct();
			endTime = Time.time;
		}
	}
	public bool getMove()
	{
		return move;
	}
}
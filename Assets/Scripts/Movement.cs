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
	private float journeyLength;
	private bool move=true;
	private Component[] children;
	private GameObject arrow;
	public GameObject effect; 
	private Color color;

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
		color= new Color (this.gameObject.GetComponentInChildren<Renderer>().material.color.r,this.gameObject.GetComponentInChildren<Renderer>().material.color.g, this.gameObject.GetComponentInChildren<Renderer>().material.color.b,1);
	}
	void FixedUpdate () {
		if (move==true)
		{
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			this.transform.position=Vector3.Lerp(pos,end,fracJourney);	
		}
		if (Time.time - startTime > 80 / speed) {
			DestroyObject (this.gameObject);
		}
	}
	void OnCollisionEnter(Collision other)
	{
		if ((this.gameObject.name == "Destruction Powerup(Clone)") && (other.gameObject.name == "Bow Arrow" || other.gameObject.name == "FireSource" || other.gameObject.name == "Arrowhead collider")) {
			Destroy (this.gameObject);
			GameObject.Find ("Horizontal").GetComponent<Lines> ().destruction = 0;
		} else if (other.gameObject.CompareTag("Text")) {
			Destroy (other.gameObject.GetComponent<Collider>());
			other.gameObject.GetComponent<GoCubes> ().destruct ();
		}
		else if (other.gameObject.name != "Bow Arrow" && other.gameObject.name != "FireSource" && other.gameObject.name != "Arrowhead collider" && other.gameObject.name != "Destruction Powerup(Clone)" && other.gameObject.name != "Laser") {
			move = false;
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
			effect.GetComponent<ParticleSystem> ().startColor = color;
			Instantiate (effect, children [childno].gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
			children[childno].gameObject.GetComponent<Cube>().destruct();
		}
	}
	public void adjust(string name)
	{
		float xpos;
		float.TryParse (name.Remove (1),out xpos);
		pos=this.transform.position;
		pos.x = xpos-1f;
		print (xpos);
		end = pos;
		end.Set (pos.x, -10f, pos.z);
		startTime = Time.time;
		journeyLength = Vector3.Distance(pos, end);
	}
	public bool getMove()
	{
		return move;
	}
}
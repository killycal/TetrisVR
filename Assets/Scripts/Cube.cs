using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {
	private bool orphan=false;
	private int num;
	private Vector3 pos;
	private Vector3 end;
	private float speed = 10f;
	private float startTime;
	private float journeyLength;
	private bool move=false;
	private float lineno=-1;
	public Lines horizontal;

	void Start () {
		horizontal = GameObject.Find ("Horizontal").GetComponent<Lines> ();
	}
	

	void FixedUpdate () {
		if (!orphan) {
			if (!this.gameObject.GetComponentInParent<Movement> ().getMove ()) {
				this.gameObject.transform.parent = GameObject.Find("Cubes").transform;
				orphan = true;
				this.gameObject.name = "cube";
			}
		}
		else
		{
			if ((this.gameObject.transform.position.y-end.y<.01)&&(move==true)){
				horizontal.toDestroy = 0;
				move=false;
				horizontal.destroy.Clear();
			}
		    else if (move == true) {
			//this.gameObject.name = "Cube";
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			this.transform.position = Vector3.Lerp (pos, end, fracJourney);
			} 
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
		lineno = int.Parse (line);
		this.gameObject.transform.SetParent (GameObject.Find (other.gameObject.name+"x").GetComponent<Transform> ());
	}
	private void adjust(float input)
	{
		int count = 0;
		for (int i = 0; i < GameObject.Find ("Horizontal").GetComponent<Lines> ().destroy.Count; i++) {
			if (lineno > GameObject.Find ("Horizontal").GetComponent<Lines> ().destroy[i])
				count++;
		move = true;
		pos = this.gameObject.transform.position;
		end.Set (pos.x, pos.y - 1f *count, pos.z);
		startTime = Time.time;
		journeyLength = Vector3.Distance (pos, end);
	}
}
}
 
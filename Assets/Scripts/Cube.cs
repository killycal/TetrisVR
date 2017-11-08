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
	private int lineno=-1;
	private Lines horizontal;
	public List<float> ypos = new List<float>();
	private Animator anim;
	private bool shoots = false;
	public GameObject projectile;
	public bool inAir = false;

	void Start () {
		horizontal = GameObject.Find ("Horizontal").GetComponent<Lines> ();
		initYPos ();
		anim = this.gameObject.GetComponent<Animator> ();
		if (Random.Range (0, 20) % 20 == 1)
			shoots = true;
	}

	void FixedUpdate () {
		if (!orphan) {
			if (!this.gameObject.GetComponentInParent<Movement> ().getMove ()) {
				Destroy (this.gameObject.GetComponent<Animator> ());
				this.gameObject.transform.parent = GameObject.Find ("Cubes").transform;
				orphan = true;
				this.gameObject.name = "cube";
				BroadcastMessage ("kys",null,SendMessageOptions.DontRequireReceiver);
			} else if (shoots){
				if (inAir == false) {
					Vector3 thing = this.transform.position;
					Instantiate (projectile, this.transform, false);
					inAir = true;
				}
			}
		}
		else
		{
			if ((this.gameObject.transform.position.y-end.y<.01)&&(move==true)){
				move=false;
				horizontal.destroy.Clear();
			}
		    else if (move == true) {
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
		else if (other.gameObject.name == "Laser")
			destruct ();
		else if (other.gameObject.name == "Arrowhead collider" && orphan == false)
			destruct();
	}
	void OnTriggerEnter(Collider other)
	{
		//print (other.gameObject.name);
		if (other.gameObject.name != "Bow Arrow" && other.gameObject.name != "FireSource" && other.gameObject.name != "Arrowhead collider" && other.gameObject.name != "Missile(Clone)") {
			string line = other.gameObject.name.Remove (0, 4);
			lineno = int.Parse (line);
			this.gameObject.transform.SetParent (GameObject.Find (other.gameObject.name + "x").GetComponent<Transform> ());
		} 
		else if (orphan == true && GameObject.Find ("Horizontal").GetComponent<Lines> ().destruction < 2) {
			Destroy (this.gameObject);
			GameObject.Find ("Horizontal").GetComponent<Lines> ().destruction++;
		}
		else if (orphan == false)
			destruct();
	}
	private void adjust()
	{
		int count = 0;
		for (int i = 0; i < GameObject.Find ("Horizontal").GetComponent<Lines> ().destroy.Count; i++) {
			if (lineno > GameObject.Find ("Horizontal").GetComponent<Lines> ().destroy [i]) {
				count++;
				move = true;
			}
		}
		pos = this.gameObject.transform.position;
		if (count > lineno)
			end.Set (pos.x, ypos [0], pos.z);
		else {
			end.Set (pos.x, ypos [lineno - count], pos.z);
		}
		startTime = Time.time;
		journeyLength = Vector3.Distance (pos, end);
		print (count);
	}
	private void adjustUp()
	{
		pos = this.gameObject.transform.position;
		end.Set (pos.x, ypos [lineno+1], pos.z);
		startTime = Time.time;
		journeyLength = Vector3.Distance (pos, end);
	}
	public void destruct()
	{
		GameObject.Find ("Back").GetComponent<SoundHandler> ().PlayDest();
		anim.SetBool ("Hit", true);
	}
	private void initYPos()
	{
		float seed = 2.5f;
		for (int i = 0; i < 23; i++) {
			ypos.Add (seed);
			seed += 1f;
		}
	}
	private void delete()
	{
		Destroy (this.gameObject);
	}
}
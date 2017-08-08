using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
 	private Lines line;
	private int num;
	private Vector3 pos;
	private Vector3 end;
	public float speed = 1.0f;
	private float startTime;
	private float endTime;
	private float journeyLength;
	private bool move=true;
	public Collider a, b, c, d;

	void Start () {
		pos=this.transform.position;
		end = pos;
		end.Set (pos.x, -10f, pos.z);
		startTime = Time.time;
		journeyLength = Vector3.Distance(pos, end);
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
		if (other.gameObject.name != "Bow Arrow" && other.gameObject.name != "FireSource" && other.gameObject.name != "Arrowhead collider") {
			move = false;

			endTime = Time.time;
		} //else {
		else {
			GameObject child = this.GetComponentInChildren<Collider>().gameObject;
			print (child.name);
			Destroy (child);
		}
		//	print (other.gameObject.name);
		//}
	}
	public bool getMove()
	{
		return move;
	}
}

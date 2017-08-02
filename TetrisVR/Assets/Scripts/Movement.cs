using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	private Vector3 pos;
	private Vector3 end;
	private bool move=true;
	public float speed = 1.0f;
	private float startTime;
	private float journeyLength;
	// Use this for initialization
	void Start () {
		pos=this.transform.position;
		end = pos;
		end.Set (pos.x, -.6f, pos.z);
		startTime = Time.time;
		journeyLength = Vector3.Distance(pos, end);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (move==true)
		{
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / journeyLength;
			//this.transform.position.Set(this.transform.position.x,pos,this.transform.position.z);
			//pos -= .1f;
			this.transform.position=Vector3.Lerp(pos,end,fracJourney);
		}
	}
	void OnCollisionEnter(Collision other)
	{
		move = false;
		print (other.gameObject);
	}
}

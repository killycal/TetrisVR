using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	private new Lines line;
	private int num;
	private Vector3 pos;
	private Vector3 end;
	public float speed = 1.0f;
	private float startTime;
	private float journeyLength;
	private bool move=true;

	void Start () {
		pos=this.transform.position;
		end = pos;
		end.Set (pos.x, -.6f, pos.z);
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
		if (Time.time-startTime>2.7/speed)
		DestroyObject(this.gameObject);
	}
	void OnCollisionEnter(Collision other)
	{
		move = false;
	}
	public bool getMove()
	{
		return move;
	}
}

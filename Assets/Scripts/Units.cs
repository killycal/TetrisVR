using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units : MonoBehaviour {
	private Vector3 pos;
	private Vector3 end;
	public float speed = 1.0f;
	private float startTime;
	private float journeyLength;
	private bool move=false;
	private float lineno=-1;
	public Lines horizontal;
	// Use this for initialization
	void Start () {
		pos=this.transform.position;
		end = pos;
		end.Set (pos.x, 0, pos.z);
		startTime = Time.time;
		journeyLength = Vector3.Distance(pos, end);
		horizontal = GameObject.Find ("Horizontal").GetComponent<Lines> ();
	}
	/*
	// Update is called once per frame
	void FixedUpdate () {
		
	}
	private void adjust(float Input)
	{
		int count = 0;

		for (int i = 0; i < GameObject.Find ("Horizontal").GetComponent<Lines> ().destroy.Count; i++) {
			if (lineno > GameObject.Find ("Horizontal").GetComponent<Lines> ().destroy [i])
				count++;
			move = true;
			pos = this.gameObject.transform.position;
			end.Set (pos.x, pos.y - .1f * count, pos.z);
			startTime = Time.time;
			journeyLength = Vector3.Distance (pos, end);
		}
	}*/
}
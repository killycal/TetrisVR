using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Go : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		GameObject.Find ("Top").GetComponent<Instantiation> ().gameOver = false;
		//BroadcastMessage ("destruct");
	}
	void OnCollisionEnter(Collision other)
	{
		GameObject.Find ("Top").GetComponent<Instantiation> ().gameOver = false;
		//BroadcastMessage ("destruct");
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class Laser : MonoBehaviour {
	private IEnumerator schedule;
	// Use this for initialization
	void Start () {
		schedule=fly ();
		StartCoroutine (schedule);

	}
	
	private IEnumerator fly()
	{
		yield return new WaitForSeconds (10);
		Destroy (this.gameObject);
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.name != "Arrowhead collider"&& other.gameObject.name != "Bow Arrow" && other.gameObject.name != "FireSource")
		Destroy (this.gameObject);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name != "Arrowhead collider"&& other.gameObject.name != "Bow Arrow" && other.gameObject.name != "FireSource")
		Destroy (this.gameObject);
	}
}

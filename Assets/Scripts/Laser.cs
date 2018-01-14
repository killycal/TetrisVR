using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class Laser : MonoBehaviour {
	private IEnumerator schedule;
	private IEnumerator schedule2;
	private bool destructible;
	// Use this for initialization
	void Start () {
		destructible = false;
		schedule=fly ();
		StartCoroutine (schedule);
		GameObject.Find ("Back").GetComponent<SoundHandler> ().PlayLaser();
	}
	
	private IEnumerator fly()
	{
		yield return new WaitForSeconds (.01f);
		destructible = true;
		yield return new WaitForSeconds (10);
		Destroy (this.gameObject);
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.name != "Arrowhead collider" && other.gameObject.name != "Bow Arrow" && other.gameObject.name != "FireSource" && destructible == true) {
			schedule2 = destroy ();
			StartCoroutine (schedule2);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name != "Arrowhead collider" && other.gameObject.name != "Bow Arrow" && other.gameObject.name != "FireSource" && destructible == true)
		{
			schedule2 = destroy ();
			StartCoroutine (schedule2);
		}
	}
	private IEnumerator destroy()
	{
		yield return new WaitForSeconds (.01f);
		Destroy (this.gameObject);
	}
}

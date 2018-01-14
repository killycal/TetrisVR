using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class Laser : MonoBehaviour {
	private IEnumerator schedule;
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
		yield return new WaitForSeconds (.1f);
		destructible = true;
		yield return new WaitForSeconds (10);
		Destroy (this.gameObject);
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.name != "Arrowhead collider"&& other.gameObject.name != "Bow Arrow" && other.gameObject.name != "FireSource"&&destructible==true)
		Destroy (this.gameObject);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name != "Arrowhead collider"&& other.gameObject.name != "Bow Arrow" && other.gameObject.name != "FireSource"&&destructible==true)
		Destroy (this.gameObject);
	}
}

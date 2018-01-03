using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {
	private IEnumerator schedule;
	private GameObject head;
	public float delay;
	// Use this for initialization
	void Start () {
		schedule=Fly ();
		head = GameObject.Find ("HeadCollider");
		StartCoroutine (schedule);
	}
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.name == "HeadCollider") {
			GameObject.Find ("Horizontal").GetComponent<Lines> ().Hit ();
			this.gameObject.GetComponent<Rigidbody> ().useGravity = true;
		}
	}
	public IEnumerator Fly()
	{
		this.gameObject.name = "Missile";
		yield return new WaitForSeconds (7f);
		this.transform.parent.GetComponent<Cube> ().inAir = false;
		GetComponent<Rigidbody> ().AddForce ((head.transform.position - this.transform.position)* 30);
		yield return new WaitForSeconds (.05f);
		this.gameObject.GetComponent<BoxCollider> ().isTrigger = false;
		yield return new WaitForSeconds (.05f);
		this.transform.parent = null;
		yield return new WaitForSeconds (15);
		kys ();
	}
	private void kys()
	{
		Destroy(this.gameObject);
	}
}

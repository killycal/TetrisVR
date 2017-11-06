using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {
	private IEnumerator schedule;
	private GameObject head;
	// Use this for initialization
	void Start () {
		schedule=Fly ();
		head = GameObject.Find ("HeadCollider");
		StartCoroutine (schedule);
	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "HeadCollider")
			GameObject.Find ("Horizontal").GetComponent<Lines> ().Hit ();

	}

	public IEnumerator Fly()
	{
		yield return new WaitForSeconds (3);
		this.transform.parent.GetComponent<Cube> ().inAir = false;
		GetComponent<Rigidbody> ().AddForce ((head.transform.position - this.transform.position)* 30);
		yield return new WaitForSeconds (.05f);
		this.transform.parent = null;
	}
	private void kys()
	{
		Destroy(this.gameObject);
	}
}

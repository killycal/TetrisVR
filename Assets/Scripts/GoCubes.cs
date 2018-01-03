using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoCubes : MonoBehaviour {
	
	private Animator anim;

	void Start () {
		anim = this.gameObject.GetComponent<Animator> ();
	}
	public void destruct()
	{
		anim.SetBool ("Hit", true);
	}
	public void unparent()
	{
		this.gameObject.transform.parent = null;
	}
	private void delete()
	{
		Destroy (this.gameObject);
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Bow Arrow" || other.gameObject.name == "FireSource" || other.gameObject.name == "Arrowhead collider"|| other.gameObject.name == "Laser") {
			GameObject.Find ("Controller").GetComponent<Instantiation> ().gameOver = false;
			destruct();
			Destroy (other.gameObject);
		}
	}
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.name == "Bow Arrow" || other.gameObject.name == "FireSource" || other.gameObject.name == "Arrowhead collider"|| other.gameObject.name == "Laser") {
			GameObject.Find ("Controller").GetComponent<Instantiation> ().gameOver = false;
			destruct();
			Destroy (other.gameObject);
		}
	}
}
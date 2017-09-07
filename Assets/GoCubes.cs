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
}
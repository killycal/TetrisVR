using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NCube : MonoBehaviour {
	private int num;
	private Vector3 pos;
	private Vector3 end;
	public List<float> ypos = new List<float>();
	private Animator anim;

	void Start () {
		anim = this.gameObject.GetComponent<Animator> ();
		if (this.gameObject.name!="ZCube")
			this.gameObject.name = "NCube";
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.name == "cube") {
			other.gameObject.GetComponent<Cube> ().destruct ();
			destruct ();
			GameObject.Find ("ZCube").GetComponent<NCube> ().destruct ();
		}
	}
	public void destruct()
	{
		GameObject.Find ("Back").GetComponent<SoundHandler> ().PlayDest();
		anim.SetBool ("Hit", true);
	}
	private void delete()
	{
		Destroy (this.gameObject);
	}
}
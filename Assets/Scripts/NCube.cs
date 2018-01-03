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
	public void destruct()
	{
		anim.SetBool ("Hit", true);
	}
	public void playDestroy()
	{
		GameObject.Find ("Back").GetComponent<SoundHandler> ().PlayDest();
	}
	private void delete()
	{
		Destroy (this.gameObject);
	}
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.name == "Plane")
			destruct ();
	}
}
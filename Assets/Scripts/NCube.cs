using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NCube : MonoBehaviour {
	private int num;
	private Vector3 pos;
	private Vector3 end;
	private float speed = 10f;
	private bool move=false;
	private int lineno=-1;
	private Lines horizontal;
	public List<float> ypos = new List<float>();
	private Animator anim;

	void Start () {
		horizontal = GameObject.Find ("Horizontal").GetComponent<Lines> ();
		anim = this.gameObject.GetComponent<Animator> ();
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.name == "cube" || other.gameObject.name == "Plane")
			move = false;
	}
	void OnTriggerEnter(Collider other)
	{
		//print (other.gameObject.name);
		/*if (other.gameObject.name != "Bow Arrow" && other.gameObject.name != "FireSource" && other.gameObject.name != "Arrowhead collider") {
			string line = other.gameObject.name.Remove (0, 4);
			lineno = int.Parse (line);
			this.gameObject.transform.SetParent (GameObject.Find (other.gameObject.name + "x").GetComponent<Transform> ());
		} 
		else
			destruct();*/
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
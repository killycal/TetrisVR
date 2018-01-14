using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DCube : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		print (other.gameObject.name);
		if (other.gameObject.name == "Laser"&&GameObject.Find("Controller").GetComponent<Instantiation>().gameOver==false) {
			int stuff = GameObject.Find ("Controller").GetComponent<Instantiation> ().count-4;

			if (GameObject.Find(stuff.ToString ()).tag=="Piece")
				GameObject.Find (stuff.ToString ()).GetComponent<Movement> ().adjust (this.gameObject.name);
			else 
				GameObject.Find (stuff.ToString ()).GetComponent<NMovement> ().adjust (this.gameObject.name);
		}
	}
}

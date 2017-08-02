using UnityEngine;
using System.Collections;
using System.Collections.Generic;





public class LineDetection : MonoBehaviour {
	private int total=0;
	private bool destroy=false;
	private IEnumerator schedule;
	void Start() {
		//schedule=generate ();
		//StartCoroutine (schedule);

	}
	void OnTriggerEnter(Collider other) {
		total++;
		if (total == 10) {
			destroy = true;
			print (destroy);
		}
	}
	void OnTriggerExit(Collider other) {
			total--;
	}
	void OnTriggerStay(Collider other){
		if ((other.attachedRigidbody.IsSleeping ())&&(destroy == true)) {
			print (total);
			Destroy(other.gameObject);
				total--;
			if (total==0) {
				destroy = false;
				print (destroy);
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDetection : MonoBehaviour {
	private int total=0;
	private bool destroy=false;
	void OnTriggerEnter(Collider other) {
		total++;
		if (total == 10) {
			destroy = true;
			print (destroy);
		}
	}
	void OnTriggerExit(Collider other) {
		if (destroy == false) {
			total--;
		}
	}
	void OnTriggerStay(Collider other){
		if (destroy == true) {
			Destroy(other.gameObject);
			if (total !=0) {
				total--;
				print (total);
			}
			if (total==0) {
				destroy = false;
				print (destroy);
			}
		}
	}
}

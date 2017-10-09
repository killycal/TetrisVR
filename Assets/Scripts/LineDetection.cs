using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


//script attached to each line

public class LineDetection : MonoBehaviour{
	private IEnumerator schedule;
	//make a list to track collided objects
	List<GameObject> cubes = new List<GameObject>();
	void Start() {

	}
	void FixedUpdate() {
		cubes.Clear(); //clear the list of all tracked objects.
	}
	void OnTriggerEnter(Collider other) {
		if (!cubes.Contains(other.gameObject)&&other.gameObject.name=="cube") //Populate list of cubes in contact with line
		{
			cubes.Add(other.gameObject);
			if (this.gameObject.name == "Line23") {
				GameObject.Find ("Controller").GetComponent<Instantiation> ().gameOver = true;
				Destroy (other.gameObject);
			}
		}
		if (cubes.Count > 9) {
			for (int i = 0; i < cubes.Count; i++)
				Destroy (cubes [i]);
			int no = int.Parse (this.gameObject.name.Remove(0,4));
			GameObject.Find("Horizontal").GetComponent<Lines>().destroy.Add(no);
			GameObject.Find ("Horizontal").BroadcastMessage("adjust");
			GameObject.Find ("Horizontal").GetComponent<Lines> ().totalDestroy++;
			GameObject.Find ("Horizontal").GetComponent<Lines> ().points += 1000*(1+GameObject.Find ("Horizontal").GetComponent<Lines> ().level*(1/4));
			GameObject.Find ("Text").GetComponent<Text> ().text = GameObject.Find ("Horizontal").GetComponent<Lines> ().points.ToString();
			GameObject.Find ("Back").GetComponent<SoundHandler> ().PlayLine();
		}

	}
	void OnTriggerStay(Collider other){
		OnTriggerEnter (other);
	}

}

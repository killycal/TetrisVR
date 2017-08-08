using UnityEngine;
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
		}
		if (cubes.Count > 9) {
			GameObject.Find ("Horizontal").GetComponent<Lines> ().toDestroy++;
			for (int i = 0; i < cubes.Count; i++)
				Destroy (cubes [i]);
			float no;
			string line=this.gameObject.name;
			no = int.Parse (line.Remove(0,4));
			GameObject.Find("Horizontal").GetComponent<Lines>().destroy.Add((int)no);
			GameObject.Find ("Horizontal").BroadcastMessage("adjust", no);
			//BroadcastMessage(
			//for (int i = no; i < 24; i++) {
			//	string l="Line{0}";
			//	l = string.Format (l, i.ToString());
				//print (l);
				//GameObject.Find (l).GetComponent<Cube> ().setMove (true);
			//}
		}

	}
	void OnTriggerStay(Collider other){
		OnTriggerEnter (other);
	}

}

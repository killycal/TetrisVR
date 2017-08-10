using UnityEngine;
using System.Collections;
public class Instantiation : MonoBehaviour {
	public GameObject O,I,I2,J,J2,J3,J4,L,L2,L3,L4,Z,Z2,S,S2,T,T2,T3,T4;
	public Vector3 zero,one,two,three,four,five,six,seven,eight,nine,ten;
	private Time time;
	private IEnumerator schedule;
	private float speed=2.0f;
	void Start() {
		schedule=generate ();
		StartCoroutine (schedule);

	}
	public IEnumerator generate(){
		//one=GameObject.Find("10").transform;
		//Instantiate(I, one, Quaternion.identity);
		float h=8.0f;
		speed=GameObject.Find ("Horizontal").GetComponent<Lines> ().speed;
		for (int i = 0; i < 10; i++) {
			
			Instantiate(Z2, one, Quaternion.identity);
			yield return new WaitForSeconds (h/speed);
			Instantiate (T4, one, Quaternion.identity);
			yield return new WaitForSeconds (h/speed);
			Instantiate (I, three, Quaternion.identity);
			yield return new WaitForSeconds (h/speed);
			Instantiate (S, four, Quaternion.identity);
			yield return new WaitForSeconds (h/speed);
			Instantiate(L2, three, Quaternion.identity);
			yield return new WaitForSeconds (h/speed);
			Instantiate (J2, five, Quaternion.identity);
			yield return new WaitForSeconds (h/speed);
			Instantiate (O, six, Quaternion.identity);
			yield return new WaitForSeconds (h/speed);
			Instantiate (I, zero, Quaternion.identity);
			yield return new WaitForSeconds (h/speed);
			Instantiate (L3, eight, Quaternion.identity);
			yield return new WaitForSeconds (h/speed);
		}

	}
	//public void ChangeSpeed(float change){
	//	speed=GameObject.Find ("Horizontal").GetComponent<Lines> ().speed+change;
	//}
}
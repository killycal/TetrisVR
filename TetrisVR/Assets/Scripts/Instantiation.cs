using UnityEngine;
using System.Collections;
public class Instantiation : MonoBehaviour {
	public GameObject O,I,I2,J,J2,J3,J4,L,L2,L3,L4,Z,Z2,S,S2,T,T2,T3,T4;
	public Vector3 zero,one,two,three,four,five,six,seven,eight,nine,ten;
	private Time time;
	private IEnumerator schedule;
	void Start() {
		schedule=generate ();
		StartCoroutine (schedule);

	}
	public IEnumerator generate(){
		//one=GameObject.Find("10").transform;
		yield return new WaitForSeconds (0);
		//Instantiate(I, one, Quaternion.identity);

		for (int i = 0; i < 10; i++) {
			
			Instantiate(Z2, one, Quaternion.identity);
			yield return new WaitForSeconds (1);
			Instantiate (T4, one, Quaternion.identity);
			yield return new WaitForSeconds (1);
			Instantiate (I, three, Quaternion.identity);
			yield return new WaitForSeconds (1);
			Instantiate (S, four, Quaternion.identity);
			yield return new WaitForSeconds (1);
			Instantiate(L2, three, Quaternion.identity);
			yield return new WaitForSeconds (1);
			Instantiate (J2, five, Quaternion.identity);
			//yield return new WaitForSeconds (1);
			//Instantiate (I, six, Quaternion.identity);
			yield return new WaitForSeconds (1);
			//Instantiate (I, seven, Quaternion.identity);
			//yield return new WaitForSeconds (1);
			Instantiate (O, six, Quaternion.identity);
			//yield return new WaitForSeconds (1);
			//Instantiate (I, nine, Quaternion.identity);
			yield return new WaitForSeconds (1);
			Instantiate (I, zero, Quaternion.identity);
			yield return new WaitForSeconds (1);
			Instantiate (L3, eight, Quaternion.identity);
			yield return new WaitForSeconds (1);
		}

	}
}

/*for (int y = 0; y < 5; y++) {	for (int x = 0; x < 5; x++) {
 * GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.AddComponent<Rigidbody>();
				cube.transform.position = new Vector3(x, y, 0);
			}
		}*/
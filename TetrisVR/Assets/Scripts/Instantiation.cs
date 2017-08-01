using UnityEngine;
using System.Collections;
public class Instantiation : MonoBehaviour {
	public GameObject O,I,J,L,Z,S,T;
	public Vector3 one,two,three,four,five,six,seven,eight,nine,ten;
	private Time time;
	private IEnumerator schedule;
	void Start() {
		schedule=generate ();
		StartCoroutine (schedule);

	}
	public IEnumerator generate(){
		//one=GameObject.Find("10").transform;
		yield return new WaitForSeconds (3);
		Instantiate(O, one, Quaternion.identity);
		yield return new WaitForSeconds (1);
		//Instantiate(I, two, Quaternion.identity);

		Instantiate(O, four, Quaternion.identity);
		yield return new WaitForSeconds (1);
		Instantiate(J, three, Quaternion.identity);
		yield return new WaitForSeconds (1);
		//Instantiate(I, five, Quaternion.identity);
		Instantiate(I, six, Quaternion.identity);
		yield return new WaitForSeconds (1);
		Instantiate(I, seven, Quaternion.identity);
		yield return new WaitForSeconds (1);
		Instantiate(I, eight, Quaternion.identity);
		yield return new WaitForSeconds (1);
		Instantiate(I, nine, Quaternion.identity);
		yield return new WaitForSeconds (1);
		Instantiate(I, ten, Quaternion.identity);
		yield return new WaitForSeconds (1);
	}
}

/*for (int y = 0; y < 5; y++) {	for (int x = 0; x < 5; x++) {
 * GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.AddComponent<Rigidbody>();
				cube.transform.position = new Vector3(x, y, 0);
			}
		}*/
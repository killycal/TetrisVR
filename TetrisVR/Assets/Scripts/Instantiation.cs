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
		GameObject a=Instantiate(J2, two, Quaternion.identity);
		//a.transform.RotateAround (one,Vector3.left, 2f);
		yield return new WaitForSeconds (1);
		Instantiate(I, two, Quaternion.identity);

	}
}

/*for (int y = 0; y < 5; y++) {	for (int x = 0; x < 5; x++) {
 * GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.AddComponent<Rigidbody>();
				cube.transform.position = new Vector3(x, y, 0);
			}
		}*/
using UnityEngine;
public class Instantiation : MonoBehaviour {
	public GameObject O,I,J,L,Z,S,T;
	public Transform one,two,three,four,five,six,seven,eight,nine,ten;
	void Start() {
		/*for (int y = 0; y < 5; y++) {
			for (int x = 0; x < 5; x++) {
				GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.AddComponent<Rigidbody>();
				cube.transform.position = new Vector3(x, y, 0);
			}
		}*/one=GameObject.Find("10").transform;
		print (one);
		Instantiate(O, one.position, Quaternion.identity, one);
	}
}
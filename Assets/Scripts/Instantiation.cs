using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class Instantiation : MonoBehaviour {
	public GameObject O,I,I2,J,J2,J3,J4,L,L2,L3,L4,Z,Z2,S,S2,T,T2,T3,T4,GO;
	public List<GameObject> NegBlock= new List<GameObject>();
	public Vector3[] position;
	private Time time;
	private IEnumerator schedule;
	private float speed=2.0f;
	public bool gameOver=false;
	public List<GameObject> blocks = new List<GameObject>();
	public Vector3[] powerupPosition;
	public GameObject D;
	private bool playing=false;
	void StartIt() {
		schedule=generate ();
		GameObject.Find ("Horizontal").GetComponent<Lines> ().ResetWorld ();
		StartCoroutine (schedule);
	}
	void Update()
	{
		if (playing == false) {
			if (gameOver == false) {
				Destroy(GameObject.Find("Longbow"));
				GameObject.Find ("Horizontal").GetComponent<Lines> ().points =0;
				GameObject.Find ("Text").GetComponent<Text> ().text = GameObject.Find ("Horizontal").GetComponent<Lines> ().points.ToString();
				StartIt ();
				playing = true;
			}
		}
		Destroy (GameObject.Find("Arrow Scale Parent"));
	}
	public IEnumerator generate(){
		float h=8.0f;
		speed=GameObject.Find ("Horizontal").GetComponent<Lines> ().speed;
		GameObject.Find ("Back").GetComponent<SoundHandler> ().PlaySong ();
		GameObject[] cubes = GameObject.FindGameObjectsWithTag ("Cube");
		for (int i=0; i< cubes.Length; i++)
			Destroy (cubes[i]);
		Batch ();
		int p = Random.Range(0,9);
		int q = 0;
		Vector3 pos;
		for (int i = 0; i < 7; i++) {
			if (i % 2 == 0)
				q = p % 10;
			else
				q = (Mathf.Abs ((p % 10) - 9));
			pos = checkPos (q, i);
			pos.y = 26 + i * 8;
			Instantiate (blocks [i], pos, Quaternion.identity);
			p++;
		}
		yield return new WaitForSeconds ((h / speed)+15f/speed);
		while (!gameOver) {
			Batch ();
			for (int j = 0; j < 1; j++) {
				if (gameOver)
					break;
				for (int i = 0; i < 7; i++) {
					if (gameOver)
						break;
					if (i % 2 == 0)
						q = p % 10;
					else 
						q=(Mathf.Abs((p%10)-9));
					
					Instantiate (blocks [i], checkPos(q,i), Quaternion.identity);
					if (gameOver)
						break;
					yield return new WaitForSeconds (h / speed);
					p++;
				}
				if (gameOver)
					break;
				Instantiate (NegBlock[Random.Range(0, NegBlock.Count)], position [Random.Range(0,8)], Quaternion.identity);
				Instantiate (D, powerupPosition[j%2], Quaternion.identity);
				yield return new WaitForSeconds (h / speed);
			}
		}
		GameObject[] pieces=GameObject.FindGameObjectsWithTag ("Piece");
		for (int i=0; i< pieces.Length; i++)
			Destroy (pieces[i]);
		Instantiate(GO, new Vector3(.5f, 12.8f, -1.2f), Quaternion.identity);
		playing = false;
		GameObject.Find ("Back").GetComponent<SoundHandler> ().Stop ();

	}
	private Vector3 checkPos(int q, int i)
	{
		if (blocks [i] == I2) {
			if (q > 6)
				return position [6];
			else
				return position [q];
		} else if (blocks [i] == L2 || blocks [i] == J2 || blocks [i] == L4 || blocks [i] == J4 || blocks [i] == Z || blocks [i] == S || blocks [i] == T || blocks [i] == T3) {
			if (q > 7)
				return position [7];
			else
				return position [q];
		}
		else if (blocks [i] == NegBlock[0] ||blocks [i] == NegBlock[1] || blocks [i] == NegBlock[2] || blocks [i] == NegBlock[3] || blocks [i] == NegBlock[4]|| blocks [i] == NegBlock[5]) {
				if (q > 7)
					return position [7];
				else
					return position [q];
			}
		else if (blocks [i] != I) {
			if (q > 8)
				return position [8];
			else
				return position [q];
		} 
		else
			return position [q];
	}
	private void Batch()
	{
		blocks.Clear ();
		List<int> IDs=new List<int>();
		for (int i = 0; i < 7; i++) {
			int rando=((int)(Random.Range (0, 8)));
			if (IDs.Contains(rando))
				rando=((int)(Random.Range (0, 8)));
			IDs.Add (rando);
		}
		for (int i = 0; i < 7; i++)
			blocks.Add (intToGO (IDs [i]));
	}
	private GameObject intToGO(int rand)
	{
		GameObject piece;
		int rot = Random.Range (0, 3);
		if (rand == 0) {
			if (rot == 1 || rot == 2)
				piece = I;
			else
				piece = I2;
		} else if (rand == 1) {
			if (rot == 0)
				piece = J;
			else if (rot == 1)
				piece = J2;
			else if (rot == 2)
				piece = J3;
			else
				piece = J4;
		} else if (rand == 2) {
			if (rot == 0)
				piece = L;
			else if (rot == 1)
				piece = L2;
			else if (rot == 2)
				piece = L3;
			else
				piece = L4;
		} else if (rand == 3)
			piece = O;
		else if (rand == 4) {
			if (rot == 1 || rot == 2)
				piece = S;
			else
				piece = S2;
		} else if (rand == 5) {
			if (rot == 0)
				piece = T;
			else if (rot == 1)
				piece = T2;
			else if (rot == 2)
				piece = T3;
			else
				piece = T4;
		} else if (rand == 6) {
			piece = NegBlock [Random.Range (0, NegBlock.Count)];
		}
		else {
			if (rot == 1 || rot==2)
				piece = Z;
			else
				piece = Z2;
		}
		return piece;

	}
}
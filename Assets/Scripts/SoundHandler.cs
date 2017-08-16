using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class SoundHandler : MonoBehaviour
{
	public AudioClip[] sounds;
	public void PlayDest()
	{	
		//GetComponent<AudioSource>().Stop();
		GetComponent<AudioSource> ().PlayOneShot (sounds [Random.Range(1,3)],1);
	}
	public void PlayLine()
	{
		GetComponent<AudioSource> ().PlayOneShot (sounds [0],1);
	}
}
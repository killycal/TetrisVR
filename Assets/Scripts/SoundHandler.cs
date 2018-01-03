using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class SoundHandler : MonoBehaviour
{
	public AudioClip[] sounds;
	public AudioClip song;
	public AudioClip laser;
	public void PlayDest()
	{	
		//GetComponent<AudioSource>().Stop();
		GetComponent<AudioSource> ().PlayOneShot (sounds [Random.Range(1,3)],1);
	}
	public void PlayLaser()
	{
		GetComponent<AudioSource> ().PlayOneShot (laser);
	}
	public void PlayLine()
	{
		GetComponent<AudioSource> ().PlayOneShot (sounds [0],1);
	}
	public void PlaySong()
	{
		GetComponent<AudioSource> ().PlayOneShot (song);
	}
	public void Stop()
	{
		GetComponent<AudioSource> ().Stop ();
	}
}
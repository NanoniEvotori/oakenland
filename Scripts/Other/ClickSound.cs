using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class ClickSound : MonoBehaviour {

	public AudioClip sound;
	public AudioSource source;

	void Start () 
	{
		source.clip = sound;
		source.playOnAwake = false;
	}

	public void PlaySound()
	{
		source.PlayOneShot (sound);
	}
}

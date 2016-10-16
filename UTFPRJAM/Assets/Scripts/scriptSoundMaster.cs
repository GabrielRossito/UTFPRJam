using UnityEngine;
using System.Collections;

public class scriptSoundMaster : MonoBehaviour {

	public int soundIndex = -1;
	public AudioClip[] sounds;

	private AudioSource source;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (soundIndex >= 0) {
			source = GetComponent<AudioSource> ();
			source.PlayOneShot (sounds[soundIndex], 1);
			soundIndex = -1;
		}
	}
}

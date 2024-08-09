using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]



/**

CREDIT FOR THE AUDIO MANAGER GOES TO BRACKEYS!

HE MADE THE AUDIO MANAGER! YOU CAN FIND IT HERE!
https://old.brackeys.com/wp-content/FilesForDownload/AudioManager.zip

thank u

*/
public class Sound {

	public string name;

	public AudioClip clip;

	[Range(0f, 1f)]
	public float volume = .75f;
	[Range(0f, 1f)]
	public float volumeVariance = .1f;

	[Range(.1f, 3f)]
	public float pitch = 1f;
	[Range(0f, 1f)]
	public float pitchVariance = .1f;

	public bool loop = false;

	public AudioMixerGroup mixerGroup;

	[HideInInspector]
	public AudioSource source;

}

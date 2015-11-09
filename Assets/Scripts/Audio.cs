using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour {

    private AudioSource sound;

    void Awake()
    {
        sound = (AudioSource)gameObject.AddComponent<AudioSource>();

        AudioClip audioClip = (AudioClip)Resources.Load("Sound/sound");

        sound.clip = audioClip;

        sound.Play();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

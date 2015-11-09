using UnityEngine;
using System.Collections;

public class Canvas : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void play()
    {
        Application.LoadLevel(1);
    }

    public void quit()
    {
        Application.Quit();
    }
}

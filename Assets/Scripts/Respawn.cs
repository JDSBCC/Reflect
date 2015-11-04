using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {

    public GameObject minion;
    private bool init = false;

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log();
        if (GetComponentInChildren<MeshRenderer>().isVisible && !init)
        {
            Instantiate(minion, new Vector3(-200, 0, -200), Quaternion.identity);
            Instantiate(minion, new Vector3(200, 0, -200), Quaternion.identity);
            Instantiate(minion, new Vector3(-200, 0, 200), Quaternion.identity);
            Instantiate(minion, new Vector3(200, 0, 200), Quaternion.identity);
            init = true;
        }

    }
}

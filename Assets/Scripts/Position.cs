using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {
    public GameObject go;

	// Use this for initialization
	void Start (){

    }
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        /*if (go.GetComponentInChildren<MeshRenderer>().enabled) {
            transform.position = new Vector3(transform.position.x, go.transform.position.y + transform.GetComponent<MeshRenderer>().bounds.size.y / 2, transform.position.z);
        }*/
    }
}

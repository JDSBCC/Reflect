using UnityEngine;
using System.Collections;

public class BaseHealth : MonoBehaviour {

    public GameObject healthPoint;
    private int health = 0;
    private bool init = false;

	// Use this for initialization
	void Start (){
    }

    void drawHealthPoint(){
        if (health != 10){
            Instantiate(healthPoint, new Vector3(transform.position.x - 50, 100, transform.position.z - 50), Quaternion.identity);
            health++;
            Invoke("drawHealthPoint", 2.0f);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (GetComponentInChildren<MeshRenderer>().isVisible && !init){
            drawHealthPoint();
            init = true;
        }
    }
}

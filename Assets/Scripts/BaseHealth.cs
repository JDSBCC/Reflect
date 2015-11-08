using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour {

    public GameObject healthPoint;
    private int health = 0;
    private bool init = false;
    private GameObject []points;

    private int minionKilled = 0;

    // Use this for initialization
    void Start (){
        points = new GameObject[10];
    }

    void drawHealthPoint(){
        if (health != 10){
            points[health] = Instantiate(healthPoint, new Vector3(transform.position.x - 50, 100, transform.position.z - 50), Quaternion.identity) as GameObject;
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

    public int getHealth()
    {
        return health;
    }

    public void setMinionKilled()
    {
        minionKilled++;
    }

    public void decreaseBaseHealth(){
        Destroy(points[health-1]);
        health--;
        if (health==0){
            GameObject.FindGameObjectWithTag("Info").GetComponentInChildren<Text>().text = "Minions killed: "+ minionKilled;
            Time.timeScale = 0;
        }
    }
}

using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {

    public GameObject minion;
    public GameObject minion_red;
    public GameObject minion_green;
    public GameObject minion_blue;
    private bool init = false;
    private static int[] choices = {200, -200};

    public int minionCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update (){
        //Debug.Log();
        if (GetComponentInChildren<MeshRenderer>().isVisible && !init){
            StartCoroutine(SpawnWaves());
            init = true;
        }

    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < minionCount; i++)
            {
                Vector3 spawnPosition = new Vector3(choices[Random.Range(0, 2)], 0, choices[Random.Range(0, 2)]);
                Instantiate(minion, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }
}
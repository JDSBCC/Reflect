using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {

    public GameObject minion;
    public GameObject minion_red;
    public GameObject minion_green;
    public GameObject minion_blue;
    private bool init = false;
    private static int[] choices = {200, -200};
    private static int[] minionChoices = {0, 0, 0, 0, 1, 2, 3, 1, 2, 3};

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
        if (GetComponentInChildren<MeshRenderer>().isVisible && !init && GetComponent<BaseHealth>().getHealth()==10){
            StartCoroutine(SpawnWaves());
            init = true;
        }

    }

    IEnumerator SpawnWaves(){
        yield return new WaitForSeconds(startWait);
        while (true){
            for (int i = 0; i < minionCount; i++){
                Vector3 spawnPosition = new Vector3(choices[Random.Range(0, 2)], 0, choices[Random.Range(0, 2)]);
                summon(spawnPosition);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            minionCount += 2;
        }
    }

    void summon(Vector3 spawnPosition){
        int value = minionChoices[Random.Range(0, 10)];
        switch (value){
            case 0:
                Instantiate(minion, spawnPosition, Quaternion.identity);
                break;
            case 1:
                Instantiate(minion_red, spawnPosition, Quaternion.identity);
                break;
            case 2:
                Instantiate(minion_green, spawnPosition, Quaternion.identity);
                break;
            case 3:
                Instantiate(minion_blue, spawnPosition, Quaternion.identity);
                break;
        }
    }
}
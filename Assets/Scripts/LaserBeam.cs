using UnityEngine;
using System.Collections;
using System;

public class LaserBeam : MonoBehaviour
{
    public Color laserColor = Color.red;
    public int laserDistance = 100;
    public float initialWidth = 0.02f, finalWidth = 0.1f;
    private GameObject colliderLight;
    private Vector3 lightPosition;
    private LineRenderer lineRenderer;

    public GameObject sparks;
    private GameObject sparksInstance;

    void Start(){
        lightPosition = new Vector3(0, 0, finalWidth);

        //creating laser
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetColors(laserColor, laserColor);
        lineRenderer.SetWidth(initialWidth, finalWidth);
        lineRenderer.SetVertexCount(2);

        Vector3 laserFinalPoint = transform.position + transform.forward * laserDistance;
        sparksInstance = Instantiate(sparks, laserFinalPoint, Quaternion.identity) as GameObject;
    }

    void Update()
    {
        Vector3 laserFinalPoint = transform.position + transform.forward * laserDistance;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward);
        Array.Sort(hits, CompareRaycastDistances);

        if (hits.Length!=0){//when laser collides with some object
            if (hits[0].transform.gameObject.CompareTag("Mirror")){//if the object is a mirror
                reflect(hits[0]);
            }else if (hits[0].transform.gameObject.CompareTag("Crystal")){//if the object is a crystal
                if (hits.Length>1 && hits[1].transform.gameObject.CompareTag("Mirror")){// if is a mirror that passes at crystal before
                    reflect(hits[1]);
                }else{
                    noReflection(laserFinalPoint, false);
                }
            }else{
                Debug.Log("collide with other thing");
                noReflection(hits[0].point, true);
            }
            changeLaserColor(hits[0].transform.parent.gameObject);
        }else{
            noReflection(laserFinalPoint, false);
            changeLaserColor(new GameObject());
        }
    }

    void reflect(RaycastHit collisionPoint){
        Vector3 finalPoint = Vector3.Reflect((collisionPoint.point - lightPosition).normalized, collisionPoint.normal);
        lineRenderer.SetVertexCount(3);
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, collisionPoint.point);
        lineRenderer.SetPosition(2, finalPoint * laserDistance);

        RaycastHit[] hits = Physics.RaycastAll(collisionPoint.point, finalPoint);
        Array.Sort(hits, CompareRaycastDistances);

        if (hits.Length!=0){
            for (int i = 0; i<hits.Length; i++){
                if (hits[i].transform.CompareTag("Minion")){
                    sparksInstance.transform.position = hits[i].point;
                    sparksInstance.transform.LookAt(hits[i].transform.position);

                    Minion minion = hits[i].transform.gameObject.GetComponentInParent<Minion>();
                    if (minion.decreaseHealth()){
                        sparksInstance.transform.position = finalPoint * laserDistance;
                        minion.die();
                    }
                    lineRenderer.SetPosition(2, hits[i].point);
                    break;
                }
            }
        }else{
            sparksInstance.transform.position = finalPoint * laserDistance;
        }
    }
    
    void noReflection(Vector3 laserFinalPoint, bool withSpark){
        lineRenderer.SetVertexCount(2);
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, laserFinalPoint);
        if (withSpark){
            sparksInstance.transform.position = laserFinalPoint;
            sparksInstance.transform.LookAt(laserFinalPoint);
        }
    }

    static int CompareRaycastDistances(RaycastHit x, RaycastHit y)
    {
        if (x.distance > y.distance)
            return 1;
        if (x.distance < y.distance)
            return -1;
        return 0;
    }

    void changeLaserColor(GameObject go){
        if (go.name == "crystal_red"){
            lineRenderer.SetColors(Color.red, Color.red);
        }else if (go.name == "crystal_green"){
            lineRenderer.SetColors(Color.green, Color.green);
        }else if (go.name == "crystal_blue"){
            lineRenderer.SetColors(Color.blue, Color.blue);
        }else{
            lineRenderer.SetColors(Color.white, Color.white);
        }

    }
}
  
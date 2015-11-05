using UnityEngine;
using System.Collections;
public class LaserBeam : MonoBehaviour
{
    public Color laserColor = Color.red;
    public int laserDistance = 100;
    public float initialWidth = 0.02f, finalWidth = 0.1f;
    private GameObject colliderLight;
    private Vector3 lightPosition;
    LineRenderer lineRenderer;

    public GameObject sparks;
    private GameObject sparksInstance;

    void Start()
    {
        colliderLight = new GameObject();
        colliderLight.AddComponent<Light>();
        colliderLight.GetComponent<Light>().intensity = 8;
        colliderLight.GetComponent<Light>().bounceIntensity = 8;
        colliderLight.GetComponent<Light>().range = finalWidth * 2;
        colliderLight.GetComponent<Light>().color = laserColor;
        lightPosition = new Vector3(0, 0, finalWidth);

        //creating laser
        lineRenderer = initLaser(gameObject);
        Vector3 laserFinalPoint = transform.position + transform.forward * laserDistance;
        sparksInstance = Instantiate(sparks, laserFinalPoint, Quaternion.identity) as GameObject;
    }

    void Update()
    {
        Vector3 laserFinalPoint = transform.position + transform.forward * laserDistance;
        RaycastHit collisionPoint;

        if (Physics.Raycast(transform.position, transform.forward, out collisionPoint, laserDistance)){//when laser collides with some object
            if (collisionPoint.transform.gameObject.CompareTag("Mirror")){
                reflect(collisionPoint);
            }
            else{
                noReflection(collisionPoint.point);
            }
        }else{
            noReflection(laserFinalPoint);
        }
    }

    void reflect(RaycastHit collisionPoint){
        RaycastHit  collisionPoint2;

        Vector3 finalPoint = Vector3.Reflect((collisionPoint.point - lightPosition).normalized, collisionPoint.normal);
        lineRenderer.SetVertexCount(3);
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, collisionPoint.point);
        lineRenderer.SetPosition(2, finalPoint * laserDistance);
        colliderLight.transform.position = collisionPoint.point;

        if (Physics.Raycast(collisionPoint.point, finalPoint, out collisionPoint2, laserDistance)){

            sparksInstance.transform.position = collisionPoint2.point;
            sparksInstance.transform.LookAt(collisionPoint2.transform.position);

            Debug.Log("HALO");
            if (collisionPoint2.transform.CompareTag("Minion"))
            {
                Minion minion = collisionPoint2.transform.gameObject.GetComponentInParent<Minion>();
                if (minion.decreaseHealth()){
                    minion.die();
                }
                lineRenderer.SetPosition(2, collisionPoint2.point);
                colliderLight.transform.position = collisionPoint2.point;
            }
        }else{
            sparksInstance.transform.position = new Vector3(-4000,-4000,-4000);
            Debug.Log("!!");
        }
    }
    
    void noReflection(Vector3 laserFinalPoint){
        lineRenderer.SetVertexCount(2);
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, laserFinalPoint);
        colliderLight.transform.position = laserFinalPoint - lightPosition;
        sparksInstance.transform.position = laserFinalPoint;
        sparksInstance.transform.LookAt(laserFinalPoint);
    }

    LineRenderer initLaser(GameObject go)
    {
        LineRenderer lr;
        lr = go.AddComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Additive"));
        lr.SetColors(laserColor, laserColor);
        lr.SetWidth(initialWidth, finalWidth);
        lr.SetVertexCount(2);
        return lr;
    }
}
  
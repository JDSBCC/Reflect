using UnityEngine;
using System.Collections;
public class LaserBeam : MonoBehaviour
{
    public Color laserColor = Color.red;
    public int laserDistance = 100;
    public float initialWidth = 0.02f, finalWidth = 0.1f;
    private GameObject colliderLight;
    private Vector3 lightPosition;

    void Start()
    {
        colliderLight = new GameObject();
        colliderLight.AddComponent<Light>();
        colliderLight.GetComponent<Light>().intensity = 8;
        colliderLight.GetComponent<Light>().bounceIntensity = 8;
        colliderLight.GetComponent<Light>().range = finalWidth * 2;
        colliderLight.GetComponent<Light>().color = laserColor;
        lightPosition = new Vector3(0, 0, finalWidth);
        //
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.SetColors(laserColor, laserColor);
        lineRenderer.SetWidth(initialWidth, finalWidth);
        lineRenderer.SetVertexCount(2);
    }
    void Update()
    {
        Debug.Log(transform.position);
        Vector3 laserFinalPoint = transform.position + transform.forward * laserDistance;
        RaycastHit collisionPoint;
        if (Physics.Raycast(transform.position, transform.forward, out collisionPoint, laserDistance))
        {
            GetComponent<LineRenderer>().SetPosition(0, transform.position);
            GetComponent<LineRenderer>().SetPosition(1, collisionPoint.point);
            colliderLight.transform.position = (collisionPoint.point - lightPosition);
        }
        else
        {
            GetComponent<LineRenderer>().SetPosition(0, transform.position);
            GetComponent<LineRenderer>().SetPosition(1, laserFinalPoint);
            colliderLight.transform.position = laserFinalPoint;
        }
    }
}
  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallexEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;
    Vector2 startingPosition;
    Vector2 camMoveSinceStart=>(Vector2)cam.transform.position- startingPosition;

    float zdistanceFromTarget => transform.position.z + followTarget.transform.position.z;
    float clippingPlane => transform.position.z + (zdistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane);
    float parallaxFactor => Mathf.Abs(zdistanceFromTarget)/ clippingPlane;

    float starting2;
    void Start()
    {

        startingPosition = transform.position;
        starting2 = transform.position.z; 
    }

    void Update()
    {
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;
        transform.position = new Vector3(newPosition.x, newPosition.y, starting2);
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARSessionOrigin))]
public class TapToPlace : MonoBehaviour
{
    [Tooltip("Prefab, который будет появляться на плоскости")]
    public GameObject prefabToPlace;

    private ARRaycastManager raycastManager;
    private ARSessionOrigin sessionOrigin;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        sessionOrigin = GetComponent<ARSessionOrigin>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;

            if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;

                Instantiate(prefabToPlace, hitPose.position, hitPose.rotation);
            }
        }
    }
}

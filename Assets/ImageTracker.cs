using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTracker : MonoBehaviour
{
    public GameObject mannequinPrefab;

    private ARTrackedImageManager trackedImageManager;
    private Dictionary<string, GameObject> instantiatedPrefabs = new Dictionary<string, GameObject>();

    private void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            GameObject newMannequin = Instantiate(mannequinPrefab, trackedImage.transform.position, trackedImage.transform.rotation);
            instantiatedPrefabs[trackedImage.referenceImage.name] = newMannequin;
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            if (instantiatedPrefabs.TryGetValue(trackedImage.referenceImage.name, out GameObject mannequin))
            {
                mannequin.transform.position = trackedImage.transform.position;
                mannequin.transform.rotation = trackedImage.transform.rotation;
            }
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            if (instantiatedPrefabs.TryGetValue(trackedImage.referenceImage.name, out GameObject mannequin))
            {
                mannequin.SetActive(false);
            }
        }
    }
}

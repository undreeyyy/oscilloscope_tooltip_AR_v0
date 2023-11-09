using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ArUcoMarkerTracker : MonoBehaviour
{

    private ARTrackedImageManager aRTrackedImageManager;

    private void Awake()
    {
        aRTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }
    private void OnEnable()
    {
        aRTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        aRTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
        {
            Debug.Log("Added: " + trackedImage.name);
        }

        foreach (var trackedImage in args.updated)
        {
            Debug.Log("Updated: " + trackedImage.name);
            //prints the distance of the markers to each other
            Debug.Log("Distance between Marker 1 and Marker 2: " + GetMarkerDistance("0", "1"));
            //prints the distance of the marker to the camera
            Debug.Log("Distance: " + GetMarkerDistance("0"));
            //prints the position of the marker
            Debug.Log("Position: " + GetMarkerPosition("0"));
            //prints the rotation of the marker
            Debug.Log("Rotation: " + GetMarkerRotation("0"));
            //prints the scale of the marker
            Debug.Log("Scale: " + GetMarkerScale("0"));

            //draw a line between the markers
            Debug.DrawLine(GetMarkerPosition("0"), GetMarkerPosition("Marker2"), Color.red);
            // draw a 1 unit circle, 5 units to the right of the marker
            Debug.DrawLine(GetMarkerPosition("0") + new Vector3(5, 0, 0), GetMarkerPosition("0") + new Vector3(5, 0, 0) + new Vector3(0, 1, 0), Color.red);

            //prints all this on screen
            GUI.Label(new Rect(10, 10, 1000, 20), "Distance between Marker 1 and Marker 2: " + GetMarkerDistance("0", "1"));
            GUI.Label(new Rect(10, 30, 1000, 20), "Distance: " + GetMarkerDistance("0"));
            GUI.Label(new Rect(10, 50, 1000, 20), "Position: " + GetMarkerPosition("0"));
            GUI.Label(new Rect(10, 70, 1000, 20), "Rotation: " + GetMarkerRotation("0"));
            GUI.Label(new Rect(10, 90, 1000, 20), "Scale: " + GetMarkerScale("0"));
        }

        foreach (var trackedImage in args.removed)
        {
            Debug.Log("Removed: " + trackedImage.name);
        }
    }

    //gets the positions of all the markers and returns them
    public Vector3 GetMarkerPosition(string markerName)
    {
        Vector3 markerPosition = new Vector3(0, 0, 0);
        foreach (var trackedImage in aRTrackedImageManager.trackables)
        {
            if (trackedImage.name == markerName)
            {
                markerPosition = trackedImage.transform.position;
            }
        }
        return markerPosition;
    }

    //calculated the distance between two markers based on the markers size and returns it
    public float GetMarkerDistance(string markerName1, string markerName2)
    {
        float markerDistance = 0;
        foreach (var trackedImage1 in aRTrackedImageManager.trackables)
        {
            if (trackedImage1.name == markerName1)
            {
                foreach (var trackedImage2 in aRTrackedImageManager.trackables)
                {
                    if (trackedImage2.name == markerName2)
                    {
                        markerDistance = Vector3.Distance(trackedImage1.transform.position, trackedImage2.transform.position);
                    }
                }
            }
        }
        return markerDistance;
    }

    //gets the rotation of the marker and returns it
    public Quaternion GetMarkerRotation(string markerName)
    {
        Quaternion markerRotation = new Quaternion(0, 0, 0, 0);
        foreach (var trackedImage in aRTrackedImageManager.trackables)
        {
            if (trackedImage.name == markerName)
            {
                markerRotation = trackedImage.transform.rotation;
            }
        }
        return markerRotation;
    }

    //gets the scale of the marker and returns it
    public Vector3 GetMarkerScale(string markerName)
    {
        Vector3 markerScale = new Vector3(0, 0, 0);
        foreach (var trackedImage in aRTrackedImageManager.trackables)
        {
            if (trackedImage.name == markerName)
            {
                markerScale = trackedImage.transform.localScale;
            }
        }
        return markerScale;
    }

    //gets the distance of the marker based on the camera and returns it
    public float GetMarkerDistance(string markerName)
    {
        float markerDistance = 0;
        foreach (var trackedImage in aRTrackedImageManager.trackables)
        {
            if (trackedImage.name == markerName)
            {
                markerDistance = Vector3.Distance(trackedImage.transform.position, Camera.main.transform.position);
            }
        }
        return markerDistance;
    }

}

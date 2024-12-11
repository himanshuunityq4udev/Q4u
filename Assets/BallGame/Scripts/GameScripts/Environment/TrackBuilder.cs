using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackBuilder : MonoBehaviour
{
    [Tooltip("List of 3D model prefabs to create the road with.")]
    public List<GameObject> roadSegments;

    [Tooltip("Number of segments in the road.")]
    public int segmentCount = 10;

    [Tooltip("Maximum retries to prevent infinite loops.")]
    public int maxRetries = 10;

    [Tooltip("Distance threshold to avoid overlapping segments.")]
    public float distanceThreshold = 10.0f;

    private List<GameObject> placedSegments = new List<GameObject>();


    private void Start()
    {
        if (!ValidateRoadSegments()) return;

        BuildRoad();
    }

    private bool ValidateRoadSegments()
    {
        if (roadSegments == null || roadSegments.Count < 2)
        {
            Debug.LogError("Please assign at least two road segment prefabs to the roadSegments list.");
            return false;
        }
        return true;
    }
    private void BuildRoad()
    {
        GameObject lastSegment = PlaceFirstSegment();

        for (int i = 1; i < segmentCount - 1; i++)
        {
            lastSegment = PlaceIntermediateSegment(lastSegment, i);
            if (lastSegment == null) break;
        }

        PlaceLastSegment(lastSegment);
    }

    private GameObject PlaceFirstSegment()
    {
        GameObject firstSegmentPrefab = roadSegments[0];
        GameObject firstSegment = Instantiate(firstSegmentPrefab, transform.position, Quaternion.identity, transform);
        placedSegments.Add(firstSegment);
        return firstSegment;
    }

    private GameObject PlaceIntermediateSegment(GameObject lastSegment, int index)
    {
        for (int attempt = 0; attempt < maxRetries; attempt++)
        {
            GameObject newSegmentPrefab = GetRandomSegmentPrefab();
            GameObject newSegment = Instantiate(newSegmentPrefab, transform.position, Quaternion.identity, transform);

            if (AlignSegment(newSegment, lastSegment) && !IsOverlap(newSegment))
            {
                placedSegments.Add(newSegment);
                return newSegment;
            }

            Destroy(newSegment);
        }

        Debug.LogWarning($"Failed to place segment at index {index} after {maxRetries} attempts.");
        return null;
    }
    private void PlaceLastSegment(GameObject lastSegment)
    {
        GameObject lastSegmentPrefab = roadSegments[roadSegments.Count - 1];
        GameObject finalSegment = Instantiate(lastSegmentPrefab, transform.position, Quaternion.identity, transform);

        if (AlignSegment(finalSegment, lastSegment))
        {
            placedSegments.Add(finalSegment);
        }
        else
        {
            Debug.LogWarning("Failed to align the last segment.");
            Destroy(finalSegment);
        }
    }

    private GameObject GetRandomSegmentPrefab()
    {
        int index = Random.Range(1, roadSegments.Count - 1);
        Debug.Log("RandomeIndex" + index);
        return roadSegments[index];
    }

    private bool AlignSegment(GameObject newSegment, GameObject lastSegment)
    {
        Transform lastEndPoint = lastSegment?.transform.Find("End");
        Transform newStartPoint = newSegment.transform.Find("Start");

        if (lastEndPoint == null || newStartPoint == null)
        {
            Debug.LogWarning("Segments should have 'Start' and 'End' child objects.");
            return false;
        }

        newSegment.transform.position = lastEndPoint.position;

        Quaternion targetRotation = Quaternion.LookRotation(lastEndPoint.forward, Vector3.up);
        newSegment.transform.rotation = targetRotation;

        Vector3 positionOffset = lastEndPoint.position - newStartPoint.position;
        newSegment.transform.position += positionOffset;

        return true;
    }

    private bool IsOverlap(GameObject newSegment)
    {
        foreach (GameObject segment in placedSegments)
        {
            if (newSegment != segment)
            {
                float distance = Vector3.Distance(newSegment.transform.position, segment.transform.position);
                if (distance < distanceThreshold)
                {
                    return true; // Overlap detected
                }
            }
        }
        return false;
    }
}

using UnityEngine;
using System.Collections.Generic;

public class RoadBuilder : MonoBehaviour
{
    [Tooltip("List of 3D model prefabs to create the road with.")]
    public List<GameObject> roadSegments;

    [Tooltip("Number of segments in the road.")]
    public int segmentCount = 10;

    [Tooltip("Maximum retries to prevent infinite loops.")]
    public int maxRetries = 10;

    [Tooltip("Maximum retries to prevent infinite loops.")]
    public float distancethreshold = 10.0f;

    private List<GameObject> placedSegments = new List<GameObject>();

    private void Start()
    {
        if (roadSegments == null || roadSegments.Count == 0)
        {
            Debug.LogError("Please assign road segment prefabs to the roadSegments list.");
            return;
        }

        BuildRoad();
    }


    private void BuildRoad()
    {
        GameObject lastSegment = null;

        // Place the first segment
        GameObject firstSegmentPrefab = roadSegments[0]; // Assuming the first prefab in the list is the "first" segment
        GameObject firstSegment = Instantiate(firstSegmentPrefab, transform.position, Quaternion.identity, transform);

        // Ensure the first segment is added to the placed segments
        placedSegments.Add(firstSegment);
        lastSegment = firstSegment;

        for (int i = 1; i < segmentCount - 1; i++) // Exclude the first and last segments
        {
            GameObject newSegment = null;
            bool placedSuccessfully = false;
            int retries = 0;

            while (!placedSuccessfully && retries < maxRetries)
            {
                retries++;

                // Pick a random model for intermediate segments
                int index = Random.Range(1, roadSegments.Count - 1); // Exclude first and last segments
                Debug.Log(index);
                GameObject segmentPrefab = roadSegments[index];
                newSegment = Instantiate(segmentPrefab, transform.position, Quaternion.identity, transform);

                if (lastSegment != null)
                {
                    Transform lastEndPoint = lastSegment.transform.Find("End");
                    Transform newStartPoint = newSegment.transform.Find("Start");

                    if (lastEndPoint != null && newStartPoint != null)
                    {
                        // Align the new segment to the last segment
                        newSegment.transform.position = lastEndPoint.position;
                        Quaternion targetRotation = Quaternion.LookRotation(lastEndPoint.forward, Vector3.up);
                        newSegment.transform.rotation = targetRotation;

                        // Adjust position after rotation to ensure alignment
                        Vector3 positionOffset = lastEndPoint.position - newStartPoint.position;
                        newSegment.transform.position += positionOffset;

                        // Check for overlap
                        if (IsOverlap(newSegment))
                        {
                            Destroy(newSegment);
                        }
                        else
                        {
                            placedSuccessfully = true;
                            placedSegments.Add(newSegment);
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Segments should have 'StartPoint' and 'EndPoint' children.");
                        Destroy(newSegment);
                        return;
                    }
                }
            }

            if (!placedSuccessfully)
            {
                Debug.LogWarning("Max retries reached. Skipping segment placement.");
                Destroy(newSegment);
                break;
            }

            lastSegment = newSegment;
        }

        // Place the last segment
        GameObject lastSegmentPrefab = roadSegments[roadSegments.Count - 1]; // Assuming the last prefab in the list is the "last" segment
        GameObject finalSegment = Instantiate(lastSegmentPrefab, transform.position, Quaternion.identity, transform);

        if (lastSegment != null)
        {
            Transform lastEndPoint = lastSegment.transform.Find("End");
            Transform finalStartPoint = finalSegment.transform.Find("Start");

            if (lastEndPoint != null && finalStartPoint != null)
            {
                // Align the last segment to the final segment
                finalSegment.transform.position = lastEndPoint.position;
                Quaternion targetRotation = Quaternion.LookRotation(lastEndPoint.forward, Vector3.up);
                finalSegment.transform.rotation = targetRotation;

                // Adjust position after rotation to ensure alignment
                Vector3 positionOffset = lastEndPoint.position - finalStartPoint.position;
                finalSegment.transform.position += positionOffset;

                placedSegments.Add(finalSegment);
            }
            else
            {
                Debug.LogWarning("Segments should have 'StartPoint' and 'EndPoint' children.");
                Destroy(finalSegment);
            }
        }
    }

    private bool IsOverlap(GameObject newSegment)
    {
        foreach (GameObject segment in placedSegments)
        {
            if (newSegment != segment)
            {
                float distance = Vector3.Distance(newSegment.transform.position, segment.transform.position);
                if (distance < distancethreshold) // Adjust threshold as needed
                {
                    return true; // Overlap detected
                }
            }
        }
        return false; // No overlap
    }
}

    //private void BuildRoad()
    //{
    //    GameObject lastSegment = null;

    //    for (int i = 0; i < segmentCount; i++)
    //    {
    //        GameObject newSegment = null;
    //        bool placedSuccessfully = false;
    //        int retries = 0;

    //        while (!placedSuccessfully && retries < maxRetries)
    //        {
    //            // Increment retries
    //            retries++;

    //            // Pick a random model from the list
    //            int index = Random.Range(0, roadSegments.Count);
    //            GameObject segmentPrefab = roadSegments[index];
    //            newSegment = Instantiate(segmentPrefab, transform.position, Quaternion.identity, transform);


    //            if (lastSegment != null)
    //            {
    //                Transform lastEndPoint = lastSegment.transform.Find("End");
    //                Transform newStartPoint = newSegment.transform.Find("Start");

    //                if (lastEndPoint != null && newStartPoint != null)
    //                {
    //                    // Align the new segment to the last segment
    //                    newSegment.transform.position = lastEndPoint.position;
    //                    Quaternion targetRotation = Quaternion.LookRotation(lastEndPoint.forward, Vector3.up);
    //                    newSegment.transform.rotation = targetRotation;

    //                    // Adjust position after rotation to ensure alignment
    //                    Vector3 positionOffset = lastEndPoint.position - newStartPoint.position;
    //                    newSegment.transform.position += positionOffset;

    //                    // Check for overlap (using a distance check for simplicity)
    //                    if (IsOverlap(newSegment))
    //                    {
    //                        Destroy(newSegment); // Destroy overlapping segment and retry
    //                    }
    //                    else
    //                    {
    //                        // No overlaps, keep this placement
    //                        placedSuccessfully = true;
    //                        placedSegments.Add(newSegment);
    //                    }
    //                }
    //                else
    //                {
    //                    Debug.LogWarning("Segments should have 'StartPoint' and 'EndPoint' children.");
    //                    Destroy(newSegment);
    //                    return;
    //                }
    //            }
    //            else
    //            {
    //                // First segment, place without overlap check
    //                placedSuccessfully = true;
    //                placedSegments.Add(newSegment);
    //            }
    //        }

    //        // If we hit max retries and couldn't place, break out to avoid infinite loops
    //        if (!placedSuccessfully)
    //        {
    //            Debug.LogWarning("Max retries reached. Skipping segment placement.");
    //            Destroy(newSegment);
    //            break;
    //        }

    //        lastSegment = newSegment;
    //    }
    //}

    // Simplified function to check if the new segment overlaps with any placed segments

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class LevelbasedTrackBuilder : MonoBehaviour
//{

//    [Tooltip("List of 3D model prefabs to create the road with.")]
//    public List<GameObject> roadSegments;

//    [Tooltip("Number of segments in the road.")]
//    public int segmentCount = 10;

//    [Tooltip("Maximum retries to prevent infinite loops.")]
//    public int maxRetries = 10;

//    [Tooltip("Distance threshold to check for overlaps.")]
//    public float distancethreshold = 10.0f;

//    [Tooltip("Player's current level or progression.")]
//    public int playerLevel = 1;

//    private List<GameObject> placedSegments = new List<GameObject>();

//    private void Start()
//    {
//        if (roadSegments == null || roadSegments.Count == 0)
//        {
//            Debug.LogError("Please assign road segment prefabs to the roadSegments list.");
//            return;
//        }

//        BuildRoad();
//    }

//    private void BuildRoad()
//    {
//        GameObject lastSegment = null;

//        // Place the first segment
//        GameObject firstSegment = InstantiateSegment(roadSegments[0]);
//        placedSegments.Add(firstSegment);
//        lastSegment = firstSegment;

//        // Place intermediate segments based on player progression
//        for (int i = 1; i < segmentCount - 1; i++)
//        {
//            GameObject newSegment = null;
//            bool placedSuccessfully = false;
//            int retries = 0;

//            while (!placedSuccessfully && retries < maxRetries)
//            {
//                retries++;
//                newSegment = InstantiateSegment(GetSegmentBasedOnProgress());

//                if (lastSegment != null)
//                {
//                    Transform lastEndPoint = lastSegment.transform.Find("End");
//                    Transform newStartPoint = newSegment.transform;

//                    if (lastEndPoint != null && newStartPoint != null)
//                    {
//                        AlignSegment(newSegment, lastEndPoint, newStartPoint);

//                        if (IsOverlap(newSegment))
//                        {
//                            Destroy(newSegment);
//                        }
//                        else
//                        {
//                            placedSuccessfully = true;
//                            placedSegments.Add(newSegment);
//                        }
//                    }
//                    else
//                    {
//                        Debug.LogWarning("Segments should have 'Start' and 'End' children.");
//                        Destroy(newSegment);
//                        return;
//                    }
//                }
//            }

//            if (!placedSuccessfully)
//            {
//                Debug.LogWarning("Max retries reached. Skipping segment placement.");
//                Destroy(newSegment);
//                break;
//            }

//            lastSegment = newSegment;
//        }

//        // Place the last segment
//        GameObject finalSegment = InstantiateSegment(roadSegments[roadSegments.Count - 1]);
//        if (lastSegment != null)
//        {
//            Transform lastEndPoint = lastSegment.transform.Find("End");
//            Transform finalStartPoint = finalSegment.transform;

//            if (lastEndPoint != null && finalStartPoint != null)
//            {
//                AlignSegment(finalSegment, lastEndPoint, finalStartPoint);
//                placedSegments.Add(finalSegment);
//            }
//            else
//            {
//                Debug.LogWarning("Segments should have 'Start' and 'End' children.");
//                Destroy(finalSegment);
//            }
//        }
//    }

//    private GameObject InstantiateSegment(GameObject segmentPrefab)
//    {
//        return Instantiate(segmentPrefab, transform.position, Quaternion.identity, transform);
//    }

//    private void AlignSegment(GameObject segment, Transform lastEndPoint, Transform newStartPoint)
//    {
//        segment.transform.position = lastEndPoint.position;

//            Quaternion targetRotation = Quaternion.LookRotation(lastEndPoint.forward, Vector3.up);
//            segment.transform.rotation = targetRotation;
    

//        // Adjust position after rotation to ensure alignment
//        Vector3 positionOffset = lastEndPoint.position - newStartPoint.position;
//        segment.transform.position += positionOffset;
//    }

//    private GameObject GetSegmentBasedOnProgress()
//    {

//        // Calculate total usable segments (excluding first and last)
//        int totalSegments = roadSegments.Count - 2; // Exclude first and last segments

//        // Ensure playerLevel affects difficulty range
//        int difficultyRange = Mathf.Clamp(playerLevel / 5, 1, totalSegments);

//        // Log diagnostic info for debugging
//        Debug.Log($"Player Level: {playerLevel}, Difficulty Range: {difficultyRange}, Total Segments: {totalSegments}");

//        // Randomly pick a segment within the allowed difficulty range
//        int randomIndex = Random.Range(1, difficultyRange + 1);
//        Debug.Log($"Selected segment index: {randomIndex}");

//        return roadSegments[randomIndex];
//    }


//    private bool IsOverlap(GameObject newSegment)
//    {
//        MeshRenderer newMeshRenderer = newSegment.GetComponent<MeshRenderer>();
//        if (newMeshRenderer == null)
//        {
//            Debug.LogWarning("No MeshRenderer found on segment, cannot check overlap.");
//            return false;
//        }

//        // Get the world space bounds of the new segment
//        Bounds newBounds = newMeshRenderer.bounds;

//        foreach (GameObject segment in placedSegments)
//        {
//            if (newSegment != segment)
//            {
//                MeshRenderer segmentMeshRenderer = segment.GetComponent<MeshRenderer>();
//                if (segmentMeshRenderer != null)
//                {
//                    Bounds segmentBounds = segmentMeshRenderer.bounds;

//                    // Check if the world bounds of the new segment intersect with the existing segment
//                    if (newBounds.Intersects(segmentBounds))
//                    {
//                        return true; // Overlap detected
//                    }
//                }
//            }
//        }
//        return false; // No overlap
//    }

//    //private bool IsOverlap(GameObject newSegment)
//    //{
//    //    foreach (GameObject segment in placedSegments)
//    //    {
//    //        if (newSegment != segment)
//    //        {
//    //            float distance = Vector3.Distance(newSegment.transform.position, segment.transform.position);
//    //            if (distance < distancethreshold)
//    //            {
//    //                return true; // Overlap detected
//    //            }
//    //        }
//    //    }
//    //    return false; // No overlap
//    //}
//}

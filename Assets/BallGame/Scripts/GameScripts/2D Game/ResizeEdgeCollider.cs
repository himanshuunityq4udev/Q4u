using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeEdgeCollider : MonoBehaviour
{
    EdgeCollider2D edgeCollider;
    // Start is called before the first frame update
    void Awake()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();
    }

    private void Start()
    {
        CreateEdgeCollider();
    }

    private void CreateEdgeCollider()
    {
        List<Vector2> edges = new List<Vector2>();
        //edges.Add(Camera.main.ScreenToWorldPoint(Vector2.zero));
        edges.Add(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)));
        edges.Add(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)));
        edges.Add(Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)));
        edges.Add(Camera.main.ScreenToWorldPoint(Vector2.zero));
        edgeCollider.SetPoints(edges);

    }
}

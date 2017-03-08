using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class NewColliderCreator : MonoBehaviour {
    private int currentPathIndex = 0;
    private PolygonCollider2D polygonCollider;
    private List<Edge> edges = new List<Edge>();
    private List<Vector2> points = new List<Vector2>();
    private Vector3[] vertices;

    private bool SameVectors(int one, int two) {
        Vector3 vone = vertices[one];
        Vector3 vtwo = vertices[two];
        if (Math.Abs(vone.x - vtwo.x) > .01 || Math.Abs(vone.y - vtwo.y) > .01) {
            return false;
        }
        else {
            return true;
        }
    }

    public void GenerateColliders(Map map, List<Edge> mapEdges) {
        edges = mapEdges;
        // Get the polygon collider (create one if necessary)
        polygonCollider = map.gameObject.GetComponent<PolygonCollider2D>();
        if (polygonCollider == null) {
            polygonCollider = map.gameObject.AddComponent<PolygonCollider2D>();
        }

        // Get the mesh's vertices for use later
        vertices = map.gameObject.GetComponent<MeshFilter>().mesh.vertices;

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        // Start edge trace
        edgeTrace(edges[0]);
        print("Edge Tracing Time: " + stopwatch.Elapsed);
    }

    void edgeTrace(Edge edge) {
        // Add this edge's vert1 coords to the point list
        points.Add(vertices[edge.vert1]);

        // Store this edge's vert2
        int vert2 = edge.vert2;

        // Remove this edge
        edges.Remove(edge);

        // Find next edge that contains vert2
        foreach (Edge nextEdge in edges) {
            if (nextEdge.vert1 == vert2 || SameVectors(nextEdge.vert1, vert2)) {
                edgeTrace(nextEdge);
                return;
            }
        }

        // No next edge found, create a path based on these points
        polygonCollider.pathCount = currentPathIndex + 1;
        polygonCollider.SetPath(currentPathIndex, points.ToArray());

        // Empty path
        points.Clear();

        // Increment path index
        currentPathIndex++;

        // Start next edge trace if there are edges left
        if (edges.Count > 0) {
            edgeTrace(edges[0]);
        }
    }
}

public class Edge {
    public int vert1;
    public int vert2;

    public Edge(int Vert1, int Vert2) {
        vert1 = Vert1;
        vert2 = Vert2;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ColliderCreator : MonoBehaviour {
    private int currentPathIndex = 0;
    private PolygonCollider2D polygonCollider;
    private List<Edge> edges = new List<Edge>();
    private List<Vector2> points = new List<Vector2>();
    private Vector3[] vertices;

    private bool SameVectors(int one, int two) {
        Vector3 vone = vertices[one];
        Vector3 vtwo = vertices[two];
        if (Math.Abs(vone.x - vtwo.x) < .01 && Math.Abs(vone.y - vtwo.y) < .01) {
            return true;
        }
        else {
            return false;
        }
    }

    void Start() {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        print("debug0" + stopwatch.Elapsed);
        // Get the polygon collider (create one if necessary)
        polygonCollider = GetComponent<PolygonCollider2D>();
        if (polygonCollider == null) {
            polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
        }

        // Get the mesh's vertices for use later
        vertices = GetComponent<MeshFilter>().mesh.vertices;

        // Get all edges from triangles
        int[] triangles = GetComponent<MeshFilter>().mesh.triangles;
        for (int i = 0; i < triangles.Length; i += 3) {
            edges.Add(new Edge(triangles[i], triangles[i + 1]));
            edges.Add(new Edge(triangles[i + 1], triangles[i + 2]));
            edges.Add(new Edge(triangles[i + 2], triangles[i]));
        }
        print("debug1" + stopwatch.Elapsed);
        // Find duplicate edges
        List<Edge> edgesToRemove = new List<Edge>();
        for (int i = 0; i < edges.Count; i++) {
            for (int n = 0; n < edges.Count; n++) {
                if (i != n) {
                    Edge edge1 = edges[i];
                    Edge edge2 = edges[n];
                    if ((edge1.vert1 == edge2.vert1 && edge1.vert2 == edge2.vert2) ||
                        (edge1.vert1 == edge2.vert2 && edge1.vert2 == edge2.vert1)) {
                        edgesToRemove.Add(edge1);
                    }
                    else {
                        if (SameVectors(edge1.vert1, edge2.vert1) && SameVectors(edge1.vert2, edge2.vert2)) {
                            edgesToRemove.Add(edge1);
                        }
                        else if (SameVectors(edge1.vert1, edge2.vert2) && SameVectors(edge1.vert2, edge2.vert1)) {
                            edgesToRemove.Add(edge1);
                        }
                    }
                }
            }
        }
        print("debug2" + stopwatch.Elapsed);
        // Remove duplicate edges (leaving only perimeter edges)
        foreach (Edge edge in edgesToRemove) {
            edges.Remove(edge);
        }

        // Start edge trace
        edgeTrace(edges[0]);
        print("debug3" + stopwatch.Elapsed);
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
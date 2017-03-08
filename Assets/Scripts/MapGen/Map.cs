using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = System.Random;

public class Map : MonoBehaviour {
    public int width, height;
    private int[,] map;
    private List<Edge> edges = new List<Edge>();
    public Camera camera;
    public float scale;

    void Start() {
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = new Color(18, 82, 47);
        MapGenerator generator = gameObject.GetComponent<MapGenerator>();
        if (generator == null) {
            generator = gameObject.AddComponent<MapGenerator>();
        }
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        map = generator.Generate(this);
        print("Cell Automata: " + stopwatch.Elapsed);

        stopwatch.Reset();
        stopwatch.Start();
        MakeMesh();
        print("Mesh Construction: " + stopwatch.Elapsed);

        NewColliderCreator collider = gameObject.AddComponent<NewColliderCreator>();

        stopwatch.Reset();
        stopwatch.Start();
        collider.GenerateColliders(this, edges);
        print("ColliderGeneration: " + stopwatch.Elapsed);
        stopwatch.Stop();
    }

    public int[,] GetMap() {
        return map;
    }

    private void MakeMesh() {
        if (map == null) {
            return;
        }
        List<Vector3> verticies = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uv = new List<Vector2>();
        Textures textures = new Textures();
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                if (map[x, y] == 1) {
                    Vector3 center = new Vector3((-width / 2) + (x * scale * 2) + scale, (-height / 2) + (y * scale * 2) + scale, 0);
                    verticies.Add(new Vector3(center.x + scale, center.y + scale, 0)); //^> 0
                    verticies.Add(new Vector3(center.x - scale, center.y + scale, 0)); //^< 1
                    verticies.Add(new Vector3(center.x + scale, center.y - scale, 0)); //v> 2
                    verticies.Add(new Vector3(center.x - scale, center.y - scale, 0)); //v< 3
                    int size = verticies.Count;
                    triangles.Add(size - 4);
                    triangles.Add(size - 2);
                    triangles.Add(size - 1);

                    triangles.Add(size - 1);
                    triangles.Add(size - 3);
                    triangles.Add(size - 4);

                    int texture = 0;
                    if (!IsInMapRange(x + 1, y) || map[x + 1, y] == 0) {
                        edges.Add(new Edge(size - 4, size - 2));
                        texture += 100;
                    }

                    if (!IsInMapRange(x, y + 1) || map[x, y + 1] == 0) {
                        edges.Add(new Edge(size - 3, size - 4));
                        texture += 1000;
                    }

                    if (!IsInMapRange(x, y - 1) || map[x, y - 1] == 0) {
                        edges.Add(new Edge(size - 2, size - 1));
                        texture += 10;
                    }
                    
                    if (!IsInMapRange(x - 1, y) || map[x - 1, y] == 0) {
                        edges.Add(new Edge(size - 1, size - 3));
                        texture += 1;
                    }
                    uv.AddRange(textures.textures[texture]);
                }
            }
        }
        MeshFilter filter = gameObject.GetComponent<MeshFilter>();
        if (filter == null) {
            filter = gameObject.AddComponent<MeshFilter>();
        }
        Mesh mesh = new Mesh();
        mesh.vertices = verticies.ToArray();
        mesh.triangles = triangles.ToArray();
        filter.mesh = mesh;
        mesh.uv = uv.ToArray();
        mesh.RecalculateNormals();
        MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
        if (renderer == null) {
            renderer = gameObject.AddComponent<MeshRenderer>();
        }
        print("Done rendering map");
    }

    public bool IsInMapRange(int x, int y) {
        return x >= 0 && x < width && y >= 0 && y < height;
    }
}
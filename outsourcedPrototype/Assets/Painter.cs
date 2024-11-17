using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Painter : MonoBehaviour
{
    public GameObject targetObj;
    protected MeshFilter targetMesh;
    protected Mesh mesh;
    protected Collider meshCol;
    public float radius;
    public Color myCol;
    public Vector3 nearestPoint;


    protected Vector3[] vertices;
    protected Color[] colors;
    protected bool[] inRange;
    protected float[] dist;
    // Start is called before the first frame update
    void Awake()
    {
        targetMesh = targetObj.GetComponent<MeshFilter>();
        mesh = targetMesh.mesh;
        meshCol = targetObj.GetComponent<Collider>();
        vertices = new Vector3[mesh.vertices.Length];
        colors = new Color[vertices.Length];
        inRange = new bool[vertices.Length];
        dist = new float[vertices.Length];

        GetVertices();
        GetColors();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetVertices()
    {

        int i = 0;
        foreach (var vert in mesh.vertices)
        { vertices[i] = targetObj.transform.TransformPoint(vert); i++; }
    }    

    public void GetColors()
    {
        int j = 0;
        foreach (var col in mesh.colors)
        { colors[j] = col; j++; }
    }

    public void PaintTarget()
    {
        GetColors();
        nearestPoint = meshCol.ClosestPoint(transform.position);
        VerticesInRange(nearestPoint);

        for (int i = 0; i < vertices.Length; i++)
        {
            if (inRange[i])
            {
                Debug.Log("SWAPPED COL");
                colors[i] = myCol;
            }
        }
        mesh.colors = colors;
    }

    public void VerticesInRange(Vector3 point)
    {

        for (int i = 0; i < vertices.Length; i++)
        {
            dist[i] = Vector3.Distance(vertices[i], point);
            if (dist[i] <= radius)
            {
                inRange[i] = true;
            }
            else {inRange[i] = false; }
        }
    }

    //public Vector3 NearestVertexTo(Vector3 point)
    //{
    //    // convert point to local space

    //    float minDistanceSqr = Mathf.Infinity;
    //    Vector3 nearestVertex = Vector3.zero;

    //    // scan all vertices to find nearest
    //    foreach (Vector3 vertex in mesh.vertices)
    //    {
    //        Vector3 diff = point - vertex;
    //        float distSqr = diff.sqrMagnitude;

    //        if (distSqr < minDistanceSqr)
    //        {
    //            minDistanceSqr = distSqr;
    //            nearestVertex = vertex;
    //        }
    //    }
    //    // convert nearest vertex back to world space
    //    return transform.TransformPoint(nearestVertex);
    //}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(nearestPoint, radius);

        if (Application.isPlaying)
        {
            //foreach (var vert in vertices)
            //{ Gizmos.DrawSphere(vert, .05f); }
        }
    }


}

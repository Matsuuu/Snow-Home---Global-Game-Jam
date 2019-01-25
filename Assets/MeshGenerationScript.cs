using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MeshGenerationScript : MonoBehaviour
{
    public GameObject snowBall;
    private SnowBallScript snowBallScript;
    public int snowWidth;
    public int snowDepth;
    public float minSnowHeight;
    public float maxSnowHeight;
    public float snowSinkHeight;
    
    private Mesh mesh;
    private MeshCollider meshCollider;
    
    private float minMeshX;
    private float minMeshZ;

    private Vector3[] vertices;

    private int[] triangles;

    private Vector3 lastContactPoint;
    // Start is called before the first frame update
    void Start()
    {
        snowBallScript = snowBall.GetComponent<SnowBallScript>();
        minMeshX = transform.parent.transform.position.x;
        minMeshZ = transform.parent.transform.position.z;
        mesh = new Mesh();
        meshCollider = GetComponent<MeshCollider>();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateMesh();
        CreateSnow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateMesh()
    {
        vertices = new Vector3[(snowWidth + 1) * (snowDepth + 1)];

        int i = 0;
        for (int z = 0; z <= snowDepth; z++)
        {
            for (int x = 0; x <= snowWidth; x++)
            {
                vertices[i] = new Vector3(x, Random.Range(minSnowHeight, maxSnowHeight), z);
                i++;
            }
        }

        triangles = new int[snowWidth * snowDepth * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < snowDepth; z++)
        {
            for (int x = 0; x < snowWidth; x++)
            {
                triangles[tris + 0] = vert;
                triangles[tris + 1] = vert + snowWidth + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + snowWidth + 1;
                triangles[tris + 5] = vert + snowWidth + 2;

                vert++;
                tris += 6;
            }

            vert++;
        }
    }

    void CreateSnow()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        meshCollider.sharedMesh = mesh;
        mesh.RecalculateNormals();

    }


    private void OnCollisionStay(Collision other)
    {
        ContactPoint contactPoint = other.contacts[0];
        if (contactPoint.point != lastContactPoint)
        {
            Vector3 contPoint = contactPoint.point;
            Vector3 roundedCollisionPoint = new Vector3((int) Math.Round(contPoint.x),
                contPoint.y,
                (int) Math.Round(contPoint.z));

            Vector3 first =
                vertices.First(vert => vert.x == roundedCollisionPoint.x && vert.z == roundedCollisionPoint.z);

            for (int i = 0; i < vertices.Length; i++)
            {
                if (vertices[i] == first)
                {
                    int targetVertice = i - snowWidth;
                    if (targetVertice > 0)
                    {
                        vertices[targetVertice] = new Vector3(vertices[targetVertice].x, snowSinkHeight, vertices[targetVertice].z);
                    }
                }
            }


            mesh.vertices = vertices;
            mesh.RecalculateNormals();
            snowBallScript.IncreaseBallSize();
            lastContactPoint = contactPoint.point;
        }
    }
}

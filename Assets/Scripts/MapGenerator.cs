using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    List<GameObject> cubes=new List<GameObject> ();

    static int xSize = 40;
    static int zSize = 40;

    GameObject[,,] cubesArray = new GameObject[xSize,10,zSize];
    float[,] perlinNoiseDistribution = new float[xSize, zSize];

    float height = 0.1f;

    private void Start()
    {
        GenerateMap();
        UpdateCube();
    }

    void GenerateMap()
    {
        for(int x = 0; x < xSize; x++)
        {
            for(int z = 0; z < zSize; z++)
            {
                float perlinNoise=Mathf.PerlinNoise(x*height,z*height)*10;
                perlinNoiseDistribution[x, z] = perlinNoise;
                for(int y = 0; y < perlinNoise; y++)
                {
                    GenerateCubes(x,y,z);
                }
            }
        }
    }

    void GenerateCubes(int x, int y, int z)
    {
        GameObject cube = new GameObject("Cube");
        MeshFilter meshFilter=cube.AddComponent<MeshFilter>();
        cube.AddComponent<MeshRenderer>().material=new Material(Shader.Find("Standard"));

        MeshData data=new MeshData();

        Mesh mesh = new Mesh();
        mesh.vertices = data.vertices.ToArray();
        mesh.triangles=data.triangles.ToArray();
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;

        cube.transform.position+=new Vector3(x,y,z);

        cubes.Add(cube);
        cubesArray[x, y, z] = cube;
    }

    void UpdateCube()
    {
        for(int x = 0; x < xSize; x++)
        {
            for(int z = 0; z < zSize; z++)
            {
                float perlinNoise = perlinNoiseDistribution[x, z];
                for (int y = 0; y < perlinNoise; y++)
                {
                    Vector3 positionUp = new Vector3(cubesArray[x,y,z].transform.position.x, cubesArray[x,y,z].transform.position.y + 1, cubesArray[x,y,z].transform.position.z);
                    bool isTop = CheckIfCubeExists(positionUp, x, y + 1, z, perlinNoise);

                    Vector3 positionDown = new Vector3(cubesArray[x,y,z].transform.position.x, cubesArray[x,y,z].transform.position.y - 1, cubesArray[x,y,z].transform.position.z);
                    bool isBottom = CheckIfCubeExists(positionDown, x, y - 1, z, perlinNoise);



                    Vector3 positionLeft = new Vector3(cubesArray[x,y,z].transform.position.x, cubesArray[x,y,z].transform.position.y, cubesArray[x,y,z].transform.position.z - 1);
                    bool isLeft = CheckIfCubeExists(positionLeft, x, y, z - 1, perlinNoise);

                    Vector3 positionRight = new Vector3(cubesArray[x,y,z].transform.position.x, cubesArray[x,y,z].transform.position.y, cubesArray[x,y,z].transform.position.z + 1);
                    bool isRight = CheckIfCubeExists(positionRight, x, y, z + 1, perlinNoise);



                    Vector3 positionFront = new Vector3(cubesArray[x,y,z].transform.position.x + 1, cubesArray[x,y,z].transform.position.y, cubesArray[x,y,z].transform.position.z);
                    bool isFront = CheckIfCubeExists(positionFront, x + 1, y, z, perlinNoise);

                    Vector3 positionBack = new Vector3(cubesArray[x,y,z].transform.position.x - 1, cubesArray[x,y,z].transform.position.y, cubesArray[x,y,z].transform.position.z);
                    bool isBack = CheckIfCubeExists(positionBack, x - 1, y, z, perlinNoise);


                    MeshData meshData = new MeshData();

                    Mesh mesh = new Mesh();
                    mesh.vertices = meshData.vertices.ToArray();
                    meshData.UpdateTriangleData(isTop, isBottom, isRight, isLeft, isFront, isBack);
                    mesh.triangles = meshData.triangles.ToArray();
                    mesh.RecalculateNormals();
                    cubesArray[x,y,z].GetComponent<MeshFilter>().mesh = mesh;
                }
            }
        }
    }

    bool CheckIfCubeExists(Vector3 direction,int x,int y,int z,float perlinNoise)
    {
        try
        {
            if (y >= 0 && y < perlinNoise && x >= 0 && x < xSize && z >= 0 && z < zSize)
            {
                if (cubesArray[x, y, z].transform.position == direction) { return true; }
            }
        }
        catch
        {
            return false;
        }
        return false;
    }
}

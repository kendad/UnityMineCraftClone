using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    List<GameObject> cubes=new List<GameObject> ();

    int xSize = 20;
    int zSize = 20;

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
    }

    void UpdateCube()
    {
        /*foreach(GameObject cube in cubes)
        {
            Vector3 positionUp = new Vector3(cube.transform.position.x, cube.transform.position.y+1, cube.transform.position.z);
            bool isTop = CheckIfCubeExists(positionUp);

            Vector3 positionDown = new Vector3(cube.transform.position.x, cube.transform.position.y-1, cube.transform.position.z);
            bool isBottom=CheckIfCubeExists(positionDown);



            Vector3 positionLeft = new Vector3(cube.transform.position.x, cube.transform.position.y, cube.transform.position.z - 1);
            bool isLeft=CheckIfCubeExists(positionLeft);

            Vector3 positionRight = new Vector3(cube.transform.position.x, cube.transform.position.y, cube.transform.position.z + 1);
            bool isRight = CheckIfCubeExists(positionRight);



            Vector3 positionFront = new Vector3(cube.transform.position.x + 1, cube.transform.position.y, cube.transform.position.z);
            bool isFront = CheckIfCubeExists(positionFront);

            Vector3 positionBack = new Vector3(cube.transform.position.x - 1, cube.transform.position.y, cube.transform.position.z);
            bool isBack=CheckIfCubeExists(positionBack);

         
            MeshData meshData = new MeshData();

            Mesh mesh = new Mesh();
            mesh.vertices = meshData.vertices.ToArray();
            meshData.UpdateTriangleData(isTop, isBottom, isRight, isLeft, isFront, isBack);
            mesh.triangles = meshData.triangles.ToArray();
            mesh.RecalculateNormals();
            cube.GetComponent<MeshFilter>().mesh = mesh;

        }*/

        for(int i = 0; i < cubes.Count; i++)
        {
            Vector3 positionUp = new Vector3(cubes[i].transform.position.x, cubes[i].transform.position.y + 1, cubes[i].transform.position.z);
            bool isTop = CheckIfCubeExists(positionUp,i+1);

            Vector3 positionDown = new Vector3(cubes[i].transform.position.x, cubes[i].transform.position.y - 1, cubes[i].transform.position.z);
            bool isBottom = CheckIfCubeExists(positionDown,i-1);



            Vector3 positionLeft = new Vector3(cubes[i].transform.position.x, cubes[i].transform.position.y, cubes[i].transform.position.z - 1);
            bool isLeft = CheckIfCubeExists(positionLeft);

            Vector3 positionRight = new Vector3(cubes[i].transform.position.x, cubes[i].transform.position.y, cubes[i].transform.position.z + 1);
            bool isRight = CheckIfCubeExists(positionRight);



            Vector3 positionFront = new Vector3(cubes[i].transform.position.x + 1, cubes[i].transform.position.y, cubes[i].transform.position.z);
            bool isFront = CheckIfCubeExists(positionFront);

            Vector3 positionBack = new Vector3(cubes[i].transform.position.x - 1, cubes[i].transform.position.y, cubes[i].transform.position.z);
            bool isBack = CheckIfCubeExists(positionBack);


            MeshData meshData = new MeshData();

            Mesh mesh = new Mesh();
            mesh.vertices = meshData.vertices.ToArray();
            meshData.UpdateTriangleData(isTop, isBottom, isRight, isLeft, isFront, isBack);
            mesh.triangles = meshData.triangles.ToArray();
            mesh.RecalculateNormals();
            cubes[i].GetComponent<MeshFilter>().mesh = mesh;
        }
    }

    bool CheckIfCubeExists(Vector3 direction)
    {
        foreach(GameObject cube in cubes)
        {
            if (cube.transform.position == direction)
            {
                return true;
            }
        }
        return false;
    }

    bool CheckIfCubeExists(Vector3 direction,int index)
    {
        if (index>=0 && index < cubes.Count)
        {
            Debug.Log(index);
            Debug.Log(direction);
            Debug.Log("###");
            if (cubes[index].transform.position == direction) { return true; }
        }
        return false;
    }
}

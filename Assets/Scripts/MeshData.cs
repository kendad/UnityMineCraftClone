using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshData
{
    public List<Vector3> vertices = new List<Vector3>();
    public List<int> triangles=new List<int>();

    public MeshData()
    {
        UpdateVerticeData();
        UpdateTriangleData();
    }

    void UpdateVerticeData()
    {
        //add Values to vertices list
        vertices.Add(new Vector3(0, 0, 0));//-0
        vertices.Add(new Vector3(0, 0, 1));//-1
        vertices.Add(new Vector3(1, 0, 0));//-2
        vertices.Add(new Vector3(1, 0, 1));//-3
        vertices.Add(new Vector3(0, 1, 0));//-4
        vertices.Add(new Vector3(1, 1, 0));//-5
        vertices.Add(new Vector3(0, 1, 1));//-6
        vertices.Add(new Vector3(1, 1, 1));//-7
        vertices.Add(new Vector3(1, 1, 0));//-8
        vertices.Add(new Vector3(1, 1, 1));//-9
        vertices.Add(new Vector3(0, 1, 0));//-10
        vertices.Add(new Vector3(0, 1, 1));//-11
    }

    public void UpdateTriangleData(bool isTop = true, bool isBottom = true, bool isRight = true,bool isLeft=true,bool isFront=true,bool isBack = true)
    {
        triangles.Clear();
        if (!isTop)
        {
            int[] tempArray = { 4, 6, 7, 8, 4, 7 };
            triangles.AddRange(tempArray);
        }
        if (!isBottom)
        {
            int[] tempArray = { 0, 1, 2, 1, 3, 2 };
            triangles.AddRange(tempArray);
        }
        if (!isRight)
        {
            int[] tempArray = { 1, 7, 6, 3, 7, 1 };
            triangles.AddRange(tempArray);
        }
        if (!isLeft)
        {
            int[] tempArray = { 0, 4, 5, 0, 5, 2 };
            triangles.AddRange(tempArray);
        }
        if (!isFront)
        {
            int[] tempArray = { 2, 8, 3, 8, 9, 3 };
            triangles.AddRange(tempArray);
        }
        if (!isBack)
        {
            int[] tempArray = { 11, 10, 0, 1, 11, 0 };
            triangles.AddRange(tempArray);
        }
    }
}

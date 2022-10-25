using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorUtils
{
    public static Vector2 VectorXZtoXY(Vector3 position)
    {
        position.y = position.z;
        return position;
    }

    public static bool IsRoundedVectorsEqual(Vector3 vector1, Vector3 vector2, int roundTo)
    {
        return RoundVector(vector1, roundTo) == RoundVector(vector2, roundTo);
    }

    public static Vector3 RoundVector(Vector3 vector, int roundTo)
    {
        float x = (float)System.Math.Round(vector.x, roundTo);
        float y = (float)System.Math.Round(vector.y, roundTo);
        float z = (float)System.Math.Round(vector.z, roundTo);
        return new Vector3(x, y, z);
    }

    public static Vector3[] BuildTraectory(Vector3 startPos, Vector3 endPos, float step)
    {
        Stack<Vector3> traectory = new Stack<Vector3>();
        traectory.Push(startPos);
        while(Vector3.Distance(traectory.Peek(), endPos) >= step)
        {
            traectory.Push(Vector3.MoveTowards(traectory.Peek(), endPos, step));
        }
        traectory.Push(endPos);
        var array = traectory.ToArray();
        Array.Reverse(array);
        return array;
    }

    public static Vector3 GetProjected(Vector3 startPos, Vector3 endPos, Vector3 pointToProject)
    {
        Vector3 startToFinish = endPos - startPos;
        Vector3 projection = Vector3.Project(pointToProject - startPos, startToFinish);
        return projection + startPos;
    }
}

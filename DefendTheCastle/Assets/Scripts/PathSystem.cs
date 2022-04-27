using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSystem : MonoBehaviour
{
    public List<Transform> wayPoints;
    public int pathCount;

    private void Awake()
    {
        pathCount = 0;
        foreach (Transform path in transform)
        {
            wayPoints.Add(path);
            pathCount++;
        }
    }
}

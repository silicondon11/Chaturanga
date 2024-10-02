using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    public IEnumerator MoveUnitCoroutine(GameObject item, List<UnityEngine.Vector3[]> positionsArrays, int destPosIdx, int destPosArrayIdx, int moveSpeed)
    {

        List<object[]> intersections = FindIntersections(positionsArrays);


        float closestDistance = float.MaxValue;
        int startPosIdx = 0;
        int startPosArrayIdx = 0;

        for (int s = 0; s < positionsArrays.Count; s++)
        {
            UnityEngine.Vector3[] posArray = positionsArrays[s];

            for (int t = 0; t < posArray.Length; t++)
            {
                UnityEngine.Vector3 position = posArray[t];
                float distance = UnityEngine.Vector3.Distance(position, item.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    startPosIdx = t;
                    startPosArrayIdx = s;
                }
            }
        }

        if (startPosArrayIdx != destPosArrayIdx)
        {
            UnityEngine.Vector3[] nextIntersections = new UnityEngine.Vector3[1];
            for (int a = 0; a < intersections.Count; a++)
            {
                object[] intersection = intersections[a];
                if ((((int)intersection[1] == startPosArrayIdx) && ((int)intersection[2] == destPosArrayIdx)) || (((int)intersection[2] == startPosArrayIdx) && ((int)intersection[1] == destPosArrayIdx)))
                {
                    System.Array.Resize(ref nextIntersections, nextIntersections.Length + 1);
                    nextIntersections[nextIntersections.Length - 1] = (UnityEngine.Vector3)intersection[0];
                }
            }

            UnityEngine.Vector3 nextIntersection = new UnityEngine.Vector3();
            if (nextIntersections.Length > 1)
            {
                nextIntersection = nextIntersections[1];
                UnityEngine.Debug.LogError("Intersection: " + nextIntersection);
            }
            else if (nextIntersections.Length == 1)
            {
                nextIntersection = nextIntersections[0];
            }
            else
            {
                UnityEngine.Debug.LogError("NO INTERSECTION FOUND");
            }

            UnityEngine.Vector3[] positionsArray = positionsArrays[startPosArrayIdx];

            int destIdx = ClosestPoint(positionsArray, nextIntersection);

            UnityEngine.Debug.LogError(destIdx);

            if (startPosIdx > destIdx)
            {
                yield return MoveUnitInDirection(item, positionsArrays, startPosIdx, destPosIdx, -1, intersections, startPosArrayIdx, destPosArrayIdx, moveSpeed);
            }
            else if (startPosIdx < destIdx)
            {
                yield return MoveUnitInDirection(item, positionsArrays, startPosIdx, destPosIdx, 1, intersections, startPosArrayIdx, destPosArrayIdx, moveSpeed);
            }
        }
        else
        {
            if (startPosIdx > destPosIdx)
            {
                yield return MoveUnitInDirection(item, positionsArrays, startPosIdx, destPosIdx, -1, intersections, startPosArrayIdx, destPosArrayIdx, moveSpeed);
            }
            else if (startPosIdx < destPosIdx)
            {
                yield return MoveUnitInDirection(item, positionsArrays, startPosIdx, destPosIdx, 1, intersections, startPosArrayIdx, destPosArrayIdx, moveSpeed);
            }
        }
    }

    private IEnumerator MoveUnitInDirection(GameObject item, List<UnityEngine.Vector3[]> positionsArrays, int startPosIdx, int endPosIdx, int direction, List<object[]> intersections, int startPosArrayIdx, int destPosArrayIdx, int moveSpeed)
    {
        int i = startPosIdx;
        int j = 0;
        bool intersectionFlag = false;
        int x = startPosArrayIdx;

        UnityEngine.Vector3[] positionsArray;
        while ((direction == 1 && i <= endPosIdx) || (direction == -1 && i >= endPosIdx) || x != destPosArrayIdx)
        {
            positionsArray = positionsArrays[x];

            if (i < positionsArray.Length)
            {
                if (item.transform.position == positionsArray[i])
                {
                    int interIdx = 0;
                    for (j = 0; j < intersections.Count; j++)
                    {
                        object[] intersection = intersections[j];
                        if (positionsArray[i] == (UnityEngine.Vector3)intersection[0] && (destPosArrayIdx == (int)intersection[1] || destPosArrayIdx == (int)intersection[2]) && destPosArrayIdx != startPosArrayIdx)
                        {
                            intersectionFlag = true;
                            int idx1 = (int)intersection[1];
                            int idx2 = (int)intersection[2];
                            if (idx1 == destPosArrayIdx)
                            {
                                x = idx1;
                                interIdx = (int)intersection[3];
                            }
                            else if (idx2 == destPosArrayIdx)
                            {
                                x = idx2;
                                interIdx = (int)intersection[4];
                            }
                        }
                    }

                    if (intersectionFlag)
                    {
                        if (interIdx > endPosIdx)
                        {
                            i = interIdx - 1;
                        }
                        else if (interIdx < endPosIdx)
                        {
                            i = interIdx + 1;
                        }
                        
                    }
                    else
                    {
                        i += direction;
                    }
                }
                else
                {
                    intersectionFlag = false;
                }

                item.transform.position = UnityEngine.Vector3.MoveTowards(item.transform.position, positionsArray[i], moveSpeed * Time.deltaTime);
            }
            yield return null;
        }
        yield return null;
    }

    private int ClosestPoint(UnityEngine.Vector3[] positionsArray, UnityEngine.Vector3 point)
    {
        float closestDistance = 10f;
        int destIdx = 0;
        for (int b = 0; b < positionsArray.Length; b++)
        {
            UnityEngine.Vector3 position = positionsArray[b];
            float distance = UnityEngine.Vector3.Distance(position, point);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                destIdx = b;
            }
        }
        return destIdx;
    }

    private List<object[]> FindIntersections(List<UnityEngine.Vector3[]> positionsArrays)
    {
        List<object[]> intersections = new List<object[]>();
        List<object[]> tempArray = new List<object[]>();

        for (int i = 0; i < positionsArrays.Count; i++)
        {
            UnityEngine.Vector3[] positionsArray = positionsArrays[i];
            for (int j = 0; j < positionsArray.Length; j++)
            {
                tempArray.Add(new object[] { positionsArray[j], i, j });
            }
        }

        for (int k = 0; k < tempArray.Count - 1; k++)
        {
            for (int l = 0; l < tempArray.Count; l++)
            {
                object[] element1 = (object[])tempArray[k];
                object[] element2 = (object[])tempArray[l];

                UnityEngine.Vector3 vector1 = (UnityEngine.Vector3)element1[0];
                UnityEngine.Vector3 vector2 = (UnityEngine.Vector3)element2[0];
                int aidx1 = (int)element1[1];
                int aidx2 = (int)element2[1];
                int idx1 = (int)element1[2];
                int idx2 = (int)element2[2];

                float distance = UnityEngine.Vector3.Distance(vector1, vector2);

                if (distance <= 2 && aidx1 != aidx2)
                {
                    object[] element = new object[] { vector1, aidx1, aidx2, idx1, idx2 };
                    intersections.Add(element);
                }
            }
        }
        return intersections;
    }
}

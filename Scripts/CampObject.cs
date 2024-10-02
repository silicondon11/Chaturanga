using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Camp", menuName = "ScriptableObjects/Camp", order = 1)]
public class CampObject : ScriptableObject
{
    public int flag = 0;

    public int wood = 0;
    public int ore = 0;
    public int food = 0;

    public UnityEngine.Vector3 loc = new UnityEngine.Vector3();
}

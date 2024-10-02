using UnityEngine;

public static class LineRendererUtility
{
    public static UnityEngine.Vector3 GetLineRendererWorldPosition(LineRenderer lineRenderer)
    {
        Transform parentTransform = lineRenderer.transform.parent;
        UnityEngine.Vector3 worldPosition = lineRenderer.transform.position + lineRenderer.GetPosition(0);
        return worldPosition;
    }
}


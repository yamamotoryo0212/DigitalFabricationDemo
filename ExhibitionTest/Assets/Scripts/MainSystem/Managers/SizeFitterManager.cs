using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeFitterManager : MonoBehaviour
{
    private Bounds _childObjBounds = new Bounds();

    public void ChangeWorldBoundsSize(float size, GameObject asset)
    {
        Bounds objBounds = CalcChildObjWorldBounds(asset, new Bounds());
        float maxlength = Mathf.Max(objBounds.size.x, objBounds.size.y, objBounds.size.z);

        if (maxlength == 0)
        {
            maxlength = 1;
        }
        float coefficient = size / maxlength;
        asset.transform.localScale = asset.transform.localScale * coefficient;
        _childObjBounds = CalcLocalObjBounds(asset);
    }

    private Bounds CalcLocalObjBounds(GameObject obj)
    {
        Bounds totalBounds = CalcChildObjWorldBounds(obj, new Bounds());
        Vector3 ObjWorldPosition = obj.transform.position;
        Vector3 ObjWorldScale = obj.transform.lossyScale;

        Vector3 totalBoundsLocalCenter = new Vector3(
            (totalBounds.center.x - ObjWorldPosition.x) / ObjWorldScale.x,
            (totalBounds.center.y - ObjWorldPosition.y) / ObjWorldScale.y,
            (totalBounds.center.z - ObjWorldPosition.z) / ObjWorldScale.z);
        Vector3 meshBoundsLocalSize = new Vector3(
            totalBounds.size.x / ObjWorldScale.x,
            totalBounds.size.y / ObjWorldScale.y,
            totalBounds.size.z / ObjWorldScale.z);

        Bounds localBounds = new Bounds(totalBoundsLocalCenter, meshBoundsLocalSize);

        return localBounds;
    }

    private Bounds CalcChildObjWorldBounds(GameObject obj, Bounds bounds)
    {
        foreach (Transform child in obj.transform)
        {
            if (!child.gameObject.activeSelf)
            {
                continue;
            }

            MeshFilter filter = child.gameObject.GetComponent<MeshFilter>();

            if (filter != null)
            {
                Vector3 ObjWorldPosition = child.position;
                Vector3 ObjWorldScale = child.lossyScale;
                MeshFilter meshFilter = filter;
                Bounds meshBounds = meshFilter.sharedMesh.bounds;

                Vector3 meshBoundsWorldCenter = meshBounds.center + ObjWorldPosition;
                Vector3 meshBoundsWorldSize = Vector3.Scale(meshBounds.size, ObjWorldScale);

                Vector3 meshBoundsWorldMin = meshBoundsWorldCenter - (meshBoundsWorldSize / 2);
                Vector3 meshBoundsWorldMax = meshBoundsWorldCenter + (meshBoundsWorldSize / 2);

                if (bounds.size == Vector3.zero)
                {
                    bounds = new Bounds(meshBoundsWorldCenter, Vector3.zero);
                }
                bounds.Encapsulate(meshBoundsWorldMin);
                bounds.Encapsulate(meshBoundsWorldMax);
            }

            bounds = CalcChildObjWorldBounds(child.gameObject, bounds);
        }
        return bounds;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPivotManager : MonoBehaviour
{
	public Vector3 FixPivot(GameObject asset)
	{
		Vector3 centerPos = Vector3.zero;
		int vertexCount = 0;

		foreach (Transform item in asset.GetComponentsInChildren<Transform>())
		{
			if (item.gameObject.TryGetComponent(out MeshFilter meshFilter))
			{
				Mesh mesh = Mesh.Instantiate(meshFilter.sharedMesh);
				foreach (Vector3 j in mesh.vertices)
				{
					vertexCount++;
					centerPos.x += j.x;
					centerPos.y += j.y;
					centerPos.z += j.z;
				}
			}
		}
		var x = centerPos.x / vertexCount;
		var y = centerPos.y / vertexCount;
		var z = centerPos.z / vertexCount;

		return new Vector3(-x * asset.transform.localScale.x, -y * asset.transform.localScale.y, -z * asset.transform.localScale.z); ;
	}

}

//foreach (Transform k in changeSizeItem.GetComponentsInChildren<Transform>())
//{
//	if (k.gameObject.TryGetComponent(out MeshFilter meshFilter))
//	{
//		Mesh mesh = Mesh.Instantiate(meshFilter.sharedMesh);
//		foreach (Vector3 l in mesh.vertices)
//		{
//			vertexCount++;
//			centerPos.x += l.x;
//			centerPos.y += l.y;
//			centerPos.z += l.z;
//		}
//	}
//}
//var x = centerPos.x / vertexCount;
//var y = centerPos.y / vertexCount;
//var z = centerPos.z / vertexCount;
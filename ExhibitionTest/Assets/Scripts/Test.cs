using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        GameObject block = (GameObject)Resources.Load("Block");
        List<Transform> transforms = new List<Transform>();
        foreach (var item in block.GetComponentsInChildren<Transform>())
        {
            if (item.name == "PlacementPoint")
            {
                foreach (var t in item.GetComponentsInChildren<Transform>())
                {
                    if (t.name == "PlacementPoint")
					{

					}
					else
					{
						transforms.Add(t);
					}
                    
                }
            }
        }
        Instantiate(block);

        GameObject pedestal = (GameObject)Resources.Load("Pedestal");
        Debug.Log(transforms.Count);
        foreach (var item in transforms)
        {
            Instantiate(pedestal, item.position, pedestal.transform.rotation);
        }
    }
}

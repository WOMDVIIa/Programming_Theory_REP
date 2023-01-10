using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woodcutter : Building
{
    // Start is called before the first frame update
    void Start()
    {
        resourceOutput = 2;
    }

    public override void GenerateResources(int[] gatheredResources)
    {
        gatheredResources[(int)MainManager.resources.wood] = resourceOutput;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Building
{
    // Start is called before the first frame update
    void Start()
    {
        resourceOutput = 1;
    }

    public override void GenerateResources(int[] gatheredResources)
    {
        gatheredResources[(int)MainManager.resources.population] = resourceOutput;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHall : Building    // INHERITANCE
{
    // Start is called before the first frame update
    void Start()
    {
        resourceOutput = 1;
    }

    public override void GenerateResources(int[] gatheredResources) // POLYMORPHISM
    {
        gatheredResources[(int)MainManager.resources.population] = resourceOutput;
        gatheredResources[(int)MainManager.resources.wood] = resourceOutput;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woodcutter : Building  // INHERITANCE
{
    static public int populationCost = 15;
    static public int woodCost = 5;

    // Start is called before the first frame update
    void Start()
    {
        resourceOutput = 2;
    }

    public override void GenerateResources(int[] gatheredResources) // POLYMORPHISM
    {
        gatheredResources[(int)MainManager.resources.wood] = resourceOutput;
    }

    public override bool HaveEnoughResources(int[] availableResources)  // POLYMORPHISM
    {
        if (populationCost <= availableResources[(int)MainManager.resources.population])
        {
            if (woodCost <= availableResources[(int)MainManager.resources.wood])
            {
                return true;
            }
        }
        return false;
    }

    public override void ConstructBuilding(int[] buildingCost)  // POLYMORPHISM
    {
        buildingCost[(int)MainManager.resources.population] = populationCost;
        buildingCost[(int)MainManager.resources.wood] = woodCost;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Building   // INHERITANCE
{
    static public int populationCost = 3;
    static public int woodCost = 15;

    // Start is called before the first frame update
    void Start()
    {
        resourceOutput = 1;
    }

    public override void GenerateResources(int[] gatheredResources) // POLYMORPHISM
    {
        gatheredResources[(int)MainManager.resources.population] = resourceOutput;
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

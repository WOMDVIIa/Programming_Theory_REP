using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    protected int resourceOutput;

    virtual public void GenerateResources(int[] gatheredResources){}

    virtual public bool HaveEnoughResources(int[] availableResources)
    {
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    protected int[] cost;
    protected int resourceOutput;

    virtual public void GenerateResources(int[] gatheredResources ){}
}

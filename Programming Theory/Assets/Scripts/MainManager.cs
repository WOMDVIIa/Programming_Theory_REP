using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;
    public enum resources
    {
        population,
        wood
    };

    public int[] resourcesBank;

    public List<Building> listOfBuildings;

    string[] resourceInterfaceBulkText = { "(Pop)ulation: ", "(W)ood: "};

    [SerializeField] TextMeshProUGUI[] resourceInterfaceText;
    [SerializeField] int numberOfResources;
    float updateResourcesStartTime = 0;
    float updateResourcesRepeatRate = 1;

    // Start is called before the first frame update
    void Start()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}

        instance = this;

        numberOfResources = Enum.GetValues(typeof(resources)).Cast<int>().Max() + 1;
        InvokeRepeating("UpdateResources", updateResourcesStartTime, updateResourcesRepeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateResources()
    {
        for (int i = 0; i < listOfBuildings.Count; i++)
        {
            int[] gatheredResources = new int[numberOfResources] ;
            listOfBuildings[i].GenerateResources(gatheredResources);

            for (int j = 0; j < numberOfResources; j++)
            {
                resourcesBank[j] += gatheredResources[j];
            }
        }

        UpdateResourcesText();
    }

    void UpdateResourcesText()
    {
        for (int i = 0; i < numberOfResources; i++)
        {
            resourceInterfaceText[i].text = resourceInterfaceBulkText[i] + resourcesBank[i];            
        }
    }
}

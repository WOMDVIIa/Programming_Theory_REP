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
    public enum buildings
    {
        house,
        woodcutter
    };

    public int numberOfResources { get; private set; }
    public int numberOfConstructableBuildings { get; private set; }

    int[] resourcesBank;
    string[] resourceInterfaceBulkText = { "(Pop)ulation: ", "(W)ood: "};
    [SerializeField] GameObject[] buildingsPrefabs;
    [SerializeField] List<Building> listOfBuildings;
    [SerializeField] Button[] buildButtons;
    [SerializeField] TextMeshProUGUI[] resourceInterfaceText;

    float updateResourcesStartTime = 0;
    float updateResourcesRepeatRate = 1;
    int constructionSitesInX = 5;
    int constructionSitesInY = 4;
    int maxNumberOfBuildings;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        GetEnumsSize();
        AddConstructButtonsListeners();
        NewArrays_CalculatableVariables();
        InvokeRepeating("UpdateResources", updateResourcesStartTime, updateResourcesRepeatRate);
    }

    void GetEnumsSize()
    {
        numberOfResources = Enum.GetValues(typeof(resources)).Cast<int>().Max() + 1;
        numberOfConstructableBuildings = Enum.GetValues(typeof(buildings)).Cast<int>().Max() + 1;
    }
    void NewArrays_CalculatableVariables()
    {
        resourcesBank = new int[numberOfResources];
        maxNumberOfBuildings = constructionSitesInX * constructionSitesInY;
    }

    void AddConstructButtonsListeners()
    {
        for (int i = 0; i < numberOfConstructableBuildings; i++)
        {
            int j = i;
            buildButtons[j].onClick.AddListener(delegate { ConstructBuilding(j); });
        }
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

    void GetNewConstructionPosition()
    {

        
    }

    public void ConstructBuilding(int buildingIndex)
    {
        if (listOfBuildings.Count < maxNumberOfBuildings)
        {
            listOfBuildings.Add(Instantiate(buildingsPrefabs[buildingIndex].gameObject, transform).GetComponent<Building>());
        }
    }
}

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
    public int[] resourcesBank { get; private set; }

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
    float groundXSize = 100;
    float groundYSize = 100;

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

    Vector3 GetNewConstructionPosition()
    {
        int xPos = (listOfBuildings.Count % constructionSitesInX);
        int yPos = listOfBuildings.Count / constructionSitesInX;

        return new Vector3(xPos * groundXSize / constructionSitesInX, 0.1f, yPos * groundYSize / constructionSitesInY);
    }

    public void ConstructBuilding(int buildingIndex)
    {
        if (listOfBuildings.Count < maxNumberOfBuildings)
        {
            Building newConstruction = buildingsPrefabs[buildingIndex].GetComponent<Building>();
            if (newConstruction.HaveEnoughResources(resourcesBank))
            {
                listOfBuildings.Add(Instantiate(newConstruction, GetNewConstructionPosition(), transform.rotation));
                PayForNewBuilding();
            }
        }
    }

    void PayForNewBuilding()
    {
        int[] newBuildingCost = new int[numberOfResources];
        listOfBuildings.Last().ConstructBuilding(newBuildingCost);
        for (int i = 0; i < numberOfResources; i++)
        {
            resourcesBank[i] -= newBuildingCost[i];
        }
        UpdateResourcesText();
    }
}

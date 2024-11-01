using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Resources;

public class ButtonHandler : MonoBehaviour
{
    public ResourceManager resourceManager;
    public ConditionChecker conditionChecker;

    void Start()
    {

    }

    // Train Soldier i�lemi: K�l�� ve Ka��t de�erini 10 art�r�r, Para de�erini 10 azalt�r
    public void TrainSoldier()
    {
        if (resourceManager.GetResourceValue(resourceManager.coinText) >= 10 && resourceManager.GetResourceValue(resourceManager.appleText) >= 10)
        {
            resourceManager.UpdateResource(resourceManager.swordText, 10);
            resourceManager.UpdateResource(resourceManager.paperText, 10);
            resourceManager.UpdateResource(resourceManager.coinText, -10);
            resourceManager.UpdateResource(resourceManager.appleText, -10);
            conditionChecker.CheckConditions();
        }
    }

    // Donate Farmer i�lemi: Elma ve Ka��t de�erini 10 art�r�r, Para de�erini 10 azalt�r
    public void DonateFarmer()
    {
        if (resourceManager.GetResourceValue(resourceManager.coinText) >= 10)
        {
            resourceManager.UpdateResource(resourceManager.appleText, 10);
            resourceManager.UpdateResource(resourceManager.paperText, 10);
            resourceManager.UpdateResource(resourceManager.coinText, -10);
            conditionChecker.CheckConditions();
        }
    }

    // Take Tax i�lemi: Para de�erini 10 art�r�r, Ka��t de�erini 10 azalt�r
    public void TakeTax()
    {
        if (resourceManager.GetResourceValue(resourceManager.paperText) >= 10)
        {
            resourceManager.UpdateResource(resourceManager.coinText, 10);
            resourceManager.UpdateResource(resourceManager.paperText, -10);
            conditionChecker.CheckConditions();
        }
    }
}

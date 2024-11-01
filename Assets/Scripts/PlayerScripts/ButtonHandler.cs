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

    // Train Soldier iþlemi: Kýlýç ve Kaðýt deðerini 10 artýrýr, Para deðerini 10 azaltýr
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

    // Donate Farmer iþlemi: Elma ve Kaðýt deðerini 10 artýrýr, Para deðerini 10 azaltýr
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

    // Take Tax iþlemi: Para deðerini 10 artýrýr, Kaðýt deðerini 10 azaltýr
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

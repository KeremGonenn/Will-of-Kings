using UnityEngine;

public class ConditionChecker : MonoBehaviour
{
    public ResourceManager resourceManager;
    public NewsManager newsManager;

    // Koþullarý kontrol eden fonksiyon
    public void CheckConditions()
    {
        int swordValue = resourceManager.GetResourceValue(resourceManager.swordText);
        int appleValue = resourceManager.GetResourceValue(resourceManager.appleText);
        int paperValue = resourceManager.GetResourceValue(resourceManager.paperText);
        int coinValue = resourceManager.GetResourceValue(resourceManager.coinText);

        // Kaðýt (Refah) 10 veya altýna düþtüyse rastgele bir mesaj seç ve iþlemi uygula
        if (paperValue <= 10)
        {
            newsManager.ApplyRandomEffect(newsManager.GetLowPaperMessages());
        }

        // Kýlýç (Asker) 10 veya altýna düþtüyse rastgele bir mesaj seç ve iþlemi uygula
        if (swordValue <= 10)
        {
            newsManager.ApplyRandomEffect(newsManager.GetLowSwordMessages());
        }

        // Elma (Yiyecek) 10 veya altýna düþtüyse rastgele bir mesaj seç ve iþlemi uygula
        if (appleValue <= 10)
        {
            newsManager.ApplyRandomEffect(newsManager.GetLowAppleMessages());
        }

        // Para (Coin) 10 veya altýna düþtüyse rastgele bir mesaj seç ve iþlemi uygula
        if (coinValue <= 10)
        {
            newsManager.ApplyRandomEffect(newsManager.GetLowCoinMessages());
        }
    }
}

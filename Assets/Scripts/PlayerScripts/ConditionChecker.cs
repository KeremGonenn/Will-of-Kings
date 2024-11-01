using UnityEngine;

public class ConditionChecker : MonoBehaviour
{
    public ResourceManager resourceManager;
    public NewsManager newsManager;

    // Ko�ullar� kontrol eden fonksiyon
    public void CheckConditions()
    {
        int swordValue = resourceManager.GetResourceValue(resourceManager.swordText);
        int appleValue = resourceManager.GetResourceValue(resourceManager.appleText);
        int paperValue = resourceManager.GetResourceValue(resourceManager.paperText);
        int coinValue = resourceManager.GetResourceValue(resourceManager.coinText);

        // Ka��t (Refah) 10 veya alt�na d��t�yse rastgele bir mesaj se� ve i�lemi uygula
        if (paperValue <= 10)
        {
            newsManager.ApplyRandomEffect(newsManager.GetLowPaperMessages());
        }

        // K�l�� (Asker) 10 veya alt�na d��t�yse rastgele bir mesaj se� ve i�lemi uygula
        if (swordValue <= 10)
        {
            newsManager.ApplyRandomEffect(newsManager.GetLowSwordMessages());
        }

        // Elma (Yiyecek) 10 veya alt�na d��t�yse rastgele bir mesaj se� ve i�lemi uygula
        if (appleValue <= 10)
        {
            newsManager.ApplyRandomEffect(newsManager.GetLowAppleMessages());
        }

        // Para (Coin) 10 veya alt�na d��t�yse rastgele bir mesaj se� ve i�lemi uygula
        if (coinValue <= 10)
        {
            newsManager.ApplyRandomEffect(newsManager.GetLowCoinMessages());
        }
    }
}

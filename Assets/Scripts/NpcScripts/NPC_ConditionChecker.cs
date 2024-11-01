//using UnityEngine;

//public class NPC_ConditionChecker : MonoBehaviour
//{
//    public NPC_ResourceManager resourceManager;
//    public NPC_NewsManager newsManager;

//    // Koþullarý kontrol eden fonksiyon
//    public void CheckConditions()
//    {
//        int swordValue = resourceManager.GetResourceValue(resourceManager.swordText);
//        int appleValue = resourceManager.GetResourceValue(resourceManager.appleText);
//        int featherValue = resourceManager.GetResourceValue(resourceManager.featherText);
//        int coinValue = resourceManager.GetResourceValue(resourceManager.coinText);

//        // Kaðýt (Refah) 10 veya altýna düþtüyse rastgele bir mesaj seç ve iþlemi uygula
//        if (featherValue <= 10)
//        {
//            newsManager.ApplyRandomEffect(newsManager.GetLowPaperMessages());
//        }

//        // Kýlýç (Asker) 10 veya altýna düþtüyse rastgele bir mesaj seç ve iþlemi uygula
//        if (swordValue <= 10)
//        {
//            newsManager.ApplyRandomEffect(newsManager.GetLowSwordMessages());
//        }

//        // Elma (Yiyecek) 10 veya altýna düþtüyse rastgele bir mesaj seç ve iþlemi uygula
//        if (appleValue <= 10)
//        {
//            newsManager.ApplyRandomEffect(newsManager.GetLowAppleMessages());
//        }

//        // Para (Coin) 10 veya altýna düþtüyse rastgele bir mesaj seç ve iþlemi uygula
//        if (coinValue <= 10)
//        {
//            newsManager.ApplyRandomEffect(newsManager.GetLowCoinMessages());
//        }
//    }
//}

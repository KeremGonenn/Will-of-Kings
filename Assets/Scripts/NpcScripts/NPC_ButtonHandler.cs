using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class NPC_ButtonHandler : MonoBehaviour
{
    public List<NPC_ResourceManager> npcResourceManagers = new List<NPC_ResourceManager>(); // Birden fazla NPC'nin Resource Manager'larýný tutar
    public ResourceManager playerResourceManager;
    public TurnManager turnManager; // TurnManager referansý

    private bool isTradeDealActive = false; // TradeDeal butonunun aktif olup olmadýðýný kontrol eder
    private int tradeDealCooldown = 3;      // TradeDeal iþlemi için 3 tur süresi
    private int currentTurnForTradeDeal = 0; // TradeDeal iþlemi yapýldýðýnda içinde bulunduðumuz tur sayýsý
    private NPC_ResourceManager currentNPC;  // TradeDeal yapýlan NPC
    private int earningsCount;

    void Start()
    {

    }

    public void HandleTurnEnd()
    {
            DailyEarnings(); // Her turda gelir güncellemesi yap

        // Eðer bir TradeDeal aktifse ve 3 tur geçmediyse cooldown süresini kontrol edelim
        if (isTradeDealActive && turnManager.turnCount >= currentTurnForTradeDeal + tradeDealCooldown)
        {
            isTradeDealActive = false; // TradeDeal cooldown süresi doldu, tekrar kullanýlabilir.
        }
    }

    // Her NPC'den her turda 5 elma eksiltme fonksiyonu
    private void DailyEarnings()
    {
        foreach (var npc in npcResourceManagers)
        {
            npc.UpdateResource(npc.appleText, 30); 
            npc.UpdateResource(npc.coinText, 30);
            npc.UpdateResource(npc.swordText, 5);
        }
        playerResourceManager.UpdateResource(playerResourceManager.appleText, -5);
        playerResourceManager.UpdateResource(playerResourceManager.coinText, -5);
    }

    // Belirtilen NPC için TradeDeal yapar ve 3 tur boyunca otomatik olarak iþlemi tekrarlar
    public void TradeDeal(int npcIndex)
    {
        // Eðer TradeDeal iþlemi aktifse butona tekrar basýlamaz
        if (isTradeDealActive)
        {
            currentNPC.tradeButtonText.text = "Currently in trade.";
            return;
        }

        ClearAllMessages(); // Diðer butonlarýn mesajlarýný temizle

        currentNPC = npcResourceManagers[npcIndex]; // Ýlgili NPC'yi seç

        if (currentNPC.GetResourceValue(currentNPC.coinText) > 0 && currentNPC.statusText.text == "Alliance")
        {
            // TradeDeal iþlemi yapýldýðý için 3 tur boyunca tekrar yapýlamayacak
            isTradeDealActive = true;
            currentTurnForTradeDeal = turnManager.turnCount; // Þu anki tur sayýsýný kaydet

            // TradeDeal iþlemini otomatik olarak 3 tur boyunca yapacak coroutine baþlatýyoruz
            StartCoroutine(HandleTradeDeal());
        }
        else
        {
            currentNPC.tradeButtonText.text = "Not enough coin";
        }
    }

    // 3 tur boyunca TradeDeal iþlemini otomatik olarak yapacak coroutine
    private IEnumerator HandleTradeDeal()
    {
        for (int i = 0; i < tradeDealCooldown; i++)
        {
            if (currentNPC.GetResourceValue(currentNPC.coinText) > 0 && currentNPC.statusText.text == "Alliance")
            {
                // Her turda kaynaklarý güncelle
                currentNPC.UpdateResource(currentNPC.featherText, 10);
                currentNPC.UpdateResource(currentNPC.coinText, -10);
                currentNPC.UpdateResource(currentNPC.appleText, 10);

                // Oyuncunun kaynaklarýný güncelle
                playerResourceManager.UpdateResource(playerResourceManager.appleText, -10);
                playerResourceManager.UpdateResource(playerResourceManager.coinText, 10);

                Debug.Log("TradeDeal iþlemi yapýldý. Tur: " + (currentTurnForTradeDeal + i + 1));
            }
            else
            {
                Debug.Log("TradeDeal iþlemi sonlandýrýldý. Yeterli kaynak yok.");
                break; // Kaynaklar tükenirse TradeDeal iþlemi durdurulur
            }

            // Bir sonraki turu bekle
            yield return new WaitUntil(() => turnManager.isPlayerTurn && turnManager.turnCount == currentTurnForTradeDeal + i + 1);
        }

        // 3 tur tamamlandýktan sonra iþlemi tekrar yapýlabilir hale getir
        isTradeDealActive = false;
        Debug.Log("TradeDeal iþlemi 3 tur sonra tamamlandý, tekrar yapýlabilir.");
    }

    public void DeclareWar(int npcIndex)
    {
        ClearAllMessages(); // Diðer butonlarýn mesajlarýný temizle

        NPC_ResourceManager currentNPC = npcResourceManagers[npcIndex]; // Ýlgili NPC'yi seç

        if (currentNPC.statusText.text == "Alliance")
        {
            currentNPC.UpdateResource(currentNPC.featherText, -50);
            currentNPC.statusText.text = "Enemy";
            currentNPC.statusText.color = Color.red; // Enemy yazýsýný kýrmýzý yap
            currentNPC.enemyList1.text = "Player";
        }
        else
        {
            currentNPC.warButtonText.text = "You are already at war with this kingdom";
        }
    }

    public void ProposeAlliance(int npcIndex)
    {
        ClearAllMessages(); // Diðer butonlarýn mesajlarýný temizle

        NPC_ResourceManager currentNPC = npcResourceManagers[npcIndex]; // Ýlgili NPC'yi seç

        if (currentNPC.statusText.text == "Enemy" && playerResourceManager.GetResourceValue(playerResourceManager.coinText) >= 50)
        {
            playerResourceManager.UpdateResource(playerResourceManager.coinText, -50);
            currentNPC.UpdateResource(currentNPC.featherText, +50);
            currentNPC.UpdateResource(currentNPC.coinText, +50);
            currentNPC.statusText.text = "Alliance";
            currentNPC.statusText.color = Color.green; // Alliance yazýsýný yeþil yap
        }
        else
        {
            currentNPC.allianceButtonText.text = "You are already at peace with this kingdom or Not enough money";
        }
    }

    // Tüm buton mesajlarýný temizleyen fonksiyon
    private void ClearAllMessages()
    {
        foreach (var npc in npcResourceManagers)
        {
            npc.tradeButtonText.text = "";
            npc.warButtonText.text = "";
            npc.allianceButtonText.text = "";
        }
    }
}

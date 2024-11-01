using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class NPC_ButtonHandler : MonoBehaviour
{
    public List<NPC_ResourceManager> npcResourceManagers = new List<NPC_ResourceManager>(); // Birden fazla NPC'nin Resource Manager'lar�n� tutar
    public ResourceManager playerResourceManager;
    public TurnManager turnManager; // TurnManager referans�

    private bool isTradeDealActive = false; // TradeDeal butonunun aktif olup olmad���n� kontrol eder
    private int tradeDealCooldown = 3;      // TradeDeal i�lemi i�in 3 tur s�resi
    private int currentTurnForTradeDeal = 0; // TradeDeal i�lemi yap�ld���nda i�inde bulundu�umuz tur say�s�
    private NPC_ResourceManager currentNPC;  // TradeDeal yap�lan NPC
    private int earningsCount;

    void Start()
    {

    }

    public void HandleTurnEnd()
    {
            DailyEarnings(); // Her turda gelir g�ncellemesi yap

        // E�er bir TradeDeal aktifse ve 3 tur ge�mediyse cooldown s�resini kontrol edelim
        if (isTradeDealActive && turnManager.turnCount >= currentTurnForTradeDeal + tradeDealCooldown)
        {
            isTradeDealActive = false; // TradeDeal cooldown s�resi doldu, tekrar kullan�labilir.
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

    // Belirtilen NPC i�in TradeDeal yapar ve 3 tur boyunca otomatik olarak i�lemi tekrarlar
    public void TradeDeal(int npcIndex)
    {
        // E�er TradeDeal i�lemi aktifse butona tekrar bas�lamaz
        if (isTradeDealActive)
        {
            currentNPC.tradeButtonText.text = "Currently in trade.";
            return;
        }

        ClearAllMessages(); // Di�er butonlar�n mesajlar�n� temizle

        currentNPC = npcResourceManagers[npcIndex]; // �lgili NPC'yi se�

        if (currentNPC.GetResourceValue(currentNPC.coinText) > 0 && currentNPC.statusText.text == "Alliance")
        {
            // TradeDeal i�lemi yap�ld��� i�in 3 tur boyunca tekrar yap�lamayacak
            isTradeDealActive = true;
            currentTurnForTradeDeal = turnManager.turnCount; // �u anki tur say�s�n� kaydet

            // TradeDeal i�lemini otomatik olarak 3 tur boyunca yapacak coroutine ba�lat�yoruz
            StartCoroutine(HandleTradeDeal());
        }
        else
        {
            currentNPC.tradeButtonText.text = "Not enough coin";
        }
    }

    // 3 tur boyunca TradeDeal i�lemini otomatik olarak yapacak coroutine
    private IEnumerator HandleTradeDeal()
    {
        for (int i = 0; i < tradeDealCooldown; i++)
        {
            if (currentNPC.GetResourceValue(currentNPC.coinText) > 0 && currentNPC.statusText.text == "Alliance")
            {
                // Her turda kaynaklar� g�ncelle
                currentNPC.UpdateResource(currentNPC.featherText, 10);
                currentNPC.UpdateResource(currentNPC.coinText, -10);
                currentNPC.UpdateResource(currentNPC.appleText, 10);

                // Oyuncunun kaynaklar�n� g�ncelle
                playerResourceManager.UpdateResource(playerResourceManager.appleText, -10);
                playerResourceManager.UpdateResource(playerResourceManager.coinText, 10);

                Debug.Log("TradeDeal i�lemi yap�ld�. Tur: " + (currentTurnForTradeDeal + i + 1));
            }
            else
            {
                Debug.Log("TradeDeal i�lemi sonland�r�ld�. Yeterli kaynak yok.");
                break; // Kaynaklar t�kenirse TradeDeal i�lemi durdurulur
            }

            // Bir sonraki turu bekle
            yield return new WaitUntil(() => turnManager.isPlayerTurn && turnManager.turnCount == currentTurnForTradeDeal + i + 1);
        }

        // 3 tur tamamland�ktan sonra i�lemi tekrar yap�labilir hale getir
        isTradeDealActive = false;
        Debug.Log("TradeDeal i�lemi 3 tur sonra tamamland�, tekrar yap�labilir.");
    }

    public void DeclareWar(int npcIndex)
    {
        ClearAllMessages(); // Di�er butonlar�n mesajlar�n� temizle

        NPC_ResourceManager currentNPC = npcResourceManagers[npcIndex]; // �lgili NPC'yi se�

        if (currentNPC.statusText.text == "Alliance")
        {
            currentNPC.UpdateResource(currentNPC.featherText, -50);
            currentNPC.statusText.text = "Enemy";
            currentNPC.statusText.color = Color.red; // Enemy yaz�s�n� k�rm�z� yap
            currentNPC.enemyList1.text = "Player";
        }
        else
        {
            currentNPC.warButtonText.text = "You are already at war with this kingdom";
        }
    }

    public void ProposeAlliance(int npcIndex)
    {
        ClearAllMessages(); // Di�er butonlar�n mesajlar�n� temizle

        NPC_ResourceManager currentNPC = npcResourceManagers[npcIndex]; // �lgili NPC'yi se�

        if (currentNPC.statusText.text == "Enemy" && playerResourceManager.GetResourceValue(playerResourceManager.coinText) >= 50)
        {
            playerResourceManager.UpdateResource(playerResourceManager.coinText, -50);
            currentNPC.UpdateResource(currentNPC.featherText, +50);
            currentNPC.UpdateResource(currentNPC.coinText, +50);
            currentNPC.statusText.text = "Alliance";
            currentNPC.statusText.color = Color.green; // Alliance yaz�s�n� ye�il yap
        }
        else
        {
            currentNPC.allianceButtonText.text = "You are already at peace with this kingdom or Not enough money";
        }
    }

    // T�m buton mesajlar�n� temizleyen fonksiyon
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

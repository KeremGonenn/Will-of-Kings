using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public bool isPlayerTurn = true; // Baþlangýçta oyuncunun sýrasý
    public Button nextTurnButton;    // Next Turn butonu
    public int turnCount = 0;        // Kaç tur geçtiðini sayacak sayaç
    public NPC_ButtonHandler npcButton; // NPC aksiyonlarýný yöneten handler
    public NPC_Decision npcDecision; // NPC'lerin kararlarýný veren script

    // Oyuncunun sýrasý geldiðinde zamaný durduracaðýz
    void Update()
    {
        if (isPlayerTurn)
        {
            Time.timeScale = 0; // Oyun durur
        }
        else
        {
            Time.timeScale = 1; // Oyun devam eder
        }
    }

    // Next Turn butonuna basýldýðýnda oyuncunun sýrasý NPC'lere geçecek
    public void NextButton()
    {
        if (isPlayerTurn)
        {
            StartNpcTurn(); // NPC turunu baþlat
            nextTurnButton.gameObject.SetActive(false); // Butonu devre dýþý býrak
            turnCount++; // Tur sayacýný bir artýr
            npcButton.HandleTurnEnd(); // NPC butonlarýnýn tur sonu aksiyonlarý
        }
    }

    // NPC'lerin sýrasýyla iþlem yapacaðý coroutine
    private IEnumerator NpcTurns()
    {
        // NPC kararlarýný verdiriyoruz
        npcDecision.MakeDecisions(); // Her NPC'nin savaþ ya da barýþ kararý verdiði fonksiyon

        // Sýrayla her NPC'nin kararlarýný uygulamasý için bekletiyoruz
        foreach (var npc in npcDecision.npcResourceManagers)
        {
            Debug.Log("NPC " + npc.name + " iþlem yapýyor.");
            yield return new WaitForSeconds(2f); // Her NPC'nin hareketi arasýnda 2 saniye bekletiyoruz
        }

        // NPC'lerin iþlemi bittikten sonra sýra tekrar oyuncuya geçecek
        isPlayerTurn = true;
        nextTurnButton.gameObject.SetActive(true); // Oyuncu tekrar hareket edebilir
    }

    // NPC'lerin sýrasýný baþlatan fonksiyon
    public void StartNpcTurn()
    {
        isPlayerTurn = false; // NPC'ler iþlem yaparken oyuncu sýrada olmasýn
        StartCoroutine(NpcTurns()); // NPC'lerin iþlemlerini baþlat
    }
}

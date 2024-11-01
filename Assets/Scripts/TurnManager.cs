using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public bool isPlayerTurn = true; // Ba�lang��ta oyuncunun s�ras�
    public Button nextTurnButton;    // Next Turn butonu
    public int turnCount = 0;        // Ka� tur ge�ti�ini sayacak saya�
    public NPC_ButtonHandler npcButton; // NPC aksiyonlar�n� y�neten handler
    public NPC_Decision npcDecision; // NPC'lerin kararlar�n� veren script

    // Oyuncunun s�ras� geldi�inde zaman� durduraca��z
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

    // Next Turn butonuna bas�ld���nda oyuncunun s�ras� NPC'lere ge�ecek
    public void NextButton()
    {
        if (isPlayerTurn)
        {
            StartNpcTurn(); // NPC turunu ba�lat
            nextTurnButton.gameObject.SetActive(false); // Butonu devre d��� b�rak
            turnCount++; // Tur sayac�n� bir art�r
            npcButton.HandleTurnEnd(); // NPC butonlar�n�n tur sonu aksiyonlar�
        }
    }

    // NPC'lerin s�ras�yla i�lem yapaca�� coroutine
    private IEnumerator NpcTurns()
    {
        // NPC kararlar�n� verdiriyoruz
        npcDecision.MakeDecisions(); // Her NPC'nin sava� ya da bar�� karar� verdi�i fonksiyon

        // S�rayla her NPC'nin kararlar�n� uygulamas� i�in bekletiyoruz
        foreach (var npc in npcDecision.npcResourceManagers)
        {
            Debug.Log("NPC " + npc.name + " i�lem yap�yor.");
            yield return new WaitForSeconds(2f); // Her NPC'nin hareketi aras�nda 2 saniye bekletiyoruz
        }

        // NPC'lerin i�lemi bittikten sonra s�ra tekrar oyuncuya ge�ecek
        isPlayerTurn = true;
        nextTurnButton.gameObject.SetActive(true); // Oyuncu tekrar hareket edebilir
    }

    // NPC'lerin s�ras�n� ba�latan fonksiyon
    public void StartNpcTurn()
    {
        isPlayerTurn = false; // NPC'ler i�lem yaparken oyuncu s�rada olmas�n
        StartCoroutine(NpcTurns()); // NPC'lerin i�lemlerini ba�lat
    }
}

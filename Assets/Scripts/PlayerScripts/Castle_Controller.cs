using UnityEngine;

public class Castle_Controller : MonoBehaviour
{
    public GameObject[] castles; // 4 farkl� kale
    public GameObject[] panels;  // 4 farkl� panel
    private bool isPanelOpen = false; // Panel a��k m� kontrol�

    public TurnManager turn;

    void Start()
    {
        // Oyunun ba��nda t�m panelleri kapat
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }

    void Update()
    {
        if (turn.isPlayerTurn && !isPanelOpen) // E�er panel a��k de�ilse oyunda t�klamalar aktif
        {
            if (Input.GetMouseButtonDown(0)) // Sol t�klama
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    GameObject clickedObject = hit.transform.gameObject;

                    // Hangi kaleye t�kland�ysa o paneli a�
                    for (int i = 0; i < castles.Length; i++)
                    {
                        if (clickedObject == castles[i]) // T�klanan kale mi?
                        {
                            CloseAllPanels(); // Di�er panelleri kapat
                            panels[i].SetActive(true); // �lgili paneli a�
                            isPanelOpen = true; // Panel a��ld���n� belirle
                            Time.timeScale = 0f; // Oyunu durdur
                        }
                    }
                }
            }
        }
    }

    // T�m panelleri kapat
    public void CloseAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
            isPanelOpen = false; // Panelin kapand���n� belirt
            Time.timeScale = 1f; // Oyunu yeniden ba�lat
        }
    }

    // Panel kapatma fonksiyonu (UI buton i�in)
    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);

    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);

    }
}

using UnityEngine;

public class Castle_Controller : MonoBehaviour
{
    public GameObject[] castles; // 4 farklý kale
    public GameObject[] panels;  // 4 farklý panel
    private bool isPanelOpen = false; // Panel açýk mý kontrolü

    public TurnManager turn;

    void Start()
    {
        // Oyunun baþýnda tüm panelleri kapat
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }

    void Update()
    {
        if (turn.isPlayerTurn && !isPanelOpen) // Eðer panel açýk deðilse oyunda týklamalar aktif
        {
            if (Input.GetMouseButtonDown(0)) // Sol týklama
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    GameObject clickedObject = hit.transform.gameObject;

                    // Hangi kaleye týklandýysa o paneli aç
                    for (int i = 0; i < castles.Length; i++)
                    {
                        if (clickedObject == castles[i]) // Týklanan kale mi?
                        {
                            CloseAllPanels(); // Diðer panelleri kapat
                            panels[i].SetActive(true); // Ýlgili paneli aç
                            isPanelOpen = true; // Panel açýldýðýný belirle
                            Time.timeScale = 0f; // Oyunu durdur
                        }
                    }
                }
            }
        }
    }

    // Tüm panelleri kapat
    public void CloseAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
            isPanelOpen = false; // Panelin kapandýðýný belirt
            Time.timeScale = 1f; // Oyunu yeniden baþlat
        }
    }

    // Panel kapatma fonksiyonu (UI buton için)
    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);

    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);

    }
}

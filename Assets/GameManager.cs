using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public AutoController autoController; // Referenz zum AutoController
    public GameObject[] avatars; // Die drei Avatare über den Tunneln
    public TMP_Text recommendationText; // UI-Text für Empfehlung
    public TMP_Text roundText; // UI-Text für Runde
    
    public TMP_Text blackBoxText; // Der Text in der BlackBox

    private int currentRound = 0;
    private int correctTunnelIndex;
    private int recommendedTunnelIndex;
    private int maxRounds = 7;

    void Start()
    {
        
        StartNewRound();
    }

    void StartNewRound()
    {
        if (currentRound >= maxRounds)
        {
            EndGame();
            return;
        }

        // Zufälliges richtiges Tunnel wählen (0 = Links, 1 = Mitte, 2 = Rechts)
        correctTunnelIndex = Random.Range(0, 3);

        // 4/7 Wahrscheinlichkeit, dass die Empfehlung korrekt ist
        if (Random.value < (4f / 7f))
        {
            recommendedTunnelIndex = correctTunnelIndex; // Richtige Empfehlung
        }
        else
        {
            do
            {
                recommendedTunnelIndex = Random.Range(0, 3);
            } while (recommendedTunnelIndex == correctTunnelIndex);
        }

        // Zeige den Avatar über dem empfohlenen Tunnel
        for (int i = 0; i < avatars.Length; i++)
        {
            avatars[i].SetActive(i == recommendedTunnelIndex);
        }

        // Empfehlungstext setzen
        recommendationText.text = "Ich empfehle dir, in den " + GetTunnelName(recommendedTunnelIndex) + " Tunnel zu fahren!";

        // Runde aktualisieren
        roundText.text = "Runde " + (currentRound + 1) + " / " + maxRounds;

        currentRound++;
    }

    public void PlayerChoseTunnel(int chosenTunnel)
    {
        // Speichere die Entscheidung
        Debug.Log("Spieler hat Tunnel gewählt: " + chosenTunnel);

        // BlackBox-UI mit Entscheidung anzeigen
        
        if (chosenTunnel == correctTunnelIndex)
        {
            blackBoxText.text = "✅ Richtige Entscheidung!";
        }
        else
        {
            blackBoxText.text = "❌ Falsche Entscheidung!";
        }

        
    }


    void EndGame()
    {
        recommendationText.text = "🎉 Spiel vorbei! Danke fürs Spielen.";
        blackBoxText.text = "🏆 Du hast das Spiel beendet!";
    }

    string GetTunnelName(int index)
    {
        if (index == 0) return "linken";
        if (index == 1) return "mittleren";
        return "rechten";
    }
}

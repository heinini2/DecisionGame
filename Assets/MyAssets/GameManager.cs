using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public AutoController autoController; // Referenz zum AutoController
    public GameObject[] avatars; // Die drei Avatare über den Tunneln
    public GameObject buttonleft;
    public GameObject buttonmiddle;
    public GameObject buttonright;
    public GameObject buttonstart;
    public GameObject spatialPanel; // Das Panel, das die Auswahl enthält
    public TMP_Text recommendationText; // UI-Text für Empfehlung
    public TMP_Text roundText; // UI-Text für Runde
    public TMP_Text blackBoxText; // Der Text in der BlackBox
    public TMP_Text scoreText; // Der Text für den Score

    public AudioSource audioSource; // Die Audioquelle für die Avatar-Sprachausgabe
    public AudioClip audioLeft; // Empfehlung für linken Tunnel
    public AudioClip audioMiddle; // Empfehlung für mittleren Tunnel
    public AudioClip audioRight; // Empfehlung für rechten Tunnel

    private int currentRound = 0;
    private int correctTunnelIndex;
    private int recommendedTunnelIndex;
    private int maxRounds = 7;
    private int avatarSet = 0;
    private int score = 0;

    void Start()
    {
        recommendationText.text = "Drücke 'Start', um das Spiel zu beginnen!";
        roundText.text = "";
        blackBoxText.text = "";
        scoreText.text = "Score: " + score;
        buttonleft.SetActive(false);
        buttonmiddle.SetActive(false);
        buttonright.SetActive(false);
        buttonstart.SetActive(true);
        foreach (GameObject avatar in avatars)
        {
            avatar.SetActive(false);
        }

    }

    public void StartGame()
    {
        StartNewRound();
        avatars[avatarSet].SetActive(true);
        // avatars[1].SetActive(false);
        // avatars[2].SetActive(false);
        buttonleft.SetActive(true);
        buttonmiddle.SetActive(true);
        buttonright.SetActive(true);
        buttonstart.SetActive(false);
        
    }

    public void StartNewRound()
    {
        scoreText.text = "Score: " + score;

        if (avatarSet == 2 && currentRound ==maxRounds)
        {
            EndGame();
            return;
        }

        if (currentRound == maxRounds)
        {
            currentRound = 0;
            avatars[avatarSet].SetActive(false);
            avatarSet++;
            avatars[avatarSet].SetActive(true);

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

        //  Empfehlung als Audio abspielen
        PlayRecommendationAudio(recommendedTunnelIndex);
        
        // Empfehlungstext setzen
        recommendationText.text = "Ich empfehle dir, in den " + GetTunnelName(recommendedTunnelIndex) + " Tunnel auszuwählen!";

        // Runde aktualisieren
        roundText.text = "Runde " + (currentRound + 1) + " / " + maxRounds;

        currentRound++;
    }

    public void PlayRecommendationAudio(int tunnelIndex)
    {

        if (audioSource == null) return; // Falls keine AudioSource vorhanden ist, nichts tun

        if (tunnelIndex == 0)
            audioSource.PlayOneShot(audioLeft); // Links
        else if (tunnelIndex == 1)
            audioSource.PlayOneShot(audioMiddle); // Mitte
        else
            audioSource.PlayOneShot(audioRight); // Rechts
    }

    public void RePlayRecommendationAudio()
    {
        int tunnelIndex = recommendedTunnelIndex;
        if (audioSource == null) return; // Falls keine AudioSource vorhanden ist, nichts tun

        if (tunnelIndex == 0)
            audioSource.PlayOneShot(audioLeft); // Links
        else if (tunnelIndex == 1)
            audioSource.PlayOneShot(audioMiddle); // Mitte
        else
            audioSource.PlayOneShot(audioRight); // Rechts
    }


    public void PlayerChoseTunnel(int chosenTunnel)
    {
        // Speichere die Entscheidung
        Debug.Log("Spieler hat Tunnel gewählt: " + chosenTunnel);

        // BlackBox-UI mit Entscheidung anzeigen
        
        if (chosenTunnel == correctTunnelIndex)
        {
            blackBoxText.text = "Richtige Entscheidung!";
            score++;
            
        }
        else
        {
            blackBoxText.text = "Falsche Entscheidung!";
        }

        
    }


    void EndGame()
    {
        recommendationText.text = "Spiel vorbei! Danke fürs Spielen.";
        blackBoxText.text = "Du hast das Spiel beendet!";
        scoreText.text = "Score: " + score;
        spatialPanel.SetActive(false);

    }

    string GetTunnelName(int index)
    {
        if (index == 0) return "linken";
        if (index == 1) return "mittleren";
        return "rechten";
    }
}

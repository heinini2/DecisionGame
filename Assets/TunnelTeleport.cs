using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelTeleport : MonoBehaviour
{


    public Transform teleportDestination; // Zielort, wohin das Auto teleportiert wird
    public AutoController autoController; // Referenz zum AutoController-Skript

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Auto muss den Tag "Player" haben!
        {
            other.transform.position = teleportDestination.position;
            other.transform.rotation = teleportDestination.rotation; // Optional: Auto richtig drehen
        }

        // Bewegung nach dem Teleport stoppen   
            autoController.StopMovement(); // Beendet die Bewegung im Auto-Skript
    }
}

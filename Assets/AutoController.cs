using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoController : MonoBehaviour
{

    public Transform leftLaneEntry;  // Eingang des linken Tunnels
    public Transform middleLaneEntry; // Eingang des mittleren Tunnels
    public Transform rightLaneEntry;  // Eingang des rechten Tunnels

    public Transform leftLaneTarget;  // Zielpunkt NACH dem Tunnel
    public Transform middleLaneTarget;
    public Transform rightLaneTarget;

    public float moveSpeed = 3f;  // Bewegungsgeschwindigkeit

    private Transform currentTarget;  // Das aktuelle Ziel des Autos
    private Transform finalTarget;    // Das Endziel nach dem Eingang

    private bool atEntry = false;  // Wurde der Eingang erreicht?

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = null;  // Anfangs bleibt das Auto stehen
        finalTarget = null;

    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget != null)
        {
            // Bewege das Auto zur aktuellen Zielposition
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, Time.deltaTime * moveSpeed);

            // Prüfen, ob das Auto den Eingang erreicht hat
            if (!atEntry && Vector3.Distance(transform.position, currentTarget.position) < 0.1f)
            {
                atEntry = true;  // Markiere, dass der Eingang erreicht wurde
                currentTarget = finalTarget;  // Jetzt zum Endziel weiterfahren
            }
        }
    }


    // Diese Funktionen werden von den Buttons aufgerufen
    public void SelectLeftLane()
    {
        currentTarget = leftLaneEntry;  // Erst zum Eingang
        finalTarget = leftLaneTarget;   // Dann weiter ins Innere
        atEntry = false;
    }

    public void SelectMiddleLane()
    {
        currentTarget = middleLaneEntry;
        finalTarget = middleLaneTarget;
        atEntry = false;
    }

    public void SelectRightLane()
    {
        currentTarget = rightLaneEntry;
        finalTarget = rightLaneTarget;
        atEntry = false;
    }
}

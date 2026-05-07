using UnityEngine;

public class RotazionePiatto : MonoBehaviour
{
    [Header("Velocità di Rotazione")]
    [Tooltip("Numeri positivi girano in un senso, negativi nell'altro.")]
    public float velocita = 15f;

    void Update()
    {
        // Questo comando fa girare l'oggetto sull'asse Y (verticale)
        // ad ogni singolo frame dell'applicazione.
        // Vector3.up significa (0, 1, 0)
        transform.Rotate(Vector3.up, velocita * Time.deltaTime);
    }
}
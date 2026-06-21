using UnityEngine;

public class SpawnerItaliano : MonoBehaviour
{
    [Header("Configuración del Spawner")]
    [SerializeField] private GameObject prefabEspadachinItaliano; // El prefab que va a clonar
    [SerializeField] private float tiempoEntreSpawns = 5f;       // Cada cuántos segundos sale uno nuevo
    [SerializeField] private float retrasoInicial = 2f;          // Cuánto espera al empezar la partida antes del primero

    private float cronometro;

    private void Start()
    {
        // Seteamos el cronómetro con el retraso inicial
        cronometro = tiempoEntreSpawns - retrasoInicial;
    }

    private void Update()
    {
        cronometro += Time.deltaTime;

        // Cuando el cronómetro llega al tiempo límite, genera un soldado
        if (cronometro >= tiempoEntreSpawns)
        {
            SpawnearTropa();
            cronometro = 0f; // Resetea el reloj
        }
    }

    private void SpawnearTropa()
    {
        if (prefabEspadachinItaliano != null)
        {
            // Instancia al espadachín exactamente en la misma posición que tenga este objeto Spawner
            Instantiate(prefabEspadachinItaliano, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("¡Te olvidaste de asignar el prefab del Espadachín Italiano en el Spawner!");
        }
    }
}
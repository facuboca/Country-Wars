using UnityEngine;

public class SpawnerHolandes : MonoBehaviour
{
    [Header("Prefabs de Enemigos")]
    // Cambiado a array para que puedas poner al Espadachín y a la Holandesa
    [SerializeField] private GameObject[] prefabsEnemigos;

    [Header("Puntos de Spawn")]
    [SerializeField] private Transform[] puntosSpawn;

    [Header("Tiempo")]
    [SerializeField] private float tiempoSpawn = 2f;

    private float temporizador = 0f;

    private void Update()
    {
        temporizador += Time.deltaTime;

        if (temporizador >= tiempoSpawn)
        {
            SpawnEnemigo();

            temporizador = 0f;
        }
    }

    private void SpawnEnemigo()
    {
        // Validaciones de seguridad por si te falta asignar algo en el Inspector
        if (prefabsEnemigos == null || prefabsEnemigos.Length == 0 || puntosSpawn == null || puntosSpawn.Length == 0) return;

        // 1. Elige qué enemigo va a salir (Espadachín o Holandesa) al azar
        int indiceEnemigo = Random.Range(0, prefabsEnemigos.Length);
        GameObject enemigoElegido = prefabsEnemigos[indiceEnemigo];

        // 2. Elige el punto de spawn (línea 1, 2 o 3) al azar
        int indiceSpawn = Random.Range(0, puntosSpawn.Length);
        Transform spawnElegido = puntosSpawn[indiceSpawn];

        // 3. Instancia el enemigo elegido en el punto correspondiente
        if (enemigoElegido != null && spawnElegido != null)
        {
            Instantiate(
                enemigoElegido,
                spawnElegido.position,
                Quaternion.identity
            );
        }
    }
}
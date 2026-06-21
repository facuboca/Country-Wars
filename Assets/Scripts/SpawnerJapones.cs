using UnityEngine;

public class SpawnerJapones : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject prefabJapones;

    [SerializeField] private GameObject prefabGeisha;

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
        // ELIGE UN SPAWN RANDOM
        int indiceSpawn =
            Random.Range(0, puntosSpawn.Length);

        Transform spawnElegido =
            puntosSpawn[indiceSpawn];

        // ELIGE TROPA RANDOM
        int unidadRandom =
            Random.Range(0, 2);

        GameObject unidadElegida;

        if (unidadRandom == 0)
        {
            unidadElegida = prefabJapones;
        }
        else
        {
            unidadElegida = prefabGeisha;
        }

        Instantiate(
            unidadElegida,
            spawnElegido.position,
            Quaternion.identity
        );
    }
}
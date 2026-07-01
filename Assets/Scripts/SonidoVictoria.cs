using UnityEngine;

public class SonidoVictoria : MonoBehaviour
{
    private void OnEnable()
    {
        AudioManager.Instance?.ReproducirSonidoVictoria();
    }
}

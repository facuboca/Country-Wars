using UnityEngine;

public class SonidoDerrota : MonoBehaviour
{
    private void OnEnable()
    {
        AudioManager.Instance?.ReproducirSonidoDerrota();
    }
}
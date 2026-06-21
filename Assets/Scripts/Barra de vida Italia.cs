using UnityEngine;
using UnityEngine.UI; // Requerido para manejar el componente Slider

public class BarraDeVidaItalia : MonoBehaviour
{
    // CAMBIADO: Ahora pide un Slider en el Inspector
    [SerializeField] private Slider sliderBarraDeVida; 

    // Esta función la llama la Bandera al iniciar el juego
    public void InicializarBarraDeVida(float cantidadVidaMaxima)
    {
        if (sliderBarraDeVida != null)
        {
            sliderBarraDeVida.maxValue = cantidadVidaMaxima;
            sliderBarraDeVida.value = cantidadVidaMaxima;
        }
    }

    // Esta función la llama la Bandera cada vez que recibe un disparo
    public void CambiarVidaActual(float vidaActual)
    {
        if (sliderBarraDeVida != null)
        {
            sliderBarraDeVida.value = vidaActual; // El slider se mueve solo
        }
    }
}
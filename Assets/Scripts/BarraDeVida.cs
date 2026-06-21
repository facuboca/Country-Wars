using UnityEngine;
using UnityEngine.UI;

public class BarradeVida : MonoBehaviour
{
    private Slider slider;

    // CAMBIADO: Awake se ejecuta ANTES que el Start de cualquier script
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void CambiarVidaMaxima(float vidaMaxima)
    {
        // Control de seguridad por si no encuentra el componente Slider
        if (slider != null)
        {
            slider.maxValue = vidaMaxima;
        }
    }

    public void CambiarVidaActual(float cantidadVida)
    {
        if (slider != null)
        {
            slider.value = cantidadVida;
        }
    }

    public void InicializarBarraDeVida(float cantidadVida)
    {
        CambiarVidaMaxima(cantidadVida);
        CambiarVidaActual(cantidadVida);
    }
}
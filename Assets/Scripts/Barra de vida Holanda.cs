using UnityEngine;
using UnityEngine.UI; // Si usás Sliders comunes

public class BarraDeVidaHolanda : MonoBehaviour
{
    [SerializeField] private Slider slider; // Tu componente de barra visual

    public void InicializarBarraDeVida(int vidaMaxima)
    {
        if (slider != null)
        {
            slider.maxValue = vidaMaxima;
            slider.value = vidaMaxima;
        }
    }

    public void CambiarVidaActual(int vida)
    {
        if (slider != null)
        {
            slider.value = vida;
        }
    }
}
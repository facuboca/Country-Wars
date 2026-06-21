using UnityEngine;
using UnityEngine.SceneManagement;

public class BanderaArgentina : MonoBehaviour
{
    [SerializeField] private BarradeVida barraDeVida;

    [Header("Vida")]
    public int vidaMaxima = 100;
    private int vidaActual;

    [Header("UI de Derrota")]
    public GameObject pantallaDerrota;

    void Start()
    {
        vidaActual = vidaMaxima;

        if (pantallaDerrota != null)
        {
            pantallaDerrota.SetActive(false);
        }

        if (barraDeVida != null)
        {
            barraDeVida.InicializarBarraDeVida(vidaMaxima);
        }
    }

    public void RecibirDanio(int danio)
    {
        vidaActual -= danio;

        if (barraDeVida != null)
        {
            barraDeVida.CambiarVidaActual(vidaActual);
        }

        if (vidaActual <= 0)
        {
            ActivarDerrota();
        }
    }

    void ActivarDerrota()
    {
        if (pantallaDerrota != null)
        {
            pantallaDerrota.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Reintentar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
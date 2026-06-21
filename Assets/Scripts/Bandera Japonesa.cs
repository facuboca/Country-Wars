using UnityEngine;
using UnityEngine.SceneManagement;

public class BanderaJaponesa : MonoBehaviour
{
    [SerializeField] private BarraDeVidaJapon barraDeVida;

    [Header("Vida")]
    public int vidaMaxima = 100;
    private int vidaActual;

    [Header("UI de Victoria")]
    public GameObject pantallaVictoria;
    public GameObject interfazDeJuego;

    void Start()
    {
        vidaActual = vidaMaxima;

        if (pantallaVictoria != null)
        {
            pantallaVictoria.SetActive(false);
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
            Victoria();
        }
    }

    void Victoria()
    {
        if (pantallaVictoria != null)
        {
            pantallaVictoria.SetActive(true);

            if (interfazDeJuego != null)
            {
                interfazDeJuego.SetActive(false);
            }

            Time.timeScale = 0f;
        }
    }

    public void SiguienteNivel()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex + 1
        );
    }
}
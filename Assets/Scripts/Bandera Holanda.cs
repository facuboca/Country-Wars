using UnityEngine;
using UnityEngine.SceneManagement;

public class BanderaHolandesa : MonoBehaviour
{
    [SerializeField] private BarraDeVidaHolanda barraDeVida; 

    [Header("Vida")]
    public int vidaMaxima = 100;
    private int vidaActual;

    [Header("UI de Victoria")]
    public GameObject pantallaVictoria;
    public GameObject interfazDeJuego;

    private void Start()
    {
        vidaActual = vidaMaxima;

        // Al empezar, nos aseguramos de que esté oculta (como en Japón)
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
        // Si ya se ganó, evitamos que las flechas extras sigan restando vida
        if (vidaActual <= 0) return;

        vidaActual -= danio;

        if (barraDeVida != null)
        {
            barraDeVida.CambiarVidaActual(vidaActual);
        }

        // Cuando la barra se agota por completo, se activa la victoria
        if (vidaActual <= 0)
        {
            Victoria();
        }
    }

    private void Victoria()
    {
        if (pantallaVictoria != null)
        {
            pantallaVictoria.SetActive(true); // <--- Esto vuelve visible la pantalla oculta

            if (interfazDeJuego != null)
            {
                interfazDeJuego.SetActive(false); // Oculta los botones de juego
            }

            Time.timeScale = 0f; // Congela el juego
        }
    }

    public void SiguienteNivel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
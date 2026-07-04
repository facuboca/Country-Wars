using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlGuardadoPartida : MonoBehaviour
{
    private const string ClavePais =
        "PaisDesbloqueado";

    [Header("Menú principal")]
    [SerializeField]
    private Button botonContinuar;

    [Header("Nueva partida")]
    [SerializeField]
    private string escenaNuevaPartida =
        "Selector De Niveles";

    private void Start()
    {
        // Si este componente está en el menú principal,
        // desactiva Continuar cuando no existe guardado.
        if (botonContinuar != null)
        {
            botonContinuar.interactable =
                SistemaGuardado.ExistePartida();
        }
    }

    public void GuardarPartida()
    {
        string escenaActual =
            SceneManager.GetActiveScene().name;

        // Evita guardar accidentalmente el menú.
        if (!escenaActual.StartsWith("Nivel "))
        {
            Debug.LogWarning(
                "La partida solamente se puede guardar " +
                "durante un nivel."
            );

            return;
        }

        int paisActual =
            PlayerPrefs.GetInt(
                ClavePais,
                1
            );

        DatosPartida datos =
            new DatosPartida
            {
                hayPartida = true,
                escenaGuardada = escenaActual,
                paisDesbloqueado = paisActual
            };

        SistemaGuardado.Guardar(datos);

        Debug.Log(
            "Guardado completado. Nivel: " +
            escenaActual
        );
    }

    public void ContinuarPartida()
    {
        DatosPartida datos =
            SistemaGuardado.Cargar();

        if (datos == null ||
            !datos.hayPartida ||
            string.IsNullOrWhiteSpace(
                datos.escenaGuardada
            ))
        {
            Debug.LogWarning(
                "No hay ninguna partida para continuar."
            );

            return;
        }

        if (!Application.CanStreamedLevelBeLoaded(
                datos.escenaGuardada
            ))
        {
            Debug.LogError(
                "La escena guardada no está incluida " +
                "en Build Profiles: " +
                datos.escenaGuardada
            );

            return;
        }

        int progresoActual =
            PlayerPrefs.GetInt(
                ClavePais,
                1
            );

        // Nunca vuelve a bloquear un país
        // que ya haya sido desbloqueado.
        int progresoFinal =
            Mathf.Max(
                progresoActual,
                datos.paisDesbloqueado
            );

        PlayerPrefs.SetInt(
            ClavePais,
            progresoFinal
        );

        PlayerPrefs.Save();

        // Evita cargar el nivel todavía pausado.
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            datos.escenaGuardada
        );
    }

    public void NuevaPartida()
    {
        SistemaGuardado.BorrarPartida();

        PlayerPrefs.SetInt(
            ClavePais,
            1
        );

        PlayerPrefs.Save();

        Time.timeScale = 1f;

        SceneManager.LoadScene(
            escenaNuevaPartida
        );
    }

    public void BorrarGuardado()
    {
        SistemaGuardado.BorrarPartida();

        if (botonContinuar != null)
        {
            botonContinuar.interactable = false;
        }
    }
}
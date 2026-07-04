using System.IO;
using UnityEngine;

public static class SistemaGuardado
{
    private const string NombreArchivo = "partida_guardada.json";

    private static string RutaArchivo
    {
        get
        {
            return Path.Combine(
                Application.persistentDataPath,
                NombreArchivo
            );
        }
    }

    public static void Guardar(DatosPartida datos)
    {
        if (datos == null)
        {
            Debug.LogWarning(
                "No hay datos para guardar."
            );

            return;
        }

        try
        {
            string json = JsonUtility.ToJson(
                datos,
                true
            );

            File.WriteAllText(
                RutaArchivo,
                json
            );

            Debug.Log(
                "Partida guardada en: " +
                RutaArchivo
            );
        }
        catch (System.Exception error)
        {
            Debug.LogError(
                "No se pudo guardar la partida: " +
                error.Message
            );
        }
    }

    public static DatosPartida Cargar()
    {
        if (!File.Exists(RutaArchivo))
        {
            Debug.LogWarning(
                "No existe una partida guardada."
            );

            return null;
        }

        try
        {
            string json = File.ReadAllText(
                RutaArchivo
            );

            DatosPartida datos =
                JsonUtility.FromJson<DatosPartida>(
                    json
                );

            return datos;
        }
        catch (System.Exception error)
        {
            Debug.LogError(
                "No se pudo cargar la partida: " +
                error.Message
            );

            return null;
        }
    }

    public static bool ExistePartida()
    {
        return File.Exists(RutaArchivo);
    }

    public static void BorrarPartida()
    {
        if (File.Exists(RutaArchivo))
        {
            File.Delete(RutaArchivo);

            Debug.Log(
                "Partida guardada eliminada."
            );
        }
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BotonTropaConCosto : MonoBehaviour
{
    [Header("Sistema de monedas")]
    [SerializeField]
    private SistemaMonedas sistemaMonedas;

    [Header("Costo")]
    [SerializeField]
    private int costo = 50;

    [SerializeField]
    private TMP_Text textoCosto;

    [Header("Crear tropa")]
    [SerializeField]
    private UnityEvent alComprarTropa;

    private Button boton;

    private void Awake()
    {
        boton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        boton.onClick.AddListener(
            IntentarComprar
        );

        if (sistemaMonedas != null)
        {
            sistemaMonedas.MonedasCambiadas +=
                ActualizarEstado;
        }
    }

    private void Start()
    {
        if (textoCosto != null)
        {
            textoCosto.text =
                costo.ToString();
        }

        ActualizarEstado(0);
    }

    private void OnDisable()
    {
        if (boton != null)
        {
            boton.onClick.RemoveListener(
                IntentarComprar
            );
        }

        if (sistemaMonedas != null)
        {
            sistemaMonedas.MonedasCambiadas -=
                ActualizarEstado;
        }
    }

    private void IntentarComprar()
    {
        if (sistemaMonedas == null)
        {
            Debug.LogWarning(
                "Falta asignar SistemaMonedas en " +
                gameObject.name
            );

            return;
        }

        if (sistemaMonedas.IntentarGastar(costo))
        {
            alComprarTropa?.Invoke();
        }
        else
        {
            Debug.Log(
                "No hay monedas suficientes."
            );
        }
    }

    private void ActualizarEstado(int monedas)
    {
        if (boton == null)
        {
            return;
        }

        boton.interactable =
            sistemaMonedas != null &&
            sistemaMonedas.PuedePagar(costo);
    }
}
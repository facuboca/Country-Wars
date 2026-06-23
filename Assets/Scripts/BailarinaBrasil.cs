using UnityEngine;
using System.Collections;

public class BailarinaBrasil : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidad = 2f;
    private float velocidadOriginal;

    [Header("Vida")]
    [SerializeField] private int vidaMaxima = 80;
    private int vidaActual;

    [Header("Ataque de plumas")]
    [SerializeField] private GameObject prefabPluma;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private int danioPluma = 15;
    [SerializeField] private float rangoAtaque = 5f;
    [SerializeField] private float tiempoEntreAtaques = 1.5f;

    [Tooltip("Capa de Messi, Gaucho y la Bandera Argentina")]
    [SerializeField] private LayerMask capaAliada;

    private float cronometroAtaque;
    private bool estaAtacando;
    private bool estaLenta;

    private Animator animator;

    private void Start()
    {
        vidaActual = vidaMaxima;
        velocidadOriginal = velocidad;

        animator = GetComponent<Animator>();

        // Permite que pueda disparar apenas encuentre un objetivo.
        cronometroAtaque = tiempoEntreAtaques;
    }

    private void Update()
    {
        DetectarObjetivo();

        if (!estaAtacando)
        {
            BailarYCaminar();
        }
    }

    private void DetectarObjetivo()
    {
        Debug.DrawRay(
            transform.position,
            Vector2.left * rangoAtaque,
            Color.magenta
        );

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.left,
            rangoAtaque,
            capaAliada
        );

        if (hit.collider != null)
        {
            estaAtacando = true;
            cronometroAtaque += Time.deltaTime;

            if (animator != null)
            {
                animator.SetBool("Bailando", false);
                animator.SetBool("Atacando", true);
            }

            if (cronometroAtaque >= tiempoEntreAtaques)
            {
                LanzarPluma();
                cronometroAtaque = 0f;
            }
        }
        else
        {
            estaAtacando = false;
            cronometroAtaque = tiempoEntreAtaques;

            if (animator != null)
            {
                animator.SetBool("Atacando", false);
                animator.SetBool("Bailando", true);
            }
        }
    }

    private void BailarYCaminar()
    {
        // Se mueve mientras reproduce la animación de baile.
        transform.Translate(
            Vector2.left * velocidad * Time.deltaTime
        );

        if (animator != null)
        {
            animator.SetBool("Bailando", true);
            animator.SetBool("Atacando", false);
        }
    }

    private void LanzarPluma()
    {
        if (prefabPluma == null)
        {
            Debug.LogWarning(
                "Falta asignar el prefab de la pluma en BailarinaBrasil."
            );

            return;
        }

        Vector3 posicionDisparo;

        if (puntoDisparo != null)
        {
            posicionDisparo = puntoDisparo.position;
        }
        else
        {
            posicionDisparo = transform.position;
        }

        GameObject nuevaPluma = Instantiate(
            prefabPluma,
            posicionDisparo,
            Quaternion.identity
        );

        PlumaBrasil pluma = nuevaPluma.GetComponent<PlumaBrasil>();

        if (pluma != null)
        {
            pluma.Inicializar(
                Vector2.left,
                danioPluma,
                capaAliada
            );
        }

        if (animator != null)
        {
            animator.SetTrigger("AtaquePlumas");
        }
    }

    public void RecibirDanio(int cantidadDanio)
    {
        vidaActual -= cantidadDanio;

        if (vidaActual <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void AplicarLentitud(
        float velocidadLenta,
        float duracion
    )
    {
        if (!estaLenta)
        {
            StartCoroutine(
                RutinaLentitud(velocidadLenta, duracion)
            );
        }
    }

    private IEnumerator RutinaLentitud(
        float velocidadLenta,
        float duracion
    )
    {
        estaLenta = true;
        velocidad = velocidadLenta;

        yield return new WaitForSeconds(duracion);

        velocidad = velocidadOriginal;
        estaLenta = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        Gizmos.DrawRay(
            transform.position,
            Vector2.left * rangoAtaque
        );
    }
}
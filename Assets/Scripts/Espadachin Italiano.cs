using UnityEngine;
using System.Collections;

public class EspadachinItaliano : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidad = 2f;
    private float velocidadOriginal;

    [Header("Vida")]
    [SerializeField] private int vidaMaxima = 100;
    private int vidaActual;

    [Header("Combate")]
    [SerializeField] private int danio = 20;
    [SerializeField] private float rangoAtaque = 1.2f;         // Distancia real del espadazo
    [SerializeField] private float largoLineaDeteccion = 1.2f; // Rango ultra corto para que se pegue por completo
    [SerializeField] private float tiempoEntreAtaques = 1.5f;
    [SerializeField] private LayerMask capaAliada;             // Capa de los argentinos/base

    private float cronometroAtaque;
    private Animator animator;
    private bool estaAtacando = false;
    private bool estaLento = false; // Controla que no se acumulen las ralentizaciones

    private void Start()
    {
        vidaActual = vidaMaxima;
        animator = GetComponent<Animator>();
        velocidadOriginal = velocidad; // Guardamos la velocidad base (2f)
    }

    private void Update()
    {
        DetectarEnemigo();

        if (!estaAtacando)
        {
            Caminar();
        }
    }

    private void DetectarEnemigo()
    {
        // Dibujamos la línea de debug en tiempo real para ver el nuevo alcance corto
        Debug.DrawRay(transform.position, Vector2.left * largoLineaDeteccion, Color.red);

        // El Raycast busca objetivos solo en esa distancia ultra pegada
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.left,
            largoLineaDeteccion,
            capaAliada
        );

        if (hit.collider != null)
        {
            // Freno inmediato al hacer contacto
            estaAtacando = true;
            cronometroAtaque += Time.deltaTime;

            if (cronometroAtaque >= tiempoEntreAtaques)
            {
                // 1. DAÑO A MESSI
                Messi messi = hit.collider.GetComponent<Messi>();
                if (messi != null)
                {
                    messi.RecibirDanio(danio);
                }

                // 2. DAÑO AL GAUCHO
                Gaucho gaucho = hit.collider.GetComponent<Gaucho>();
                if (gaucho != null)
                {
                    gaucho.RecibirDanio(danio);
                }

                // 3. DAÑO A LA BANDERA 
                BanderaArgentina bandera = hit.collider.GetComponent<BanderaArgentina>();
                if (bandera != null)
                {
                    bandera.RecibirDanio(danio);
                }
                else
                {
                    // Respaldo por mensaje si el script varía
                    hit.collider.gameObject.SendMessage("RecibirDanio", danio, SendMessageOptions.DontRequireReceiver);
                }

                cronometroAtaque = 0f;
            }

            if (animator != null)
            {
                animator.SetBool("Caminando", false);
                animator.SetBool("Atacando", true);
            }
        }
        else
        {
            estaAtacando = false;
            cronometroAtaque = 0f;

            if (animator != null)
            {
                animator.SetBool("Atacando", false);
                animator.SetBool("Caminando", true);
            }
        }
    }

    private void Caminar()
    {
        transform.Translate(
            Vector2.left * velocidad * Time.deltaTime
        );

        if (animator != null)
        {
            animator.SetBool("Caminando", true);
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

    // NUEVA FUNCIÓN: Llamada automáticamente por la Boleadora al impactar
    public void AplicarLentitud(float factorVelocidadLenta, float duracion)
    {
        if (!estaLento)
        {
            StartCoroutine(RutinaLentitud(factorVelocidadLenta, duracion));
        }
    }

    private IEnumerator RutinaLentitud(float factorVelocidadLenta, float duracion)
    {
        estaLento = true;
        velocidad = factorVelocidadLenta; // Baja la velocidad (ej: a 0.5f)

        yield return new WaitForSeconds(duracion); // Espera los 2 segundos de la boleadora

        velocidad = velocidadOriginal; // Restaura la velocidad original (2f)
        estaLento = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.left * largoLineaDeteccion);
    }
}
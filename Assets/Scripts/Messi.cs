using UnityEngine;

public class Messi : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidad = 2f;

    [Header("Vida")]
    [SerializeField] private int vidaMaxima = 100;
    private int vidaActual;

    [Header("Combate")]
    [SerializeField] private float rangoAtaque = 7f;
    [SerializeField] private float tiempoEntreAtaques = 2f;
    [SerializeField] private LayerMask capaEnemiga;

    [Header("Flecha")]
    [SerializeField] private GameObject prefabFlecha;

    private float cronometroAtaque;
    private Animator animator;
    private bool estaAtacando = false;

    private void Start()
    {
        vidaActual = vidaMaxima;
        animator = GetComponent<Animator>();
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
        Debug.DrawRay(
            transform.position,
            Vector2.right * rangoAtaque,
            Color.cyan
        );

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.right,
            rangoAtaque,
            capaEnemiga
        );

        if (hit.collider != null)
        {
            estaAtacando = true;
            cronometroAtaque += Time.deltaTime;

            if (cronometroAtaque >= tiempoEntreAtaques)
            {
                cronometroAtaque = 0f;
            }

            if (animator != null)
            {
                animator.SetFloat("Atacando", 1);
                animator.SetFloat("Caminando", 0);
            }
        }
        else
        {
            estaAtacando = false;
            cronometroAtaque = 0f;

            if (animator != null)
            {
                animator.SetFloat("Atacando", 0);
                animator.SetFloat("Caminando", 1);
            }
        }
    }

    private void Caminar()
    {
        transform.Translate(
            Vector2.right * velocidad * Time.deltaTime
        );
    }

    // ESTA FUNCIÓN SE LLAMA DESDE EL ANIMATION EVENT DE MESSI
    public void LanzarFlecha()
    {
        if (prefabFlecha != null)
        {
            GameObject flecha = Instantiate(
                prefabFlecha,
                transform.position,
                Quaternion.identity
            );

            Rigidbody2D rb = flecha.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // Le aplica velocidad física hacia la derecha (Igual que el Gaucho)
                rb.linearVelocity = Vector2.right * 8f; 
            }
        }
        else
        {
            Debug.LogWarning("Falta asignar prefabFlecha en Messi");
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(
            transform.position,
            Vector2.right * rangoAtaque
        );
    }
}
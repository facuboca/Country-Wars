using UnityEngine;

public class Geisha : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] private int vidaMaxima = 90;
    private int vidaActual;

    [Header("Flor Curativa")]
    [SerializeField] private GameObject prefabFlor;

    private Animator animator;

    private void Start()
    {
        vidaActual = vidaMaxima;

        animator = GetComponent<Animator>();

        InvokeRepeating(
            nameof(LanzarFlor),
            1f,
            2f
        );
    }

    private void Update()
    {
        if (animator != null)
        {
            animator.SetFloat(
                "Curando",
                1
            );

            animator.SetFloat(
                "Caminando",
                0
            );
        }
    }

    public void LanzarFlor()
    {
        if (prefabFlor != null)
        {
            GameObject flor =
                Instantiate(
                    prefabFlor,
                    transform.position,
                    Quaternion.identity
                );

            Rigidbody2D rb =
                flor.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity =
                    Vector2.left * 4f;
            }

            Debug.Log(
                "Geisha lanzó flor"
            );
        }
        else
        {
            Debug.LogWarning(
                "Falta asignar prefabFlor"
            );
        }
    }

    public void RecibirDanio(
        int cantidadDanio
    )
    {
        vidaActual -= cantidadDanio;

        if (vidaActual <= 0)
        {
            Destroy(gameObject);
        }
    }
}
using UnityEngine;

public class FlorHolandesa : MonoBehaviour
{
    [Header("Configuración del Proyectil")]
    [SerializeField] private float velocidad = 5f;
    [SerializeField] private int danio = 15;
    [SerializeField] private float tiempoVida = 3f;

    private Vector2 direccionVuelo = Vector2.left; // Dirección por defecto por si acaso

    private void Start()
    {
        // Se destruye sola si no choca con nadie
        Destroy(gameObject, tiempoVida);
    }

    // Esta función la va a llamar la Holandesa al clonar la flor
    public void ConfigurarObjetivo(Transform objetivo)
    {
        if (objetivo != null)
            {
            // Calculamos la dirección exacta desde la flor hacia Messi/Gaucho/Bandera
            Vector2 posicionObjetivo = targetPositionConOffset(objetivo);
            direccionVuelo = (posicionObjetivo - (Vector2)transform.position).normalized;
            
            // Opcional: Rotar la flor para que apunte visualmente hacia donde vuela
            float angulo = Mathf.Atan2(direccionVuelo.y, direccionVuelo.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angulo);
        }
    }

    private void Update()
    {
        // Ahora vuela con la dirección corregida hacia su objetivo real
        transform.Translate(direccionVuelo * velocidad * Time.deltaTime, Space.World);
    }

    // Un pequeño ajuste por si el centro del objetivo está en los pies
    private Vector2 targetPositionConOffset(Transform t)
    {
        // Si el objetivo es una bandera o personaje alto, le sumamos un poquito en Y 
        // para que la flor apunte al centro del sprite y no al piso
        return new Vector2(t.position.x, t.position.y + 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. DETECTAR SI IMPACTA CONTRA MESSI
        Messi messi = collision.GetComponent<Messi>();
        if (messi != null)
        {
            messi.RecibirDanio(danio);
            Destroy(gameObject);
            return;
        }

        // 2. DETECTAR SI IMPACTA CONTRA EL GAUCHO
        Gaucho gaucho = collision.GetComponent<Gaucho>();
        if (gaucho != null)
        {
            gaucho.RecibirDanio(danio);
            Destroy(gameObject);
            return;
        }

        // 3. DETECTAR SI IMPACTA CONTRA LA BANDERA ARGENTINA
        BanderaArgentina banderaArg = collision.GetComponent<BanderaArgentina>();
        if (banderaArg != null)
        {
            banderaArg.RecibirDanio(danio);
            Destroy(gameObject);
            return;
        }
    }
}
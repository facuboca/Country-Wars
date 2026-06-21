using UnityEngine;

public class Boleadora : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidad = 10f;

    [Header("Daño")]
    [SerializeField] private int danio = 25;

    [Header("Lentitud")]
    [SerializeField] private float velocidadLenta = 0.5f;
    [SerializeField] private float duracionLentitud = 2f;

    [Header("Tiempo de vida")]
    [SerializeField] private float tiempoDeVida = 5f;

    private void Start()
    {
        Destroy(gameObject, tiempoDeVida);
    }

    private void Update()
    {
        transform.Translate(
            Vector2.right * velocidad * Time.deltaTime
        );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ==========================================
        // SECCIÓN JAPÓN Y HOLANDA
        // ==========================================

        // ESPADACHÍN JAPONÉS
        TropaJaponesa enemigo = other.GetComponent<TropaJaponesa>();
        if (enemigo != null)
        {
            enemigo.RecibirDanio(danio);
            enemigo.AplicarLentitud(velocidadLenta, duracionLentitud);
            Destroy(gameObject);
            return;
        }

        // ESPADACHÍN HOLANDÉS
        EspadachinHolandes holandes = other.GetComponent<EspadachinHolandes>();
        if (holandes != null)
        {
            holandes.RecibirDanio(danio);
            holandes.AplicarLentitud(velocidadLenta, duracionLentitud);
            Destroy(gameObject);
            return;
        }

        // GEISHA
        Geisha geisha = other.GetComponent<Geisha>();
        if (geisha != null)
        {
            geisha.RecibirDanio(danio);
            Destroy(gameObject);
            return;
        }

        // BANDERA JAPONESA
        BanderaJaponesa banderaJapon = other.GetComponent<BanderaJaponesa>();
        if (banderaJapon != null)
        {
            banderaJapon.RecibirDanio(danio);
            Destroy(gameObject);
            return; // Agregado return para mantener orden
        }

        // ==========================================
        // NUEVA SECCIÓN ITALIA
        // ==========================================

        // ESPADACHÍN ITALIANO (Daño + Ralentización)
        EspadachinItaliano enemigoItaliano = other.GetComponent<EspadachinItaliano>();
        if (enemigoItaliano != null)
        {
            enemigoItaliano.RecibirDanio(danio);
            
            // Nota: Para que esta linea no te de error, tu script 'EspadachinItaliano' 
            // debe tener una funcion publica llamada 'AplicarLentitud' igual que los otros.
            // Si todavia no la tiene, avisame y la agregamos en un toque.
            enemigoItaliano.AplicarLentitud(velocidadLenta, duracionLentitud);

            Destroy(gameObject);
            return;
        }

        // BANDERA ITALIANA (Solo daño)
        BanderaItalia banderaItaliana = other.GetComponent<BanderaItalia>();
        if (banderaItaliana != null)
        {
            banderaItaliana.RecibirDanio(danio); // Resta vida e impacta la barra de UI
            Destroy(gameObject);
            return;
        }
    }
}
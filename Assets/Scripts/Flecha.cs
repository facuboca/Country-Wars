using UnityEngine;

public class Flecha : MonoBehaviour
{
    [SerializeField] private int danio = 20;
    [SerializeField] private float tiempoDeVida = 3f;

    private void Start()
    {
        // Se destruye automáticamente a los 3 segundos si erra el tiro
        Destroy(gameObject, tiempoDeVida);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ==========================================
        // HOLANDA
        // ==========================================

        // ESPADACHÍN HOLANDÉS
        EspadachinHolandes enemigoHolandes =
            collision.GetComponent<EspadachinHolandes>();

        if (enemigoHolandes != null)
        {
            enemigoHolandes.RecibirDanio(danio);
            Destroy(gameObject);
            return;
        }

        // ARQUERO HOLANDÉS
        ArqueroHolandes arqueroHolandes =
            collision.GetComponent<ArqueroHolandes>();

        if (arqueroHolandes != null)
        {
            arqueroHolandes.RecibirDanio(danio);
            Destroy(gameObject);
            return;
        }

        // HOLANDESA
        Holandesa holandesa =
            collision.GetComponent<Holandesa>();

        if (holandesa != null)
        {
            holandesa.RecibirDanio(danio);
            Destroy(gameObject);
            return;
        }

        // BANDERA HOLANDESA
        BanderaHolandesa banderaHolandesa =
            collision.GetComponent<BanderaHolandesa>();

        if (banderaHolandesa != null)
        {
            banderaHolandesa.RecibirDanio(danio);
            Destroy(gameObject);
            return;
        }

        // ==========================================
        // ITALIA
        // ==========================================

        // ESPADACHÍN ITALIANO
        EspadachinItaliano enemigoItaliano =
            collision.GetComponent<EspadachinItaliano>();

        if (enemigoItaliano != null)
        {
            enemigoItaliano.RecibirDanio(danio);
            Destroy(gameObject);
            return;
        }

        // PIZZERO ITALIANO
        PizzeroItaliano pizzeroItaliano =
            collision.GetComponent<PizzeroItaliano>();

        if (pizzeroItaliano != null)
        {
            pizzeroItaliano.RecibirDanio(danio);
            Destroy(gameObject);
            return;
        }

        // BANDERA ITALIANA
        BanderaItalia banderaItaliana =
            collision.GetComponent<BanderaItalia>();

        if (banderaItaliana != null)
        {
            banderaItaliana.RecibirDanio(danio);
            Destroy(gameObject);
            return;
        }
    }
}
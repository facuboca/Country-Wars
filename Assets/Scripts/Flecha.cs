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
        // SECCIÓN HOLANDA (Tus detecciones anteriores)
        // ==========================================
        
        // 1. COMPROBAR SI LE PEGA AL SOLDADO (ESPADACHÍN HOLANDÉS)
        EspadachinHolandes enemigoHolandes = collision.GetComponent<EspadachinHolandes>();
        if (enemigoHolandes != null)
        {
            enemigoHolandes.RecibirDanio(danio); 
            Destroy(gameObject); // La flecha desaparece al impactar
            return; 
        }

        // 2. COMPROBAR SI LE PEGA A LA HOLANDESA
        Holandesa holandesa = collision.GetComponent<Holandesa>();
        if (holandesa != null)
        {
            holandesa.RecibirDanio(danio); 
            Destroy(gameObject); 
            return;
        }

        // 3. COMPROBAR SI LE PEGA A LA BANDERA HOLANDESA
        BanderaHolandesa banderaHolandesa = collision.GetComponent<BanderaHolandesa>();
        if (banderaHolandesa != null)
        {
            banderaHolandesa.RecibirDanio(danio); 
            Destroy(gameObject); 
            return;
        }

        // ==========================================
        // NUEVA SECCIÓN ITALIA (Agregado para este nivel)
        // ==========================================

        // 4. COMPROBAR SI LE PEGA AL ESPADACHÍN ITALIANO
        EspadachinItaliano enemigoItaliano = collision.GetComponent<EspadachinItaliano>();
        if (enemigoItaliano != null)
        {
            enemigoItaliano.RecibirDanio(danio);
            Destroy(gameObject); // La flecha desaparece
            return;
        }

        // 5. COMPROBAR SI LE PEGA A LA BANDERA ITALIANA
        // Acordate de que cambiamos el nombre a "BanderaItalia" para solucionar el error de MonoBehaviour
        BanderaItalia banderaItaliana = collision.GetComponent<BanderaItalia>();
        if (banderaItaliana != null)
        {
            banderaItaliana.RecibirDanio(danio); // Le resta vida a la bandera y actualiza la barra
            Destroy(gameObject); // La flecha desaparece
            return;
        }
    }
}
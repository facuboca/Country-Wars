using UnityEngine;
using UnityEngine.EventSystems;

public class CameraDrag2D : MonoBehaviour
{
    public float dragSpeed = 0.02f;

    public float limiteIzquierdo = -20f;
    public float limiteDerecho = 20f;

    private Vector3 ultimoMousePos;

    void Update()
    {
        // Verifica si existe EventSystem antes de usarlo
        if (EventSystem.current != null &&
            EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            ultimoMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - ultimoMousePos;

            transform.position -= new Vector3(delta.x * dragSpeed, 0, 0);

            float x = Mathf.Clamp(
                transform.position.x,
                limiteIzquierdo,
                limiteDerecho
            );

            transform.position = new Vector3(
                x,
                transform.position.y,
                transform.position.z
            );

            ultimoMousePos = Input.mousePosition;
        }
    }
}
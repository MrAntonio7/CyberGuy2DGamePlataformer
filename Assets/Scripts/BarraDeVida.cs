using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    public Slider slider; // Arrastra el objeto Slider aqu� desde el Inspector
    public float valorMaximo = 50f;
    public GameObject boss;
    public float valorBarra;
    void Start()
    {
        valorMaximo = boss.GetComponent<BossController>().vidas;
        // Configura el valor m�ximo de la barra de vida
        slider.maxValue = valorMaximo;

        // Inicializa la barra de vida con el valor m�ximo
        ActualizarBarraDeVida(valorMaximo);
    }

    // M�todo para actualizar la barra de vida con un nuevo valor
    public void ActualizarBarraDeVida(float nuevoValor)
    {
        // Asegura que el valor est� dentro del rango permitido
        float valorBarra = Mathf.Clamp(nuevoValor, 0f, valorMaximo);

        // Actualiza el valor del Slider
        slider.value = valorBarra;
    }
}

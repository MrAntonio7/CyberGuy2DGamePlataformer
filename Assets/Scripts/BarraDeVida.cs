using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    public Slider slider; // Arrastra el objeto Slider aquí desde el Inspector
    public float valorMaximo = 50f;
    public GameObject boss;
    public float valorBarra;
    void Start()
    {
        valorMaximo = boss.GetComponent<BossController>().vidas;
        // Configura el valor máximo de la barra de vida
        slider.maxValue = valorMaximo;

        // Inicializa la barra de vida con el valor máximo
        ActualizarBarraDeVida(valorMaximo);
    }

    // Método para actualizar la barra de vida con un nuevo valor
    public void ActualizarBarraDeVida(float nuevoValor)
    {
        // Asegura que el valor esté dentro del rango permitido
        float valorBarra = Mathf.Clamp(nuevoValor, 0f, valorMaximo);

        // Actualiza el valor del Slider
        slider.value = valorBarra;
    }
}

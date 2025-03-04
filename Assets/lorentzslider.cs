using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class lorentzslider : MonoBehaviour
{
    public float beta = 0.0f;
    private float targetbeta = 0.0f;
    public float gamma = 0.0f;
    [SerializeField] private Slider _slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        _slider.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
    }

    // Update is called once per frame
    void Update()
    {
        if (beta > (targetbeta))
        {
            beta = (beta) - 0.01f;
        }
        if (beta < (targetbeta))
        {
            beta = (beta) + 0.01f;
        }
        Debug.Log(beta);
        gamma = 1/Mathf.Sqrt(1-beta);
    }

    public void ValueChangeCheck()
    {
        targetbeta = (float)(_slider.value);
    }
}

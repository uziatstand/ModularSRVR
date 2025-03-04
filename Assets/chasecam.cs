using UnityEngine;

public class chasecam : MonoBehaviour
{
    public GameObject player;
    public GameObject slider;
    private float senslocal = 2.0f;
    public float sens = 2.0f;
    private float x;
    private float y;
    private Vector3 rotateValue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // OH MY GOD? NEW VECTOR3? LIKE, FROM THE HIT SOFTWARE UNITY???
        senslocal = slider.GetComponent<sensslider>().sensitivity;
        y = Input.GetAxis("Mouse X")*senslocal;
        x = Input.GetAxis("Mouse Y")*senslocal;
        rotateValue = new Vector3(x,y*-1,0);
        transform.eulerAngles = transform.eulerAngles - rotateValue;
    }
}

using UnityEngine;
using System.Collections;

public class wallmesh1 : MonoBehaviour {

	private Vector3[] vertices = new Vector3[4];
    private Vector2[] uv = new Vector2[4];
    private Vector3[] baseVert = new Vector3[4];
    private Vector3[] move = new Vector3[4];
    private int[] triangles = new int[6];

    private GameObject meshObject;
    public GameObject slider;
    private Mesh mesh;
    private bool motion = true;

    void Start()
    {
        GenerateMeshData();
        
        mesh = new Mesh();
        mesh.name = "Wall mesh 1";

        meshObject = new GameObject("Mesh object", typeof(MeshRenderer), typeof(MeshFilter));

        meshObject.GetComponent<MeshFilter>().mesh = mesh;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    private void GenerateMeshData()
    {
        vertices[0] = new Vector3(-100,0,20);
        vertices[1] = new Vector3(-100,50,20);
        vertices[2] = new Vector3(100,50,20);
        vertices[3] = new Vector3(100,0,20);

        baseVert[0] = new Vector3(-100,0,20);
        baseVert[1] = new Vector3(-100,50,20);
        baseVert[2] = new Vector3(100,50,20);
        baseVert[3] = new Vector3(100,0,20);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;
    }

    void Update()
    {
        Vector3 base0 = Vector3.Scale(baseVert[0],new Vector3(1/(slider.GetComponent<lorentzslider>().gamma),1,1));
        Vector3 base1 = Vector3.Scale(baseVert[1],new Vector3(1/(slider.GetComponent<lorentzslider>().gamma),1,1));
        Vector3 base2 = Vector3.Scale(baseVert[2],new Vector3(1/(slider.GetComponent<lorentzslider>().gamma),1,1));
        Vector3 base3 = Vector3.Scale(baseVert[3],new Vector3(1/(slider.GetComponent<lorentzslider>().gamma),1,1));

        Vector3 move0 = move[0];
        Vector3 move1 = move[1];
        Vector3 move2 = move[2];
        Vector3 move3 = move[3];

        move0.x += -0.01f*slider.GetComponent<lorentzslider>().beta;
        move1.x += -0.01f*slider.GetComponent<lorentzslider>().beta;
        move2.x += -0.01f*slider.GetComponent<lorentzslider>().beta;
        move3.x += -0.01f*slider.GetComponent<lorentzslider>().beta;

        move[0] = move0;
        move[1] = move1;
        move[2] = move2;
        move[3] = move3;

        vertices[0] = base0 + move0;
        vertices[1] = base1 + move1;
        vertices[2] = base2 + move2;
        vertices[3] = base3 + move3;

        mesh.vertices = vertices;
    }    
}
using UnityEngine;
using System.Collections;

public class wallmesh1 : MonoBehaviour {

    // define all the things we use to construct a mesh as an array of Vector3s. I don't know why unity requires me to use Vector3s but this can and does screw me over later
	private Vector3[] vertices = new Vector3[4]; // mesh node positions; IS modified
    private Vector2[] uv = new Vector2[4]; // material stuff - currently unused (TODO)
    private Vector3[] baseVert = new Vector3[4]; // initial mesh node positions to be squashed according to Lorentz factor; NOT modified
    private Vector3[] move = new Vector3[4]; // used to track how far the mesh should have moved; IS modified
    private int[] triangles = new int[6]; // establish an array to assign a coordinate set to a node and make the mesh; NOT modified

    // define all the objects we steal variables from and all the variables/objects we need
    private GameObject meshObject;
    public GameObject slider;
    private Mesh mesh;
    private bool motion = true;

    void Start()
    {
        // call custom function
        GenerateMeshData();
        
        // spawn mesh object
        mesh = new Mesh();
        mesh.name = "Wall mesh 1";

        meshObject = new GameObject("Mesh object", typeof(MeshRenderer), typeof(MeshFilter));

        meshObject.GetComponent<MeshFilter>().mesh = mesh;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    // put some values into the arrays
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
        // the violence begins
        // Overview of the used method. This is super technical, so skip this paragraph if you want and are not Uzi.
        // Vector3s in Unity are "structs" which means that if you call a "directly defined" vector as above, it can't be directly altered by certain operations - most frustratingly the +- operators
        // So in order to get around that, we have to define all these temporary variables and edit their individual xyz components, then feed those variables back into the actual vectors
        // To ensure that the temporary variables act as variables and not structs, we have to avoid using new Vector3 to define them, as this will turn them into structs.
        // Hence, all these variables refer to other existing vectors, in order to get vectors that act as variables and not structs.
        // This lets us use vector addition.

        // With that out of the way.

        // define throwaway variable-vector basen. Value is defined relative to baseVert, and scaled to 1/gamma: this will be used for length contraction later
        // the value of gamma is stolen from the object slider
        // TODO: make this a for loop
        Vector3 base0 = Vector3.Scale(baseVert[0],new Vector3(1/(slider.GetComponent<lorentzslider>().gamma),1,1));
        Vector3 base1 = Vector3.Scale(baseVert[1],new Vector3(1/(slider.GetComponent<lorentzslider>().gamma),1,1));
        Vector3 base2 = Vector3.Scale(baseVert[2],new Vector3(1/(slider.GetComponent<lorentzslider>().gamma),1,1));
        Vector3 base3 = Vector3.Scale(baseVert[3],new Vector3(1/(slider.GetComponent<lorentzslider>().gamma),1,1));

        // define throwaway variable-vector moven and give it the value of move[n]. This will be used to move the meshes later
        Vector3 move0 = move[0];
        Vector3 move1 = move[1];
        Vector3 move2 = move[2];
        Vector3 move3 = move[3];

        // decrement the value of moven according to beta every frame: when beta is 0, don't decrement the value. when beta is 1, you've reached maximum decrement per update
        // this tracks the total displacement of the meshes so far
        // the value of beta is stolen from the object slider
        // TODO: make this a for loop
        move0.x += -0.01f*slider.GetComponent<lorentzslider>().beta;
        move1.x += -0.01f*slider.GetComponent<lorentzslider>().beta;
        move2.x += -0.01f*slider.GetComponent<lorentzslider>().beta;
        move3.x += -0.01f*slider.GetComponent<lorentzslider>().beta;

        // set the value of move[n] to our updated value of moven
        // TODO: make this a for loop
        move[0] = move0;
        move[1] = move1;
        move[2] = move2;
        move[3] = move3;

        // we now have an updated value for both the mesh node positions' base values squashed according to gamma, and the total displacement accumulated so far
        // we add them together
        // what this basically does is update the mesh node positions to the sum of:
          // before any movement is applied, the length-contraction of the initial positions
          // the displacement so far
        // which results in the length contraction updating in real time as you change the slider in addition to the movement that a non-zero value of beta implies
        // TODO: account for aberration angle in the merge!

        // this took me 8 hours to figure out
        vertices[0] = base0 + move0;
        vertices[1] = base1 + move1;
        vertices[2] = base2 + move2;
        vertices[3] = base3 + move3;

        // finally, actually apply the updated node positions to the mesh
        mesh.vertices = vertices;
    }    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversedBubble : MonoBehaviour
{

    float time;
    float volume;//puissance sonore de la source du son en mW
    float intensite;//calcule l'intensité sonore actuelle du son
    public Renderer rend;
    public Color color;
    //static float maxIntensite = 90f; //volume max en bD
    // Start is called before the first frame update
    float origineScale;
    void Start()
    {
        origineScale = Mathf.Max(transform.parent.transform.lossyScale.x, transform.parent.transform.lossyScale.y, transform.parent.transform.lossyScale.z);
        transform.localScale = new Vector3(1 / origineScale, 1 / origineScale, 1 / origineScale);

        time = transform.root.gameObject.GetComponent<Instrument>().time;
        volume = transform.root.gameObject.GetComponent<Instrument>().volume;
        rend = GetComponent<Renderer>();
        MeshFilter filter = GetComponent(typeof(MeshFilter)) as MeshFilter;
        if (filter != null)
        {
            Mesh mesh = filter.mesh;

            Vector3[] normals = mesh.normals;
            for (int i = 0; i < normals.Length; i++)
                normals[i] = -normals[i];
            mesh.normals = normals;

            for (int m = 0; m < mesh.subMeshCount; m++)
            {
                int[] triangles = mesh.GetTriangles(m);
                for (int i = 0; i < triangles.Length; i += 3)
                {
                    int temp = triangles[i + 0];
                    triangles[i + 0] = triangles[i + 1];
                    triangles[i + 1] = temp;
                }
                mesh.SetTriangles(triangles, m);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x> 1)
        {
            volume = volume / (4 * Mathf.PI * (float)Mathf.Pow((float)transform.localScale.x, 2f));
        }
        intensite = 10*Mathf.Log10(volume / (4 * Mathf.PI / Mathf.Pow(transform.localScale.x, 2f)));
        time = transform.root.gameObject.GetComponent<Instrument>().time;

        transform.localScale = new Vector3((transform.lossyScale.x + time) / origineScale, (transform.lossyScale.x + time) / origineScale, (transform.lossyScale.x + time) / origineScale);
        if ((origineScale * transform.localScale.x) >= 250 || (origineScale * transform.localScale.x) < 1)
        {
            Destroy(this.gameObject, 1.0f);
        }

        Color oldColor = rend.material.color;
        rend.material.color = new Color(oldColor.r, oldColor.g, oldColor.b, 0.5f - ((float)(origineScale * transform.localScale.x) / 500.0f));
    }
}

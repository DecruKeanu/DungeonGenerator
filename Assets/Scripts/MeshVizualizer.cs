using UnityEngine;

public sealed class MeshVizualizer : MonoBehaviour
{
    [SerializeField]
    Material _material = null;

    // Start is called before the first frame update
    public void Visualize(Mesh mesh)
    {
        MeshFilter filter = GetComponent<MeshFilter>( );
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>( );

        filter.mesh = mesh;
        meshRenderer.material = _material;
    }
}

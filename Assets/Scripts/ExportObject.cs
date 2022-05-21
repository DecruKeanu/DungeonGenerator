using SFB;
using UnityEngine;
using UnityFBXExporter;

public sealed class ExportObject : MonoBehaviour
{
    [SerializeField]
    GameObject _objectToExport = null;

    public void OnExport()
    {
        if (_objectToExport.GetComponent<MeshFilter>().mesh.vertexCount == 0)
            return;

        string path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "", "fbx");

        FBXExporter.ExportGameObjToFBX(_objectToExport, path, false, false);
    }
}

using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshVizualizer))]
public sealed class MeshGenerator : MonoBehaviour
{
    private MeshOptimizer _meshOptimizer = new MeshOptimizer();
    private List<GameObject> _corridorCubes = new List<GameObject>();

    public void CreateMesh()
    {
        _meshOptimizer.Clear();
        RoomManager roomManager = FindObjectOfType<RoomManager>();

        List<RoomGenerator> roomGenerators = roomManager.RoomGenerators;
        List<GameObject> corridorObjects = FindObjectOfType<CorridorGenerator>()._corridorObjects;

        foreach (GameObject corridorObject in corridorObjects)
        {
            if (corridorObject == null)
                continue;

            foreach (Transform child in corridorObject.transform)
            {
                if (child == null)
                    continue;

                _corridorCubes.Add(child.gameObject);
            }
        }

        _meshOptimizer.Initialize(roomManager.GridSize);

        foreach (RoomGenerator room in roomGenerators)
        {
            _meshOptimizer.ProcessCubes(room.Cubes);
        }
        _meshOptimizer.ProcessCubes(_corridorCubes);

        Mesh mesh = _meshOptimizer.GenerateMesh();
        GetComponent<MeshVizualizer>().Visualize(mesh);
        GetComponent<MeshCollider>( ).sharedMesh = mesh;

        GameObject dungeon = GameObject.Find("Dungeon");
        
        foreach (Transform child in dungeon.transform)
        {
            Destroy(child.gameObject);
        }

        roomGenerators.Clear();
        corridorObjects.Clear();
    }
}

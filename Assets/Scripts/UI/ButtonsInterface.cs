using UnityEngine;

public sealed class ButtonsInterface : MonoBehaviour
{
    [SerializeField]
    RoomManager _roomManager = null;
    [SerializeField]
    CorridorGenerator _corridorGenerator = null;
    [SerializeField]
    MeshGenerator _meshGenerator = null;
    [SerializeField]
    ExploreMode _exploreMode = null;
    [SerializeField]
    GameObject _explorer = null;

    private bool _isGenerating = false,_doOnce = false, _doTwice = true;

    public void OnGenerate()
    {
        _isGenerating = true;
        _doOnce = false;
        _doTwice = true;
    }

    public void OnExplore()
    {
        _exploreMode._isExploring = true;
    }

    private void Update()
    {
        if (_isGenerating)
        {
            if (_doTwice == false)
            {
                _meshGenerator.CreateMesh();

                _doTwice = true;
                _isGenerating = false;
            }

            if (_doOnce == false)
            {
                _roomManager.GenerateRooms();
                _roomManager.PlaceRooms();
                _explorer.transform.position = _roomManager.RoomGenerators[0].CenterPoint();

                _corridorGenerator.CreateCorridors();

                _doOnce = true;
                _doTwice = false;
            }
        }
    }
}

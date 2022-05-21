using UnityEngine;

public sealed class CameraBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject _centerPoint = null;

    private float _rotationSpeed = 45f;

    [SerializeField]
    ExploreMode _exploreMode = null;

    [SerializeField]
    GameObject _mainCamera = null;

    [SerializeField]
    GameObject _exploreCamera = null;

    private void Update()
    {
        if ( _exploreMode._isExploring )
        {
            if ( _exploreCamera.activeSelf == false)
            {
                _exploreCamera.SetActive( true );
                _mainCamera.SetActive( false );
            }
            return;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            float rotationAngle = _rotationSpeed * Time.deltaTime;

            _centerPoint.transform.Rotate(new Vector3(0, rotationAngle));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            float rotationAngle = _rotationSpeed * Time.deltaTime;

            _centerPoint.transform.Rotate(new Vector3(0, -rotationAngle));
        }
    }
}

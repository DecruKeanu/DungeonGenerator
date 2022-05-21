using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreMode : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCamera = null;

    [SerializeField]
    private Camera _exploreCamera = null;

    public bool _isExploring = false;

    private float _moveSpeed = 4f;
    private float _mouseSpeed = 4f;

    private CharacterController _controller = null;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>( );
    }

    // Update is called once per frame
    void Update()
    {
        if ( _isExploring == false )
            return;

        Cursor.lockState = CursorLockMode.Locked;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(0, mouseX * _mouseSpeed, 0);
        Vector3 movement = transform.right * moveX + _exploreCamera.transform.forward * moveZ;
        _controller.Move(movement.normalized * _moveSpeed * Time.deltaTime);

        _exploreCamera.transform.Rotate(-mouseY * _mouseSpeed, 0, 0);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isExploring = false;
            Cursor.lockState = CursorLockMode.None;
            _mainCamera.gameObject.SetActive(true);
            _exploreCamera.gameObject.SetActive(false);
        }
    }
}

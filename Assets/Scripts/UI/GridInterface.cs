using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public sealed class GridInterface : MonoBehaviour
{
    [SerializeField]
    private Slider _sizeSlider = null;
    [SerializeField]
    private TMP_InputField _sizeInputField = null;

    [SerializeField]
    private GridRenderer _gridRenderer = null;

    [SerializeField]
    private GameObject _camera = null;

    private int _cachedSize = 40;

    public void OnSizeSliderChange(Single newSize)
    {
        int size = (int)newSize;
        float cameraMovement = (_cachedSize - size) * 1.5f;
        Vector3 oldPos = _camera.transform.localPosition;
        _camera.transform.localPosition = new Vector3(oldPos.x, oldPos.y, oldPos.z + cameraMovement);

        _gridRenderer.DrawGrid(size);
        _sizeInputField.text = newSize.ToString();

        _cachedSize = size;
    }

    public void OnSizeValueChange(string newSize)
    {
        if (string.IsNullOrEmpty(newSize))
            return;

        int size = int.Parse(newSize);
        if (size >= _sizeSlider.minValue && size <= _sizeSlider.maxValue)
            _sizeSlider.value = size;

        if (size < (int)_sizeSlider.minValue)
            size = (int)_sizeSlider.minValue;
        else if (size > (int)_sizeSlider.maxValue)
            size = (int)_sizeSlider.maxValue;

        _gridRenderer.DrawGrid(size);
    }

    public void OnSizeValueEndEdit(string newSize)
    {
        if (string.IsNullOrEmpty(newSize))
        {
            _sizeInputField.text = _sizeSlider.minValue.ToString();
            return;
        }

        int size = int.Parse(newSize);
        if (size < (int)_sizeSlider.minValue)
            _sizeInputField.text = _sizeSlider.minValue.ToString();
        else if (size > (int)_sizeSlider.maxValue)
            _sizeInputField.text = _sizeSlider.maxValue.ToString();
    }
}

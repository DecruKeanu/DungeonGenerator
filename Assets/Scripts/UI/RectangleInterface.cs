using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public sealed class RectangleInterface : MonoBehaviour
{
    [SerializeField]
    private Slider _minWidthSlider = null, _maxWidthSlider = null;
    [SerializeField]
    private Slider _minHeightSlider = null, _maxHeightSlider = null;
    [SerializeField]
    private Slider _minDepthSlider = null, _maxDepthSlider = null;
    [SerializeField]
    private TMP_InputField _minWidthInputField = null, _maxWidthInputField = null;
    [SerializeField]
    private TMP_InputField _minHeightInputField = null, _maxHeightInputField = null;
    [SerializeField]
    private TMP_InputField _minDepthInputField = null, _maxDepthInputField = null;

    [SerializeField]
    private RoomManager _roomManager = null;

    public void OnMinWidthSliderChange(Single newValue)
    {
        _roomManager.MinWidth = (int)newValue;
        _minWidthInputField.text = newValue.ToString();
    }
    public void OnMinWidthValueChange(string newValue)
    {
        if (string.IsNullOrEmpty(newValue))
            return;

        int value = int.Parse(newValue);
        if (value >= _minWidthSlider.minValue && value <= _minWidthSlider.maxValue)
            _minWidthSlider.value = value;

        if (value < (int)_minWidthSlider.minValue)
            value = (int)_minWidthSlider.minValue;
        else if (value > (int)_minWidthSlider.maxValue)
            value = (int)_minWidthSlider.maxValue;

        _roomManager.MinWidth = value;
    }
    public void OnMinWidthValueEndEdit(string newSize)
    {
        if (string.IsNullOrEmpty(newSize))
        {
            _minWidthInputField.text = _minWidthSlider.minValue.ToString();
            return;
        }

        int size = int.Parse(newSize);
        if (size < (int)_minWidthSlider.minValue)
            _minWidthInputField.text = _minWidthSlider.minValue.ToString();
        else if (size > (int)_minWidthSlider.maxValue)
            _minWidthInputField.text = _minWidthSlider.maxValue.ToString();
    }

    public void OnMaxWidthSliderChange(Single newValue)
    {
        _roomManager.MaxWidth = (int)newValue;
        _maxWidthInputField.text = newValue.ToString();
    }
    public void OnMaxWidthValueChange(string newValue)
    {
        if (string.IsNullOrEmpty(newValue))
            return;

        int value = int.Parse(newValue);
        if (value >= _maxWidthSlider.minValue && value <= _maxWidthSlider.maxValue)
            _maxWidthSlider.value = value;

        if (value < (int)_maxWidthSlider.minValue)
            value = (int)_maxWidthSlider.minValue;
        else if (value > (int)_maxWidthSlider.maxValue)
            value = (int)_maxWidthSlider.maxValue;

        _roomManager.MaxWidth = value;
    }
    public void OnMaxWidthValueEndEdit(string newSize)
    {
        if (string.IsNullOrEmpty(newSize))
        {
            _maxWidthInputField.text = _maxWidthSlider.minValue.ToString();
            return;
        }

        int size = int.Parse(newSize);
        if (size < (int)_maxWidthSlider.minValue)
            _maxWidthInputField.text = _maxWidthSlider.minValue.ToString();
        else if (size > (int)_maxWidthSlider.maxValue)
            _maxWidthInputField.text = _maxWidthSlider.maxValue.ToString();
    }

    public void OnMinHeightSliderChange(Single newValue)
    {
        _roomManager.MinHeight = (int)newValue;
        _minHeightInputField.text = newValue.ToString();
    }
    public void OnMinHeightValueChange(string newValue)
    {
        if (string.IsNullOrEmpty(newValue))
            return;

        int value = int.Parse(newValue);
        if (value >= _minHeightSlider.minValue && value <= _minHeightSlider.maxValue)
            _minHeightSlider.value = value;

        if (value < (int)_minHeightSlider.minValue)
            value = (int)_minHeightSlider.minValue;
        else if (value > (int)_minHeightSlider.maxValue)
            value = (int)_minHeightSlider.maxValue;

        _roomManager.MinHeight = value;
    }
    public void OnMinHeightValueEndEdit(string newSize)
    {
        if (string.IsNullOrEmpty(newSize))
        {
            _minHeightInputField.text = _minHeightSlider.minValue.ToString();
            return;
        }

        int size = int.Parse(newSize);
        if (size < (int)_minHeightSlider.minValue)
            _minHeightInputField.text = _minHeightSlider.minValue.ToString();
        else if (size > (int)_minHeightSlider.maxValue)
            _minHeightInputField.text = _minHeightSlider.maxValue.ToString();
    }

    public void OnMaxHeightSliderChange(Single newValue)
    {
        _roomManager.MaxHeight = (int)newValue;
        _maxHeightInputField.text = newValue.ToString();
    }
    public void OnMaxHeightValueChange(string newValue)
    {
        if (string.IsNullOrEmpty(newValue))
            return;

        int value = int.Parse(newValue);
        if (value >= _maxHeightSlider.minValue && value <= _maxHeightSlider.maxValue)
            _maxHeightSlider.value = value;

        if (value < (int)_maxHeightSlider.minValue)
            value = (int)_maxHeightSlider.minValue;
        else if (value > (int)_maxHeightSlider.maxValue)
            value = (int)_maxHeightSlider.maxValue;

        _roomManager.MaxHeight = value;
    }
    public void OnMaxHeightValueEndEdit(string newSize)
    {
        if (string.IsNullOrEmpty(newSize))
        {
            _maxHeightInputField.text = _maxHeightSlider.minValue.ToString();
            return;
        }

        int size = int.Parse(newSize);
        if (size < (int)_maxHeightSlider.minValue)
            _maxHeightInputField.text = _maxHeightSlider.minValue.ToString();
        else if (size > (int)_maxHeightSlider.maxValue)
            _maxHeightInputField.text = _maxHeightSlider.maxValue.ToString();
    }

    public void OnMinDepthSliderChange(Single newValue)
    {
        _roomManager.MinDepth = (int)newValue;
        _minDepthInputField.text = newValue.ToString();
    }
    public void OnMinDepthValueChange(string newValue)
    {
        if (string.IsNullOrEmpty(newValue))
            return;

        int value = int.Parse(newValue);
        if (value >= _minDepthSlider.minValue && value <= _minDepthSlider.maxValue)
            _minDepthSlider.value = value;

        if (value < (int)_minDepthSlider.minValue)
            value = (int)_minDepthSlider.minValue;
        else if (value > (int)_minDepthSlider.maxValue)
            value = (int)_minDepthSlider.maxValue;

        _roomManager.MinDepth = value;
    }
    public void OnMinDepthValueEndEdit(string newSize)
    {
        if (string.IsNullOrEmpty(newSize))
        {
            _minDepthInputField.text = _minDepthSlider.minValue.ToString();
            return;
        }

        int size = int.Parse(newSize);
        if (size < (int)_minDepthSlider.minValue)
            _minDepthInputField.text = _minDepthSlider.minValue.ToString();
        else if (size > (int)_minDepthSlider.maxValue)
            _minDepthInputField.text = _minDepthSlider.maxValue.ToString();
    }

    public void OnMaxDepthSliderChange(Single newValue)
    {
        _roomManager.MaxDepth = (int)newValue;
        _maxDepthInputField.text = newValue.ToString();
    }
    public void OnMaxDepthValueChange(string newValue)
    {
        if (string.IsNullOrEmpty(newValue))
            return;

        int value = int.Parse(newValue);
        if (value >= _maxDepthSlider.minValue && value <= _maxDepthSlider.maxValue)
            _maxDepthSlider.value = value;

        if (value < (int)_maxDepthSlider.minValue)
            value = (int)_maxDepthSlider.minValue;
        else if (value > (int)_maxDepthSlider.maxValue)
            value = (int)_maxDepthSlider.maxValue;

        _roomManager.MaxDepth = value;
    }
    public void OnMaxDepthValueEndEdit(string newSize)
    {
        if (string.IsNullOrEmpty(newSize))
        {
            _maxDepthInputField.text = _maxDepthSlider.minValue.ToString();
            return;
        }

        int size = int.Parse(newSize);
        if (size < (int)_maxDepthSlider.minValue)
            _maxDepthInputField.text = _maxDepthSlider.minValue.ToString();
        else if (size > (int)_maxDepthSlider.maxValue)
            _maxDepthInputField.text = _maxDepthSlider.maxValue.ToString();
    }
}

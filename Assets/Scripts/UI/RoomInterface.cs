using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public sealed class RoomInterface : MonoBehaviour
{
    [SerializeField]
    private Slider _rectAmountSlider = null, _rectOffsetSlider = null;
    [SerializeField]
    private TMP_InputField _rectAmountInputField = null, _rectOffsetInputField = null;

    [SerializeField]
    private RoomManager _roomManager = null;

    public void OnRectAmountSliderChange(Single newSize)
    {
        _roomManager._numberOfRects = (int)newSize;
        _rectAmountInputField.text = newSize.ToString();
    }

    public void OnRectAmountValueChange(string newSize)
    {
        if (string.IsNullOrEmpty(newSize))
            return;

        int value = int.Parse(newSize);
        if (value >= _rectAmountSlider.minValue && value <= _rectAmountSlider.maxValue)
            _rectAmountSlider.value = value;

        if (value < (int)_rectAmountSlider.minValue)
            value = (int)_rectAmountSlider.minValue;
        else if (value > (int)_rectAmountSlider.maxValue)
            value = (int)_rectAmountSlider.maxValue;

        _roomManager._numberOfRects = value;
    }

    public void OnRectAmountValueEndEdit(string newSize)
    {
        if (string.IsNullOrEmpty(newSize))
        {
            _rectAmountInputField.text = _rectAmountSlider.minValue.ToString();
            return;
        }

        int size = int.Parse(newSize);
        if (size < (int)_rectAmountSlider.minValue)
            _rectAmountInputField.text = _rectAmountSlider.minValue.ToString();
        else if (size > (int)_rectAmountSlider.maxValue)
            _rectAmountInputField.text = _rectAmountSlider.maxValue.ToString();
    }

    public void OnRectOffsetSliderChange(Single newSize)
    {
        _roomManager._maxOffset = (int)newSize;
        _rectOffsetInputField.text = newSize.ToString();
    }

    public void OnRectOffsetValueChange(string newSize)
    {
        if (string.IsNullOrEmpty(newSize))
            return;

        int value = int.Parse(newSize);
        if (value >= _rectOffsetSlider.minValue && value <= _rectOffsetSlider.maxValue)
            _rectOffsetSlider.value = value;

        if (value < (int)_rectOffsetSlider.minValue)
            value = (int)_rectOffsetSlider.minValue;
        else if (value > (int)_rectOffsetSlider.maxValue)
            value = (int)_rectOffsetSlider.maxValue;

        _roomManager._maxOffset = value;
    }

    public void OnRectOffsetValueEndEdit(string newSize)
    {
        if (string.IsNullOrEmpty(newSize))
        {
            _rectOffsetInputField.text = _rectOffsetSlider.minValue.ToString();
            return;
        }

        int size = int.Parse(newSize);
        if (size < (int)_rectOffsetSlider.minValue)
            _rectOffsetInputField.text = _rectOffsetSlider.minValue.ToString();
        else if (size > (int)_rectOffsetSlider.maxValue)
            _rectOffsetInputField.text = _rectOffsetSlider.maxValue.ToString();
    }
}

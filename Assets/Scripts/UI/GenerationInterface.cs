using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public sealed class GenerationInterface : MonoBehaviour
{
    [SerializeField]
    private Slider _roomAmountSlider = null, _spawnAttemptsSlider = null;
    [SerializeField]
    private TMP_InputField _roomAmountInputField = null, _spawnAttemptsInputField = null;

    [SerializeField]
    private RoomManager _roomManager = null;

    public void OnRoomAmountSliderChange(Single newSize)
    {
        _roomManager._numberOfRooms = (int)newSize;
        _roomAmountInputField.text = newSize.ToString();
    }
    public void OnRoomAmountValueChange(string newSize)
    {
        if (string.IsNullOrEmpty(newSize))
            return;

        int value = int.Parse(newSize);
        if (value >= _roomAmountSlider.minValue && value <= _roomAmountSlider.maxValue)
            _roomAmountSlider.value = value;

        if (value < (int)_roomAmountSlider.minValue)
            value = (int)_roomAmountSlider.minValue;
        else if (value > (int)_roomAmountSlider.maxValue)
            value = (int)_roomAmountSlider.maxValue;

        _roomManager._numberOfRooms = value;
    }
    public void OnRoomAmountValueEndEdit(string newSize)
    {
        if (string.IsNullOrEmpty(newSize))
        {
            _roomAmountInputField.text = _roomAmountSlider.minValue.ToString();
            return;
        }

        int size = int.Parse(newSize);
        if (size < (int)_roomAmountSlider.minValue)
            _roomAmountInputField.text = _roomAmountSlider.minValue.ToString();
        else if (size > (int)_roomAmountSlider.maxValue)
            _roomAmountInputField.text = _roomAmountSlider.maxValue.ToString();
    }

    public void OnSpawnAttemptsSliderChange(Single newSize)
    {
        _roomManager._spawnAttempts = (int)newSize;
        _spawnAttemptsInputField.text = newSize.ToString();
    }

    public void OnSpawnAttemptsValueChange(string newSize)
    {
        if (string.IsNullOrEmpty(newSize))
            return;

        int value = int.Parse(newSize);
        if (value >= _spawnAttemptsSlider.minValue && value <= _spawnAttemptsSlider.maxValue)
            _spawnAttemptsSlider.value = value;

        if (value < (int)_spawnAttemptsSlider.minValue)
            value = (int)_spawnAttemptsSlider.minValue;
        else if (value > (int)_spawnAttemptsSlider.maxValue)
            value = (int)_spawnAttemptsSlider.maxValue;

        _roomManager._spawnAttempts = value;
    }

    public void OnSpawnAttemptsValueEndEdit(string newSize)
    {
        if (string.IsNullOrEmpty(newSize))
        {
            _spawnAttemptsInputField.text = _spawnAttemptsSlider.minValue.ToString();
            return;
        }

        int size = int.Parse(newSize);
        if (size < (int)_spawnAttemptsSlider.minValue)
            _spawnAttemptsInputField.text = _spawnAttemptsSlider.minValue.ToString();
        else if (size > (int)_spawnAttemptsSlider.maxValue)
            _spawnAttemptsInputField.text = _spawnAttemptsSlider.maxValue.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// list of helper functions
/// </summary>
public static class Helpers 
{
    private static Camera _camera;
    /// <summary>
    /// Returns the Camera tagged as "MainCamera"
    /// </summary>
    public static Camera Camera
    {
        get
        {
            if (_camera == null)
                _camera = Camera.main;
            
            return _camera;
        }
    }

    
    private static readonly Dictionary<float, WaitForSeconds> _waitDictionary = new Dictionary<float, WaitForSeconds>();
    /// <summary>
    /// Returns a dictionary for WaitForSeconds for your Coroutines.
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static WaitForSeconds GetWaitForSeconds(float time)
    {
        if (_waitDictionary.TryGetValue(time, out var _wait))
            return _wait;
        _waitDictionary[time] = new WaitForSeconds(time);
        return _waitDictionary[time];
    }

    private static PointerEventData _eventDataCurrentPosition;
    private static List<RaycastResult> _results;
    /// <summary>
    /// Returns true if your cursor is over any UI element
    /// </summary>
    /// <returns></returns>
    public static bool IsOverUI()
    {
        _eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        _results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(_eventDataCurrentPosition, _results);
        return _results.Count > 0;
    }

    /// <summary>
    /// Returns the coresponding world position of a UI element in the Canvas
    /// </summary>
    /// <param name="_element"></param>
    /// <returns></returns>
    public static Vector2 GetWorldPositionOfCanvasElement(RectTransform _element)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(_element, _element.position, Camera, out var _result);
        return _result;
    }

    /// <summary>
    /// Deletes all the children of the passed Object
    /// </summary>
    /// <param name="_parent"></param>
    public static void DeleteChildren(this Transform _parent)
    {
        foreach (Transform _child in _parent)
            Object.Destroy(_child.gameObject);
    }

    /// <summary>
    /// Returns true if the check returns a collision in the provided LayerMask.
    /// The position must be positioned at the feet of the character
    /// </summary>
    /// <param name="_groundPosition"></param>
    /// <param name="_checkRadius"></param>
    /// <param name="_groundMask"></param>
    /// <returns></returns>
    public static bool CheckGround(Vector3 _groundPosition, float _checkRadius, LayerMask _groundMask)
    {
        return Physics.CheckSphere(_groundPosition, _checkRadius, _groundMask);
    }

    /// <summary>
    /// Returns the jumpForce needed to get to a pre determined Height
    /// </summary>
    /// <param name="_jumpHeight"></param>
    /// <param name="_gravity"></param>
    /// <returns></returns>
    public static float GetJumpForceFromHeight(float _jumpHeight, float _gravity)
    {
        return Mathf.Sqrt(_jumpHeight * -2f * _gravity);
    }

    /// <summary>
    /// Wraper for Debug.Log
    /// </summary>
    /// <param name="_message"></param>
    public static void Log(string _message) => Debug.Log(_message);

    /// <summary>
    /// Wraper for Debug.LogWarning
    /// </summary>
    /// <param name="_message"></param>
    public static void Warning(string _message) => Debug.LogWarning(_message);

    /// <summary>
    /// Wraper for Debug.LogError
    /// </summary>
    /// <param name="_message"></param>
    public static void Error(string _message) => Debug.LogError(_message);
}

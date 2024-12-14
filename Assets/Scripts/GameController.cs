using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlType { Normal, worldTilt }

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public ControlType controlType;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Toggles control type between world tilt and normal
    public void ToggleWorldTilt(bool _tilt)
    {
        if (_tilt)
            controlType = ControlType.worldTilt;
        else
            controlType = ControlType.Normal;
    }

}

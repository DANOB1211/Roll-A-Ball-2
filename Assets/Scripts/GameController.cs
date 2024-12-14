using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlType { Normal, worldTilt }
public enum GameType { Normal, SpeedRun }

public enum WallType { Normal, Punishing }

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameType gameType;
    public ControlType controlType;
    public WallType wallType;

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

    //Sets game type from selection
    public void SetGameType(GameType _gameType)
    {
        gameType = _gameType;
    }

    //to toggle between speedrun on or off
    public void ToggleSpeedRun(bool _speedRun)
    {
        if (_speedRun)
            SetGameType(GameType.SpeedRun);
        else
            SetGameType(GameType.Normal);
    }

    //Toggles control type between world tilt and normal
    public void ToggleWorldTilt(bool _tilt)
    {
        if (_tilt)
            controlType = ControlType.worldTilt;
        else
            controlType = ControlType.Normal;
    }

    //Toggles between punishing walls on or off
    public void ToggleWallType(bool _punishing)
    {
        if (_punishing)
            wallType = WallType.Punishing;
        else
            wallType = WallType.Normal;
    }

}

using System;
using System.Collections.Generic;
using _2DMovementController.Scripts;
using LvlFacesManagement;
using UnityEngine;

public interface IPlayerActiveManager
{
    public event Action<PlayerEnum> ChangePlayerActive;
}
public class PlayerActiveManager : MonoBehaviour, IPlayerActiveManager
{
    public event Action<PlayerEnum> ChangePlayerActive;
    private bool _mCanUseChange = true;
    private float _mChangeWaitTime = .7f;
    private float _mCurrentWaitTime = 0;

    [SerializeField] private PlayerController mPlayer1;
    [SerializeField] private PlayerController mPlayer2;
    [SerializeField] private PlayerController mPlayer3;

    private Dictionary<PlayerEnum, IPlayerController> _mPlayerControllers = new();

    private PlayerEnum _mActivePlayer = PlayerEnum.Player1;
    // Start is called before the first frame update
    void Start()
    {
        //Start Dictionary
        _mPlayerControllers.Clear();
        _mPlayerControllers.Add(PlayerEnum.Player1, mPlayer1);
        _mPlayerControllers.Add(PlayerEnum.Player2, mPlayer2);
        _mPlayerControllers.Add(PlayerEnum.Player3, mPlayer3);
        TogglePlayer(_mActivePlayer);
    }

    // Update is called once per frame
    void Update()
    {
        if (ChangedPlayerActive)
        {
            LaunchPlayerToggle();
        }
        ProcessLockTime();
    }
    private bool ChangedPlayerActive => Input.GetKeyDown(KeyCode.L) &&
                                        _mPlayerControllers[_mActivePlayer].State.Grounded && _mCanUseChange;
    private void LaunchPlayerToggle()
    {
        _mCanUseChange = false;
        ChangePlayerIndex();
        TogglePlayer(_mActivePlayer);
        if (ChangePlayerActive != null)
        {
            ChangePlayerActive.Invoke(_mActivePlayer);
        }
    }
    private void ProcessLockTime()
    {
        if (!_mCanUseChange)
        {
            _mCurrentWaitTime += Time.deltaTime;
            if (_mCurrentWaitTime >= _mChangeWaitTime)
            {
                _mCanUseChange = true;
                _mCurrentWaitTime = 0;
            }
        }
    }

    private void ChangePlayerIndex()
    {
        if ((int)_mActivePlayer == 3)
        {
            _mActivePlayer = PlayerEnum.Player1;
        }
        else
        {
            _mActivePlayer++;
        }
    }

    void TogglePlayer(PlayerEnum activePlayer)
    {
        foreach (var playerController in _mPlayerControllers)
        {
            var isActive = playerController.Key == activePlayer ? true : false;
            playerController.Value.TogglePlayer(isActive);
        }
    }

}
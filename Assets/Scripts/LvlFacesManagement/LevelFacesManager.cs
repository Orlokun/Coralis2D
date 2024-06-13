using System;
using System.Collections.Generic;
using System.Linq;
using TransferObject;
using UnityEngine;

namespace LvlFacesManagement
{
    public interface ILevelFacesManager
    {
        public Dictionary<PlayerEnum, ILevelFace> GetLevelFaces { get; }
        public void RotateLevelRow(int rotatedRow, int direction);
        public void RotatePlayerBaseObjectsInRow(PlayerEnum player, int rotatedRow, int direction);
    }

    public class LevelFacesManager : MonoBehaviour, ILevelFacesManager
    {
        public Dictionary<PlayerEnum, ILevelFace> GetLevelFaces => _mLvlFaces;
        private Dictionary<PlayerEnum, ILevelFace> _mLvlFaces = new();
        
        [SerializeField] private List<LevelFace> _mStartLevelFaces;
        private void Awake()
        {
            PopulateInitDictionary();
        }
        
        private void PopulateInitDictionary()
        {
            _mLvlFaces.Clear();
            foreach (var startLevelFace in _mStartLevelFaces)
            {
                startLevelFace.Init();
                _mLvlFaces.Add(startLevelFace.PlayerOwner, startLevelFace);
            }
        }
        
        public void RotateLevelRow(int rotatedRow, int direction)
        {
            Dictionary<PlayerEnum, Transform> rowObjectHolder = new();
            
            foreach (var lvlFace in _mLvlFaces)
            {
                //1. Get Row Parent Objects
                rowObjectHolder.Add(lvlFace.Key, lvlFace.Value.GetRowParentTransform(rotatedRow));
            }
            foreach (var lvlFace in _mLvlFaces)
            {
                //2. Get transferable objects on each players screen
                var rowObjects = lvlFace.Value.GetRowObjects(rotatedRow);
                //3. Get new owner
                var newOwner = GetChangedPlayerEnum(lvlFace.Key, direction);
                //4. Update Position
                rowObjects.ForEach(x =>
                {
                    x.UpdatePosition(rowObjectHolder[newOwner], newOwner);
                });
            }
            //b. Clear Data and update with new transforms
            UpdateObjectsData(rotatedRow);
        }

        public void RotatePlayerBaseObjectsInRow(PlayerEnum player, int rotatedRow, int direction)
        {
            Dictionary<PlayerEnum, Transform> rowObjectHolder = new();
            foreach (var lvlFace in _mLvlFaces)
            {
                //1. Get Row Parent Objects
                rowObjectHolder.Add(lvlFace.Key, lvlFace.Value.GetRowParentTransform(rotatedRow));
            }
            foreach (var lvlFace in _mLvlFaces)
            {
                //2. Get transferable objects on each players screen
                var rowObjects = lvlFace.Value.RowPlayerBasedObjects(rotatedRow, player);
                if (rowObjects.Count == 0)
                {
                    continue;
                }
                
                //3. Get new owner
                var newOwner = GetChangedPlayerEnum(lvlFace.Key, direction);
                //4. Update Position
                rowObjects.ForEach(x =>
                {
                    x.UpdatePosition(rowObjectHolder[newOwner], newOwner);
                });
            }
            UpdatePlayerBasedObjectsData(rotatedRow, player);
        }

        private void UpdateObjectsData(int currentRow)
        {
            foreach (var lvlFace in _mLvlFaces)
            {
                lvlFace.Value.UpdateRowObjectsData(currentRow);
            }
        }
        private void UpdatePlayerBasedObjectsData(int currentRow, PlayerEnum interactionOwner)
        {
            foreach (var lvlFace in _mLvlFaces)
            {
                lvlFace.Value.UpdateRowPlayerBasedObjectData(currentRow, interactionOwner);
            }
        }
        private PlayerEnum GetChangedPlayerEnum(PlayerEnum currentOwner, int direction)
        {
            if (!IsPlayerAllowed(currentOwner))
            {
                Debug.LogError("Current Owner must be one of three players. Not any or none");
                return PlayerEnum.Any;
            }
            if (direction < 0)
            {
                if (currentOwner == PlayerEnum.Player1)
                {
                    return PlayerEnum.Player3;
                }
                return currentOwner-1;
            }
            if(direction>0)
            {
                if (currentOwner == PlayerEnum.Player3)
                {
                    return PlayerEnum.Player1;
                }
                return currentOwner + 1;
            }
            Debug.LogError("[LevelFacesManager.GetChangedPlayerEnum] Player Swap Logic must not reach this point");
            return PlayerEnum.Any;
        }

        private bool IsPlayerAllowed(PlayerEnum playerEnum)
        {
            return playerEnum == PlayerEnum.Player1 || playerEnum == PlayerEnum.Player2 ||
                   playerEnum == PlayerEnum.Player3;
        }
    }
}
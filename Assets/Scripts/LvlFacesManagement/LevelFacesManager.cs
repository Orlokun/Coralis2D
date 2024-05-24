using System.Collections.Generic;
using UnityEngine;

namespace LvlFacesManagement
{
    public class LevelFacesManager : MonoBehaviour, ILevelFacesManager
    {
        public Dictionary<PlayerEnum, ILevelFaceObjectManager> LevelFaces => _mLvlFacesManagers;
        private Dictionary<PlayerEnum, ILevelFaceObjectManager> _mLvlFacesManagers = new();
        [SerializeField] private List<ILevelFaceObjectManager> _mStartLevelFaces;
    
        public void Awake()
        {
            PopulateInitDictionary();
        }

        private void PopulateInitDictionary()
        {
            _mLvlFacesManagers.Clear();
            foreach (var startLevelFace in _mStartLevelFaces)
            {
                startLevelFace.PopulateStartList();
                _mLvlFacesManagers.Add(startLevelFace.PlayerOwner, startLevelFace);
            }
        }
    }

    public interface ILevelFacesManager
    {
        public Dictionary<PlayerEnum, ILevelFaceObjectManager> LevelFaces { get; }
    }
}
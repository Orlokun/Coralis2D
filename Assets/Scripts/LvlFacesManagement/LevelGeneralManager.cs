using UnityEngine;

namespace LvlFacesManagement
{
    public class LevelGeneralManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }

//If this enum ever gets used as index, make sure to use -1
    public enum PlayerEnum
    {
        Player1 = 1,
        Player2,
        Player3,
        Any
    }
}
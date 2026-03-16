using System;
using System.Collections.Generic;
using UnityEngine;
using RoomSystem;
namespace GraphViewTools
{
    
    [CreateAssetMenu, Serializable]
    public class RoomData : ScriptableObject
    {
        public string SceneName;
        public string Guid;
        public List<DoorData> Entrances; // use GUIDS
        public List<DoorData> ExitPositions; 
        public List<string> SpawnPositions; //positions are keys for tags, since I expect duplicate tags but unique positions
        //maybe add item spawners, if the enemy death location isn't the spawn position
        // ??


    }
    
}
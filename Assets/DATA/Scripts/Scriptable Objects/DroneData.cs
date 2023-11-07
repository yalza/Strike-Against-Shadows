using UnityEngine;

namespace DATA.Scripts.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "Drone Data", menuName = "ScriptableObjects/Drone Data", order = 2)]
    public class DroneData : TurretData
    {
        public float moveSpeed;
    }
}
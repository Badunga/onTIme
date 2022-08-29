using UnityEngine;

namespace OnTime.Plattform
{
    public class StartPositionCalculator
    {
        private const int END_MOVE = 25;
        public Vector3 OnLeftSide(Transform plattformPosition)
        {
            return new Vector3(plattformPosition.position.x + END_MOVE, plattformPosition.position.y, plattformPosition.position.z);
        }
        
        public Vector3 OnRightSide(Transform plattformPosition)
        {
            return new Vector3(plattformPosition.position.x - END_MOVE, plattformPosition.position.y, plattformPosition.position.z);
        }
        
        public Vector3 EndOfUp(Transform plattformPosition)
        {
            return new Vector3(plattformPosition.position.x , plattformPosition.position.y, plattformPosition.position.z + END_MOVE);
        }
        
        public Vector3 EndOfDown(Transform plattformPosition)
        {
            return new Vector3(plattformPosition.position.x , plattformPosition.position.y, plattformPosition.position.z - END_MOVE);
        }
    }
}
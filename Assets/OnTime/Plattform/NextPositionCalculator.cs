using UnityEngine;

namespace OnTime.Plattform
{
    public class NextPositionCalculator
    {
        private const float NEXT_POSITION = 25;
        
        public Vector3 NextPositionOfZ(Transform plattformX)
        {
            return new Vector3(plattformX.position.x, plattformX.position.y, plattformX.position.z + NEXT_POSITION);
        }
        
        public Vector3 NextPositionOfX(Transform plattformZ)
        {
            return new Vector3(plattformZ.position.x + NEXT_POSITION, plattformZ.position.y, plattformZ.position.z);
        }
    }
}
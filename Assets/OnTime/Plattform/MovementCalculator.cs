using UnityEngine;

namespace OnTime.Plattform
{
    public class MovementCalculator
    {
        public Vector3 GenerateNextMovement( Vector3 plattFormLeft, Vector3 plattFormRight, float speedUp)
        {
            var time = Mathf.PingPong((Time.time*speedUp ), 1);
            return Vector3.Lerp(plattFormLeft, plattFormRight, time);
        }
    }
}
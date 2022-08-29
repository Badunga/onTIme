using OnTime.Plattform;
using UnityEngine;

namespace OnTime.Player
{
    public class PlayerPosition
    {
        public Vector3 ForJump(Vector3 position, PlattformAxis plattformAxis, float plattformJumpPosition)
        {
            switch (plattformAxis)
            {
                case PlattformAxis.X:
                    return new Vector3(position.x, position.y, plattformJumpPosition);

                case PlattformAxis.Z:
                    return new Vector3(plattformJumpPosition, position.y, position.z);

                default:
                    return Vector3.zero;
            }
        }

        public Vector3 AfterJump(PlattformAxis plattformAxis, Transform player, float distance, float differenceBetweenPlattformAndPlayer)
        {
            var difference = differenceBetweenPlattformAndPlayer / 2;

            if (differenceBetweenPlattformAndPlayer < 0 )
            {
                return player.position;
            }
            switch (plattformAxis)
            {
                case PlattformAxis.Z:
                    return new Vector3(player.position.x, player.position.y, ChooseOperator(distance, player.position.z, difference));
                case PlattformAxis.X:
                    return new Vector3(ChooseOperator(distance, player.position.x, difference), player.position.y, player.position.z);
                default:
                    return Vector3.zero;
            }
        }

        private float ChooseOperator(float distance, float playerPositionAxis, float differenceBetweenPlattformAndPlayer)
        {
            return distance < 0 ? (playerPositionAxis + differenceBetweenPlattformAndPlayer) : (playerPositionAxis - differenceBetweenPlattformAndPlayer);
        }
    }
}
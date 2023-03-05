using System;

namespace Data.ValueObjects
{
    [Serializable]
    public struct PlayerData
    {
        public MovementData MovementData;
        public ScaleData ScaleData;

        public PlayerData(MovementData movementData, ScaleData scaleData)
        {
            MovementData = movementData;
            ScaleData = scaleData;
        }
    }

    [Serializable]
    public struct MovementData
    {
        public float ForwardSpeed;
        public float SidewaysSpeed;

        public MovementData(float forwardSpeed, float sidewaysSpeed)
        {
            ForwardSpeed = forwardSpeed;
            SidewaysSpeed = sidewaysSpeed;
        }
    }

    [Serializable]
    public struct ScaleData
    {
        public float ScaleFactor;

        public ScaleData(float scaleFactor)
        {
            ScaleFactor = scaleFactor;
        }
    }
}

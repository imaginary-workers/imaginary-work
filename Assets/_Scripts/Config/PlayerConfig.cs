using UnityEngine;

namespace Game.Config
{
    [System.Serializable]
    public class PlayerConfig
    {
        public float topClamp = 90.0f;
        public float bottomClamp = -90.0f;
        public float rotationSpeed = 20f;
        public bool invertedYAxis = false;
        public bool invertedXAxis = false;

        public PlayerConfig(PlayerConfig playerConfig)
        {
            topClamp = playerConfig.topClamp;
            bottomClamp = playerConfig.bottomClamp;
            rotationSpeed = playerConfig.rotationSpeed;
            invertedYAxis = playerConfig.invertedYAxis;
            invertedXAxis = playerConfig.invertedXAxis;
        }

        public PlayerConfig()
        {
        }
    }
}
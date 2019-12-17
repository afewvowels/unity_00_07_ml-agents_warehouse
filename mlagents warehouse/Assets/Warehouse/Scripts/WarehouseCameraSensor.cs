using UnityEngine;

namespace MLAgents.Sensor
{
    public class WarehouseCameraSensor : SensorComponent
    {
        public new Camera camera;

        public string sensorName = "BoxGuyCameraSensor";
        public int width = 84;
        public int height = 84;
        public bool grayscale;

        public override ISensor CreateSensor()
        {
            return new CameraSensor(camera, width, height, grayscale, sensorName);
        }

        public override int[] GetObservationShape()
        {
            return new[] { height, width, grayscale ? 1 : 3 };
        }
    }
}
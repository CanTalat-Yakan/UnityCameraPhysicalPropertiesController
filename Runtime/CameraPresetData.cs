using UnityEngine;

namespace UnityEssentials
{
    public enum SensorStandard
    {
        Standard,               // X: 50     Y: 50
        Hero12Black,            // X: 7.6    Y: 5.7
        Hero11Black,            // X: 8      Y: 6
        Hero10Black,            // X: 6.17   Y: 4.55
        _8mm,                   // X: 4.8    Y: 3.5
        Super8mm,               // X: 5.79   Y: 4.01
        _16mm,                  // X: 10.26  Y: 7.49
        Super16mm,              // X: 12.522 Y: 7.417
        _35mm_2Perf,            // X: 21.95  Y: 9.35
        _35mm_Academy,          // X: 21.946 Y: 16.002
        Super35,                // X: 24.89  Y: 18.66
        _35mm_TV_Projection,    // X: 20.726 Y: 15.545
        _35mm_Full_Aperture,    // X: 24.892 Y: 18.669
        _35mm_185_Projection,   // X: 20.955 Y: 11.328
        _35mm_Anamorphic,       // X: 21.946 Y: 18.593
        _65mm_ALEXA,            // X: 54.12  Y: 25.59
        _70mm,                  // X: 52.476 Y: 23.012
        _70mm_IMAX,             // X: 70.41  Y: 52.63
    }

    [CreateAssetMenu(fileName = "CameraPreset_", menuName = "Camera/Camera Preset")]
    public class CameraPresetData : ScriptableObject
    {
        [Space]
        public Vector2 ShutterRange = new(2000, 50);

        [Space]
        public Vector2 FStopRange = new(3.5f, 5.6f);
        public Vector2Int ISORange = new(100, 2000);

        [Space]
        public Vector2 FocalLengthRange = new(22f, 70f);
        public bool LensDistortion = true;

        [Space]
        public SensorStandard SensorType = SensorStandard.Super35;
        [HideInInspector] public Vector2 SensorSize; // The size calculated from the sensor type 

        public void OnValidate()
        {
            // Auto-configure based on standard
            switch (SensorType)
            {
                case SensorStandard.Standard:
                    SensorSize = new Vector2(50f, 50f);
                    break;
                case SensorStandard.Hero12Black:
                    SensorSize = new Vector2(7.6f, 5.7f);
                    break;
                case SensorStandard.Hero11Black:
                    SensorSize = new Vector2(8f, 6f);
                    break;
                case SensorStandard.Hero10Black:
                    SensorSize = new Vector2(6.17f, 4.55f);
                    break;
                case SensorStandard._8mm:
                    SensorSize = new Vector2(4.8f, 3.5f);
                    break;
                case SensorStandard.Super8mm:
                    SensorSize = new Vector2(5.79f, 4.01f);
                    break;
                case SensorStandard._16mm:
                    SensorSize = new Vector2(10.26f, 7.49f);
                    break;
                case SensorStandard.Super16mm:
                    SensorSize = new Vector2(12.522f, 7.417f);
                    break;
                case SensorStandard._35mm_2Perf:
                    SensorSize = new Vector2(21.95f, 9.35f);
                    break;
                case SensorStandard._35mm_Academy:
                    SensorSize = new Vector2(21.946f, 16.002f);
                    break;
                case SensorStandard.Super35:
                    SensorSize = new Vector2(24.89f, 18.66f);
                    break;
                case SensorStandard._35mm_TV_Projection:
                    SensorSize = new Vector2(20.726f, 15.545f);
                    break;
                case SensorStandard._35mm_Full_Aperture:
                    SensorSize = new Vector2(24.892f, 18.669f);
                    break;
                case SensorStandard._35mm_185_Projection:
                    SensorSize = new Vector2(20.955f, 11.328f);
                    break;
                case SensorStandard._35mm_Anamorphic:
                    SensorSize = new Vector2(21.946f, 18.593f);
                    break;
                case SensorStandard._65mm_ALEXA:
                    SensorSize = new Vector2(54.12f, 25.59f);
                    break;
                case SensorStandard._70mm:
                    SensorSize = new Vector2(52.476f, 23.012f);
                    break;
                case SensorStandard._70mm_IMAX:
                    SensorSize = new Vector2(70.41f, 52.63f);
                    break;
                default:
                    SensorSize = Vector2.zero;
                    break;
            }
        }
    }
}
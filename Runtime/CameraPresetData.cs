using UnityEngine;

namespace UnityEssentials
{
    public enum VideoStandard
    {
        Dynamic,   // No fixed framerate (e.g., virtual cameras, screen capture)
        NTSC,       // 29.97fps interlaced (60i) – North America, Japan
        PAL,        // 25fps interlaced (50i) – Europe, Australia
        SECAM,      // 25fps interlaced (50i) – France, ex-USSR
        ATSC,       // Modern digital broadcast (varies, typically 1080i60 or 720p60)
        DCI,        // Digital Cinema (24fps progressive)
        HDTV_720p,  // 720p60 (common for sports/broadcast)
        HDTV_1080i, // 1080i60 (common for broadcast)
        HDTV_1080p, // 1080p24/30/60 (Blu-ray, streaming)
        UHDTV_4K,   // 2160p24/30/60 (4K UHD)
        UHDTV_8K,   // 4320p60 (8K UHD, experimental)
        FILM_24p,   // Pure 24fps progressive (film standard)
        FILM_48p    // High-frame-rate film (e.g., "The Hobbit" at 48fps)
    }

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
        [Header("Camera Identity")]
        public string ModelName = "Generic Camera";
        public string Manufacturer = "Manufacturer";
        public int ReleaseYear = 2025;

        [Header("Standards")]
        public VideoStandard VideoStandard = VideoStandard.Dynamic;
        public SensorStandard SensorType = SensorStandard.Super35;

        [Header("Shutter Control")]
        public Vector2 ShutterRange = new(60, 1);

        [Header("Exposure Settings")]
        public Vector2 FStopRange = new(1.2f, 22f);
        public Vector2Int ISORange = new(100, 25600); 
        public float NoiseStrength = 0.5f;

        [Header("Lens Specifications")]
        public Vector2 FocalLengthRange = new(24f, 70f);
        public bool AffectLensDistortion = false;

        [Header("Timing Settings")]
        [HideInInspector] public Vector2 SensorSize;    // The size calculated from the sensor type 
        [HideInInspector] public float FrameRate;       // Actual frames per second (e.g., 29.97)
        [HideInInspector] public bool Interlaced;       // Whether to use interlaced scanning
        [HideInInspector] public float FieldRate;       // Fields per second (e.g., 60 for NTSC)

        public void OnValidate()
        {
            // Auto-configure based on standard
            switch (VideoStandard)
            {
                case VideoStandard.Dynamic:
                    FrameRate = 0;
                    Interlaced = false;
                    FieldRate = 0;
                    break;

                case VideoStandard.NTSC:
                    FrameRate = 29.97f;
                    Interlaced = true;
                    FieldRate = 59.94f;
                    break;

                case VideoStandard.PAL:
                case VideoStandard.SECAM:
                    FrameRate = 25f;
                    Interlaced = true;
                    FieldRate = 50f;
                    break;

                case VideoStandard.ATSC:
                    // ATSC supports multiple formats; default to 1080i60
                    FrameRate = 29.97f;
                    Interlaced = true;
                    FieldRate = 59.94f;
                    break;

                case VideoStandard.DCI:
                case VideoStandard.FILM_24p:
                    FrameRate = 24f;
                    Interlaced = false;
                    FieldRate = 24f;
                    break;

                case VideoStandard.HDTV_720p:
                    FrameRate = 60f;
                    Interlaced = false;
                    FieldRate = 60f;
                    break;

                case VideoStandard.HDTV_1080i:
                    FrameRate = 29.97f;
                    Interlaced = true;
                    FieldRate = 59.94f;
                    break;

                case VideoStandard.HDTV_1080p:
                    // Common options: 24p, 30p, 60p (default to 30p)
                    FrameRate = 30f;
                    Interlaced = false;
                    FieldRate = 30f;
                    break;

                case VideoStandard.UHDTV_4K:
                    // Common options: 24p, 30p, 60p (default to 30p)
                    FrameRate = 30f;
                    Interlaced = false;
                    FieldRate = 30f;
                    break;

                case VideoStandard.UHDTV_8K:
                    // Typically 60fps due to high data rate
                    FrameRate = 60f;
                    Interlaced = false;
                    FieldRate = 60f;
                    break;

                case VideoStandard.FILM_48p:
                    FrameRate = 48f;
                    Interlaced = false;
                    FieldRate = 48f;
                    break;

                default:
                    Debug.LogWarning("Unknown video standard. Using default NTSC settings.");
                    FrameRate = 29.97f;
                    Interlaced = true;
                    FieldRate = 59.94f;
                    break;
            }

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
                    SensorSize = Vector2.zero; // Fallback in case of an invalid value
                    break;
            }
        }
    }
}
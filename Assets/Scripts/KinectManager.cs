using UnityEngine;
using UnityEngine.UI;

using Windows.Kinect;

using System.Linq;

public class KinectManager : MonoBehaviour
{
    private KinectSensor _sensor;
    private BodyFrameReader _bodyFrameReader;
    private Body[] _bodies = null;

    public GameObject kinectAvailableText;
    public Text handXText;

    public bool IsAvailable;
    public float PaddlePosition;
    public bool IsFire;



   // public static GM instance = null;


    public static KinectManager instance = null;

    public Body[] GetBodies()
    {
        return _bodies;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        _sensor = KinectSensor.GetDefault();

        if (_sensor != null)
        {
            IsAvailable = _sensor.IsAvailable;

            kinectAvailableText.SetActive(IsAvailable);

            _bodyFrameReader = _sensor.BodyFrameSource.OpenReader();

            if (!_sensor.IsOpen)
            {
                _sensor.Open();
            }

            _bodies = new Body[_sensor.BodyFrameSource.BodyCount];
        }
    }

    // Update is called once per frame
    void Update()
    {
        IsAvailable = _sensor.IsAvailable;

        if (_bodyFrameReader != null)
        {
            var frame = _bodyFrameReader.AcquireLatestFrame();

            if (frame != null)
            {
                frame.GetAndRefreshBodyData(_bodies);

                foreach (var body in _bodies.Where(b => b.IsTracked))
                {
                    IsAvailable = true;

                    if (body.HandRightConfidence == TrackingConfidence.High && body.HandRightState == HandState.Lasso)
                    {
                        IsFire = true;
                    }

                    else if (body.HandRightConfidence == TrackingConfidence.High && body.HandRightState == HandState.Open)
                    {

                        Time.timeScale = 0;
                        //_bodyFrameReader.IsPaused = true;
                        //_bodyFrameReader.Dispose();
                        // _bodyFrameReader = null;

                       
                    }

                    else if (Time.timeScale == 0 && body.HandRightConfidence == TrackingConfidence.High && body.HandRightState == HandState.Closed)
                    {
                        Time.timeScale = 1;
                        Update();
                    }

                    /*  if (_bodyFrameReader.IsPaused == true || body.HandRightConfidence == TrackingConfidence.High && body.HandRightState == HandState.Closed) {

                          PaddlePosition = RescalingToRangesB(-1, 1, -8, 8, body.Lean.X);
                          handXText.text = PaddlePosition.ToString();
                      }
                      */

                    else
                    {
                        PaddlePosition = RescalingToRangesB(-1, 1, -8, 8, body.Lean.X);
                        handXText.text = PaddlePosition.ToString();
                    }
                }

                frame.Dispose();
                frame = null;
            }
        }
    }

    
  
    
    
    static float RescalingToRangesB(float scaleAStart, float scaleAEnd, float scaleBStart, float scaleBEnd, float valueA)
    {
        return (((valueA - scaleAStart) * (scaleBEnd - scaleBStart)) / (scaleAEnd - scaleAStart)) + scaleBStart;
    }

    void OnApplicationQuit()
    {
        if (_bodyFrameReader != null)
        {
            //_bodyFrameReader.IsPaused = true;
         //   _bodyFrameReader.Dispose();
            //_bodyFrameReader = null;
          
        }
        

    

       //if (_sensor != null)
      //  {
      //      if (_sensor.IsOpen)
       //     {
       //         _sensor.Close();
      //      }

      //      _sensor = null;
     //   }
    }
}






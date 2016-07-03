using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;

namespace MainProject
{
    
   public class CustomListner : Listener
    {
        public event Action<Vector> OnFingersRegistered;
        public event Action<GestureList> OnGestureMade;
        public event Action<Frame> onFrameDetected;


        public long _now;
        public long _previous;
        public long _timeDiffetence;
        static void ActivateMouse(Frame frame)
        {
            var flag = frame.Fingers.Frontmost.IsValid;
           if (!flag) return;
            LeapAsMouse.EnableCursorLeap(frame);
          // LeapAsMouse.EnableClick(frame);
        }

        public override void OnConnect (Controller controller)
        {
            foreach (var gesture in (Gesture.GestureType[])Enum.GetValues(typeof(Gesture.GestureType))) 
            controller.EnableGesture(gesture);
        }
        public override void OnFrame(Controller controller)
        {
            using (var frame = controller.Frame())
            {

                _now = frame.Timestamp;
                _timeDiffetence = _now - _previous;

                if (frame.Hands.IsEmpty | _timeDiffetence < 5000) return;

                _previous = frame.Timestamp;


                if (frame.Gestures().Count > 0 & OnGestureMade != null)
                    OnGestureMade(frame.Gestures());
                if (frame.Fingers.Count > 0 & OnFingersRegistered != null)
                {
                  var vector = frame.InteractionBox.NormalizePoint(frame.Fingers.Frontmost.StabilizedTipPosition);
                    OnFingersRegistered(vector);
                }
                ActivateMouse(controller.Frame());
                onFrameDetected(controller.Frame());
            }
        }
    }
}

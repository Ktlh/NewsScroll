using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;


namespace MainProject
{
    public static class LeapGestures
    {
        public static readonly Dictionary<Gesture.GestureType, string> GestureTypesLookup
            = new Dictionary<Gesture.GestureType, string>()
            {
                { Gesture.GestureType.TYPEKEYTAP,"Tap gesture" },
                { Gesture.GestureType.TYPECIRCLE, "Circle gesture"},
            
                {Gesture.GestureType.TYPESWIPE,"Swipe gesture"},
                {Gesture.GestureType.TYPESCREENTAP,"Screen tap" }
               


            };


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;

namespace MainProject

{
    public class CustomHandler : IDisposable
    {
        public CustomListner Listener { get; set; }
        private readonly Controller _myController;
        private bool _disposed;

        public CustomHandler()
        {
            Listener = new CustomListner();
            _myController = new Controller();
            _myController.AddListener(Listener);

            CustomListner tempListener = null;
            Controller tempControler = null;

            try
            {
                tempListener = new CustomListner();
                tempControler = new Controller();
                tempControler.AddListener(tempListener);

                Listener = tempListener;
                _myController = tempControler;
                tempControler = null;
                tempListener = null;
            }
            finally
            {
                if (tempControler != null) tempControler.Dispose();
                if (tempListener != null) tempListener.Dispose();
            }
        }

        ~CustomHandler()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _myController.RemoveListener(Listener);
                _myController.Dispose();
                Listener.Dispose();
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
         public Controller getcontroler()
         {
             return _myController;
         
    }
    }
}

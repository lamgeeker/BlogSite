using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    public delegate void Notification();
    class UpdateNotification
    {
        public event Notification OnNotification;
       public  void Notify()
        {
            OnNotification?.Invoke();
            
        }
    }
}

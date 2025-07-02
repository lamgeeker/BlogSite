using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PostInfo;
namespace file
{
    public interface ISaveService<T> 
    {
        void Save(List<T> items);
        List<T> Load();
    }

    
}

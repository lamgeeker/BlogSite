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
        Task SaveToFileAsync(List<T> items);
        Task<List<T>> LoadFromFileAsync(bool urLock);
    }

    
}

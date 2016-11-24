using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repositories
{
    public interface IRepository<T>
    {
        T Create(T t); //Creates an instance of the object T
        T Read(T t); // Reads an instane of the object T 
        List<T> ReadAll(); // Reads a list of the instances
        bool Delete(T t); // Deletes an instance of the object T 
        bool Update(T t); // Updates an instance of the object T 
    }
}

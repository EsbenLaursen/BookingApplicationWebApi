using System.Collections.Generic;

namespace DLL.DAL.Repositories
{
    public interface IRepository<T>
    {
        T Create(T t); //Creates an instance of the object T
        T Read(int id); // Reads an instane of the object T 
        List<T> ReadAll(); // Reads a list of the instances
        bool Delete(T t); // Deletes an instance of the object T 
        bool Update(T t); // Updates an instance of the object T 
    }
}

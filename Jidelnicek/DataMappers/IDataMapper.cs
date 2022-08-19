using System.Collections.Generic;

namespace Jidelnicek.DataMappers;

public interface IDataMapper<T>
{
    IEnumerable<T> GetAll();
    bool Insert(T entity);
    bool Update(T entity);
    bool Delete(int id);
}
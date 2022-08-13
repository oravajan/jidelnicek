using System.Collections.Generic;

namespace Jidelnicek.DataMappers;

public interface IDataMapper<T>
{
    List<T> GetAll();
    bool Insert(T entity);
    bool Update(T entity);
    bool Delete(int id);
}
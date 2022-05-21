using System.Collections.Generic;

namespace Jidelnicek.DataMappers;

public interface IDataMapper<T>
{
    List<T> GetAll();
    T? Get(int id);
    bool Insert(T entity);
    bool Update(T entity);
    bool Delete(int id);
}
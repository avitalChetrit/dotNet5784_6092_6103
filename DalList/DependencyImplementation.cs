﻿namespace Dal;
using DalApi;
using DO;
//using System.Collections.Generic;


internal class DependencyImplementation : IDependency
{
    /// <summary>
    /// Create an object of type Dependency
    /// </summary>
    /// <param name="item"></param>
    /// <returns>int</returns>
    public int Create(Dependency item)
    {
        //for entities with auto id
        int id = DataSource.Config.NextDependencyId;
        Dependency copy = item with { Id = id };
        DataSource.Dependencys.Add(copy);
        return id;
    }

    /// <summary>
    /// Delete an object of type Dependency
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DalDeletionImpossible"></exception>
    public void Delete(int id)
    {
        bool isExist = DataSource.Dependencys.Exists(x => x.Id == id);//checks if there is an objects with the same id on list
        if (isExist)//such item is on list
        {
            Dependency d = DataSource.Dependencys.Find(x => x.Id == id)!;//finds the object with the same id
            DataSource.Dependencys.Remove(d);//removes the old objects
        }
        else//such item is not on list
        {
            throw new DalDoesNotExistException($"Dependency with ID={id} does Not exist");
        }
    }

    /// <summary>
    /// Read an object of type Dependency
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Dependency? Read(int id)
    {
        return DataSource.Dependencys.FirstOrDefault(item => item.Id == id);
    }

    /// <summary>
    /// ReadAll objects of type Dependency according to filter
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null) //stage 2
    {
        if (filter != null)
        {
            return from item in DataSource.Dependencys
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Dependencys
               select item;
    }

    /// <summary>
    /// Update an object of type Dependency
    /// </summary>
    /// <param name="item"></param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Update(Dependency item)
    {
        bool isExist = DataSource.Dependencys.Exists(x => x.Id == item.Id);//checks if there is an objects with the same id on list
        if (isExist)//such item is on list
        {
            Dependency dep = DataSource.Dependencys.Find(x => x.Id == item.Id)!;//finds the object with the same id
            DataSource.Dependencys.Remove(dep);//removes the old objects
            DataSource.Dependencys.Add(item);//add the new object
        }
        else//such item is not on list
        {
            throw new DalDoesNotExistException($"Dependency with ID={item.Id} does Not exist");
        }
    }
    /// <summary>
    /// Read an object of type Dependency according to filter
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public Dependency? Read(Func<Dependency, bool> filter) // stage 2
    {
        return DataSource.Dependencys.FirstOrDefault(item => filter(item));
    }
    /// <summary>
    /// Clear all objects of Dependencys
    /// </summary>
    public void Clear()
    {
        DataSource.Dependencys.Clear();
    }
}

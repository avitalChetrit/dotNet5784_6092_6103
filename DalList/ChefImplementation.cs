﻿namespace Dal;
using DalApi;
using DO;

internal class ChefImplementation : IChef
{
    /// <summary>
    /// Create an object of class chef 
    /// </summary>
    /// <param name="item"></param>
    /// <returns>int</returns>
    /// <exception cref="DalAlreadyExistsException"></exception>
    public int Create(Chef item)
    {
    
        if (Read(item.ChefId) is not null)
        {
            throw new DalAlreadyExistsException($"Chef with ID={item.ChefId} already exists");
        }
        DataSource.Chefs.Add(item);//add c to chefs list
        return item.ChefId;//return c chefId
    }

    /// <summary>
    /// Delete an object of class chef 
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DalDeletionImpossible"></exception>
    public void Delete(int id)  //Chef can't be deleted!!!
    {
        throw new DalDeletionImpossible("Can't delete the chef object!");
    }

    /// <summary>
    /// Read an object of class chef with the given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Chef?</returns>
    public Chef? Read(int id)
    {
        return DataSource.Chefs.FirstOrDefault(item => item.ChefId == id);
    }

    /// <summary>
    /// Read an object of class chef with the given filter
    /// </summary>
    /// <param name="filter"></param>
    /// <returns>Chef?</returns>
    public Chef? Read(Func<Chef, bool> filter) // stage 2
    {
        return DataSource.Chefs.FirstOrDefault(item => filter(item));
    }
    /// <summary>
    /// Read objects of class chef with the given filter
    /// </summary>
    /// <param name="filter"></param>
    /// <returns>IEnumerable<Chef></returns>
    public IEnumerable<Chef> ReadAll(Func<Chef, bool>? filter = null) //stage 2
    {
        if (filter != null)
        {
            return from item in DataSource.Chefs
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Chefs
               select item;
    }

    /// <summary>
    /// update an object of class chef 
    /// </summary>
    /// <param name="item"></param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Update(Chef item)
    {
        bool isExist = DataSource.Chefs.Exists(x => x.ChefId == item.ChefId);//checks if there is an objects with the same id on list
        if (isExist)//such item is on list
        {
            Chef c = DataSource.Chefs.Find(x => x.ChefId == item.ChefId)!;//finds the object with the same id
            DataSource.Chefs.Remove(c);//removes the old objects
            DataSource.Chefs.Add(item);//add the new object
        }
        else//such item is not on list
        {
            throw new DalDoesNotExistException($"Chef with ID={item.ChefId} does Not exist");
        }
    }

    public void Clear()
    {
        DataSource.Chefs.Clear(); 
    }
}
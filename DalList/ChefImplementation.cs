namespace Dal;
using DalApi;
using DO;

internal class ChefImplementation : IChef
{
    /// <summary>
    /// Create a new object of class chef
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
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
    /// Delete an object of type Chef
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DalDoesNotExistException"></exception>
    public void Delete(int id)  //Chef can't be deleted!!!
    {
        bool isExist = DataSource.Chefs.Exists(x => x.ChefId == id);//checks if there is an objects with the same id on list
        if (isExist)//such item is on list
        {
            Chef c = DataSource.Chefs.Find(x => x.ChefId == id)!;//finds the object with the same id
            DataSource.Chefs.Remove(c);//removes the old objects
        }
        else//such item is not on list
        {
            throw new DalDoesNotExistException($"Chef with ID={id} does Not exist");
        }
    }

    /// <summary>
    /// Read an object of type Chef
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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
    /// ReadAll objects of type chef according to filter
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
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
    /// Update an object of type chef according to ID
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

    /// <summary>
    /// Read an object of type chef according to filter
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public Chef? Read(Func<Chef, bool> filter) // stage 2
    {
        return DataSource.Chefs.FirstOrDefault(item => filter(item));
    }

    /// <summary>
    /// Clear the dataSource of Chefs
    /// </summary>
    public void Clear()
    {
        DataSource.Chefs.Clear(); 
    }
}
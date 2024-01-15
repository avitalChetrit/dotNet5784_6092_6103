namespace Dal;
using DalApi;
using DO;

internal class ChefImplementation : IChef
{
    public int Create(Chef item)
    {
        //bool isExist;//check if there's already a chef in list chefs with the same ID
        //isExist = DataSource.Chefs.Exists(x => x.ChefId == item.ChefId);
        if (Read(item.ChefId) is not null)
        {
            throw new DalAlreadyExistsException($"Chef with ID={item.ChefId} already exists");
        }
        DataSource.Chefs.Add(item);//add c to chefs list
        return item.ChefId;//return c chefId
    }

    public void Delete(int id)  //Chef can't be deleted!!!
    {
        throw new DalDeletionImpossible("Can't delete the chef object!");
    }

    public Chef? Read(int id)
    {
        return DataSource.Chefs.FirstOrDefault(item => item.ChefId == id);
        //return DataSource.Chefs.Select(item => item);
        //return DataSource.Chefs.Where(filter);

    }

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
    public Chef? Read(Func<Chef, bool> filter) // stage 2
    {
        return DataSource.Chefs.FirstOrDefault(item => filter(item));


        //return from item in DataSource.Chefs
        //       where filter(item)
        //       select item;

    }

}
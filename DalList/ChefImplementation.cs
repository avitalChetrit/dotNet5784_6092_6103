namespace Dal;
using DalApi;
using DO;

public class ChefImplementation : IChef
{
    public int Create(Chef item)
    {
        bool isExist;//check if there's already a chef in list chefs with the same ID
        isExist = DataSource.Chefs.Exists(x => x.ChefId == item.ChefId);
        if (isExist)
        {
            throw new Exception($"Chef with ID={item.ChefId} already exists");
        }
        DataSource.Chefs.Add(item);//add c to chefs list
        return item.ChefId;//return c chefId
    }

    public void Delete(int id)  //Chef can't be deleted!!!
    {
        throw new Exception("Can't delete the chef object!");
    }

    public Chef? Read(int id)
    {
        bool isExist;
        isExist = DataSource.Chefs.Exists(x => x.ChefId == id);//checks if there's an object with id in chefs list 
        if (isExist)//object id is found on list
        {
            Chef c= DataSource.Chefs.Find(x => x.ChefId == id)!;
            return c;
        }
        else//object id is not on list
        {
            return null;
        }
    }

    public List<Chef> ReadAll()
    {
        return new List<Chef>(DataSource.Chefs);//retuns copy of chefs list
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
            throw new Exception($"Chef with ID={item.ChefId} does Not exist");
        }
    }
}

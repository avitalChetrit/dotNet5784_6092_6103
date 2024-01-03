namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class ChefImplementation : IChef
{
    public int Create(Chef item)
    {
        Chef c = item;//create a copy of item called c
        int chefId = NextChefId();//create a chefId to variable c
        c.ChefId = chefId;
        Chefs.add(c);//add c to chefs list
        return item.ChefId;//return c chefId
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Chef? Read(int id)
    {
        bool isExist;
        isExist=chefs.Exists(x => x.ChefId == id);//checks if there's an object with id in chefs list 
        if (isExist)//object id is found on list
        {
            Chef c=chefs.Find(x => x.ChefId == id);
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
        bool isExist = chefs.Exists(x => x.ChefId == item.id);//checks if there is an objects with the same id on list
        if (isExist)//such item is on list
        {
            Chef c = chefs.Find(x => x.ChefId == item.id);//finds the object with the same id
            chefs.remove(c);//removes the old objects
            chefs.add(item);//add the new object
        }
        else//such item is not on list
        {
            throw new NotImplementedException("Object of type chef with such ID does NOT exist");
        }
    }
}

namespace Dal;
using DalApi;
using DO;
//using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        //for entities with auto id
        int id = DataSource.Config.NextDependencyId;
        Dependency copy = item with { Id = id };
        DataSource.Dependencys.Add(copy);
        return id;
    }

    public void Delete(int id)
    {
        throw new Exception("Can't delete the Dependency object!");
    }

    public Dependency? Read(int id)
    {
        bool isExist;
        isExist = DataSource.Dependencys.Exists(x => x.Id == id);//checks if there's an object with id in Dependency list 
        if (isExist)//object Id's was found on list
        {
            Dependency dep = DataSource.Dependencys.Find(x => x.Id == id)!;
            return dep;
        }
        else//object Id's is not on list
            return null;
        
    }

    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencys);//retuns copy of Dependencys list
    }

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
            throw new Exception($"Dependency with ID={item.Id} does Not exist");
        }
    }
}

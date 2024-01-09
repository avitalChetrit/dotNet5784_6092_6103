namespace Dal;
using DalApi;
using DO;
//using System.Collections.Generic;

internal class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        //for entities with auto id
        int id = DataSource.Config.NextTaskId;
        Task copy = item with { Id = id };
        DataSource.Tasks.Add(copy);
        return id;
    }

    public void Delete(int id)
    {
        throw new DalDeletionImpossible("Can't delete the Task object!");
    }

    public Task? Read(int id)
    {
        bool isExist;
        isExist = DataSource.Tasks.Exists(x => x.Id == id);//checks if there's an object with id in Task list 
        if (isExist)//object Id's was found on list
        {
            Task dep = DataSource.Tasks.Find(x => x.Id == id)!;
            return dep;
        }
        else//object Id's is not on list
            return null;
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks); //retuns copy of Tasks list();
    }

    public void Update(Task item)
    {
        bool isExist = DataSource.Tasks.Exists(x => x.Id == item.Id);//checks if there is an objects with the same id on list
        if (isExist)//such item is on list
        {
            Task dep = DataSource.Tasks.Find(x => x.Id == item.Id)!;//finds the object with the same id
            DataSource.Tasks.Remove(dep);//removes the old objects
            DataSource.Tasks.Add(item);//add the new object
        }
        else//such item is not on list
        {
            throw new DalDoesNotExistException($"Task with ID={item.Id} does Not exist");
        }
    }
}

namespace Dal;
using DalApi;
using DO;
using System.Threading.Tasks;
using Task = DO.Task;

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
        return DataSource.Tasks.FirstOrDefault(item => item.Id == id);
    }

    public IEnumerable<Task?> ReadAll(Func<Task?, bool>? filter = null) //stage 2
    {
        if (filter != null)
        {
            return from item in DataSource.Tasks
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Tasks
               select item;
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
    public Task? Read(Func<Task, bool> filter) // stage 2
    {
        return DataSource.Tasks.FirstOrDefault(item => filter(item));
    }

    public void Clear()
    {
        DataSource.Tasks.Clear();
    }
}

namespace Dal;
using DalApi;
using DO;
using System.Threading.Tasks;
using Task = DO.Task;

//using System.Collections.Generic;

internal class TaskImplementation : ITask
{
    /// <summary>
    /// Create an object of type Task
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int Create(Task item)
    {
        //for entities with auto id
        int id = DataSource.Config.NextTaskId;
        Task copy = item with { Id = id };
        DataSource.Tasks.Add(copy);
        return id;
    }

    /// <summary>
    /// Delete an object of type Task
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DalDeletionImpossible"></exception>
    public void Delete(int id)
    {
        bool isExist = DataSource.Tasks.Exists(x => x.Id == id);//checks if there is an objects with the same id on list
        if (isExist)//such item is on list
        {
            Task t = DataSource.Tasks.Find(x => x.Id == id)!;//finds the object with the same id
            DataSource.Tasks.Remove(t);//removes the old objects
        }
        else//such item is not on list
        {
            throw new DalDoesNotExistException($"Task with ID={id} does Not exist");
        }
    }

    /// <summary>
    /// Read an object of type Task
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task? Read(int id)
    {
        return DataSource.Tasks.FirstOrDefault(item => item.Id == id);
    }

    /// <summary>
    /// ReadAll objects of type Task according to filter
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Update an object of type Task
    /// </summary>
    /// <param name="item"></param>
    /// <exception cref="DalDoesNotExistException"></exception>
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
    /// <summary>
    /// Read an object of type Task according to filter
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public Task? Read(Func<Task, bool> filter) // stage 2
    {
        return DataSource.Tasks.FirstOrDefault(item => filter(item));
    }

    /// <summary>
    /// Clear Tasks from all objects
    /// </summary>
    public void Clear()
    {
        DataSource.Tasks.Clear();
    }
}

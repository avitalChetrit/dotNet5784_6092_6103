namespace BlImplementation;
using BlApi;
using BO;
using System.Runtime.InteropServices;

internal class ChefImplementation : IChef
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// create a new chef
    /// </summary>
    /// <param name="item"></param>
    /// <returns>int</returns>
    /// <exception cref="BO.BlWrongInputException"></exception>
    /// <exception cref="BO.BlAlreadyExistsException"></exception>
    public int Create(BO.Chef item)
    {
        //check if input is valid, if not throw exce
        if (item.Id <= 0 || item.Name == "" || item.Cost <= 0 || !item.Email.Contains("@gmail.com"))
        {
            throw new BO.BlWrongInputException("Invalid Input");
        }
        //create a new chef of Data type 
        DO.Chef doChef = new DO.Chef(item.Id, (DO.ChefExperience?)item.Level, 
                                        item.Name, item.Email, item.Cost);
       
        //Create might throw an execption
        try
        {
            int idChef = _dal.Chef.Create(doChef);
            return idChef;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Chef with ID={item.Id} already exists", ex);
        }
    }
    /// <summary>
    /// Delete an object
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="BO.BlDeletionImpossible"></exception>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public void Delete(int id)
    {
        //recieve the chef data type w this id
        DO.Chef item = _dal.Chef.Read(id)!;
        if (item == null)
            throw new BO.BlDoesNotExistException($"Chef with ID={id} does Not exist");

        //check if this chef has a task, if so don't delete, if not delete
        var tasks = _dal.Task.ReadAll();
        bool taskExists = tasks.Any(task => task.Id == id);
        if (taskExists)
            throw new BO.BlDeletionImpossible($"Chef with ID={id} cannot be deleted!");
        else
            _dal.Chef.Delete(id);
    }

    /// <summary>
    /// Read an object of type chef
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public BO.Chef? Read(int id)
    {
        //recieve the chef data type w this id
        DO.Chef? item = _dal.Chef.Read(id);
        if (item == null)
            throw new BO.BlDoesNotExistException($"Chef with ID={id} does Not exist");

        //find if this chef has a current task
        DO.Task? dotask = _dal.Task.ReadAll().FirstOrDefault(task => task.ChefId == id && task.CompleteDate == null);
        BO.TaskInChef? taskInChef = (dotask!=null)? new BO.TaskInChef() { Id = dotask.Id, Alias = dotask.Alias,}: null; 
        
        //create new logic chef
        return new BO.Chef()
        {
            Id = id,
            Name = item.Name,
            Email = item.Email,
            Level = (BO.ChefExperience?)item.Level,
            Cost = item.Cost,
            Task = taskInChef,
        };
    }

    /// <summary>
    /// ReadAll logical chefs (according to filter)
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<BO.Chef> ReadAll(Func<BO.Chef, bool>? filter = null)
    {
                 
        return (from DO.Chef doChef in _dal.Chef.ReadAll() //for each chef from the data list of chefs
                let boChef= Read(doChef.ChefId)            //create a new logic chef
                where (filter==null)? true: filter(boChef) //if it answer the filter
                select boChef);                            //choose it
    }

    /// <summary>
    /// recieve a logical chef (BO) and update it on data layer (DO)
    /// </summary>
    /// <param name="item"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Update(BO.Chef boChef)
    {
        DO.Chef? doChef = _dal.Chef.Read(boChef.Id);
        if (doChef == null)
            throw new BO.BlDoesNotExistException($"Chef with ID={boChef.Id} does Not exist");
        if(boChef.Level< (BO.ChefExperience)doChef.Level!)
            throw new BO.BlWrongInputException("Invalid Input");

        DO.Task? doTask = _dal.Task.Read(boChef.Task.Id);
        if (doTask == null)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={boChef.Task.Id} does Not exist");
        }
        doTask = doTask with { ChefId = boChef.Id };
        _dal.Task.Update(doTask);

        _dal.Chef.Update(doChef);
    }

}

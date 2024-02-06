namespace BlImplementation;
using BlApi;


internal class ChefImplementation : IChef
{
    private DalApi.IDal _dal; /*= /*DalApi.Factory.Get*/

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

        //TODO: FIX THE UPDATE TASK
        _dal.Chef.Update(doChef);


    }
    //public int Create(BO.Chef item)
    //{
    //    if(item.Id<=0 || item.Name=="" || item.Cost<=0 || !item.Email.Contains("@gmail.com"))
    //    {
    //        throw new BO.BlWorngInputException("Worng Input");
    //    }
    //    DO.Chef doChef = new DO.Chef
    //    (item.Id, (DO.ChefExperience?)item.Level, item.Name, item.Email, item.Cost);
    //    try
    //    {
    //        int idChef = _dal.Chef.Create(doChef);
    //        return idChef;
    //    }
    //    catch (DO.DalAlreadyExistsException ex)
    //    {
    //        //throw new BO.BlAlreadyExistsException($"Student with ID={boChef.Id} already exists", ex);
    //        throw new Exception(); //to לתקן
    //    }

    //}

    //public void Delete(int id)
    //{
    //    throw new NotImplementedException();
    //}

    //public BO.Chef? Read(int id)
    //{
    //    DO.Chef? doChef = _dal.Chef.Read(id);
    //    if (doChef == null)
    //        throw new Exception(); //to לתקן     
    //                               //throw new BO.BlDoesNotExistException($"Student with ID={id} does Not exist");

    //    //ChefSpeciality speciality1;
    //    //if (doChef.Cost < 100)
    //    //    speciality1= ChefSpeciality.Baker;
    //    //else if(doChef.Cost > 100&& doChef.Cost<200)
    //    //    speciality1= ChefSpeciality.Dairy;
    //    //else if (doChef.Cost > 200 && doChef.Cost < 300)
    //    //    speciality1 = ChefSpeciality.SeaFood;
    //    //else 
    //    //    speciality1 = ChefSpeciality.Meat;

    //    IEnumerable<DO.Task?> TaskList= _dal.Task.ReadAll();
    //    DO.Task? t= TaskList.FirstOrDefault(item => item.ChefId==id);

    //    return new BO.Chef()
    //    {
    //        Id = id,
    //        Name = doChef.Name,
    //        Level = (BO.ChefExperience?)doChef.Level,
    //        Email = doChef.Email,
    //        Cost = doChef.Cost,
    //        // famous means that the chef is charging more than 1k and he's an expert.
    //        Famous= (doChef.Cost>1000 && doChef.Level== DO.ChefExperience.Expert)? true : false,
    //        Task= t.Id
    //        //Speciality = speciality1
    //    };
    //}
    //public IEnumerable<BO.Chef> ReadAll()
    //{
    //    return (from DO.Chef doChef in _dal.Chef.ReadAll()
    //            select new BO.Chef
    //            {
    //                Id = doChef.ChefId,
    //                Name = doChef.Name,
    //                Level = (BO.ChefExperience?)doChef.Level,
    //                Email = doChef.Email,
    //                Cost = doChef.Cost,
    //                Famous = (doChef.Cost > 1000 && doChef.Level == DO.ChefExperience.Expert) ? true : false,
    //                //CurrentYear = (BO.Year)(DateTime.Now.Year - doStudent.RegistrationDate.Year)
    //            });

    //}

    //public void Update(BO.Chef boChef)
    //{
    //    if (boChef.Id <= 0 || boChef.Name == "" || boChef.Cost <= 0 || !boChef.Email.Contains("@gmail.com"))
    //    {
    //        throw new Exception(); //to לתקן
    //    }
    //    //DO.Chef doChef = new DO.Chef
    //    //(boChef.Id, (DO.ChefExperience?)boChef.Level, boChef.Name, boChef.Email, boChef.Cost);
    //    //try
    //    //{

    //    //    int idChef = _dal.Chef.Create(doChef);
    //    //    return idChef;
    //    //}
    //    //catch (DO.DalAlreadyExistsException ex)
    //    //{
    //    //    //throw new BO.BlAlreadyExistsException($"Student with ID={boChef.Id} already exists", ex);
    //    //    throw new Exception(); //to לתקן
    //    //}
    //}
}

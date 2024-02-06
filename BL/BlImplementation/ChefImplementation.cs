namespace BlImplementation;
using BlApi;


internal class ChefImplementation : IChef
{
    private DalApi.IDal _dal; /*= /*DalApi.Factory.Get*/

    public int Create(BO.Chef item)
    {
        if (item.Id <= 0 || item.Name == "" || item.Cost <= 0 || !item.Email.Contains("@gmail.com"))
        {
            throw new BO.BlWrongInputException("Invalid Input");
        }
        DO.Chef doChef = new DO.Chef
        (item.Id, (DO.ChefExperience?)item.Level, item.Name, item.Email, item.Cost);
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
    public void Delete(int id)
    {
        try
        {
            DO.Chef item = _dal.Chef.Read(id)!;
            var tasks = _dal.Task.ReadAll();
            bool taskExists = tasks.Any(task => task.Id == id);
            if (taskExists)
            {
                throw new BO.BlDeletionImpossible($"Chef with ID={id} cannot be deleted!");
            }
            else
            {
                _dal.Chef.Delete(id);
            }
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Chef with ID={id} does Not exist", ex);
        }
    }

    public BO.Chef? Read(int id)
    {
        DO.Chef? item = _dal.Chef.Read(id);
        if (item == null)
            throw new BO.BlDoesNotExistException($"Chef with ID={id} does Not exist");

        DO.Task? dotask = _dal.Task.ReadAll().FirstOrDefault(task => task.ChefId == id && task.CompleteDate == null);
        BO.TaskInChef? taskInChef = (dotask!=null)? new BO.TaskInChef() { Id = dotask.Id, Alias = dotask.Alias, }: null; 
        
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

    public IEnumerable<BO.Chef> ReadAll(Func<BO.Chef, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Chef item)
    {
        throw new NotImplementedException();
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

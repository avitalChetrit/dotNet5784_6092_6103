namespace BlImplementation;
using BlApi;
using BO;
//using DalApi;

//using BO;
//using DalApi;
//using DO;
//using System.Collections.Generic;

internal class ChefImplementation : IChef
{
    private static DalApi.IDal? _dal; //stage 4
    public int Create(BO.Chef boChef)
    {
        if(boChef.Id<=0 || boChef.Name=="" || boChef.Cost<=0 || !boChef.Email.Contains("@gmail.com"))
        {
            throw new Exception(); //to לתקן
        }
        DO.Chef doChef = new DO.Chef
        (boChef.Id, (DO.ChefExperience?)boChef.Level, boChef.Name, boChef.Email, boChef.Cost);
        try
        {
            int idChef = _dal.Chef.Create(doChef);
            return idChef;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            //throw new BO.BlAlreadyExistsException($"Student with ID={boChef.Id} already exists", ex);
            throw new Exception(); //to לתקן
        }

    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Chef? Read(int id)
    {
        DO.Chef? doChef = _dal.Chef.Read(id);
        if (doChef == null)
            throw new Exception(); //to לתקן     
                                   //throw new BO.BlDoesNotExistException($"Student with ID={id} does Not exist");

        //ChefSpeciality speciality1;
        //if (doChef.Cost < 100)
        //    speciality1= ChefSpeciality.Baker;
        //else if(doChef.Cost > 100&& doChef.Cost<200)
        //    speciality1= ChefSpeciality.Dairy;
        //else if (doChef.Cost > 200 && doChef.Cost < 300)
        //    speciality1 = ChefSpeciality.SeaFood;
        //else 
        //    speciality1 = ChefSpeciality.Meat;

        IEnumerable<DO.Task?> TaskList= _dal.Task.ReadAll();
        DO.Task? t= TaskList.FirstOrDefault(item => item.ChefId==id);

        return new BO.Chef()
        {
            Id = id,
            Name = doChef.Name,
            Level = (BO.ChefExperience?)doChef.Level,
            Email = doChef.Email,
            Cost = doChef.Cost,
            // famous means that the chef is charging more than 1k and he's an expert.
            Famous= (doChef.Cost>1000 && doChef.Level== DO.ChefExperience.Expert)? true : false,
            Task= t.Id
            //Speciality = speciality1
        };
    }
    public IEnumerable<BO.Chef> ReadAll()
    {
        return (from DO.Chef doChef in _dal.Chef.ReadAll()
                select new BO.Chef
                {
                    Id = doChef.ChefId,
                    Name = doChef.Name,
                    Level = (BO.ChefExperience?)doChef.Level,
                    Email = doChef.Email,
                    Cost = doChef.Cost,
                    Famous = (doChef.Cost > 1000 && doChef.Level == DO.ChefExperience.Expert) ? true : false,
                    //CurrentYear = (BO.Year)(DateTime.Now.Year - doStudent.RegistrationDate.Year)
                });

    }

    public void Update(BO.Chef boChef)
    {
        if (boChef.Id <= 0 || boChef.Name == "" || boChef.Cost <= 0 || !boChef.Email.Contains("@gmail.com"))
        {
            throw new Exception(); //to לתקן
        }
        //DO.Chef doChef = new DO.Chef
        //(boChef.Id, (DO.ChefExperience?)boChef.Level, boChef.Name, boChef.Email, boChef.Cost);
        //try
        //{
     
        //    int idChef = _dal.Chef.Create(doChef);
        //    return idChef;
        //}
        //catch (DO.DalAlreadyExistsException ex)
        //{
        //    //throw new BO.BlAlreadyExistsException($"Student with ID={boChef.Id} already exists", ex);
        //    throw new Exception(); //to לתקן
        //}
    }
}

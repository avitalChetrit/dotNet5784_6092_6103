namespace BlImplementation;
using BlApi;
using BO;
using DO;
//using BO;
//using DalApi;
using System.Data;

//using BO;
//using System;
//using System.Collections.Generic;


internal class TaskImplementation : ITask
{
    private static DalApi.IDal? _dal; //stage 4
    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public int Create(BO.Task item)
    {
        DO.Task doTask = new DO.Task(item.Id, item.Description, item.Alias, false,
            (DO.ChefExperience)item.Complexity, item.CreatedAtDate, item.RequiredTime, 
            item.StartDate, item.ScheduledDate, null, item.CompleteDate, item.Deliveables, item.Remarks, item.Chef.Id);
        if (item.Id <= 0 && item.Alias == "")
            throw new BO.BlWrongInputException("Wrong Input");

        try
        {
            int idTask = _dal.Task.Create(doTask);
            return idTask;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Student with ID={item.Id} already exists", ex);
        }

    }

    public void Delete(int id)
    {
        DO.Task? item = _dal.Task.Read(id);
        if (item == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        _dal.Task.Delete(id);
    }

    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        return (from DO.Task doTask in _dal.Chef.ReadAll() //for each chef from the data list of chefs
                let item = Read(doTask.ChefId)            //create a new logic chef
                where (filter == null) ? true : filter(item) //if it answer the filter
                select item);
    }

    public void Update(BO.Task item)
    {
        //DO.Chef? doChef = _dal.Chef.Read(item.Id);
        //if (doChef == null)
        //    throw new BO.BlDoesNotExistException($"Chef with ID={boChef.Id} does Not exist");
        //if (item.Level < (BO.ChefExperience)doChef.Level!)
        //    throw new BO.BlWrongInputException("Invalid Input");

        //DO.Task? doTask = _dal.Task.Read(boChef.Task.Id);
        //if (doTask == null)
        //{
        //    throw new BO.BlDoesNotExistException($"Task with ID={boChef.Task.Id} does Not exist");
        //}
        //doTask = doTask with { ChefId = boChef.Id };
        //_dal.Task.Update(doTask);


        //_dal.Chef.Update(doChef);
    }

    public void UpdateDate(int id, DateTime d)
    {
        BO.Task? item = Read(id);
        item.Dependecies.All( task=> DO.Task t=_dal.Task.Read(task.Id))

    }

    public BO.Task? Read(int id)
    {
        DO.Task? item = _dal.Task.Read(id);
        if (item == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");

        //function to find DeadLineDate, and isMileStons
        BO.ChefInTask c;
        if (item.ChefId == null)
        {
            c = null;
        }
        else
        {
            DO.Chef? chefi = _dal.Chef.Read(item.ChefId);
            c = new BO.ChefInTask() { Id = chefi.ChefId, Name = chefi.Name };
        }

        IEnumerable<Dependency>? ls = _dal.Dependency.ReadAll();
        List<int> lint = ((List<int>)(from Dependency dep in ls //for each chef from the data list of chefs
                                      where (dep.PreTask == id)
                                      select dep.CurrTask));                            //choose it


        TaskInList taskInList = new TaskInList()
        {
             Id=item.Id,
             Description= item.Description,
             Alias= item.Alias,
             Status=
        }


        return new BO.Task()
        {
            Id = id,
            Description = item.Description,
            Alias = item.Alias,
            Complexity = (BO.ChefExperience)item.Complexity,
            CreatedAtDate = item.CreatedAtDate,
            Status = findStat(),
            Dependecies =
            RequiredTime = item.RequiredTime,
            StartDate = item.StartDate,
            ScheduledDate = item.ScheduledDate,
            ForecastDate = item.StartDate + item.RequiredTime,
            CompleteDate = item.CompleteDate,
            Deliveables = item.Deliveables,
            Remarks = item.Remarks,
            Chef = c
        };
    }

    public BO.Status findStat(BO.Task item)
    {
        if(item.Status== Unscheduled)
            return BO.Status.Unscheduled;
        else
    }
}

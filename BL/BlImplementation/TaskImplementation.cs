namespace BlImplementation;
using BlApi;
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
        DO.Task doTask = new DO.Task (item.Id, item.Description, item.Alias, false,
            (DO.ChefExperience)item.Complexity, item.CreatedAtDate, item.RequiredTime, item.StartDate, item.ScheduledDate, null, item.CompleteDate, item.Deliveables, item.Remarks, item.Chef.Id);
        if(item.Id<=0 && item.Alias=="" )
            throw new BO.BlWrongInputException("Wrong Input");
        try
        {
            int idStud = _dal.Task.Create(doTask);
            return idStud;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Student with ID={item.Id} already exists", ex);
        }

    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Task item)
    {
        throw new NotImplementedException();
    }

    public void UpdateDate(int id, DateTime d)
    {
        throw new NotImplementedException();
    }

    BO.Task? ITask.Read(int id)
    {
        DO.Task? item = _dal.Task.Read(id);
        if (item == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");

        //function to find DeadLineDate, and isMileStons


        return new BO.Task()
        {
            Id = id,
            Description = item.Description,
            Alias = item.Alias,
            Complexity = (BO.ChefExperience)item.Complexity,
            CreatedAtDate = item.CreatedAtDate,
            RequiredTime = item.RequiredTime,
            StartDate = item.StartDate,
            ScheduledDate = item.ScheduledDate,
            DeadLineDate = item.StartDate + item.RequiredTime,
            CompleteDate = item.CompleteDate,
            Deliveables = item.Deliveables,
            Remarks = item.Remarks,
            Chef = item.ChefId
        };
    }
}

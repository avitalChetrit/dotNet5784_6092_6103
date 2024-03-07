using BO;
using DalApi;
using DO;

namespace BlImplementation;
using BlApi;
using BO;
using DO;
//using DalApi;
using System.Data;
using System.Runtime.InteropServices;

//using BO;
//using System;
//using System.Collections.Generic;


internal class TaskImplementation : ITask
{
    private static DalApi.IDal? _dal = DalApi.Factory.Get; //stage 4

    private readonly IBl _bl;
    internal TaskImplementation(IBl bl) => _bl = bl;

    /// <summary>
    /// Create a logical Task
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public int Create(BO.Task item)
    {
        //Can only create object on first stage
        if (Schedule.level != ScheduleLevel.Planning)
            throw new BO.BlUnableToPreformActionInThisProjectStageException("Can't Create task In This Project Stage");

        item.CreatedAtDate = _bl.Clock;

        DO.Task doTask = new DO.Task(item.Id, item.Description, item.Alias, false,
            (DO.ChefExperience)item.Complexity, item.CreatedAtDate, item.RequiredTime,
            item.StartDate, item.ScheduledDate, null, item.CompleteDate, item.Deliveables,
            item.Remarks, item.Chef?.Id);
        
        //Check Input
        if (item.Id <= 0 && item.Alias == "")
            throw new BO.BlWrongInputException("Wrong Input");
        //Create Task
        try
        {
            int idTask = _dal.Task.Create(doTask);
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={item.Id} already exists", ex);
        }

        //create dependecies
        item.Dependecies?.ForEach(Dep =>
        { _dal.Dependency.Create(new Dependency(0, Dep.Id, item.Id)); });

        return item.Id;
    }

    public void Delete(int id)
    {
        //Can only del object on first stage
        if (Schedule.level != ScheduleLevel.Planning)
            throw new BO.BlUnableToPreformActionInThisProjectStageException("Can't Delete chef In This Project Stage");
        BO.Task? item = Read(id);
        //Check if task exists
        if (item == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        //check if this task has no dependecies
        if (item.Dependecies != null)
            throw new BO.BlDeletionImpossible($"Task with ID={id} can not be deleted");
        _dal.Task.Delete(id);
    }

    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        return (from DO.Task doTask in _dal.Task.ReadAll()
                let item = doToBoTask(doTask.Id, doTask)
                where (filter == null) ? true : filter(item)
                select item);
    }

    public void Update(BO.Task boTask)
    {
        DO.Task? doTask = _dal.Task.Read(boTask.Id);
        if (doTask == null)
            throw new BO.BlDoesNotExistException($"Chef with ID={doTask.Id} does Not exist");
        DO.Task updateTask = new DO.Task
        {
            Id = boTask.Id,
            Description = boTask.Description ?? doTask.Description,
            Alias = boTask.Alias ?? doTask.Alias,
            CreatedAtDate = boTask.CreatedAtDate ?? doTask.CreatedAtDate,
            RequiredTime = boTask.RequiredTime ?? doTask.RequiredTime,
            StartDate = boTask.StartDate ?? doTask.StartDate,
            ScheduledDate = boTask.ScheduledDate ?? doTask.ScheduledDate,
            CompleteDate = boTask.CompleteDate ?? doTask.CompleteDate,
            Deliveables = boTask.Deliveables ?? doTask.Deliveables,
            Remarks = boTask.Remarks ?? doTask.Remarks,
            ChefId = (boTask.Chef!=null) ? boTask.Chef.Id : doTask.ChefId,
            Complexity = (DO.ChefExperience)boTask.Complexity,
        };


        _dal.Task.Update(updateTask);

        if(boTask.Dependecies!=null)
        {
            IEnumerable<Dependency> dependencies = _dal.Dependency.ReadAll();
            foreach (var dep in dependencies)
            {
                if (dep.CurrTask == boTask.Id)
                    _dal.Dependency.Delete(dep.Id);
            }
            foreach (var dep in boTask.Dependecies)
            {
                DO.Dependency dep1 = new Dependency { CurrTask = boTask.Id, PreTask = dep.Id };
                _dal.Dependency.Create(dep1);
            }
        }

    }
    public void UpdateDate(int id, DateTime d)
    {
        BO.Task? item = Read(id);
        if (item == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");

        foreach(var dep in item.Dependecies)
        {
            BO.Task? task = Read(dep.Id);
            if (task == null)
                throw new BlDoesNotExistException($"Task with ID={id} does Not exist");
            if (task.ScheduledDate == null)
                throw new BlWrongInputException("Previous task has no Scheduled Date");
            if (task.ForecastDate > d)
                throw new BlWrongInputException("Date is too early");
        }
        if (item != null)
            item.ScheduledDate = d;

        Update(item);
    }

    public BO.Task? Read(int id)
    {
        DO.Task? item = _dal.Task.Read(id);
        return doToBoTask(id, item);
    }

    public bool isThereCycle(int currTask , int preTask)
    {
        if (currTask == preTask) return true;

        IEnumerable<Dependency>? dependencies = _dal?.Dependency.ReadAll(dep => dep.CurrTask == currTask)!;
        foreach(var dep in dependencies)
        {
           return isThereCycle(dep.CurrTask, preTask);
        }
        return false;
    }
    private BO.Task? doToBoTask(int id, DO.Task? item)
    {
        if (item == null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");

        //function to find DeadLineDate, and isMileStons=false

        BO.ChefInTask c;
        if (item.ChefId == null)
        {
            c = null;
        }
        else
        {
            DO.Chef? chefi = _dal.Chef.Read((int)item.ChefId);
            c = new BO.ChefInTask() { Id = chefi.ChefId, Name = chefi.Name };
        }

        IEnumerable<int>? ls = _dal.Dependency.ReadAll(dep => dep.CurrTask == id)
            .Select(dep => dep.PreTask);
                                //choose it

        List<BO.TaskInList> Dependecies = new List<BO.TaskInList>();
        foreach (var dep in ls)
        {
            DO.Task pre = _dal.Task.Read(dep)!;
            TaskInList taskInList = new TaskInList()
            {
                Id = pre.Id,
                Description = pre.Description,
                Alias = pre.Alias,
                Status = findStat(pre.Id)
            };

            Dependecies.Add(taskInList);
        }


        return new BO.Task()
        {
            Id = id,
            Description = item.Description,
            Alias = item.Alias,
            Complexity = (BO.ChefExperience)item.Complexity,
            CreatedAtDate = item.CreatedAtDate,
            Status = findStat(item.Id),
            Dependecies = Dependecies,
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

    public BO.Status findStat(int id)
    {
        DO.Task item = _dal.Task.Read(id);

        if (item.ScheduledDate == null)
        {
            return BO.Status.UnScheduled;
        }

        if (item.CompleteDate != null)
        {
            return BO.Status.Done;
        }

        if (item.StartDate == null)
        {
            return BO.Status.Scheduled;
        }

        return BO.Status.OnTrack;

    }

}

//if (item == null)
//            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");

//        //function to find DeadLineDate, and isMileStons=false

//        BO.ChefInTask c;
//        if (item.ChefId == null)
//        {
//            c = null;
//        }
//        else
//{
//    DO.Chef? chefi = _dal.Chef.Read((int)item.ChefId);
//    c = new BO.ChefInTask() { Id = chefi.ChefId, Name = chefi.Name };
//}

//IEnumerable<Dependency>? ls = _dal.Dependency.ReadAll();
//List<int> lint = ((List<int>)(from Dependency dep in ls //for each chef from the data list of chefs
//                              where (dep.CurrTask == id)
//                              select dep.PreTask));                            //choose it

//List<BO.TaskInList> Dependecies = new List<BO.TaskInList>();
//foreach (var dep in lint)
//{
//    DO.Task pre = _dal.Task.Read(dep);
//    TaskInList taskInList = new TaskInList()
//    {
//        Id = pre.Id,
//        Description = pre.Description,
//        Alias = pre.Alias,
//        Status = findStat(pre.Id)
//    };

//    Dependecies.Add(taskInList);
//}


//return new BO.Task()
//{
//    Id = id,
//    Description = item.Description,
//    Alias = item.Alias,
//    Complexity = (BO.ChefExperience)item.Complexity,
//    CreatedAtDate = item.CreatedAtDate,
//    Status = findStat(item.Id),
//    Dependecies = Dependecies,
//    RequiredTime = item.RequiredTime,
//    StartDate = item.StartDate,
//    ScheduledDate = item.ScheduledDate,
//    ForecastDate = item.StartDate + item.RequiredTime,
//    CompleteDate = item.CompleteDate,
//    Deliveables = item.Deliveables,
//    Remarks = item.Remarks,
//    Chef = c
//};
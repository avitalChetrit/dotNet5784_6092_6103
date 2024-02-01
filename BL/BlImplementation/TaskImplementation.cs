namespace BlImplementation;
using BlApi;
using DalApi;
using System.Data;

//using BO;
//using System;
//using System.Collections.Generic;


internal class TaskImplementation : ITask
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public int Create(BO.Task item)
    {
        DO.Task doTask = new DO.Task (item.Id, item.Alias, item.IsActive, item.BirthDate);
        if(item.Id<=0 && item.Alias=="" )
        //    throw BO.BlWorngInputExeption($"Student with ID={boStudent.Id} already exists", ex);
        //try
        //{
        //    int idStud = _dal.Student.Create(doStudent);
        //    return idStud;
        //}
        //catch (DO.DalAlreadyExistsException ex)
        //{
        //    throw new BO.BlAlreadyExistsException($"Student with ID={boStudent.Id} already exists", ex);
        //}

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
        throw new NotImplementedException();
    }
}

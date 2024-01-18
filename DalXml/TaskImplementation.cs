﻿using Task = DO.Task;
using DalApi;
using DO;
using System.Data.Common;

namespace Dal;

internal class TaskImplementation : ITask
{
    readonly string s_tasks_xml = "tasks";

    public void Clear()
    {
        List<Task> Tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);  //Load
        Tasks.Clear();
        XMLTools.SaveListToXMLSerializer(Tasks, s_tasks_xml);  //save
    }

    public int Create(Task item)
    {
        List<Task> Tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);  //Load

        //for entities with auto id
        int id = Config.NextTaskId;
        Task copy = item with { Id = id };
        Tasks.Add(copy);

        XMLTools.SaveListToXMLSerializer(Tasks, s_tasks_xml);  //save

        return id;//return id
    }

    public void Delete(int id)
    {
        throw new DalDeletionImpossible("Can't delete the Task object!");
    }

    public Task? Read(int id)
    {
        List<Task> Tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);  //Load
        return Tasks.FirstOrDefault(item => item.Id == id);
    }

    public Task? Read(Func<Task, bool> filter)
    {
        List<Task> Tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);  //Load
        return Tasks.FirstOrDefault(item => filter(item));
    }

    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        List<Task> Tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);  //Load
        if (filter != null)
        {
            return from item in Tasks
                   where filter(item)
                   select item;
        }
        return from item in Tasks
               select item;
    }

    public void Update(Task item)
    {
        List<Task> Tasks = XMLTools.LoadListFromXMLSerializer<Task>(s_tasks_xml);  //Load

        bool isExist = Tasks.Exists(x => x.Id == item.Id);//checks if there is an objects with the same id on list
        if (isExist)//such item is on list
        {
            Task dep = Tasks.Find(x => x.Id == item.Id)!;//finds the object with the same id
            Tasks.Remove(dep);//removes the old objects
            Tasks.Add(item);//add the new object
            XMLTools.SaveListToXMLSerializer(Tasks, s_tasks_xml);  //save
        }
        else//such item is not on list
        {
            throw new DalDoesNotExistException($"Task with ID={item.Id} does Not exist");
        }

    }
}
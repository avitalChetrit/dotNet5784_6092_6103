using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class ScheduleImplementation : ISchedule
{
    readonly string s_startOfProject_xml = "startOfProject";

    public void Delete()
    {
        List<String> StartDate = new List<String>();
        StartDate.Clear();
        XMLTools.SaveListToXMLSerializer(StartDate, s_startOfProject_xml);  //save
    }

    public DateTime? Read()
    {
        List<String> StartDate = XMLTools.LoadListFromXMLSerializer<String>(s_startOfProject_xml);  //Load
       if(StartDate.Any())
            return DateTime.Parse(StartDate[0]);
       else
            return null;
    }

    public void Update(DateTime? dateTime)
    {
        List<String> StartDate = new List<String>();
        if (dateTime.HasValue) 
            StartDate.Add(dateTime.ToString()!);
        else StartDate.Clear();
        XMLTools.SaveListToXMLSerializer(StartDate, s_startOfProject_xml);  //save
    }
}

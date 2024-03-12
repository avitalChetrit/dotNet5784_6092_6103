using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class ScheduleImplementation
{
    readonly string s_schedule_xml = "schedule";

    public void Update()
    {
        List<string> dateList = new List<string>();
        dateList.Add(Schedule.StartDate.ToString());
        XMLTools.SaveListToXMLSerializer(dateList, s_schedule_xml);  //save
    }
}

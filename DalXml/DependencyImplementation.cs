﻿using DalApi;
using DO;
using System.Xml.Linq;
namespace Dal;

internal class DependencyImplementation : IDependency
{
    readonly string s_dependencys_xml = "dependencys";


    static Dependency getDependency(XElement d)
    {
        return new Dependency()
        {
            Id = d.ToIntNullable("Id") ?? throw new FormatException("can't convert id"),

            PreTask = d.ToIntNullable("PreTask") ?? null,

            CurrTask = d.ToIntNullable("CurrTask") ?? null
        };
    }

    static XElement toXElement(Dependency item)
    {
        XElement id = new XElement("Id", item.Id);
        XElement preTask = new XElement("PreTask", item.PreTask);
        XElement currTask = new XElement("CurrTask", item.CurrTask);

        XElement e=new XElement("Dependency", id, preTask, currTask);

        return e;
    }

    public int Create(Dependency item)
    {
        XElement rootDep = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        int id = Config.NextDependencyId;
        Dependency copy = item with { Id = id };
        rootDep.Add(toXElement(copy));
        XMLTools.SaveListToXMLElement(rootDep, s_dependencys_xml);
        return copy.Id;
    }

    public void Delete(int id) //שאלנו את המרצה מה לעשות בקשר למחיקות והיא אמרה שהיא תבדוק ותעדכן אותנו
    {
        throw new DalDeletionImpossible("Can't delete the Dependenc object!");
    }

    public Dependency? Read(int id)
    {
        XElement? dependencyElem = XMLTools.LoadListFromXMLElement(s_dependencys_xml).Elements().FirstOrDefault(d=>(int ?)d.Element("Id") ==id); 
        return dependencyElem is null? null: getDependency(dependencyElem);
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        return XMLTools.LoadListFromXMLElement(s_dependencys_xml).Elements().Select(d=> getDependency(d)).FirstOrDefault(filter);
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        if(filter == null)
            return XMLTools.LoadListFromXMLElement(s_dependencys_xml).Elements().Select(d=> getDependency(d));
        else
            return XMLTools.LoadListFromXMLElement(s_dependencys_xml).Elements().Select(d => getDependency(d)).Where(filter);
    }

    public void Update(Dependency item)
    {
        XElement rootDep = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        XElement? elementItem = (from p in rootDep.Elements("Dependency")
                                 where (int)p.Element("Id") == item.Id
                                select p).FirstOrDefault();

        if (elementItem == null) 
        {
            throw new DalDoesNotExistException($"Dependency with ID={item.Id} does Not exist");
        }
        elementItem.Remove();
        rootDep.Add(toXElement(item));
        XMLTools.SaveListToXMLElement(rootDep, s_dependencys_xml);
    }

    public void Clear()
    {
        XElement rootDep = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        rootDep.RemoveAll();
        XMLTools.SaveListToXMLElement(rootDep, s_dependencys_xml);
    }
}

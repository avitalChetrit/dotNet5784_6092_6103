using DalApi;
using DO;
using System.Xml.Linq;
namespace Dal;

internal class DependencyImplementation : IDependency
{
    readonly string s_dependencys_xml = "dependencys";

    /// <summary>
    /// Convert XElement to Dependency
    /// </summary>
    /// <param name="d"></param>
    /// <returns></returns>
    /// <exception cref="FormatException"></exception>
    static Dependency getDependency(XElement d)
    {
        return new Dependency()
        {
            Id = d.ToIntNullable("Id") ?? throw new FormatException("can't convert id"),

            PreTask = d.ToIntNullable("PreTask") ?? 0,

            CurrTask = d.ToIntNullable("CurrTask") ?? 0
        };
    }

    /// <summary>
    /// Convert Dependency to XElement
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    static XElement toXElement(Dependency item)
    {
        XElement id = new XElement("Id", item.Id);
        XElement preTask = new XElement("PreTask", item.PreTask);
        XElement currTask = new XElement("CurrTask", item.CurrTask);

        XElement e=new XElement("Dependency", id, preTask, currTask);

        return e;
    }

    /// <summary>
    /// Create an object of type Dependency
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int Create(Dependency item)
    {
        XElement rootDep = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        int id = Config.NextDependencyId;
        Dependency copy = item with { Id = id };
        rootDep.Add(toXElement(copy));
        XMLTools.SaveListToXMLElement(rootDep, s_dependencys_xml);
        return copy.Id;
    }

    /// <summary>
    /// Delete an object of type Dependency
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DalDeletionImpossible"></exception>
    public void Delete(int id) //שאלנו את המרצה מה לעשות בקשר למחיקות והיא אמרה שהיא תבדוק ותעדכן אותנו
    {
        XElement rootDep = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        XElement? elementItem = (from p in rootDep.Elements("Dependency")
                                 where (int)p.Element("Id") == id
                                 select p).FirstOrDefault();

        if (elementItem == null)
        {
            throw new DalDoesNotExistException($"Dependency with ID={id} does Not exist");
        }
        elementItem.Remove();
        XMLTools.SaveListToXMLElement(rootDep, s_dependencys_xml);

    }

    /// <summary>
    /// Read an object of type Dependency
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Dependency? Read(int id)
    {
        XElement? dependencyElem = XMLTools.LoadListFromXMLElement(s_dependencys_xml).Elements().FirstOrDefault(d=>(int ?)d.Element("Id") ==id); 
        return dependencyElem is null? null: getDependency(dependencyElem);
    }

    /// <summary>
    /// Read an object of type Dependency according to filter
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        return XMLTools.LoadListFromXMLElement(s_dependencys_xml).Elements().Select(d=> getDependency(d)).FirstOrDefault(filter);
    }

    /// <summary>
    /// ReadAll objects of type Dependency according to filter
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        if(filter == null)
            return XMLTools.LoadListFromXMLElement(s_dependencys_xml).Elements().Select(d=> getDependency(d));
        else
            return XMLTools.LoadListFromXMLElement(s_dependencys_xml).Elements().Select(d => getDependency(d)).Where(filter);
    }

    /// <summary>
    /// Update an object of type Dependency
    /// </summary>
    /// <param name="item"></param>
    /// <exception cref="DalDoesNotExistException"></exception>
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

    /// <summary>
    /// Clear Dependency dataSource
    /// </summary>
    public void Clear()
    {
        XElement rootDep = XMLTools.LoadListFromXMLElement(s_dependencys_xml);
        rootDep.RemoveAll();
        XMLTools.SaveListToXMLElement(rootDep, s_dependencys_xml);
    }
}

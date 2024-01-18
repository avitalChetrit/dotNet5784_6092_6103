using DalApi;
using DO;
using System.Xml.Linq;
namespace Dal;

internal class DependencyImplementation : IDependency
{
    readonly string s_dependencys_xml = "dependencys";
    XElement dependencyRoot;

    static Dependency getDependency(XElement d)
    {
        return new Dependency()
        {
            Id = d.ToIntNullable("Id") ?? throw new FormatException("can't convert id"),

            PreTask = d.ToIntNullable("PreTask") ?? null,

            CurrTask = d.ToIntNullable("CurrTask") ?? null
        };
    }



    public int Create(Dependency item)
    {

        XElement id = new XElement("id", item.Id);
        XElement preTask = new XElement("preTask", item.PreTask);
        XElement currTask = new XElement("currTask", item.CurrTask);

        dependencyRoot.Add(new XElement("dependency", id, preTask, currTask));
        dependencyRoot.Save(s_dependencys_xml);
        return item.Id;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Dependency? Read(int id)
    {
        //XElement
        XElement? studentElem = XMLTools.LoadListFromXMLElement(s_students_xal).Elements().FirstOrDefault(st(int ?)st.Element("Id") id); return studentElem is null null getStudent(student Elem);

    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Dependency item)
    {
        throw new NotImplementedException();
    }
}

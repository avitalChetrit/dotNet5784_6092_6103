using DalApi;
using DO;
using System.Data.Common;

namespace Dal;

internal class ChefImplementation : IChef
{
    readonly string s_chefs_xml = "chefs";

    public int Create(Chef item)
    {
        List<Chef> Chefs = XMLTools.LoadListFromXMLSerializer<Chef>(s_chefs_xml);  //Load

        if (Read(item.ChefId) is not null)
        {
            throw new DalAlreadyExistsException($"Chef with ID={item.ChefId} already exists");
        }
        Chefs.Add(item);//add c to chefs list

        XMLTools.SaveListToXMLSerializer(Chefs, s_chefs_xml);  //save

        return item.ChefId;//return c chefId
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Chef? Read(int id)
    {
        throw new NotImplementedException();
    }

    public Chef? Read(Func<Chef, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Chef?> ReadAll(Func<Chef, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Chef item)
    {
        throw new NotImplementedException();
    }
}

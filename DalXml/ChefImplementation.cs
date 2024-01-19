using DalApi;
using DO;
using System.Data.Common;

namespace Dal;

internal class ChefImplementation : IChef
{
    readonly string s_chefs_xml = "chefs";

    public void Clear()
    {
        List<Chef> Chefs = XMLTools.LoadListFromXMLSerializer<Chef>(s_chefs_xml);  //Load
        Chefs.Clear();
        XMLTools.SaveListToXMLSerializer(Chefs, s_chefs_xml);  //save
    }

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
        throw new DalDeletionImpossible("Can't delete the chef object!");
    }

    public Chef? Read(int id)
    {
        List<Chef> Chefs = XMLTools.LoadListFromXMLSerializer<Chef>(s_chefs_xml);  //Load
        return Chefs.FirstOrDefault(item => item.ChefId == id);
    }

    public Chef? Read(Func<Chef, bool> filter)
    {
        List<Chef> Chefs = XMLTools.LoadListFromXMLSerializer<Chef>(s_chefs_xml);  //Load
        return Chefs.FirstOrDefault(item => filter(item));
    }

    public IEnumerable<Chef?> ReadAll(Func<Chef, bool>? filter = null)
    {
        List<Chef> Chefs = XMLTools.LoadListFromXMLSerializer<Chef>(s_chefs_xml);  //Load
        if (filter != null)
        {
            return from item in Chefs
                   where filter(item)
                   select item;
        }
        return from item in Chefs
               select item;
    }

    public void Update(Chef item)
    {
        List<Chef> Chefs = XMLTools.LoadListFromXMLSerializer<Chef>(s_chefs_xml);  //Load

        bool isExist = Chefs.Exists(x => x.ChefId == item.ChefId);//checks if there is an objects with the same id on list
        if (isExist)//such item is on list
        {
            Chef c = Chefs.Find(x => x.ChefId == item.ChefId)!;//finds the object with the same id
            Chefs.Remove(c);//removes the old objects
            Chefs.Add(item);//add the new object
            XMLTools.SaveListToXMLSerializer(Chefs, s_chefs_xml);  //save
        }
        else//such item is not on list
        {
            throw new DalDoesNotExistException($"Chef with ID={item.ChefId} does Not exist");
        }
    }
}

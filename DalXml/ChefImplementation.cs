using DalApi;
using DO;

namespace Dal;

internal class ChefImplementation : IChef
{
    readonly string s_chefs_xml = "chefs";

    public int Create(Chef item)
    {
        throw new NotImplementedException();
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

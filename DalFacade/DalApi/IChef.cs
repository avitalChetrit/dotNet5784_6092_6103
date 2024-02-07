namespace DalApi;
using DO;


public interface IChef : ICrud<Chef>
{
    Chef? Read(int? chefId);
}


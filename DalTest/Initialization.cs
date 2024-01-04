namespace DalTest;
using DalApi;
using DO;

internal static class Intialization
{
    private static IChef? s_dalChef; //stage 1
    //private static ICourse? s_dalCourse; //stage 1
    //private static ILink? s_dalLink; //stage 1

    private static readonly Random s_rand = new();//field, which all entities will use, to generate random numbers while filling in the values of the objects.
}

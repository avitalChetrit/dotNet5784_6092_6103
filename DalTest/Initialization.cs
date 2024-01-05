namespace DalTest;
using DalApi;
using DO;
using System.Collections.Generic;

internal static class Initialization
{
    private static IChef? s_dalChef; //stage 1
    private static ITask? s_dalTask; //stage 1
    private static IDependency? s_dalDependency; //stage 1

    private static readonly Random s_rand = new();//field, which all entities will use, to generate random numbers while filling in the values of the objects.

    private static void createChefs()
    {
        //names options
        string[] chefNames =
        {
            "Dani Levi", "Eli Amar", "Yair Cohen",
            "Ariela Levin", "Dina Klein", "Shira Israelof"
        };
        //range of id
        const int MIN_ID = 200000000;
        const int MAX_ID = 400000000;
        // initialise for level 
        ChefExperience _level = ChefExperience.Beginner; //ranodm enum?
        //rang of cost
        double? _cost;
        const int MIN_COST = 100;
        const int MAX_COST = 500;

        //loop to initialize var
        foreach (var _name in chefNames)
        {
            int _id;
            do
                _id = s_rand.Next(MIN_ID, MAX_ID);      // create random id
            while (s_dalChef!.Read(_id) != null); //while there is no object with the same random id , continue

            _cost = s_rand.Next(MIN_COST, MAX_COST);      // create random cost

            Chef newCh = new(_id, _level, _name, _cost); //create new chef 

            s_dalChef!.Create(newCh);     //adding chefs into the list
        }

    }
    private static void createTasks()
    {
        //Alias for tasks
        string[] Alias =
        {"Make bread", "Wash dishes", "Cook Pasta", "Cook Ravioli","Buy Dishes",
        "Clean Tables","Order Suplly", "Clean Kitchen", "Bake cake"};

        //range of id
        const int MIN_ID = 200000000;
        const int MAX_ID = 400000000;

        //intialize values
        foreach (var _AlisasName in Alias)
        {
            //find a unique random number 
            int _id;
            do
                _id = s_rand.Next(MIN_ID, MAX_ID);
            while (s_dalTask!.Read(_id) != null);

            //add description
            string _description = "need to" + _AlisasName;

            //at first it will be false
            bool _isMilestone = false;

            ChefExperience? _complexity = ChefExperience.Beginner;

            //Create a random date between 1/1/2000 - Today
            DateTime start = new DateTime(2000, 1, 1);
            int range = (DateTime.Today - start).Days;
            DateTime _createdAtDate = start.AddDays(s_rand.Next(range));

            // Rndom time range from 1 day to 30 days
            int randomDays = s_rand.Next(1, 30);
            TimeSpan _requiredTime = TimeSpan.FromDays(randomDays);

            DateTime? _startDate = null;
            DateTime? _scheduledDate = null;

            // Create a random future day by adding today a random namber of days
            int daysToAddRand = s_rand.Next(1, 365);
            DateTime currentDate = DateTime.Today;
            DateTime _deadLineDate = currentDate.AddDays(daysToAddRand);

            //rest of dates - null
            DateTime? _completeDate = null;
            string? _deliveables = null;
            string? _remarks = null;
            int? _ChefId = null;
            Task newTask = new(_id, _AlisasName, _description, _isMilestone, _complexity, _createdAtDate, _requiredTime, _startDate, _scheduledDate, _deadLineDate, _completeDate, _deliveables, _remarks, _ChefId);

            s_dalTask!.Create(newTask);
        }

    }
    private static void createDependencys()
    {

    }

    public static void Do(IChef? dalChef, ITask? dalTask, IDependency? dalDependency)     //
    {
        s_dalChef = dalChef ?? throw new NullReferenceException("DAL can not be null!");
        createChefs();

        s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        createTasks();

        s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        createDependencys();
    }
}

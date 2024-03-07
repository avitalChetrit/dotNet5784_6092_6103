namespace DalTest;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Security.Cryptography;

public static class Initialization
{
    //private static IChef? s_dalChef; //stage 1
    //private static ITask? s_dalTask; //stage 1
    //private static IDependency? s_dalDependency; //stage 1

    private static IDal? s_dal; //stage 2


    private static readonly Random s_rand = new();//field, which all entities will use, to generate random numbers while filling in the values of the objects.

    private static void createChefs()
    {
        //names options
        string[] chefNames =
        {
            "Eyal Shani", "Gordon Ramsay", "Asaf Granit",
            "Yosi Shitrit", "Moshik Rot", "Meir Adoni"
        };
        //range of id
        const int MIN_ID = 200000000;
        const int MAX_ID = 400000000;
        // initialise for level 
        ChefExperience _level = ChefExperience.Beginner; //ranodm enum?
        string? _email;
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
            while (s_dal!.Chef.Read(_id) != null); //while there is no object with the same random id , continue

            _cost = s_rand.Next(MIN_COST, MAX_COST);      // create random cost

            _email = _name.Replace(" ", "");
            _email += "@gmail.com";

            Chef newCh = new(_id, _level, _name, _email, _cost); //create new chef 

            s_dal!.Chef.Create(newCh);     //adding chefs into the list
        }

    }
    private static void createTasks()
    {
        //Alias for tasks
        string[] Alias = {
    "Order Supply",
    "Chop Vegetables",
    "Marinate Meat",
    "Preheat Oven",
    "Boil Water",
    "Saute Onions",
    "Grill Chicken",
    "Bake Cookies",
    "Blend Smoothie",
    "Roast Vegetables",
    "Steam Broccoli",
    "Slice Bread",
    "Prepare Salad",
    "Cook Rice",
    "Fry Eggs",
    "Mix Batter",
    "Toast Bread",
    "Make Coffee",
    "Whisk Eggs",
    "Peel Potatoes",
    "Chop Herbs",
    "Cook Pasta",
    "Add Seasoning",
    "Clean Pots",
    "Peel Fruits",
    "Clean Kitchen",
    "Set The Tables",
    "Heat Oil",
    "Fry Chips",
    "Bake Cake"
};

        //range of id
        const int MIN_ID = 200000000;
        const int MAX_ID = 400000000;

        //intialize values
        foreach (var _AlisasName in Alias)
        {
            //find a unique random number 
            int _id= 0;   //will get a value in create metod.
            
            //add description
            string _description = "need to " + _AlisasName;

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
            DateTime? _ScheduledDate = null;

            // Create a random future day by adding today a random namber of days
            int daysToAddRand = s_rand.Next(1, 365);
            DateTime currentDate = DateTime.Today;
            DateTime _deadLineDate = currentDate.AddDays(daysToAddRand);

            //rest of dates - null
            DateTime? _completeDate = null;
            string? _deliveables = null;
            string? _remarks = null;
            int? _ChefId = null;
            Task newTask = new(_id, _AlisasName, _description, _isMilestone, _complexity, _createdAtDate, _requiredTime, _startDate, _ScheduledDate, _deadLineDate, _completeDate, _deliveables, _remarks, _ChefId);

            s_dal!.Task.Create(newTask);
        }

    }
    private static void createDependencys()
    {
        List<Task> copyListTask = s_dal!.Task.ReadAll().ToList<Task>();
        
        int _id = 0;
        Dependency newDepend = new(_id, copyListTask[0].Id, copyListTask[1].Id); //Task 1 depend on Task 0
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[0].Id, copyListTask[19].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[0].Id, copyListTask[2].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[0].Id, copyListTask[11].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[1].Id, copyListTask[9].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[1].Id, copyListTask[10].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[18].Id, copyListTask[14].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[11].Id, copyListTask[16].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[4].Id, copyListTask[21].Id); //Pasta
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[21].Id, copyListTask[22].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[1].Id, copyListTask[5].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[1].Id, copyListTask[12].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[3].Id, copyListTask[7].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[4].Id, copyListTask[13].Id); //Rice
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[4].Id, copyListTask[17].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[15].Id, copyListTask[7].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[0].Id, copyListTask[21].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[0].Id, copyListTask[13].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[23].Id, copyListTask[21].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[23].Id, copyListTask[13].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[18].Id, copyListTask[16].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[19].Id, copyListTask[6].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[20].Id, copyListTask[6].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[22].Id, copyListTask[6].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[24].Id, copyListTask[8].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[7].Id, copyListTask[25].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[16].Id, copyListTask[25].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[12].Id, copyListTask[25].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[0].Id, copyListTask[19].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[27].Id, copyListTask[28].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[27].Id, copyListTask[5].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[7].Id, copyListTask[26].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[6].Id, copyListTask[26].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[16].Id, copyListTask[26].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[11].Id, copyListTask[26].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[15].Id, copyListTask[16].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[2].Id, copyListTask[25].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[18].Id, copyListTask[29].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[15].Id, copyListTask[29].Id);
        s_dal!.Dependency.Create(newDepend);
        newDepend = new(_id, copyListTask[3].Id, copyListTask[29].Id);
        s_dal!.Dependency.Create(newDepend);
        
    }

    //public static void Do(IDal dal) //stage 2
    public static void Do() //stage 4    
    {
        //s_dal = dal ?? throw new NullReferenceException("DAL object can not be null!"); //stage 2
        Reset();

        //s_dalChef = dalChef ?? throw new NullReferenceException("DAL can not be null!");
        createChefs();

        //s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        createTasks();

        //s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        createDependencys();
    }

    public static void Reset()
    {
        s_dal = Factory.Get; //stage 4

        s_dal!.Dependency.Clear();
        s_dal!.Chef.Clear();
        s_dal!.Task.Clear();
    }
}

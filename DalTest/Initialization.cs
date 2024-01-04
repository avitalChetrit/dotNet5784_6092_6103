﻿namespace DalTest;
using DalApi;
using DO;
using System.Collections.Generic;

internal static class Initialization
{
    private static IChef? s_dalChef; //stage 1
    //private static ICourse? s_dalCourse; //stage 1
    //private static ILink? s_dalLink; //stage 1

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


            //DateTime start = new DateTime(1995, 1, 1);
            //int range = (DateTime.Today - start).Days;
            //DateTime _bdt = start.AddDays(s_rand.Next(range));

            Chef newCh = new(_id, _level, _name, _cost); //create new chef 

            s_dalChef!.Create(newCh);     //adding chefs into the list
        }

    }

    public static void Do(IChef? dalChef)     //
    {
        s_dalChef = dalChef ?? throw new NullReferenceException("DAL can not be null!");
        createChefs();
    }
}

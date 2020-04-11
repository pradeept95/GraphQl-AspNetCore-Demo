﻿using Application.Data.DataContext;
using Application.Data.Model;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace App.GraphQL.Type
{
    public class AppQueryType : ObjectGraphType
    {
        public AppQueryType(ApplicationDbContext db)
        {
            Name = "Query";

            //get all employee list
            Field<ListGraphType<EmployeeType>>(
             name: "employees",
             resolve: context =>
             {
                 return db.Employee
                    .Include(x => x.Addresses)
                    .Include(x => x.Phones)
                    .Include(x => x.EmailAddresses);
             });

            //get employee by id
            Field<EmployeeType>(
            name: "employee",
             arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }),
            resolve: context =>
            {

                var id = context.GetArgument<int>("id");

                return db.Employee
                   .Include(x => x.Addresses)
                   .Include(x => x.Phones)
                   .Include(x => x.EmailAddresses)
                   .FirstOrDefault(x => x.Id == id);
            });
        }


    }
}

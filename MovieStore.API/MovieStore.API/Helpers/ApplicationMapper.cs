using AutoMapper;
using MovieStore.API.Data;
using MovieStore.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.API.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Movies, MovieModel>().ReverseMap(); // To map the properties of an entity class to model class and vice versa
            CreateMap<Customers, CustomerModel>().ReverseMap(); // To map the properties of an entity class to model class and vice versa
            CreateMap<ApplicationUser, UserModel>().ReverseMap(); // To map the properties of an entity class to model class and vice versa
            CreateMap<ApplicationUser, AllUsersModel>().ReverseMap(); // To map the properties of an entity class to model class and vice versa
        }
    }
}

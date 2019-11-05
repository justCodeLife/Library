using System;
using AutoMapper;
using Library.Models.ViewModels;

namespace Library.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddEditBookViewModel, Book>();
        }
    }
}
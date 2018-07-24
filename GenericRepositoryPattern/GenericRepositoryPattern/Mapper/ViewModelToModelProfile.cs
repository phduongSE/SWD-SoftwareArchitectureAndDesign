using AutoMapper;
using GenericRepositoryPattern.Models;
using GenericRepositoryPattern.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericRepositoryPattern.Mapper
{
    public class ViewModelToModelProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "ModelToViewModel";
            }
        }

        public ViewModelToModelProfile()
        {
            CreateMap<BookEM, Book>()
                .ForMember(m => m.Id, map => map.MapFrom(em => em.Id))
                .ForMember(m => m.Title, map => map.MapFrom(em => em.Title))
                .ForMember(m => m.PublishDate, map => map.MapFrom(em => em.PublishDate))
                .ForMember(m => m.AuthorId, map => map.MapFrom(em => em.AuthorId));

            CreateMap<AuthorVM, Author>()
                .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))
                .ForMember(vm => vm.Name, map => map.MapFrom(m => m.Name))
                .ForMember(vm => vm.BirthDay, map => map.MapFrom(m => m.BirthDay));
        }
    }
}
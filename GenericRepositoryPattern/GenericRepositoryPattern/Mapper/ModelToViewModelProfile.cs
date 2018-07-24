using AutoMapper;
using GenericRepositoryPattern.Models;
using GenericRepositoryPattern.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericRepositoryPattern.Mapper
{
    public class ModelToViewModelProfile : Profile
    {
        public override string ProfileName {
            get
            {
                return "ModelToViewModel";
            }
        }

        public ModelToViewModelProfile()
        {
            CreateMap<Book, BookVM>()
                .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))
                .ForMember(vm => vm.Title, map => map.MapFrom(m => m.Title))
                .ForMember(vm => vm.PublishDate, map => map.MapFrom(m => m.PublishDate))
                .ForMember(vm => vm.AuthorName, map => map.MapFrom(m => m.Author.Name));
            CreateMap<Book, BookEM>()
                .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))
                .ForMember(vm => vm.Title, map => map.MapFrom(m => m.Title))
                .ForMember(vm => vm.PublishDate, map => map.MapFrom(m => m.PublishDate))
                .ForMember(vm => vm.AuthorId, map => map.MapFrom(m => m.AuthorId));

            CreateMap<Author, AuthorVM>()
                .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))
                .ForMember(vm => vm.Name, map => map.MapFrom(m => m.Name))
                .ForMember(vm => vm.BirthDay, map => map.MapFrom(m => m.BirthDay));
            CreateMap<Author, AuthorDetail>()
                .ForMember(vm => vm.Id, map => map.MapFrom(m => m.Id))
                .ForMember(vm => vm.Name, map => map.MapFrom(m => m.Name))
                .ForMember(vm => vm.BirthDay, map => map.MapFrom(m => m.BirthDay))
                .ForMember(vm => vm.BookList, map => map.MapFrom(m => m.BookList));
        }
    }
}
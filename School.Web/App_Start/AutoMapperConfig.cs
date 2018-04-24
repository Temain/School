using AutoMapper;
using School.Web.Models;
using School.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School.Web.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Teacher, TeacherViewModel>();
                cfg.CreateMap<TeacherViewModel, Teacher>();
                cfg.CreateMap<Student, StudentViewModel>()
                    .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.Number + src.Class.Prefix));
                cfg.CreateMap<StudentViewModel, Student>();
                cfg.CreateMap<Class, ClassViewModel>();
                cfg.CreateMap<ClassViewModel, Class>();
                cfg.CreateMap<Discipline, DisciplineViewModel>()
                    .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher.LastName + " "
                        + (string.IsNullOrWhiteSpace(src.Teacher.FirstName) ? src.Teacher.FirstName[0] + "." : "")
                        + (string.IsNullOrWhiteSpace(src.Teacher.MiddleName) ? src.Teacher.MiddleName[0] + "." : "")))
                    .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.Number + src.Class.Prefix));
                cfg.CreateMap<DisciplineViewModel, Discipline>();
                cfg.CreateMap<Material, MaterialViewModel>();
                cfg.CreateMap<MaterialViewModel, Material>();
                cfg.CreateMap<Lesson, LessonViewModel>();
                cfg.CreateMap<LessonDetail, LessonDetailViewModel>();
                cfg.CreateMap<LessonDetailViewModel, LessonDetail>();
            });
        }
    }
}
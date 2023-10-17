// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using AutoMapper;
using AutoMapperStudy.Models;

Console.WriteLine("Hello, World!");

var config = new MapperConfiguration(config =>
    config.CreateMap<Student, StudentDT>()
);

Student student = new Student
{
    Name = "Shivan",
    Age = 20,
    Address = "Bihar",
    Department = "IT"
};

var mapper = new Mapper(config);
var studentDT = mapper.Map<StudentDT>(student);

Console.WriteLine($@"
    Name: {studentDT.Name} 
    Age: {studentDT.Age}
    Addres: {studentDT.Address}
    Department: {studentDT.Department} 
");
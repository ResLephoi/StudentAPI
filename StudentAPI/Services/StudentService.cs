using Newtonsoft.Json;
using StudentAPI.Interfaces;
using StudentAPI.Models;
using System;

namespace StudentAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly string _filePath = "data.json";
        public Task<bool> CreateStudent(Student student)
        {
            try
            {
                List<Student> students = new List<Student>();

                if (File.Exists(_filePath))
                {
                    var jsonData = File.ReadAllText(_filePath);
                    students = JsonConvert.DeserializeObject<List<Student>>(jsonData);
                }

                if (students.Any())
                {
                    student.Id = students.Max(x => x.Id) + 1;
                }
                else
                {
                    student.Id = 1;
                }

                students.Add(student);

                SaveToFile(students);
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }
           
        }
        public IEnumerable<Student> GetAllStudents()
        {
            if (!File.Exists(_filePath))
                return new List<Student>();

            var jsonData = File.ReadAllText(_filePath);
            var students =  JsonConvert.DeserializeObject<List<Student>>(jsonData);
            return students;
        }
        public Student GetStudentById(int Id)
        {
            try
            {
                if (!File.Exists(_filePath))
                    return new Student();

                var jsonData = File.ReadAllText(_filePath);
                var students = JsonConvert.DeserializeObject<List<Student>>(jsonData);
                var existingStudent = students.Where(x => x.Id == Id).FirstOrDefault();

                return existingStudent;
            }
            catch (Exception ex)
            {
                return null;
            }            
        }
        public Task<bool> UpdateStudent(Student student)
        {
            try
            {
                var students = GetAllStudents().ToList();
                var existingStudent = students.FirstOrDefault(x => x.Id == student.Id);
                if (existingStudent != null)
                {
                    existingStudent.FullName = student.FullName;
                    existingStudent.StudentNumber = student.StudentNumber;
                    SaveToFile(students);
                    return Task.FromResult(true);
                }
                return Task.FromResult(false);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
           
        }
        public Task<bool> DeleteStudent(int StudentId)
        {
            try
            {
                var students = GetAllStudents().ToList();
                var student = students.FirstOrDefault(x => x.Id == StudentId);
                if (student != null)
                {
                    students.Remove(student);
                    SaveToFile(students);
                    return Task.FromResult(true);
                }
                return Task.FromResult(false);
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }

        }

        private void SaveToFile(List<Student> student)
        {
            var jsonData = JsonConvert.SerializeObject(student, Formatting.Indented);
            File.WriteAllText(_filePath, jsonData);
        }
    }
}

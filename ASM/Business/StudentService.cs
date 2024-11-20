using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ASM.DataAccess;
using static System.Net.WebRequestMethods;

namespace ASM.Business
{
    /// <summary>
    /// Lớp này chứa các logic nghiệp vụ liên quan đến sinh viên.
    /// Nó sử dụng lớp StudentRepository để thực hiện các thao tác liên quan đến sinh viên như quản lý thông tin sinh viên, kiểm tra điều kiện, và các xử lý nghiệp vụ khác.
    /// </summary>

    internal class StudentService
    {
        private readonly StudentRepository _studentRepository;

        public StudentService(StudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public void AddNewStudent()
        {
            Student newStudent = StudentInput();
            _studentRepository.AddStudent(newStudent);
            Console.WriteLine("Sinh viên đã được thêm vào thành công");
        }

        public void ShowAllStudents()
        {
            List<Student> students = _studentRepository.GetAllStudents();
            PrintStudents(students);
        }

        public void ShowStudentsOrderedByMarkDesc()
        {
            List<Student> students = _studentRepository.GetStudentsOrderedByMarkDesc();
            PrintStudents(students);
        }

        public void ShowTop5Students()
        {
            List<Student> students = _studentRepository.GetTop5Students();
            PrintStudents(students);
        }

        public void ShowStudentsByMarkRange()
        {
            float minMark, maxMark;
            while (true) { 
                Console.Write("Nhập khoảng điểm min: ");
                string minInput = Console.ReadLine();

                if (float.TryParse(minInput, out minMark))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Giá trị không hợp lệ. Vui lòng nhập lại.");
                }
            }
            
            while (true)
            {
                Console.Write("Nhập khoảng điểm max: ");
                string maxInput = Console.ReadLine();

                if (float.TryParse(maxInput, out maxMark))
                {
                    if (minMark < maxMark) {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Điểm tối đa phải lớn hơn điểm tối thiểu. Vui lòng nhập lại.");
                    }
                }
                else
                {
                    Console.WriteLine("Giá trị không hợp lệ. Vui lòng nhập lại.");
                }
            }

            List<Student> students = _studentRepository.GetStudentsByMarkRange(minMark, maxMark);
            if(students.Count > 0)
            {
                PrintStudents(students);
            }else
            {
                Console.WriteLine($"Không có sinh viên nào trong khoảng {minMark} - {maxMark} điểm.");
            }
        }

        public void UpdateStudent()
        {
            Console.Write("Nhập ID của sinh viên: ");
            int stId = int.Parse(Console.ReadLine());

            if (_studentRepository.IsStudentIDExists(stId))
            {
                Student student = StudentInput(stId); 

                _studentRepository.UpdateStudent(student);
                
                Console.WriteLine("Cập nhập thông tin thành công");
            }
            else
            {
                Console.WriteLine($"Không tìm thấy sinh viên có ID {stId}");
            }
        }

        private Student StudentInput(int stId = 0)
        {
            Console.Write("Nhập tên của sinh viên: ");
            string name = Console.ReadLine();
            while (string.IsNullOrEmpty(name)) 
            {
                Console.WriteLine("Họ tên không được để trống vui lòng nhập lại.");
                Console.Write("Nhập tên của sinh viên: ");
                name = Console.ReadLine();
            }

            Console.Write("Nhập diểm của sinh viên: ");
            float mark;
            while (!float.TryParse(Console.ReadLine(), out mark) || mark < 0 || mark > 10)
            {
                Console.WriteLine("Điểm phải là số thực nằm trong khoảng từ 0 - 10.");
                Console.Write("Nhập diểm của sinh viên: ");
            }
            
            Console.Write("Nhập email của sinh viên: ");
            string email = Console.ReadLine();
            string emailPattern = @"^[^@\s]+@fpt\.edu\.vn$";
            while (!Regex.IsMatch(email, emailPattern)) 
            {
                Console.WriteLine("Email không hợp lệ. Vui lòng nhập lại email (phải là @fpt.edu.vn)");
                Console.Write("Nhập email của sinh viên: ");
                email = Console.ReadLine();
            }

            Console.Write("Nhập class ID của sinh viên: ");
            int idClass = int.Parse(Console.ReadLine());
            
            Student student = new Student()
            {
                Name = name,
                Mark = mark,
                Email = email,
                IdClass = idClass
            };

            if (stId != 0) student.StId = stId;

            return student;
        }

        private string GetPerformance(Student student)
        {
            if (student.Mark < 5) return "Yếu";
            else if (student.Mark < 6.5) return "Trung bình";
            else if (student.Mark < 7.5) return "Khá";
            else if (student.Mark < 9) return "Giỏi";
            else return "Xuất sắc";
        }
        private void PrintStudents(List<Student> students, bool printName = true, 
            bool printMark = true, bool printEmail = true, bool printIdClass = true, bool printPerformance = true)
        {
            foreach (Student student in students)
            {
                string output = $"ID: {student.StId}";
                if (printName) output += $", Tên sinh viên: {student.Name}";
                if (printMark) output += $", Điểm: {student.Mark}";
                if (printEmail) output += $", Email: {student.Email}";
                if (printPerformance) output += $", Học lực: {GetPerformance(student)}";
                if (printIdClass) output += $", Class ID: {student.IdClass}";

                Console.WriteLine(output);
            }
        }
    }
}

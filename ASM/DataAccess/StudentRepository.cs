using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ASM.DataAccess
{
    /// <summary>
    /// Sinh viên này tương tác với dữ liệu lớp học từ SQL Server.
    /// Nó cung cấp các chức năng để lấy, thêm, cập nhật và xóa thông tin sinh viên.
    /// </summary>
    internal class StudentRepository
    {
        string connectionString = @"Data Source=localhost;Initial Catalog=Asm_C#2;Integrated Security=True;";

        public void AddStudent(Student newStudent)
        {
            using (dbASMDataContext db = new dbASMDataContext(connectionString))
            {
                db.Students.InsertOnSubmit(newStudent);
                db.SubmitChanges();
            }
        }
        

        public bool UpdateStudent(Student updateStudent) 
        {
            using (dbASMDataContext db = new dbASMDataContext(connectionString))
            {
                var student = db.Students.FirstOrDefault(s => s.StId == updateStudent.StId);

                if (student != null) {
                    student.Name = updateStudent.Name;
                    student.Mark = updateStudent.Mark;
                    student.Email = updateStudent.Email;
                    student.IdClass = updateStudent.IdClass;

                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
        }

        public List<Student> GetAllStudents()
        {
            using (dbASMDataContext db = new dbASMDataContext(connectionString))
            {
                List<Student> students = db.Students.ToList();
                return students;
            }
        }

        

        public List<Student> GetStudentsByMarkRange(float minMark, float maxMark)
        {
            using (dbASMDataContext db = new dbASMDataContext(connectionString))
            {
                List<Student> students = db.Students
                                            .Where(s => (s.Mark >= minMark && s.Mark <= maxMark))
                                            .ToList();
                return students;
            }

        }

        public List<Student> GetTop5Students()
        {
            using (dbASMDataContext db = new dbASMDataContext(connectionString))
            {
                List<Student> students = db.Students
                                            .OrderByDescending(s => s.Mark)
                                            .Take(5)
                                            .ToList();
                return students;
            }
        }

        public List<Student> GetStudentsOrderedByMarkDesc()
        {
            using (dbASMDataContext db = new dbASMDataContext(connectionString))
            {
                List<Student> students = db.Students
                                            .OrderByDescending(s => s.Mark)
                                            .ToList();
                return students;
            }
        }

        public bool IsStudentIDExists(int id)
        {
            using (dbASMDataContext db = new dbASMDataContext(connectionString))
            {
                return db.Students.Any(s => s.StId == id);
            }
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.DataAccess
{
    /// <summary>
    /// Lớp này tương tác với dữ liệu lớp học từ SQL Server.
    /// Nó cung cấp các chức năng để lấy, thêm, cập nhật và xóa thông tin lớp học.
    /// </summary>
    internal class ClassRepository
    {
        string connectionString = @"Data Source=localhost;Initial Catalog=Asm_C#2;Integrated Security=True;";

        public void AddClass(Class newClass)
        {
            using (dbASMDataContext db = new dbASMDataContext(connectionString))
            {
                db.Classes.InsertOnSubmit(newClass);
                db.SubmitChanges();
            }
        }

        // làm dư để đó có khi cần
        public List<Class> GetAllClass()
        {
            using (dbASMDataContext db = new dbASMDataContext(connectionString))
            {
                List<Class> classes = db.Classes.ToList();
                return classes;
            }
        }

        public List<Student> GetStudentsByClass(int IDClass)
        {
            using (dbASMDataContext db = new dbASMDataContext())
            {
                return db.Students
                         .Where(s => s.IdClass == IDClass)
                         .ToList();
            }
        }

    }
}

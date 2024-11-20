using ASM.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ASM.Business
{
    /// <summary>
    /// Lớp này chứa các logic nghiệp vụ liên quan đến lớp.
    /// Nó sử dụng lớp ClassRepository để thực hiện các thao tác liên quan đến lớp như quản lý thông tin lớp, kiểm tra điều kiện, và các xử lý nghiệp vụ khác.
    /// </summary>

    internal class ClassService
    {

        private readonly ClassRepository _classRepository;

        public ClassService(ClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public void AddNewClass()
        {
            Console.Write("Nhập tên của lớp: ");
            string nameClass = Console.ReadLine();


            Class newClass = new Class()
            {
                NameClass = nameClass
            };

            _classRepository.AddClass(newClass);
            Console.WriteLine("Lớp đã được thêm vào thành công");
        }   



        public void CalculateAverageMarksInThread()
        {
            Thread dtbThread = new Thread(() =>
            {
                Console.WriteLine("Thread DTB đang chạy...");

                var classes = _classRepository.GetAllClass();
                using (StreamWriter writer = new StreamWriter(@"D:\Asm_C#2.txt", false))
                {
                    writer.WriteLine("Điểm trung bình của từng lớp:");
                    foreach (var classInfo in classes)
                    {
                        var students = _classRepository.GetStudentsByClass(classInfo.IdClass);

                        if (students.Any())
                        {
                            var avg = students.Average(s => s.Mark);
                            writer.WriteLine($"Lớp ID: {classInfo.IdClass}, Tên lớp: {classInfo.NameClass}, Điểm trung bình: {avg}");
                        }
                        else {
                            writer.WriteLine($"Lớp ID: {classInfo.IdClass}, Tên lớp: {classInfo.NameClass}, không có sinh viên");
                        }

                    }

                }

                Console.WriteLine("Thread DTB đã hoàn thành và ghi kết quả vào file.");
            })
            {
                Name = "DTB"
            };
            
            dtbThread.Start();
            dtbThread.Join();
        }
    }
}

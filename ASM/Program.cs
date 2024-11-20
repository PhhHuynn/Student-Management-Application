using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASM.Business;
using ASM.DataAccess;

namespace ASM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            //format value
            int lineLength = 100;

            StudentService studentService = new StudentService(new StudentRepository());
            ClassService classService = new ClassService(new ClassRepository());

            int choice;
            do
            {
                Console.WriteLine();
                Console.WriteLine(new string('-', lineLength));
                Console.WriteLine(FormatTaskDescription(1, "Nhập danh sách Class", lineLength));
                Console.WriteLine(FormatTaskDescription(2, "Nhập danh sách Student", lineLength));
                Console.WriteLine(FormatTaskDescription(3, "Xuất danh sách học viên", lineLength));
                Console.WriteLine(FormatTaskDescription(4, "Tìm kiếm học viên theo khoảng Mark", lineLength));
                Console.WriteLine(FormatTaskDescription(5, "Cập nhật thông tin học viên theo StId", lineLength));
                Console.WriteLine(FormatTaskDescription(6, "Xuất học viên theo thứ tự điểm từ cao tới thấp", lineLength));
                Console.WriteLine(FormatTaskDescription(7, "Xuất 5 học viên có điểm cao nhất", lineLength));
                Console.WriteLine(FormatTaskDescription(8, "Tính điểm trung bình theo từng lớp và ghi vào tập tin Asm_C#2.txt", lineLength));
                Console.WriteLine(FormatTaskDescription(0, "Thoát", lineLength));
                Console.WriteLine(new string('-', lineLength));


                while (true)
                {
                    Console.Write("Chọn bài (từ 0 đến 8): ");
                    if (!int.TryParse(Console.ReadLine(), out choice))
                    {
                        continue;
                    }
                    break;
                }

                switch (choice)
                {
                    case 1:
                        classService.AddNewClass();
                        break;
                    case 2:
                        studentService.AddNewStudent();
                        break;
                    case 3:
                        studentService.ShowAllStudents();
                        break;
                    case 4:
                        studentService.ShowStudentsByMarkRange();
                        break;
                    case 5:
                        studentService.UpdateStudent();
                        break;
                    case 6:
                        studentService.ShowStudentsOrderedByMarkDesc();
                        break;
                    case 7:
                        studentService.ShowTop5Students();
                        break;
                    case 8:
                        classService.CalculateAverageMarksInThread();
                        break;
                    case 0:
                        Console.WriteLine("Thoát chương trình");
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ");
                        break;
                }
            } while (choice != 0);

        }

        static string FormatTaskDescription(int taskNumber, string taskName, int length)
        {
            return $"| Chức năng {taskNumber}  : {taskName}.".PadRight(length - 1) + '|';
        }
    
    }
}

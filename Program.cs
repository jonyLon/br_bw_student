using System.Collections.Generic;
using System.Text;

namespace br_bw_student
{

    class Student
    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Specialty { get; protected set; }

        public List<int> Marks { get; protected set; }
        public Student(string first, string last, string spec, params int[] marks)
        {
            FirstName = first;
            LastName = last;
            Specialty = spec;
            Marks = new List<int>();
            Marks.AddRange(marks);
        }
        public override string ToString()
        {
            return $"Name: {FirstName} {LastName}\nSpeciality: {Specialty}\nMarks: " + String.Join(", ", Marks); 
        }

    }

    class FileWorker
    {
        public static void SaveStudents(List<Student> students, string fname)
        {

            using (BinaryWriter bw = new BinaryWriter(new FileStream(fname, FileMode.Create, FileAccess.Write)))
            {
                bw.Write(students.Count);
                foreach (Student student in students)
                {
                    bw.Write(student.FirstName);
                    bw.Write(student.LastName);
                    bw.Write(student.Specialty);
                    bw.Write(student.Marks.Count);
                    foreach (var item in student.Marks)
                    {
                        bw.Write(item);
                    }
                }
            }
        }
        public static List<Student> LoadStudents(string fname)
        {
            List <Student> students = new List<Student>();
            using (BinaryReader br = new BinaryReader(new FileStream(fname, FileMode.Open, FileAccess.Read)))
            {
                int count = br.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    string first = br.ReadString();
                    string last = br.ReadString();
                    string spec = br.ReadString();
                    int[] marks = new int[br.ReadInt32()];
                    for (int j = 0; j < marks.Length; j++)
                    {
                        marks[j] = br.ReadInt32();
                    }
                    students.Add(new Student(first, last, spec, marks));
                }
            }
            return students;
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            List<Student> SList = new List<Student>();
            SList.Add(new Student("Jony", "Spencer", "Mananger", new int[] { 5, 6, 6, 9, 10 }));
            SList.Add(new Student("George", "Woodenberg", "Doctor", new int[] { 10, 7, 8, 9, 10 }));
            FileWorker.SaveStudents(SList,"students.dat");
            List<Student> Test = FileWorker.LoadStudents("students.dat");
            foreach (Student student in Test)
            {
                Console.WriteLine(student.ToString());
            }
        }
    }
}
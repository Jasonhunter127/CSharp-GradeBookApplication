using System;
using GradeBook.Enums;
using System.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            // Cannot calculate letter grade for less than 5 students
            if (Students.Count < 5)
            {
                throw new InvalidOperationException();
            }

            // Get the number of students that comprise 20% and a list of grades in order
            int threshold = (int)Math.Ceiling(Students.Count * 0.2);
            var grades = Students.OrderByDescending(s => s.AverageGrade)
                                .Select(e => e.AverageGrade)
                                .ToList();

            // The threshold acts as an index in the list
            if (grades[threshold - 1] <= averageGrade)
                return 'A';     // Top 20% of students
            else if (grades[(threshold * 2) - 1] <= averageGrade)
                return 'B';     // Top 40% of students
            else if (grades[(threshold * 3) - 1] <= averageGrade)
                return 'C';     // Top 60% of students
            else if (grades[(threshold * 4) - 1] <= averageGrade)
                return 'D';     //Top 80% of students

            // Return base case
            return 'F';
        }

        public override void CalculateStatistics()
        {
            // Cannot calculate statistics for less than 5 students
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students " +
                                    "with grades in order to properly calculate a " +
                                    "studen't overall grade.");
                return;
            }

            // Call base method
            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            // Cannot calculate statistics for less than 5 students
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with " +
                                    "grades in order to properly calculate a student's " +
                                    "overall grade.");
                return;
            }

            // Call base method
            base.CalculateStudentStatistics(name);
        }
    }
}
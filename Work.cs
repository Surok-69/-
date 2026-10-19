using System;

namespace Lab3
{
    public abstract class Work
    {
        public string StudentName { get; }
        public string Topic { get; }
        public DateTime Date { get; }

        protected Work(string student, string topic, DateTime date)
        {
            StudentName = student;
            Topic = topic;
            Date = date;
        }

        public abstract string Describe();
    }

    public class LabWork : Work
    {
        public LabWork(string s, string t, DateTime d) : base(s, t, d) { }

        public override string Describe() =>
            $"Лабораторная | {StudentName} | {Topic} | {Date:yyyy.MM.dd}";
    }

    public class CourseWork : Work
    {
        public string Supervisor { get; }

        public CourseWork(string s, string t, DateTime d, string sup) : base(s, t, d)
        {
            Supervisor = sup;
        }

        public override string Describe() =>
            $"Курсовая | {StudentName} | {Topic} | {Date:yyyy.MM.dd} | {Supervisor}";
    }

    public class DiplomaWork : Work
    {
        public string Supervisor { get; }
        public int Grade { get; }

        public DiplomaWork(string s, string t, DateTime d, string sup, int g)
            : base(s, t, d)
        {
            Supervisor = sup;
            Grade = g;
        }

        public override string Describe() =>
            $"Диплом | {StudentName} | {Topic} | {Date:yyyy.MM.dd} | {Supervisor} | Оценка: {Grade}";
    }

    public class TestWork : Work
    {
        public string Teacher { get; }
        public int QuestionsCount { get; }

        public TestWork(string s, string t, DateTime d, string teacher, int q)
            : base(s, t, d)
        {
            Teacher = teacher;
            QuestionsCount = q;
        }

        public override string Describe() =>
            $"Контрольная | {StudentName} | {Topic} | {Date:yyyy.MM.dd} | {Teacher} | Вопросов: {QuestionsCount}";
    }
}

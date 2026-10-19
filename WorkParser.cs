using System;

namespace Lab3
{
    public static class WorkParser
    {
        public static Work Parse(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                throw new WorkFormatException("Строка пустая");

            string[] p = line.Split('|');

            if (p.Length < 4)
                throw new WorkFormatException($"Мало полей: {p.Length}, нужно ≥ 4");

            return Create(p);
        }

        private static Work Create(string[] p)
        {
            string type = p[0].Trim().ToLower();
            switch (type)
            {
                case "lab": return MakeLab(p);
                case "course": return MakeCourse(p);
                case "diploma": return MakeDiploma(p);
                case "test": return MakeTest(p);
                default:
                    throw new WorkFormatException($"Неизвестный тип: {type}");
            }
        }

        private static Work MakeLab(string[] p) =>
            new LabWork(p[1].Trim(), p[2].Trim(), ParseDate(p[3]));

        private static Work MakeCourse(string[] p)
        {
            if (p.Length < 5)
                throw new WorkFormatException("Курсовая: нужно 5 полей");
            return new CourseWork(p[1].Trim(), p[2].Trim(), ParseDate(p[3]), p[4].Trim());
        }

        private static Work MakeDiploma(string[] p)
        {
            if (p.Length < 6)
                throw new WorkFormatException("Диплом: нужно 6 полей");
            return new DiplomaWork(p[1].Trim(), p[2].Trim(),
                ParseDate(p[3]), p[4].Trim(), ParseInt(p[5], "оценка"));
        }

        private static Work MakeTest(string[] p)
        {
            if (p.Length < 6)
                throw new WorkFormatException("Контрольная: нужно 6 полей");
            return new TestWork(p[1].Trim(), p[2].Trim(),
                ParseDate(p[3]), p[4].Trim(), ParseInt(p[5], "количество вопросов"));
        }

        private static DateTime ParseDate(string s)
        {
            if (!DateTime.TryParseExact(s.Trim(), "yyyy.MM.dd", null,
                System.Globalization.DateTimeStyles.None, out DateTime d))
                throw new WorkFormatException($"Неверная дата: {s}");
            return d;
        }

        private static int ParseInt(string s, string field)
        {
            if (!int.TryParse(s.Trim(), out int n))
                throw new WorkFormatException($"Неверное число в поле «{field}»: {s}");
            return n;
        }
    }
}

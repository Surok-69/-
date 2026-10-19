using System.IO;

namespace Lab3
{
    class Program
    {
        static readonly WorkRepository repo = new WorkRepository();
        static readonly ConsoleUI ui = new ConsoleUI();

        static void Main()
        {
            while (true)
            {
                ui.ShowMenu();
                string choice = ui.Ask("Ваш выбор: ");

                if (choice == "7")
                {
                    ShowAll();
                    return;
                }

                Handle(choice);
            }
        }

        static void Handle(string choice)
        {
            switch (choice)
            {
                case "1": AddLab(); break;
                case "2": AddCourse(); break;
                case "3": AddDiploma(); break;
                case "4": AddTest(); break;
                case "5": LoadFromFile(); break;
                case "6": LoadFromString(); break;
                default: ui.Show("Неверный выбор"); break;
            }
        }



        static void AddLab()
        {
            var (s, t, d) = AskCommon();
            repo.Add(new LabWork(s, t, d));
            ui.Show("Добавлено");
        }

        static void AddCourse()
        {
            var (s, t, d) = AskCommon();
            string sup = ui.Ask("Руководитель: ");
            repo.Add(new CourseWork(s, t, d, sup));
            ui.Show("Добавлено");
        }

        static void AddDiploma()
        {
            var (s, t, d) = AskCommon();
            string sup = ui.Ask("Руководитель: ");
            int grade = int.Parse(ui.Ask("Оценка: "));
            repo.Add(new DiplomaWork(s, t, d, sup, grade));
            ui.Show("Добавлено");
        }

        static void AddTest()
        {
            var (s, t, d) = AskCommon();
            string teacher = ui.Ask("Преподаватель: ");
            int q = int.Parse(ui.Ask("Количество вопросов: "));
            repo.Add(new TestWork(s, t, d, teacher, q));
            ui.Show("Добавлено");
        }



        static (string, string, System.DateTime) AskCommon()
        {
            string s = ui.Ask("Студент: ");
            string t = ui.Ask("Тема: ");
            System.DateTime d = System.DateTime.ParseExact(
                ui.Ask("Дата (гггг.мм.дд): ").Trim(), "yyyy.MM.dd", null);
            return (s, t, d);
        }



        static void LoadFromFile()
        {
            string path = ui.Ask("Путь к файлу: ");
            if (!File.Exists(path))
            {
                ui.Show("Файл не найден");
                return;
            }

            foreach (string line in File.ReadAllLines(path))
                TryAdd(line);
        }



        static void LoadFromString()
        {
            ui.Show("Введите строки (пустая — конец):");

            string line;
            while (!string.IsNullOrWhiteSpace(line = ui.Ask("")))
                TryAdd(line);
        }



        static void TryAdd(string line)
        {
            try
            {
                repo.Add(WorkParser.Parse(line));
            }
            catch (WorkFormatException ex)
            {
                ui.Show($"Ошибка: {ex.Message}");
            }
        }


        static void ShowAll()
        {
            ui.Show($"\n=== Всего работ: {repo.Count} ===");
            foreach (Work w in repo.GetAll())
                ui.ShowWork(w);
        }
    }
}


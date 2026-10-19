using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab3;

namespace Lab3.Tests
{
    [TestClass]
    public class WorkParserTests
    {

        [DataTestMethod]
        [DataRow("lab|Иванов|ООП|2024.01.15", typeof(LabWork))]
        [DataRow("course|Петров|БД|2024.02.20|Сидоров", typeof(CourseWork))]
        [DataRow("diploma|Смирнов|МО|2024.03.25|Кузнецов|5", typeof(DiplomaWork))]
        [DataRow("test|Сидорова|Алг|2024.04.10|Орлов|15", typeof(TestWork))]
        public void Parse_ValidLine_ReturnsCorrectType(string line, Type expected)
        {
            Work w = WorkParser.Parse(line);
            Assert.IsInstanceOfType(w, expected);
        }


        [DataTestMethod]
        [DataRow(null)]                                              // null
        [DataRow("")]                                                // пустая
        [DataRow("   ")]                                             // пробелы
        [DataRow("lab|Иванов")]                                      // мало полей
        [DataRow("unknown|A|B|2024.01.01")]                          // неизвестный тип
        [DataRow("lab|Иванов|ООП|не-дата")]                          // битая дата
        [DataRow("diploma|Смирнов|МО|2024.03.25|Кузнецов|abc")]      // битая оценка
        public void Parse_InvalidLine_Throws(string line)
        {
            Assert.ThrowsException<WorkFormatException>(
                () => WorkParser.Parse(line));
        }


        [DataTestMethod]
        [DataRow("lab|Иванов|ООП|2024.01.15", "Иванов")]
        [DataRow("course|Петров|БД|2024.02.20|Сидоров", "Петров")]
        [DataRow("diploma|Смирнов|МО|2024.03.25|Кузнецов|5", "Смирнов")]
        [DataRow("test|Сидорова|Алг|2024.04.10|Орлов|15", "Сидорова")]
        public void Describe_ContainsStudentName(string line, string student)
        {
            Work w = WorkParser.Parse(line);
            StringAssert.Contains(w.Describe(), student);
        }
    }
}

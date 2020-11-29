using Microsoft.Extensions.DependencyInjection;
using Schedule.ClassLibrary;
using ScheduleMaster.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ScheduleMaster.Models
{
    public static class MainModel
    {

        #region Сервисы

        private static IValidatorService Validator { get; set; }
        private static IDataService DataService { get; set; }

        #endregion

        #region Коллекции данных

        public static ObservableCollection<Subject> Subjects { get; set; }
        public static ObservableCollection<Teacher> Teachers { get; set; }
        public static ObservableCollection<Lessons> Lessons { get; set; }
        public static ObservableCollection<Group> Groups { get; set; }

        #endregion
        static MainModel()
        {
            #region Инициализация сервисов

            Validator = App.Host.Services.GetRequiredService<IValidatorService>();
            DataService = App.Host.Services.GetRequiredService<IDataService>();

            #endregion
            #region Инициалиция коллекций данных

            Subjects = new ObservableCollection<Subject>();
            Teachers = new ObservableCollection<Teacher>();
            Lessons = new ObservableCollection<Lessons>();
            Groups = new ObservableCollection<Group>();

            #endregion
        }
        /// <summary>
        /// Проверка на возможность создания объекта типа "Предмет"
        /// </summary>
        /// <param name="name">Имя объекта</param>
        /// <param name="error">Текст ошибки</param>
        /// <returns>Значение true, если объект был успешно создан; в противном случае — значение false.</returns>
        public static bool AddSubject(string name, out string error)
        {
            var subject = new Subject() { Name = name };
            if (!Validator.Validate(subject, out error)) return false;
            Subjects.Add(subject);
            return true;
        }

        /// <summary>
        /// Проверка на возможность создания объекта типа "Преподаватель"
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="patronymic">Отчество</param>
        /// <param name="subjects">Предметы которые преподает</param>
        /// <param name="error">Текст ошибки</param>
        /// <returns>Значение true, если объект был успешно создан; в противном случае — значение false.</returns>
        public static bool AddTeacher(string name, string surname, string patronymic, ICollection<Subject> subjects, out string error)
        {
            var teacher = new Teacher()
            {
                Name = name,
                Surname = surname,
                Patronymic = patronymic,
                Subjects = subjects
            };
            if (!Validator.Validate(teacher, out error)) return false;
            Teachers.Add(teacher);
            return true;
        }

        /// <summary>
        /// Проверка на возможность создания объекта типа "Дисциплина"
        /// </summary>
        /// <param name="teachers">Преподаватели</param>
        /// <param name="subject">Предмет</param>
        /// <param name="count">Количество занятий</param>
        /// <param name="error">Текст ошибки</param>
        /// <returns>Значение true, если объект был успешно создан; в противном случае — значение false.</returns>
        public static bool AddLesson(ICollection<Teacher> teachers, Subject subject, int count, out string error)
        {
            var lesson = new Lessons()
            {
                Teachers = teachers,
                Subject = subject,
                Count = count
            };
            if (!Validator.Validate(lesson, out error)) return false;
            Lessons.Add(lesson);
            return true;
        }

        /// <summary>
        /// Проверка на возможность создания объекта типа "Дисциплина"
        /// </summary>
        /// <param name="name">Имя группы</param>
        /// <param name="lessons">Дисциплины</param>
        /// <param name="error">Текст ошибки</param>
        /// <returns>Значение true, если объект был успешно создан; в противном случае — значение false.</returns>
        public static bool AddGroup(string name, ICollection<Lessons> lessons, out string error)
        {
            var group = new Group()
            {
                Name = name,
                Lessons = lessons
            };
            if (!Validator.Validate(group, out error)) return false;
            Groups.Add(group);
            return true;
        }

        public static void SaveData()
        {
            DataService.SaveAsync(Groups, Environment.CurrentDirectory);
        }

    }
}
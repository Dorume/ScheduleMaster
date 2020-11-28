using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Schedule.DAL.Context;
using Schedule.DAL.Entityes;

namespace ScheduleMaster.Data
{
    public class DbInitializer
    {
        private readonly ScheduleDb _Db;
        private readonly ILogger<DbInitializer> _Logger;

        public DbInitializer(ScheduleDb db, ILogger<DbInitializer> logger)
        {
            _Db = db;
            _Logger = logger;
        }

        public async Task InitializeAsync()
        {
            var timer = Stopwatch.StartNew();
            _Logger.LogInformation("Initialize DB...");
            await _Db.Database.EnsureDeletedAsync().ConfigureAwait(false); //Не забудь
            _Logger.LogInformation("Deleted DB for {0}", timer.ElapsedMilliseconds);

            _Logger.LogInformation("Migrating DB for {0}", timer.ElapsedMilliseconds);
            await _Db.Database.MigrateAsync();
            _Logger.LogInformation("Db migrated for {0}", timer.ElapsedMilliseconds);

            if (await _Db.Subjects.AnyAsync()) return;

            await InitializeAll();

            _Logger.LogInformation("Db initialized for {0}", timer.Elapsed.TotalSeconds);
        }

        private const int SubjectsCount = 10;
        private Subject[] _Subjects;

        private const int TeachersCount = 10;
        private Teacher[] _Teachers;

        private const int LessonsCount = 10;
        private Lesson[] _Lessons;

        private const int GroupsCount = 10;
        private Group[] _Groups;

        private async Task InitializeAll()
        {
            var rnd = new Random();
            _Subjects = Enumerable.Range(1,SubjectsCount).Select(s => new Subject
            {
                Name = $"Subject {s}"
            }).ToArray();
            
            _Teachers = Enumerable.Range(1,TeachersCount).Select(s => new Teacher
            {
                Name = $"TeacherName {s}",
                Surname = $"TeacherSurname {s}",
                Patronymic = $"TeacherPatronymic {s}"
            }).ToArray();

            _Groups = Enumerable.Range(1, GroupsCount).Select(g => new Group
            {
                Name = $"Group {g}"
            }).ToArray();
            _Lessons = Enumerable.Range(1, LessonsCount).Select(l => new Lesson
            {
                Subject = rnd.NextItem(_Subjects),
                Teacher = rnd.NextItem(_Teachers),
                Group = rnd.NextItem(_Groups),
                Count = rnd.Next(9)
            }).ToArray();


            await _Db.Subjects.AddRangeAsync(_Subjects);
            await _Db.Teachers.AddRangeAsync(_Teachers);
            await _Db.Lesson.AddRangeAsync(_Lessons);
            await _Db.Groups.AddRangeAsync(_Groups);
            await _Db.SaveChangesAsync();
        }
    }
}
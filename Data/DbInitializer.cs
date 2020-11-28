using System;
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

            await InitializeSubjects();
            await InitializeLessons();
            await InitializeGroups();

            _Logger.LogInformation("Db initialized for {0}", timer.Elapsed.TotalSeconds);
        }

        private const int SubjectsCount = 10;
        private Subject[] _Subjects;

        private const int TeachersCount = 10;
        private Teacher[] _Teachers;

        private async Task InitializeSubjects()
        {
            var time = Stopwatch.StartNew();
            _Logger.LogInformation("Initializing subjects");
            var rnd = new Random();
            _Subjects = Enumerable.Range(1, SubjectsCount)
                .Select(s => new Subject
                {
                    Name = $"Subject {s}"
                }).ToArray();

            _Teachers = Enumerable.Range(1, TeachersCount)
                .Select(s => new Teacher()
                {
                    Name = $"Teacher {s}"
                }).ToArray();

            foreach (var subject in _Subjects)
            {
                subject.Teachers.Add(rnd.NextItem(_Teachers));
            }

            await _Db.Teachers.AddRangeAsync(_Teachers);
            await _Db.Subjects.AddRangeAsync(_Subjects);
            await _Db.SaveChangesAsync();
            _Logger.LogInformation("Initialized subjects for {0}", time.ElapsedMilliseconds);
        }


        private const int LessonsCount = 10;
        private Lesson[] _Lessons;

        private async Task InitializeLessons()
        {
            Random rnd = new Random();
            var time = Stopwatch.StartNew();
            _Logger.LogInformation("Initializing lessons");
            _Lessons = Enumerable.Range(1, LessonsCount)
                .Select(l => new Lesson
                {
                    Count = rnd.Next(8),
                    Subject = rnd.NextItem(_Subjects),
                    Teacher = rnd.NextItem(_Teachers)
                }).ToArray();

            await _Db.Lessons.AddRangeAsync(_Lessons);
            await _Db.SaveChangesAsync();
            _Logger.LogInformation("Initialized lessons for {0}", time.ElapsedMilliseconds);

        }

        private const int GroupsCount = 10;
        private Group[] _Groups;

        private async Task InitializeGroups()
        {
            var time = Stopwatch.StartNew();
            _Logger.LogInformation("Initializing groups");
            Random rnd = new Random();
            _Groups = Enumerable.Range(1, GroupsCount)
                .Select(l => new Group
                {
                    Lessons = _Lessons
                }).ToArray();

            await _Db.Groups.AddRangeAsync(_Groups);
            await _Db.SaveChangesAsync();
            _Logger.LogInformation("Initialized groups for {0}", time.ElapsedMilliseconds);
        }
    }
}
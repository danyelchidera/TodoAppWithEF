using Data;
using Data.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    public class Tests
    {
        private DbContextOptionsBuilder<AppDbContext> builder;

        [SetUp]
        public void Setup()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            this.builder = new DbContextOptionsBuilder<AppDbContext>();
            this.builder.UseInMemoryDatabase("Test")
                   .UseInternalServiceProvider(serviceProvider);

            
        }


        [Test]
        public async Task CreateTaskShould()
        {
            using (var context = new AppDbContext(builder.Options))
            {
                Data.Models.Task task = new Data.Models.Task("1");

                var sut = new TaskRepository(context);
                var res = await sut.CreateTask(task);

                var count = await context.Tasks.CountAsync();
                Assert.That(res.Id, Is.GreaterThan(0));
                Assert.AreEqual(count, 1);
            }
        }

        [Test]
        public async Task DeleteTaskShould()
        {
            using (var context = new AppDbContext(builder.Options))
            {
                context.Tasks.Add(new Data.Models.Task("1"));
                context.Tasks.Add(new Data.Models.Task("2"));
                await context.SaveChangesAsync();

                var sut = new TaskRepository(context);
                var res = sut.DeleteTaskById(1);
                var count = await context.Tasks.CountAsync();  

                Assert.AreEqual(res.Id, 1);
                Assert.AreEqual(count, 1);
            }
        }

        [Test]
        public async Task GetAllTasksShould()
        {
            using (var context = new AppDbContext(builder.Options))
            {
                context.Tasks.Add(new Data.Models.Task("1"));
                context.Tasks.Add(new Data.Models.Task("2"));
                await context.SaveChangesAsync();

                var sut = new TaskRepository(context);
                var res = sut.GetAllTasks();
                var count = await context.Tasks.CountAsync();

                Assert.AreEqual(count, 2);
            }
        }

        [Test]
        public async Task SearchShould()
        {
            using (var context = new AppDbContext(builder.Options))
            {
                context.Tasks.Add(new Data.Models.Task("14"));
                context.Tasks.Add(new Data.Models.Task("32"));
                context.Tasks.Add(new Data.Models.Task("13"));
                context.Tasks.Add(new Data.Models.Task("27"));
                await context.SaveChangesAsync();

                var sut = new TaskRepository(context);
                var res = await sut.FindTasks("2");
                var count = res.Count;

                Assert.AreEqual(count, 2);
            }
        }

        [Test]
        public async Task DeleteMultpleTaskShould()
        {
            using (var context = new AppDbContext(builder.Options))
            {
                context.Tasks.Add(new Data.Models.Task("14"));
                context.Tasks.Add(new Data.Models.Task("32"));
                context.Tasks.Add(new Data.Models.Task("13"));
                context.Tasks.Add(new Data.Models.Task("27"));
                await context.SaveChangesAsync();

                var ids = new List<int>() { 1, 2 };

                var sut = new TaskRepository(context);

                await sut.DeleteMultpleTasks(ids);
                var count = await context.Tasks.CountAsync();

                Assert.AreEqual(count, 2);
            }
        }

        [Test]
        public async Task DeletesOnlyIdsInDb()
        {
            using (var context = new AppDbContext(builder.Options))
            {
                context.Tasks.Add(new Data.Models.Task("14"));
                context.Tasks.Add(new Data.Models.Task("32"));
                context.Tasks.Add(new Data.Models.Task("13"));
                context.Tasks.Add(new Data.Models.Task("27"));
                await context.SaveChangesAsync();

                var ids = new List<int>() { 1, 8 };

                var sut = new TaskRepository(context);

                await sut.DeleteMultpleTasks(ids);
                var count = await context.Tasks.CountAsync();

                Assert.AreEqual(count, 3);
            }
        }

        [Test]
        public async Task EditTaskShould()
        {
            using (var context = new AppDbContext(builder.Options))
            {
                var task = new Data.Models.Task("14");
                context.Tasks.Add(task);
                await context.SaveChangesAsync();
                context.Entry(task).State = EntityState.Detached;

                task.TodoTask = "15";
               
                var sut = new TaskRepository(context);

                await sut.EditTask(task);
                var testTask = context.Tasks.Find(1);

                Assert.AreEqual(testTask.TodoTask, "15");
            }
        }
    }
}
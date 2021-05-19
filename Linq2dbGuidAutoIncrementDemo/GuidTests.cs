using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace Linq2dbGuidAutoIncrementDemo
{
    public class GuidTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public GuidTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Entities_With_Guid_Can_Be_Copied_Into_Temp_Table()
        {
            var dbFactory = new EfCoreSqliteInMemoryDbFactory();

            var context = dbFactory.CreateDbContext<MainDbContext>(logger: s => _testOutputHelper.WriteLine(s));

            context.AddRange(new List<Person>
            {
                new() {Name = "John Doe"},
                new() {Name = "Jane Doe"}
            });
            context.SaveChanges();

            var people = context.People.ToList();

            var connection = context.CreateLinqToDbConnection();

            var tempTable = connection.CreateTempTable(people, new BulkCopyOptions {KeepIdentity = true});

            tempTable.ToList().Should().BeEquivalentTo(people);
        }
    }
}

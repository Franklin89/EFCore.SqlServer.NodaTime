﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.NodaTime.Extensions;
using NodaTime;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SimplerSoftware.EntityFrameworkCore.SqlServer.NodaTime.Tests
{
    public class LocalDateQueryTests : QueryTestBase
    {
        public LocalDateQueryTests(DatabaseTestFixture databaseTestFixture)
            : base(databaseTestFixture) { }

        [Fact]
        public async Task LocalDate_can_be_used_in_query()
        {
            var raceResults = await this.Db.Race.Where(r => r.Date >= new LocalDate(2019, 7, 1)).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceSelectStatement} WHERE [r].[Date] >= '2019-07-01'"),
                condense(this.Db.Sql));

            Assert.Equal(6, raceResults.Count);
        }

        [Fact]
        public async Task LocalDate_PlusYears()
        {
            var raceResults = await this.Db.Race.Where(r => r.Date.PlusYears(1) >= new LocalDate(2019, 7, 1)).ToListAsync();

            Assert.Equal(
               condense(@$"{RaceSelectStatement} WHERE DATEADD(year, CAST(1 AS int), [r].[Date]) >= '2019-07-01'"),
               condense(this.Db.Sql));

            Assert.Equal(12, raceResults.Count);
        }

        [Fact]
        public async Task LocalDate_PlusMonths()
        {
            var raceResults = await this.Db.Race.Where(r => r.Date.PlusMonths(1) >= new LocalDate(2019, 7, 1)).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceSelectStatement} WHERE DATEADD(month, CAST(1 AS int), [r].[Date]) >= '2019-07-01'"),
                condense(this.Db.Sql));

            Assert.Equal(7, raceResults.Count);
        }

        [Fact]
        public async Task LocalDate_PlusDays()
        {
            var raceResults = await this.Db.Race.Where(r => r.Date.PlusDays(45) >= new LocalDate(2019, 7, 1)).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceSelectStatement} WHERE DATEADD(day, CAST(45 AS int), [r].[Date]) >= '2019-07-01'"),
                condense(this.Db.Sql));

            Assert.Equal(7, raceResults.Count);
        }

        [Fact]
        public async Task LocalDate_PlusWeeks()
        {
            var raceResults = await this.Db.Race.Where(r => r.Date.PlusWeeks(5) >= new LocalDate(2019, 7, 1)).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceSelectStatement} WHERE DATEADD(week, CAST(5 AS int), [r].[Date]) >= '2019-07-01'"),
                condense(this.Db.Sql));

            Assert.Equal(7, raceResults.Count);
        }

        [Fact]
        public async Task LocalDate_PlusQuarters()
        {
            var raceResults = await this.Db.Race.Where(r => r.Date.PlusQuarters(1) >= new LocalDate(2019, 7, 1)).ToListAsync();

            Assert.Equal(
                condense(@$"{RaceSelectStatement} WHERE DATEADD(quarter, CAST(1 AS int), [r].[Date]) >= '2019-07-01'"),
                condense(this.Db.Sql));

            Assert.Equal(9, raceResults.Count);
        }

        [Fact]
        public async Task LocalDate_DatePart_Year()
        {
            var raceResults = await this.Db.Race.Where(r => r.Date.Year == 2019).ToListAsync();
            Assert.Equal(
                condense(@$"{RaceSelectStatement} WHERE DATEPART(year, [r].[Date]) = 2019"),
                condense(this.Db.Sql));

            Assert.Equal(12, raceResults.Count);
        }

        [Fact]
        public async Task LocalDate_DatePart_Quarter()
        {
            var raceResults = await this.Db.Race.Where(r => r.Date.Quarter() == 4).ToListAsync();
            Assert.Equal(
                condense(@$"{RaceSelectStatement} WHERE DATEPART(quarter, [r].[Date]) = 4"),
                condense(this.Db.Sql));

            Assert.Equal(3, raceResults.Count);
        }

        [Fact]
        public async Task LocalDate_DatePart_Month()
        {
            var raceResults = await this.Db.Race.Where(r => r.Date.Month == 12).ToListAsync();
            Assert.Equal(
                condense(@$"{RaceSelectStatement} WHERE DATEPART(month, [r].[Date]) = 12"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task LocalDate_DatePart_DayOfYear()
        {
            var raceResults = await this.Db.Race.Where(r => r.Date.DayOfYear == 1).ToListAsync();
            Assert.Equal(
               condense(@$"{RaceSelectStatement} WHERE DATEPART(dayofyear, [r].[Date]) = 1"),
               condense(this.Db.Sql));

            Assert.Single(raceResults);
        }

        [Fact]
        public async Task LocalDate_DatePart_Day()
        {
            var raceResults = await this.Db.Race.Where(r => r.Date.Day == 1).ToListAsync();
            Assert.Equal(
                condense(@$"{RaceSelectStatement} WHERE DATEPART(day, [r].[Date]) = 1"),
                condense(this.Db.Sql));

            Assert.Equal(12, raceResults.Count);
        }

        [Fact]
        public async Task LocalDate_DatePart_Week()
        {
            var raceResults = await this.Db.Race.Where(r => r.Date.Week() == 1).ToListAsync();
            Assert.Equal(
                condense(@$"{RaceSelectStatement} WHERE DATEPART(week, [r].[Date]) = 1"),
                condense(this.Db.Sql));

            Assert.Single(raceResults);
        }
    }
}

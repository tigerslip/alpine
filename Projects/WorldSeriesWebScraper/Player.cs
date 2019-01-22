using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldSeriesWebScraper
{
    // name, age, country, dob, salary
    class Player
    {
        public string Name { get;}
        public int Age { get; }
        public string Country { get; }
        public DateTime DateOfBirth { get; }
        public decimal? Salary { get; }

        public Player(
            string name,
            int age,
            string country,
            DateTime dateOfBirth,
            decimal? salary)
        {
            Name = name;
            Age = age;
            Country = country;
            DateOfBirth = dateOfBirth;
            Salary = salary;
        }
    }
}

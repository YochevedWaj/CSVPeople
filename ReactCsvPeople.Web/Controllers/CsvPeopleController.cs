using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReactCsvPeople.Data;
using ReactCsvPeople.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactCsvPeople.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CsvPeopleController : ControllerBase
    {
        private readonly string _connectionString;

        public CsvPeopleController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        [HttpGet]
        [Route("getpeople")]
        public List<Person> GetPeople()
        {
            var repo = new PeopleRepository(_connectionString);
            return repo.GetAll();
        }

        [HttpPost]
        [Route("deleteall")]
        public void DeleteAll()
        {
            var repo = new PeopleRepository(_connectionString);
            repo.DeleteAll();
        }

        [HttpPost]
        [Route("upload")]
        public void Upload(UploadViewModel viewModel)
        {
            int index = viewModel.Base64People.IndexOf(",") + 1;
            string base64 = viewModel.Base64People.Substring(index);
            byte[] bytes = Convert.FromBase64String(base64);
            var people = GetfromCsvBytes(bytes);
            var repo = new PeopleRepository(_connectionString);
            repo.AddMultiple(people);
        }


        [HttpGet]
        [Route("generatecsv/{amount}")]
        public IActionResult GenerateCsv(int amount)
        {
            var people = GetPeople(amount);
            var csv = GetCsv(people);
            byte[] bytes = Encoding.UTF8.GetBytes(csv);
            return File(bytes, "text/csv", "people.csv");
        }

        public List<Person> GetfromCsv(string csv)
        {
            var stringReader = new StringReader(csv);
            using var reader = new CsvReader(stringReader, CultureInfo.InvariantCulture);
            return reader.GetRecords<Person>().ToList();
        }
        private string GetCsv(List<Person> people)
        {
            var builder = new StringBuilder();
            var stringWriter = new StringWriter(builder);
            using var csv = new CsvWriter(stringWriter, CultureInfo.InvariantCulture);
            csv.WriteRecords(people);
            return builder.ToString();
        }

        static List<Person> GetfromCsvBytes(byte[] csvBytes)
        {
            using var memoryStream = new MemoryStream(csvBytes);
            var streamReader = new StreamReader(memoryStream);
            using var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            return reader.GetRecords<Person>().ToList();
        }
        private List<Person> GetPeople(int count)
        {
        
            return Enumerable.Range(1, count).Select(_ =>
            {
                return new Person
                {
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    Age = Faker.RandomNumber.Next(20, 100),
                    Email = Faker.Internet.Email(),
                    Address = Faker.Address.StreetAddress()
                };
            }).ToList();
        }
    }
}

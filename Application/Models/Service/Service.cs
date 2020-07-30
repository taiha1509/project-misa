using Application.Mpdels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Application.Models.Service
{
    public class Service
    {
        private readonly string _dataFile = @"Data\data.xml";
        private readonly XmlSerializer _serializer = new XmlSerializer(typeof(HashSet<Book>));
        public HashSet<Brochure> Books { get; set; }

        public Service()
        {
            if (!File.Exists(_dataFile))
            {
                Books = new HashSet<Brochure>() {
                    new Brochure{Id = 1, Name = "ASP.NET Core for dummy", Authors = "Trump D.", Publisher = "Washington", Year = 2020},
                    new Brochure{Id = 2, Name = "Pro ASP.NET Core", Authors = "Putin V.", Publisher = "Moscow", Year = 2020},
                    new Brochure{Id = 3, Name = "ASP.NET Core Video course", Authors = "Obama B.", Publisher = "Washington", Year = 2020},
                    new Brochure{Id = 4, Name = "Programming ASP.NET Core MVC", Authors = "Clinton B.", Publisher = "Washington", Year = 2020},
                    new Brochure{Id = 5, Name = "ASP.NET Core Razor Pages", Authors = "Yelstin B.", Publisher = "Moscow", Year = 2020},
                };
            }
            else
            {
                using var stream = File.OpenRead(_dataFile);
                Books = _serializer.Deserialize(stream) as HashSet<Brochure>;
            }
        }

        public Brochure[] Get() => Books.ToArray();

        public Brochure Get(int id) => Books.FirstOrDefault(b => b.Id == id);

        public bool Add(Brochure book) => Books.Add(book);

        public Brochure Create()
        {
            var max = Books.Max(b => b.Id);
            var b = new Brochure()
            {
                Id = max + 1,
                Year = DateTime.Now.Year
            };
            return b;
        }

        public bool Update(Brochure book)
        {
            var b = Get(book.Id);
            return b != null && Books.Remove(b) && Books.Add(book);
        }

        public bool Delete(int id)
        {
            var b = Get(id);
            return b != null && Books.Remove(b);
        }

        public void SaveChanges()
        {
            using var stream = File.Create(_dataFile);
            _serializer.Serialize(stream, Books);
        }
    }
}
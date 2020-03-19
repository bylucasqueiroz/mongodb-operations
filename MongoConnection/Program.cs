using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Linq.Expressions;

namespace MongoConnection
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "mongodb+srv://<login>:<password@<cluster>.mongodb.net/<database>?retryWrites=true&w=majority";
            IMongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase("application");
            IMongoCollection<Infos> colInfos = database.GetCollection<Infos>("infos");

            Console.WriteLine("Insert? (i)");

            string insert = Console.ReadLine();

            if (insert == "i")
            {
                Infos doc = new Infos();
                doc.Name = "Lucas";
                doc.Surname = "Queiroz";
                doc.Date = DateTime.Now;
                doc.Age = 24;
                doc.Height = 1.85;
                Console.WriteLine(colInfos.Database.DatabaseNamespace);
                colInfos.InsertOne(doc);
            }

            Console.WriteLine("Filter? (f)");

            string filter = Console.ReadLine();

            if (filter == "f")
            {
                Console.WriteLine("ID:");

                string id = Console.ReadLine();

                if (id.Trim() != null)
                {
                    Expression<Func<Infos, bool>> filterResponse  = x => x.Id.Equals(ObjectId.Parse(id));

                    Infos news = colInfos.Find(filterResponse).FirstOrDefault();

                    Console.WriteLine(news.ToJson());
                }
            }

            Console.WriteLine("Update? (u)");

            string update = Console.ReadLine();

            if (update == "u")
            {
                Console.WriteLine("Name to update:");

                string name = Console.ReadLine();

                Expression<Func<Infos, bool>> filterResponse = x => x.Name.Equals(name);
                Infos info = colInfos.Find(filterResponse).FirstOrDefault();

                info.Name = "John";
                info.Surname = "Due";
                info.Date = DateTime.Now;
                info.Age = 40;
                info.Height = 1.95;

                ReplaceOneResult result = colInfos.ReplaceOne(filterResponse, info);
            }

            Console.WriteLine("Get all? (g)");

            string getAll = Console.ReadLine();

            if (getAll == "g")
            {
                IMongoQueryable<Infos> info = colInfos.AsQueryable().OrderBy(x => x.Name);
                info.ForEachAsync(x => Console.WriteLine(x.ToJson()));
            }

            Console.WriteLine("Delete? (d)");

            string delete = Console.ReadLine();

            if (delete == "d")
            {
                Console.WriteLine("ID:");

                string id = Console.ReadLine();


                if (id != null)
                {
                    Expression<Func<Infos, bool>> filterDelete = x => x.Id.Equals(ObjectId.Parse(id));
                    DeleteResult delresult = colInfos.DeleteOne(filterDelete);
                }
            }

            //https://medium.com/@fulviocanducci/mongodb-opera%C3%A7%C3%B5es-crud-com-c-2af4e77c046
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneBook
{
    class Program
    {
        static void Main(string[] args)
        {
            PhoneBook pb = new PhoneBook("phonebook.xml");

            PhoneBook.Entry e = pb.GetByName("tomas");
            if(e != null)
            {
                Console.WriteLine("{0} {1} {2}", e.Name, e.Phone, e.Type);
            }

            pb.InsertOrUpdate(new PhoneBook.Entry { Name = "itai", Phone = "543543543", Type = "woki-toki" });

            //   pb.InsertOrUpdate(new PhoneBook.Entry { Name = "itai", Phone = "432423", Type = "private" });
            ////   pb.InsertOrUpdate(new PhoneBook.Entry { Name = "tomas", Phone = "54354353", Type = "private" });
            IEnumerable<PhoneBook.Entry> contacts = pb.Iterate();

            foreach (PhoneBook.Entry contact in contacts)
            {
                Console.WriteLine("{0} {1} {2}", contact.Name, contact.Phone, contact.Type);
            }

            //PhoneBook.Entry res = pb.GetByName("tomer");
            //if(res != null)
            //{
            //    Console.WriteLine(res.Phone);
            //}

        }
    }
}

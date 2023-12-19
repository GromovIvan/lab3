using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lab3
{
    public class NoteBook
    {
        private List<Contact> _contacts = new List<Contact>();

        public void ViewAllContacts()
        {
            Console.WriteLine("Все контакты:");

            for (int i = 0; i < _contacts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Имя: {_contacts[i].Name}\n   " +
                                  $"Фамили: {_contacts[i].Surname}\n   " +
                                  $"Телефон: {_contacts[i].Phone}\n   " +
                                  $"E-mail: {_contacts[i].Email}\n");
            }
        }

        public void AddNewContact()
        {

            Contact contact = new Contact();

            Console.WriteLine("Новый контакт");
            Console.Write("Имя: ");
            contact.Name = Console.ReadLine().ToString();

            Console.Write("Фамилия: ");
            contact.Surname = Console.ReadLine().ToString();

            Console.Write("Телефон: ");
            contact.Phone = Console.ReadLine().ToString();

            Console.Write("E-mail: ");
            contact.Email = Console.ReadLine().ToString();


            _contacts.Add(contact);

            Console.WriteLine("Контакт создан.");
        }

        public void AddNewContact(Contact contact)
        {
            _contacts.Add(contact);
        }

        public void SearchContacts(int searchOption)
        {
            Console.Write("Введите поисковые данные: ");
            string searchString = Console.ReadLine();

            Console.WriteLine("Поиск…");
            Console.WriteLine("Результаты:");

            switch (searchOption)
            {
                case 1:
                    SearchAndDisplay(contact => contact.Name.Contains(searchString));
                    break;
                case 2:
                    SearchAndDisplay(contact => contact.Surname.Contains(searchString));
                    break;
                case 3:
                    SearchAndDisplay(contact => (contact.Name + " " + contact.Surname).Contains(searchString));
                    break;
                case 4:
                    SearchAndDisplay(contact => contact.Phone.Contains(searchString));
                    break;
                case 5:
                    SearchAndDisplay(contact => contact.Email.Contains(searchString));
                    break;
                default:
                    Console.WriteLine("Некорректные Поисковые данные.");
                    break;
            }
        }

        private void SearchAndDisplay(Func<Contact, bool> predicate)
        {
            int resultCount = 0;
            for (int i = 0; i < _contacts.Count; i++)
            {
                if (predicate(_contacts[i]))
                {
                    resultCount++;
                    Console.WriteLine($"#{resultCount}  Имя: {_contacts[i].Name}\n   " +
                                        $"Фамилия: {_contacts[i].Surname}\n   " +
                                        $"Телефон: {_contacts[i].Phone}\n   " +
                                        $"E-mail: {_contacts[i].Email}\n");
                }
            }

            if (resultCount == 0)
            {
                Console.WriteLine("Ничего не найдено.");
            }
        }

        public void ExportToSql()
        {
            using (var dbContext = new AppDbContext())
            {
                var allContacts = dbContext.Contacts.ToList();
                dbContext.Contacts.RemoveRange(allContacts);
                dbContext.SaveChanges();

                dbContext.Contacts.AddRange(_contacts);
                dbContext.SaveChanges();
            }
        }
        public void ExportToJson()
        {
            string jsonString = JsonSerializer.Serialize(_contacts);
            File.WriteAllText("Contacts.json", jsonString);
            Console.WriteLine("Данные успешно записаны в json");
        }
        public void ExportToXml()
        {
            XmlSerializer mySerializer = new XmlSerializer(typeof(List<Contact>));
            StreamWriter myWriter = new StreamWriter("Contacts.xml");
            mySerializer.Serialize(myWriter, _contacts);
            myWriter.Close();
            Console.WriteLine("Данные успешно записаны в xml");
        }
        public void ImportFromJson()
        {
            string jsonString = File.ReadAllText("Contacts.json");
            _contacts = JsonSerializer.Deserialize<List<Contact>>(jsonString);
            Console.WriteLine("Данные успешно считаны из файла JSON: Contacts.json");
        }
        public void ImportFromXml()
        {
            XmlSerializer mySerializer = new XmlSerializer(typeof(List<Contact>));
            using var myFileStream = new FileStream("Contacts.xml", FileMode.Open);
            _contacts = (List<Contact>)mySerializer.Deserialize(myFileStream);
            Console.WriteLine("Данные успешно считаны из файла XML: Contacts.xml");
        }
        public void ImportFromSql()
        {
            using (var dbContext = new AppDbContext())
            {
                _contacts = dbContext.Contacts.ToList();

            }
            Console.WriteLine("Данные успешно считаны из бд SQLite");
        }

        public List<Contact> GetAllContacts()
        {
            return _contacts;
        }
    }
}

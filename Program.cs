namespace lab3
{
    internal class Program
    {
        static NoteBook notebook = new NoteBook();

        static void Main(string[] args)
        {
            int choice;
            do
            {
                ShowMenu();
                choice = GetUserChoice();

                switch (choice)
                {
                    case 1:
                        notebook.ViewAllContacts();
                        break;
                    case 2:
                        SearchOptions();
                        break;
                    case 3:
                        notebook.AddNewContact();
                        break;
                    case 4:
                        notebook.ExportToJson();
                        break;
                    case 5:
                        notebook.ExportToXml();
                        break;
                    case 6:
                        notebook.ExportToSql();
                        break;
                    case 7:
                        notebook.ImportFromJson();
                        break;
                    case 8:
                        notebook.ImportFromXml();
                        break;
                    case 9:
                        notebook.ImportFromSql();
                        break;
                    case 10:
                        Console.WriteLine("Ожидайте...");
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                        break;
                }

            } while (choice != 10);
        }

        static void ShowMenu()
        {
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Посмотреть все контакты");
            Console.WriteLine("2. Поиск");
            Console.WriteLine("3. Создать новый контакт");
            Console.WriteLine("4. Экспортировать в JSON");
            Console.WriteLine("5. Экспортировать в XML");
            Console.WriteLine("6. Экспортировать в SQLite");
            Console.WriteLine("7. Импортировать из JSON");
            Console.WriteLine("8. Импортировать из XML");
            Console.WriteLine("9. Импортировать из SQLite");
            Console.WriteLine("10. Выход");
            Console.Write("> ");
        }

        static int GetUserChoice()
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Некорректный ввод. Введите число.");
                Console.Write("> ");
            }
            return choice;
        }

        static void SearchOptions()
        {
            Console.WriteLine("Поиск по:");
            Console.WriteLine("1. Имени");
            Console.WriteLine("2. Фамилии");
            Console.WriteLine("3. Имени и фамилии");
            Console.WriteLine("4. Телефону");
            Console.WriteLine("5. E-mail");
            Console.Write("> ");

            notebook.SearchContacts(GetUserChoice());
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;

class Contact
{
    public string Name { get; set; }
    public string Phone { get; set; }

    public override string ToString()
    {
        return $"{Name} - {Phone}";
    }
}

class Program
{
    private static string filePath = "contacts.txt";
    private static List<Contact> contacts = new List<Contact>();

    static void Main(string[] args)
    {
        LoadContacts();

        while (true)
        {
            Console.WriteLine("\nПриложение для управления контактами");
            Console.WriteLine("1. Добавить контакт");
            Console.WriteLine("2. Просмотреть контакты");
            Console.WriteLine("3. Удалить контакт");
            Console.WriteLine("4. Выйти");
            Console.Write("Выберите опцию: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddContact();
                    break;
                case "2":
                    ViewContacts();
                    break;
                case "3":
                    DeleteContact();
                    break;
                case "4":
                    SaveContacts();
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте снова.");
                    break;
            }
        }
    }

    static void AddContact()
    {
        Console.Write("Введите имя контакта: ");
        var name = Console.ReadLine();
        Console.Write("Введите телефон контакта: ");
        var phone = Console.ReadLine();

        contacts.Add(new Contact { Name = name, Phone = phone });
        Console.WriteLine("Контакт добавлен!");
    }

    static void ViewContacts()
    {
        Console.WriteLine("\nВаши контакты:");
        foreach (var contact in contacts)
        {
            Console.WriteLine(contact);
        }
    }

    static void DeleteContact()
    {
        Console.Write("Введите имя контакта для удаления: ");
        var name = Console.ReadLine();
        var contactToRemove = contacts.Find(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (contactToRemove != null)
        {
            contacts.Remove(contactToRemove);
            Console.WriteLine("Контакт удален!");
        }
        else
        {
            Console.WriteLine("Контакт не найден.");
        }
    }

    static void LoadContacts()
    {
        if (!File.Exists(filePath))
        {
            // Создать файл, если он не существует
            File.Create(filePath).Close(); // Закрыть файл сразу после создания
            Console.WriteLine("Файл контактов не найден. Создан новый файл.");
            return; // Выход из метода, так как файл только что был создан
        }

        var lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            var parts = line.Split('-');
            if (parts.Length == 2)
            {
                contacts.Add(new Contact { Name = parts[0].Trim(), Phone = parts[1].Trim() });
            }
        }
    }

    static void SaveContacts()
    {
        var lines = new List<string>();
        foreach (var contact in contacts)
        {
            lines.Add(contact.ToString());
        }
        File.WriteAllLines(filePath, lines);
    }
}

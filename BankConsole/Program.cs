using BankConsole;

if (args.Length == 0)
{
    EmailService.SendMail();
}
else
{
    ShowMenu();
}


void ShowMenu()
{
    Console.Clear();
    Console.WriteLine("Seleccione una opción");
    Console.WriteLine("1. Crear un Usuario nuevo.");
    Console.WriteLine("2. Eliminar un usuario existente.");
    Console.WriteLine("3. Salir");

    int option = 0;
    do
    {
        string input = Console.ReadLine();

        if (!int.TryParse(input, out option))
            Console.WriteLine("Debes ingresar un número (1, 2 o 3). ");
        else if (option > 3)
            Console.WriteLine("Debes ingresar un número válido (1, 2 o 3).");

    } while (option == 0 || option > 3);

    switch (option)
    {
        case 1:
            CreateUser();
            break;
        case 2:
            DeleteUser();
            break;
        case 3:
            Environment.Exit(0);
            break;
    }



}
void CreateUser()
{
    Console.Clear();
    Console.WriteLine("Ingresa la información del usuario: ");
    Console.Write("ID: ");
    int ID = int.Parse(Console.ReadLine());
    string userSearch = Storage.SearchUser(ID);
    do
    {
        if (ID < 0) 
        {
            Console.Write("El ID debe de ser un número entero positivo.\n ID: ");
            ID = int.Parse(Console.ReadLine());
            userSearch = Storage.SearchUser(ID);
        }
        else if (userSearch.Equals("User found"))
        {
            Console.Write("Ya existe algún usuario con ese ID.\n ID: ");
            ID = int.Parse(Console.ReadLine());
            userSearch = Storage.SearchUser(ID);
        }
        else if (ID < 0 && userSearch.Equals("User found"))
        {
            Console.Write("Ya existe usuario con ese ID o el ID debe de ser entero positivo.");
            ID = int.Parse(Console.ReadLine());
            userSearch = Storage.SearchUser(ID);
        }
    } while (ID < 0  || userSearch.Equals("User found"));

    Console.Write("Nombre: ");
    string name = Console.ReadLine();

    Console.Write("Email: ");
    string email = Console.ReadLine();
    var result = User.IsValidEmail(email);
    while (result == false)
    {
        Console.Write("Ingrese un Email válido.\n Email: ");
        email = Console.ReadLine();
        result = User.IsValidEmail(email);
    }

    Console.Write("Saldo: ");
    decimal balance = decimal.Parse(Console.ReadLine());
    string isDecimal = User.IsDecimalBalance(balance);
    while (balance < 0 || !isDecimal.Equals("Is decimal"))
    {
        Console.Write("Saldo debe ser decimal positivo.\n Saldo: ");
        balance = decimal.Parse(Console.ReadLine());
    }

    Console.Write("Escribe 'c' si el usuario es Cliente, 'e' si es Empleado: ");
    char userType = char.Parse(Console.ReadLine());
    while (!userType.Equals('c') && !userType.Equals('e'))
    {
        Console.Write("Dato no válido\n Ingresa 'c' si el usuario es Cliente, 'e' si es Empleado: ");
        userType = char.Parse(Console.ReadLine());
    }

    User newUser;

    if (userType.Equals('c'))
    {
        Console.Write("Regimen Fiscal: ");
        char TaxRegime = char.Parse(Console.ReadLine());

        newUser = new Client(ID, name, email, balance, TaxRegime);
    }
    else
    {
        Console.Write("Departamentento: ");
        string department = Console.ReadLine();

        newUser = new Employee(ID, name, email, balance, department);
    }

    Storage.AddUser(newUser);


    Console.WriteLine("Usuario creado.");
    Thread.Sleep(2000);
    ShowMenu();
}

void DeleteUser()
{
    Console.Clear();

    Console.Write("Ingresa el ID del usuario a eliminar: ");
    int ID = int.Parse(Console.ReadLine());
    string userSearch = Storage.SearchUser(ID);
do
{
     if (ID < 0)
        {
            Console.Write("El ID debe de ser un número entero positivo.\n ID: ");
            ID = int.Parse(Console.ReadLine());
            userSearch = Storage.SearchUser(ID);
        }
        else if (userSearch.Equals("User not found"))
        {
            Console.Write("No existe algún usuario con ese ID.\n ID: ");
            ID = int.Parse(Console.ReadLine());
            userSearch = Storage.SearchUser(ID);
        }
        else if (ID < 0 && userSearch.Equals("User not found"))
        {
            Console.Write("No existe usuario con ese ID o el ID debe de ser entero positivo.\n ID: ");
            ID = int.Parse(Console.ReadLine());
            userSearch = Storage.SearchUser(ID);
        }
} while ((ID < 0 || userSearch.Equals("User not found")));


 string result = Storage.DeleteUser(ID);

   
    if (result.Equals("Success") && userSearch.Equals("User found"))
    {
        Console.Write("Usuario eliminado.");
        Thread.Sleep(2000);
        ShowMenu();
    }

}
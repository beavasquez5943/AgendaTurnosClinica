using System;
namespace AgendaTurnosClinica
{
    struct Paciente
    {
        public string Nombre;
        public string Cedula;
        public int Edad;

        public Paciente(string nombre, string cedula, int edad)
        {
            Nombre = nombre;
            Cedula = cedula;
            Edad = edad;
        }
    }

    class Turno
    {
        public Paciente Paciente;
        public string Fecha; // formato: "YYYY-MM-DD"
        public string Hora;  // formato: "HH:mm"
        public string Medico;

        public Turno(Paciente paciente, string fecha, string hora, string medico)
        {
            Paciente = paciente;
            Fecha = fecha;
            Hora = hora;
            Medico = medico;
        }

        public void Mostrar()
        {
            Console.WriteLine("Turno:");
            Console.WriteLine($"Paciente: {Paciente.Nombre} - Cédula: {Paciente.Cedula} - Edad: {Paciente.Edad}");
            Console.WriteLine($"Fecha: {Fecha} - Hora: {Hora} - Médico: {Medico}");
            Console.WriteLine("------------------------------------");
        }
    }

    class Programa
    {
        static Turno[] turnos = new Turno[100]; // vector de turnos
        static int contador = 0;

        static void Main()
        {
            int opcion;
            do
            {
                Console.WriteLine("\n--- Agenda de Turnos de la Clínica ---");
                Console.WriteLine("1. Agendar turno");
                Console.WriteLine("2. Consultar turnos por paciente");
                Console.WriteLine("3. Mostrar todos los turnos");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        AgendarTurno();
                        break;
                    case 2:
                        ConsultarTurnos();
                        break;
                    case 3:
                        MostrarTodos();
                        break;
                }

            } while (opcion != 0);
        }

        static void AgendarTurno()
        {
            Console.Write("Nombre del paciente: ");
            string nombre = Console.ReadLine();
            Console.Write("Cédula: ");
            string cedula = Console.ReadLine();
            Console.Write("Edad: ");
            int edad = int.Parse(Console.ReadLine());

            Console.Write("Fecha del turno (YYYY-MM-DD): ");
            string fecha = Console.ReadLine();
            Console.Write("Hora del turno (HH:mm): ");
            string hora = Console.ReadLine();
            Console.Write("Nombre del médico: ");
            string medico = Console.ReadLine();

            // Validar duplicado
            for (int i = 0; i < contador; i++)
            {
                if (turnos[i].Paciente.Cedula == cedula &&
                    turnos[i].Fecha == fecha &&
                    turnos[i].Hora == hora)
                {
                    Console.WriteLine("⚠️ Ya existe un turno asignado para este paciente en esa fecha y hora.");
                    return;
                }
            }

            Paciente paciente = new Paciente(nombre, cedula, edad);
            Turno nuevoTurno = new Turno(paciente, fecha, hora, medico);
            turnos[contador++] = nuevoTurno;

            Console.WriteLine("✅ Turno agendado correctamente.");
        }

        static void ConsultarTurnos()
        {
            Console.Write("Ingrese el nombre del paciente: ");
            string nombre = Console.ReadLine();
            bool encontrado = false;

            for (int i = 0; i < contador; i++)
            {
                if (turnos[i].Paciente.Nombre.ToLower() == nombre.ToLower())
                {
                    turnos[i].Mostrar();
                    encontrado = true;
                }
            }

            if (!encontrado)
                Console.WriteLine("❌ No se encontraron turnos para ese paciente.");
        }

        static void MostrarTodos()
        {
            Console.WriteLine("\n--- Lista de todos los turnos ---");
            if (contador == 0)
            {
                Console.WriteLine("No hay turnos registrados.");
            }
            else
            {
                for (int i = 0; i < contador; i++)
                {
                    turnos[i].Mostrar();
                }
            }
        }
    }
}

using System;
using System.Linq;

namespace Empresa
{
    // Definición de la enumeración para los cargos
    public enum Cargos
    {
        Auxiliar,
        Administrativo,
        Ingeniero,
        Especialista,
        Investigador
    }

    public class Empleado
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public char EstadoCivil { get; set; }
        public DateTime FechaIngreso { get; set; }
        public double SueldoBasico { get; set; }
        public Cargos Cargo { get; set; }

        // Método para calcular la antigüedad en la empresa
        public int CalcularAntiguedad()
        {
            return DateTime.Now.Year - FechaIngreso.Year;
        }

        // Método para calcular la edad del empleado
        public int CalcularEdad()
        {
            return DateTime.Now.Year - FechaNacimiento.Year;
        }

        // Método para calcular los años restantes para la jubilación
        public int CalcularAniosParaJubilacion()
        {
            const int EdadJubilacion = 65;
            return EdadJubilacion - CalcularEdad();
        }

        // Método para calcular el salario del empleado
        public double CalcularSalario()
        {
            int antiguedad = CalcularAntiguedad();
            double adicional = 0;

            if (antiguedad <= 20)
            {
                adicional = SueldoBasico * (0.01 * antiguedad);
            }
            else
            {
                adicional = SueldoBasico * 0.25;
            }

            if (Cargo == Cargos.Ingeniero || Cargo == Cargos.Especialista)
            {
                adicional *= 1.5;
            }

            if (EstadoCivil == 'C')
            {
                adicional += 150000;
            }

            return SueldoBasico + adicional;
        }

        public override string ToString()
        {
            return $"{Nombre} {Apellido}, Edad: {CalcularEdad()}, Antigüedad: {CalcularAntiguedad()} años, Salario: {CalcularSalario():C}, Años para jubilación: {CalcularAniosParaJubilacion()}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Crear un arreglo de empleados y cargar datos para 3 empleados
            Empleado[] empleados = new Empleado[3];

            empleados[0] = new Empleado
            {
                Nombre = "Juan",
                Apellido = "Pérez",
                FechaNacimiento = new DateTime(1980, 5, 20),
                EstadoCivil = 'C',
                FechaIngreso = new DateTime(2005, 4, 15),
                SueldoBasico = 650000,
                Cargo = Cargos.Ingeniero
            };

            empleados[1] = new Empleado
            {
                Nombre = "Ana",
                Apellido = "Gómez",
                FechaNacimiento = new DateTime(1990, 10, 30),
                EstadoCivil = 'S',
                FechaIngreso = new DateTime(2010, 6, 20),
                SueldoBasico = 550000,
                Cargo = Cargos.Administrativo
            };

            empleados[2] = new Empleado
            {
                Nombre = "Luis",
                Apellido = "Martínez",
                FechaNacimiento = new DateTime(1975, 2, 14),
                EstadoCivil = 'C',
                FechaIngreso = new DateTime(2000, 8, 1),
                SueldoBasico = 750000,
                Cargo = Cargos.Especialista
            };

            // Calcular el monto total de salarios
            double montoTotalSalarios = empleados.Sum(e => e.CalcularSalario());
            Console.WriteLine($"Monto Total en Salarios: {montoTotalSalarios:C}");

            // Encontrar al empleado más próximo a jubilarse
            Empleado proximoAJubilarse = empleados.OrderBy(e => e.CalcularAniosParaJubilacion()).First();
            Console.WriteLine("\nEmpleado más próximo a jubilarse:");
            Console.WriteLine(proximoAJubilarse);
        }
    }
}

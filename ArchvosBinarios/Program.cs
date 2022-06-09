using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArchvosBinarios
{
    class Program
    {
        class ArchivoBinarioEmpleados
        {


            // Declaracion de flujos 
            BinaryWriter bw = null;
            BinaryReader br = null;

            // Campos de la clase
            string nombre, direccion;
            long telefono;
            int numEmp, diasTrabajados;
            float salarioDiario, salario;

            public void CrearArchivo(string archivo)
            {
                char resp;

                try
                {
                    // Creacion del flujo para escribir datos al archivo
                    bw = new BinaryWriter(new FileStream(archivo, FileMode.Create, FileAccess.Write));

                    // Captura de datos
                    do
                    {
                        Console.Clear();
                        Console.Write("Numero del Empleado: ");
                        numEmp = Int32.Parse(Console.ReadLine());
                        Console.Write("Nombre del Empleado: ");
                        nombre = Console.ReadLine();
                        Console.Write("Direccion del Empleado: ");
                        direccion = Console.ReadLine();
                        Console.Write("Telefono del Empleado: ");
                        telefono = Int64.Parse(Console.ReadLine());
                        Console.Write("Dias Trabajados del empleado: ");
                        diasTrabajados = Int32.Parse(Console.ReadLine());
                        Console.Write("Salario Diario del Empleado: ");
                        salarioDiario = Single.Parse(Console.ReadLine());

                        //Escribe los datos en el archivo
                        bw.Write(numEmp);
                        bw.Write(nombre);
                        bw.Write(direccion);
                        bw.Write(telefono);
                        bw.Write(diasTrabajados);
                        bw.Write(salarioDiario);

                        Console.Write("\n \n Deseas almacenar otro registro (s/n) ");
                        resp = char.Parse(Console.ReadLine());
                    } while ((resp == 's') || (resp == 'S'));
                }
                catch(IOException e)
                {
                    Console.WriteLine("\nError: " + e.Message);
                    Console.WriteLine("\nRuta: " + e.StackTrace);
                }
                finally
                {
                    if (bw != null)
                    {
                        bw.Close();
                    }
                    Console.WriteLine("Presione enter para terminar de escribir ");
                    Console.ReadKey();
                }

            }
            public void MostrarArchivo(string archivo)
            {
                try
                {
                    // Verifica si existe el archivo
                    if(File.Exists(archivo))
                    {
                        // Creacion dekl flujo para leer datos del archivo
                        br = new BinaryReader(new FileStream(archivo, FileMode.Open, FileAccess.Read));

                        //Despliegue de datos
                        Console.Clear();
                        do
                        {
                            // Lectura de registros mientras no llegue a EndOfFile
                            numEmp = br.ReadInt32();
                            nombre = br.ReadString();
                            direccion = br.ReadString();
                            telefono = br.ReadInt64();
                            diasTrabajados = br.ReadInt32();
                            salarioDiario = br.ReadSingle();

                            //Muestra los datos
                            Console.WriteLine("Numero del Empleado: " + numEmp);
                            Console.WriteLine("Nombre del Empleado: " + nombre);
                            Console.WriteLine("Direccion del Empleado: " + direccion);
                            Console.WriteLine("Telefono del Empleado: " + telefono);
                            Console.WriteLine("Dias Trabajados: " + diasTrabajados);
                            Console.WriteLine("Salario Diario del Empleado: {0:C} ", salarioDiario);
                            Console.WriteLine("Sueldo total del Empleado: {0:C} ", (diasTrabajados * salarioDiario));
                        } while (true);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\n\nEl Archivo " + archivo + "no Existe");
                        Console.WriteLine("Presione enter para continuar");
                        Console.ReadKey();
                    }
                }
                catch(EndOfStreamException )
                {
                    Console.WriteLine("\n\nFin del Listado de Empleados");
                    Console.WriteLine("Presione enter para continuar");
                    Console.ReadKey();
                }
                finally
                {
                    if (br != null)
                    {
                        br.Close();
                    }
                    Console.WriteLine("Presione enter para continuar");
                    Console.ReadKey();
                }
            }
        }
        static void Main(string[] args)
        {
            //declaración variables auxiliares
            string Arch = null;
            int opcion;
            //creación del objeto
            ArchivoBinarioEmpleados A1 = new ArchivoBinarioEmpleados();
            //Menu de Opciones
            do
            {
                Console.Clear();
                Console.WriteLine("\n*** ARCHIVO BINARIO EMPLEADOS***");
                Console.WriteLine("1.- Creación de un Archivo.");
                Console.WriteLine("2.- Lectura de un Archivo.");
                Console.WriteLine("3.- Salida del Programa.");
                Console.Write("\nQue opción deseas: ");
                opcion = Int16.Parse(Console.ReadLine());
                switch (opcion)
                {
                    case 1:
                        //bloque de escritura

                        try
                        {
                            //captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo a Crear: "); Arch = Console.ReadLine();
                            
//verifica si esxiste el archivo
                            char resp = 's';
                            if (File.Exists(Arch))
                            {
                                Console.Write("\nEl Archivo Existe!!, Deseas Sobreescribirlo(s / n) ? ");
                                

                                resp = Char.Parse(Console.ReadLine());
                            }
                            if ((resp == 's') || (resp == 'S'))
                                A1.CrearArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError : " + e.Message);
                            Console.WriteLine("\nRuta : " + e.StackTrace);
                        }
                        break;
                    case 2:
                        //bloque de lectura
                        try
                        {
                            //captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo  que deseas Leer: "); Arch = Console.ReadLine();
                            A1.MostrarArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError : " + e.Message);
                            Console.WriteLine("\nRuta : " + e.StackTrace);
                        }
                        break;
                    case 3:
                        Console.Write("\nPresione <enter> para Salir del Programa.");
                        

                        Console.ReadKey();
                        break;
                    default:
                        Console.Write("\nEsa Opción No Existe!!, Presione< enter > para Continuar...");
                        Console.ReadKey();
                        break;
                }
            } while (opcion != 3);
        }
    }
}

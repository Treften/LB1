using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Threading;
using NLog;

namespace LB1
{
    class Server
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static ManualResetEvent allDone = new ManualResetEvent(false);
        private UdpClient udpClient_S;
        private int port;
        Controller controller;
        byte[] mes;
        string prog = "";
        string help = "\n1 -- Вывести все записи\n2 -- Вывести по индексу\n3 -- Добавить запись\n4 -- Удалить запись";
        string name = "";
        string surname = "";
        string addres = "";
        int age = 0;
        bool family=false;
        bool error = false;
        public Server(int _port)
        {
            udpClient_S = new UdpClient(_port);
            this.port = _port;
            controller = new Controller();
            Console.WriteLine("Сервер работает");
            logger.Debug("Server is up");
        }
        public void StartListenAsync()
        {
            logger.Error("Server is listen");
            while (true)
            {
                allDone.Reset();
                udpClient_S.BeginReceive(RequestCallback, udpClient_S);
                allDone.WaitOne();
            }
        }
        private void RequestCallback(IAsyncResult ar)
        {

            allDone.Set();
            var listener = (UdpClient)ar.AsyncState;
            var ep = (IPEndPoint)udpClient_S.Client.LocalEndPoint;
            var res = listener.EndReceive(ar, ref ep);
            string data = Encoding.Unicode.GetString(res);
            logger.Debug("Server is up");
            switch (prog)
            {
                default:
                    if (!error & prog == "")
                    {

                        switch (data)
                        {
                            case "1":

                                mes = Encoding.Unicode.GetBytes(controller.PrintAll() + help);
                                udpClient_S.SendAsync(mes, mes.Length, ep);
                                break;

                            case "2":
                                mes = Encoding.Unicode.GetBytes("\nВведите индекс: ");
                                udpClient_S.SendAsync(mes, mes.Length, ep);
                                prog = "outind";

                                break;

                            case "3":
                                try
                                {

                                    mes = Encoding.Unicode.GetBytes("\nВведите имя: ");
                                    udpClient_S.SendAsync(mes, mes.Length, ep);
                                    prog = "name";
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                                break;

                            case "4":
                                try
                                {
                                    mes = Encoding.Unicode.GetBytes("\nВведите индекс: ");
                                    udpClient_S.SendAsync(mes, mes.Length, ep);
                                    prog = "delind";

                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                                break;

                            default:
                                mes = Encoding.Unicode.GetBytes("\nПопробуйте снова" + help);
                                udpClient_S.SendAsync(mes, mes.Length, ep);
                                break;
                        }
                    }
                    break;
                case "outind":
                    try
                    {
                        int ind = Convert.ToInt32(data);
                        mes = Encoding.Unicode.GetBytes(controller.PrintByIndex(ind) + help);
                        udpClient_S.SendAsync(mes, mes.Length, ep);
                        prog = "";
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;

                case "delind":
                    try
                    {
                        int ind = Convert.ToInt32(data) ;
                        mes = Encoding.Unicode.GetBytes(controller.DeleteByIndex(ind) + help);
                        udpClient_S.SendAsync(mes, mes.Length, ep);
                        prog = "";
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;

                case "name":
                    try
                    {
                        name = data;
                        mes = Encoding.Unicode.GetBytes("\nВведите фамилию: ");
                        prog = "surname";
                        udpClient_S.SendAsync(mes, mes.Length, ep);
                       
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;

                case "surname":
                    try
                    {
                        surname = data;
                        mes = Encoding.Unicode.GetBytes("\nВведите адрес: " );
                        prog = "addres";
                        udpClient_S.SendAsync(mes, mes.Length, ep);
                        
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;

                case "addres":
                    try
                    {
                        addres = data;
                        mes = Encoding.Unicode.GetBytes("\nВведите возраст: " );
                        udpClient_S.SendAsync(mes, mes.Length, ep);
                        prog = "age";
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;

                case "age":
                    try
                    {
                        age = Convert.ToInt32(data);
                        mes = Encoding.Unicode.GetBytes("\nВведите семейный статус y-- женат n -- холост: " );
                        udpClient_S.SendAsync(mes, mes.Length, ep);
                        prog = "family";
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;

                case "family":
                    try
                    {
                        if (data=="y")
                        {
                            family = true;
                            prog = "";
                            mes = Encoding.Unicode.GetBytes("\nСущность добавлена " + help);
                            controller.list.Add(new Pawn(name, surname, addres, age, family));
                            error = false;
                        }
                        else if (data == "n")
                        {
                            family = false;
                            prog = "";
                            mes = Encoding.Unicode.GetBytes("\nСущность добавлена " + help);
                            
                            using (PawnContext db=new PawnContext())
                            {
                                db.Pawns.Add(new Pawn(name, surname, addres, age, family));
                                db.SaveChanges();
                            }
                            error = false;
                        }
                        else
                        {
                            mes = Encoding.Unicode.GetBytes("\nВведите семейный статус y-- женат n -- холост: " );
                            error = true;
                        }
                    
                        udpClient_S.SendAsync(mes, mes.Length, ep);
                        
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
               
            }
           
            
        }
        static void Main(string[] args)
        {


            Server server = new Server(8001);
            logger.Debug("Server is up");
            server.StartListenAsync();
            System.ConsoleKey key = Console.ReadKey().Key;
            while (key != ConsoleKey.Escape)
            {


            }
        }
    }
}

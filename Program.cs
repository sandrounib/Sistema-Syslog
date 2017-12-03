using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace Syslog
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                /// <summary>
                /// criação da variável opção a ser usada no Menu
                /// título do sistema
                /// criação da instancia/Objeto da classe Usuário.
                /// </summary>
                string opt = "";
                Usuario usuario = new Usuario();
                    Console.Clear();                    
                    System.Console.WriteLine("********************************");
                    System.Console.WriteLine(" | SYSLOG - Logon no Sistema | ");
                    System.Console.WriteLine("********************************");
                                    
                do
                {                      
                    System.Console.WriteLine("\nDigite uma opção");
                    System.Console.Write("\n1 -Cadastrar | 2- Logar | 3- Logout | 9- Sair : ");
                    opt = Console.ReadLine();   
                    switch (opt)
                    {
                        case "1" :                                                         
                            usuario.Cadastrar();
                            break;

                        case "2":
                            //Evento LoginUp recebendo uma nova instancia do delegate que chama o método LoginReg
                            usuario.LoginsUp += new Usuario.deleg1(LoginReg);
                            usuario.Logar();//chamada do método void Logar

                            break;

                        case "3":
                            usuario.LogOutUp += new Usuario.deleg2(LogOutReg);
                            usuario.Logout();                            
                            break;                        
                        default:
                        break;
                    }                 
                    
                } while (opt!="9");
            }
            catch (System.Exception)
            {
                
                throw;
            }           
        
        }

        static void LogOutReg(string email){
            StreamReader srLerArq = new StreamReader("cadUsuario.csv");
            string linha;
            while ((linha=srLerArq.ReadLine())!=null)
            {
                string[] dados = linha.Split(';');
                if (dados[1]==email)
                {
                    if (!File.Exists("Superior.csv"))
                    {
                        StreamWriter swCriaArq = new StreamWriter("Superior.csv",true);
                        swCriaArq.WriteLine("Nome do Usuário;Email; Login ou Logout; Data do Login/Logout");
                        swCriaArq.Close();                        
                    }
                    StreamWriter swSuperior = new StreamWriter("Superior.csv");
                    swSuperior.WriteLine(dados[0]+ ";"+ dados[1]+ ";" + ";Logout;"+ DateTime.Now);
                    swSuperior.Close();
                if (!File.Exists("LogSistema.csv"))
                {
                    StreamWriter swCriaArq = new StreamWriter("LogSistema.csv",true);
                    swCriaArq.WriteLine("Nome do Usuário;Email; Login ou Logout; Data do Login/Logout");
                    swCriaArq.Close();                    
                }                    
                StreamWriter swLog = new StreamWriter("LogSistema.csv",true);
                swLog.WriteLine(dados[0]+ ";"+ dados[1]+ ";Logout;"+ DateTime.Now);
                swLog.Close();
                srLerArq.Close();
                break;
                }
                
            }

        }
        static void LoginReg(string email){
            StreamReader srLerArq = new StreamReader("cadUsuario.csv");
                string linha;
                while ((linha= srLerArq.ReadLine())!=null)
                {
                    string[] dados = linha.Split(';');
                    if (dados[1]==email)
                    {
                        if (!File.Exists("Supervisor.csv"))
                        {
                            StreamWriter swCriaArq = new StreamWriter("Supervisor.csv",true);
                            swCriaArq.WriteLine("Nome do Usuário;Email;Login/Logout;Data do Login/Logout");
                            swCriaArq.Close();                        
                        }
                            StreamWriter swSupervisor = new StreamWriter("Supervisor.csv",true);
                            swSupervisor.WriteLine(dados[0]+";"+dados[1]+";Login;"+DateTime.Now);
                            swSupervisor.Close();                    
                                }
                                if(!File.Exists("LogSistema.csv")){
                                    StreamWriter swCriaArq = new StreamWriter("LogSistema.csv",true);
                                    swCriaArq.WriteLine("Nome do Usuário;Email;Login/Logout;Data do Login/Logout");
                                    swCriaArq.Close();                                     
                                }
                                StreamWriter swLog = new StreamWriter("LogSistema.csv",true);
                                swLog.WriteLine(dados[0]+";"+dados[1]+";Login;"+DateTime.Now);
                srLerArq.Close();
                swLog.Close();          
                                    
            }
                         

        }

    }
}

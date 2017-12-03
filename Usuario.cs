using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Syslog
{
    public class Usuario
    {   
        /// <summary>
        /// Propriedades dos dados do Usuário
        /// </summary>
        /// <returns></returns>     
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        //delegate tipo void que receberá uma string
        public delegate void deleg1(string email);

        //Evento do tipo deleg1 e nome LoginUp 
        public event deleg1 LoginsUp;


        public delegate void deleg2(string email);
        public event deleg2 LogOutUp;



        /// <summary>
        /// Método responsável para receber dados do usuário 
        /// Após o recebimento ele criar um arquivo .csv e grava os dados nesse arquivo
        /// a senha digitada será encriptada antes de salvar no arquivo .csv
        /// </summary>
        public void Cadastrar(){
            System.Console.Write("\nDigite o Nome:");
            this.Nome = Console.ReadLine();
            System.Console.Write("Digite o Email: ");
            this.Email = Console.ReadLine();
            System.Console.Write("Digite a Senha: ");
            this.Senha = Console.ReadLine();
            if(!File.Exists("cadUsuario.csv")){
            StreamWriter criarArquivo = new StreamWriter("cadUsuario.csv",true);
                criarArquivo.WriteLine("Nome; Email; Senha");
                System.Console.WriteLine("Arquivo com título criado com sucesso!");
                criarArquivo.Close();
            }
            StreamWriter escreverNoArquivo = new StreamWriter("cadUsuario.csv",true);
            escreverNoArquivo.WriteLine(Nome+";"+Email+";"+encriptSenha(Senha));
            System.Console.WriteLine("Dados inseridos com sucesso no arquivo cadUsuario.");
            escreverNoArquivo.Close();

        }

        public void Logar(){
            System.Console.WriteLine("Qual é seu E-mail? ");
            this.Email = Console.ReadLine();
            StreamReader lerArquivo = new StreamReader("cadUsuario.csv");
            string linha;
            while ((linha=lerArquivo.ReadLine())!=null)
            {
                string[] dados = linha.Split(';');
                if (dados[1]==this.Email)
                {
                    System.Console.WriteLine("Qual é sua senha? ");
                    this.Senha = Console.ReadLine();
                    //variável senhaEncriptada recebe a senha encriptada
                    string senhaEncriptada = encriptSenha(Senha);
                    if (senhaEncriptada==dados[2])
                    {
                        System.Console.WriteLine("Login efetuado com sucesso!");
                        string Email = dados[1];  
                        this.LoginsUp(Email);                        
                    }
                    else{
                        System.Console.WriteLine("Senha Incorreta!");
                        return;
                    }
                }                    
                               
            }
            lerArquivo.Close();
            return;

        }
        public void Logout(){
            System.Console.WriteLine("Qual é seu email? ");
            this.Email = Console.ReadLine();
            StreamReader srLerArq = new StreamReader("cadUsuario.csv");
            string linha;
            while ((linha=srLerArq.ReadLine())!=null)
            {
                string[] dados = linha.Split(';');
                if (dados[1]==this.Email){
                    System.Console.WriteLine("Logout efetuado com sucesso!");
                    this.LogOutUp(Email);
                }
                
            }
            srLerArq.Close();
            return;
        }

        /// <summary>
        /// Método que recebe uma senha, transforma em bytes,encripta e retorna
        /// para quem chamou o método a senha recebida encriptada.
        /// </summary>
        /// <param name="senha"></param>
        /// <returns></returns>
        public string encriptSenha(string senha){
            byte[] senhaAtual;
            byte[] senhaAlteradaEncriptada;
            SHA512 md5;
            senhaAtual = Encoding.Default.GetBytes(senha);
            md5 = SHA512.Create();
            senhaAlteradaEncriptada = md5.ComputeHash(senhaAtual);
            return Convert.ToBase64String(senhaAlteradaEncriptada);          
        }
    }
}
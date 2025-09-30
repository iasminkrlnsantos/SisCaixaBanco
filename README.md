# SisCaixaBanco
Documentação de Execução Local - SisCaixaBanco
Pré-requisitos
•	.NET 6 SDK
•	SQL Server
•	Editor recomendado: Visual Studio 2022 ou superior
Passos para Configuração
1.	Clone o repositório
    git clone https://github.com/iasminkrlnsantos/SisCaixaBanco.git
2.	Configure o banco de dados
•	Crie um banco de dados SQL Server local.
   Execute o script para criação do banco de dados disponibilizado na pasta
•	Atualize, se necessário, a string de conexão no arquivo appsettings.json:
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=SisCaixaBanco;User Id=usuario;Password=senha;"
     }
4.	Restaure os pacotes NuGet 
   dotnet restore
5. Set SisCaixaBanco como Startup Project
6. Compile e rode o projeto 

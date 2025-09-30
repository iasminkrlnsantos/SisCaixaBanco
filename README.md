# SisCaixaBanco

## 📌 Documentação de Execução Local

### ✅ Pré-requisitos
- .NET 6 SDK  
- SQL Server  
- Editor recomendado: Visual Studio 2022 ou superior  

---

## ⚙️ Passos para Configuração

1. **Clone o repositório**
```bash
git clone https://github.com/iasminkrlnsantos/SisCaixaBanco.git
```

2. **Configure o banco de dados**
- Crie um banco de dados SQL Server local.  
- Execute o script para criação do banco de dados disponibilizado na pasta raíz desse repositório (DbCaixaBanco.sql).  
- Atualize, se necessário, a string de conexão no arquivo `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SisCaixaBanco;User Id=usuario;Password=senha;"
  }
}
```

3. **Restaure os pacotes NuGet**
```bash
dotnet restore
```

4. **Defina o projeto de inicialização**
- No Visual Studio, configure **SisCaixaBanco** como *Startup Project*.

5. **Compile e rode o projeto**
```bash
dotnet build
dotnet run --project SisCaixaBanco/SisCaixaBanco.csproj
```

---

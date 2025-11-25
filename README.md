<h1 align="center">⚡API Enercheck</h1>
<p align="center">
  <img src="https://img.shields.io/badge/Status-Em%20Desenvolvimento-yellow?style=for-the-badge"/>
  <img src="https://img.shields.io/badge/Ferramenta-ASP.NET%20Core%208.0-purple?style=for-the-badge"/>
  <img src="https://img.shields.io/badge/IA-Gemini-green?style=for-the-badge"/>
  <img src="https://img.shields.io/badge/Framework-Entity Framework Core-blue?style=for-the-badge"/>
  
  
</p>
<h2>📖 Descrição</h2>
<p>Esta API oferece um sistema completo para gerenciamento e autenticação de usuários, permitindo a associação de cada conta a planos de serviço e aos projetos cadastrados por seus criadores. Além disso, integra um módulo de inteligência artificial capaz de analisar plantas elétricas enviadas pelos usuários.</p>
<hr>

## ⚙️ Tecnologias Utilizadas
- **ASP.NET Core 8 (C#)**
- **Entity Framework Core**
- **Microsoft SQL**
- **ASP.NET Identity**
- **Swagger**
- **Google GenAI (Gemini)**
<hr>

## 📦 Estrutura do Projeto
```bash
/APIEnercheck
├── Controllers/
│   └── ...
│
├── Data/
│   └── ...
│
├── Migrations/
│   └── ...
│
├── Models/
│   └── ...
│
├── Properties/
│   └── ...
│
├── Services/
│   └── ...
│
├── APIEnercheck.csproj
├── APIEnercheck.http
├── Program.cs
├── appsettings.json
└── appsettings.Development.json
```
<hr>

## 🚀 Como Executar o Projeto

### 🔸 Clonar o projeto
```bash
git clone https://github.com/ThiagoM22/APIEnercheck.git
cd APIEnercheck
```

### 🔸 Configurar Banco de Dados
```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=NOME_DB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 🔸 Aplicar Migrations
```bash
Update-Database
```

### 🔸 Executar o Projeto
```bash
dotnet run
```

<hr>

## 📚 Documentação (Swagger)
Após iniciar o projeto, acesse:

```bash
http://localhost:5000/swagger
```
Aqui você encontrará todos os endpoints documentados via Swagger
<hr>

## 🔐 Autenticação
- **JWT Bearer Token** 
- **ASP.NET Identity**
  Enviar token nas requisições:
```bash
Authorizathion: Bearer SEU_TOKEN
```

<hr>

## 📌 Endpoints Principais

- ###  👤 Usuarios

| Método | Rota | Descrição |
|--------|------|-----------|
| **GET** | ```/api/Usuarios``` | Lista de usuarios | 
| **GET** | ```/api/Usuarios{id}``` | Busca usuário por ID | 
| **GET** | ```/api/Usuarios/me``` | Busca usuário logado | 
| **POST** | ```/api/Usuarios``` | Cria um novo usuário | 
| **POST** | ```/api/Usuarios/Cliente``` | Cria um novo usuário | 
| **POST** | ```/api/Usuarios/Admin``` | Cria um novo usuário | 
| **PUT** | ```/api/Usuarios{id}``` | Atualiza o usuário |
| **PUT** | ```/api/Usuarios/usuario/add/plano``` | Adiciona um plano ao usuário |
| **DELETE** | ```/api/Usuarios{id}``` | Remove um usuário | 

- ###  📁 Projetos

| Método | Rota | Descrição |
|--------|------|-----------|
| **GET** | ```/api/`Projetos``` | Lista de planos | 
| **POST** | ```/api/Projetos``` | Cria um projeto | 
| **POST** | ```/api/Projetos/projeto/{id}/analisar``` | Cria uma analise de uma planta elétrica | 
| **PUT** | ```/api/{id}``` | Atualizar plano | 
| **DELETE** | ```/api/projetos{id}``` | Remove um plano | 

- ###  🧩 Planos

| Método | Rota | Descrição |
|--------|------|-----------|
| **GET** | ```/api/Planos``` | Lista de planos | 
| **GET** | ```/api/Planos/{id}``` | Busca plano por ID | 
| **POST** | ```/api/Planos``` | Cria um plano | 
| **PUT** | ```/api/{id}``` | Atualizar plano | 
| **DELETE** | ```/api/Planos{id}``` | Remove um plano | 



<hr>

## 📄 Licença
- **Este projeto está licenciado sob a MIT License.**

<hr>

## 🧑‍💻 Desenvolvido por 
<table align="center">
    <tr>
        <td align="center">
            <a href="https://github.com/ThiagoM22" style="text-decoration: none;">
                <img src="https://avatars.githubusercontent.com/u/158314044" width="100px;" alt="Avatar de Thiago Mazzi"/><br />
                <sub><b>Thiago Mazzi</b></sub><br />
                💻 Dev FullStack
            </a>
        </td>
        <td align="center">
            <a href="URL_GITHUB_LUIZ" style="text-decoration: none;">
                <img src="https://avatars.githubusercontent.com/u/158313981" width="100px;" alt="Avatar de Luiz Ghilherme"/><br />
                <sub><b>Luiz Ghilherme</b></sub><br />
                💻 Dev FullStack
            </a>
        </td>
    </tr>

</table>




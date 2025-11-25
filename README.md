<h1 align="center">⚡API Enercheck</h1>
<p align="center">   <img src="https://img.shields.io/badge/Status-Em%20Desenvolvimento-yellow?style=for-the-badge"/>   <img src="https://img.shields.io/badge/Ferramenta-ASP.NET%20Core%208.0-purple?style=for-the-badge"/>   <img src="https://img.shields.io/badge/IA-Gemini-green?style=for-the-badge"/>   <img src="https://img.shields.io/badge/Framework-Entity%20Framework%20Core-blue?style=for-the-badge"/>   <img src="https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge"/> </p>
  
</p>
<h2>📖 Descrição</h2>
<p>Esta API oferece uma solução completa para gerenciamento de usuários, planos de serviço e projetos, focada no setor elétrico.

Seu principal diferencial é a integração de um Módulo de Inteligência Artificial, alimentado pelo Google Gemini, que permite o envio e análise automatizada de plantas elétricas. A plataforma garante a autenticação segura de contas e a associação de usuários a diferentes níveis de planos e aos projetos que gerenciam.</p>
<hr>

## 🧠 Módulo de Inteligência Artificial (Gemini)
O recurso central da Enercheck é a capacidade de processar e analisar projetos elétricos.

### Como Funciona:

1.  O usuário envia a imagem ou PDF da planta elétrica para o endpoint de análise do projeto.
2.  A API utiliza o modelo Gemini para interpretar o diagrama e o layout.
3.  O resultado é um relatório estruturado que fornece insights técnicos e validações.

### 📊 Análises e Retornos:

- **Validação de Conformidade:** Verifica se a planta segue padrões e normas elétricas vigentes (e.g., NBRs).
- **Identificação de Componentes:** Reconhecimento e listagem de elementos (disjuntores, fiação, tomadas, etc.).
- **Sugestões de Otimização:** Recomendações para eficiência energética ou ajustes no dimensionamento.

<hr>

## ⚙️ Tecnologias Utilizadas
- **Linguagem/Plataforma: ASP.NET Core 8 (C#)**
- **ORM: Entity Framework Core**
- **Banco de Dados: Microsoft SQL Server**
- **Autenticação: ASP.NET Identity e JWT Bearer Token**
- **Documentação: Swagger**
- **Inteligência Artificial: Google GenAI (Gemini)**
<hr>

## 📦 Estrutura do Projeto
```bash
/APIEnercheck
├── Controllers/
│   └── (Lógica de Rota: Usuários, Projetos, Planos)
├── Data/
│   └── (Contexto do EF Core e Configurações de BD)
├── Migrations/
│   └── (Histórico de Migrações do Banco de Dados)
├── Models/
│   └── (Entidades do Projeto: User, Project, Plan, etc.)
├── Properties/
├── Services/
│   └── (Regras de Negócio e Lógica da IA/Gemini)
├── APIEnercheck.csproj
├── APIEnercheck.http
├── Program.cs 
├── appsettings.json 
└── appsettings.Development.json
```
<hr>

## 🚀 Como Executar o Projeto

### 🔸 Requisitos Prévios
- **.NET 8.0**
- Um servidor **Microsoft SQL Server** ou **LocalDB** (para desenvolvimento).

### 1. Clonar o projeto
```bash
git clone https://github.com/ThiagoM22/APIEnercheck.git
cd APIEnercheck
```

### 2. Configurar Banco de Dados
```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=NOME_DB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 3. Aplicar Migrations
```bash
dotnet ef database update
# OU, se usando Package Manager Console: Update-Database
```

### 4. Executar o Projeto
```bash
dotnet run
```

<hr>

## 📚 Documentação e Autentificação

### 🔑 Autenticação
A API utiliza **JWT Bearer Token** e **ASP.NET Identity** para controle de acesso baseado em roles (Cliente, Admin).

Para fazer requisições autenticadas, inclua o cabeçalho:
```bash
Authorization: Bearer SEU_TOKEN_JWT
```

### 📄 Documentação (Swagger)
Após iniciar o projeto, acesse a documentação interativa com todos os endpoints disponíveis:
```bash
http://localhost:5000/swagger 
# (A porta pode variar dependendo da configuração do Kestrel))
```
<hr>

## 📌 Endpoints Principais

- ###  👤 Usuarios

| Método | Rota | Descrição |
|--------|------|-----------|
| **GET** | ```/api/Usuarios``` | Lista de usuarios | 
| **GET** | ```/api/Usuarios{id}``` | Busca usuário por ID | 
| **GET** | ```/api/Usuarios/me``` | Busca usuário logado | 
| **POST** | ```/api/Usuarios/Cliente``` | Cria um novo usuário Cliente | 
| **POST** | ```/api/Usuarios/Admin``` | Cria um novo usuário Admin | 
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
Este projeto está licenciado sob a Licença MIT. Para mais detalhes, consulte o arquivo **LICENSE**.

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge)](./)
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




# 🏥 HOSPISIM - Sistema de Gestão Clínica

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-12-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white)

<br>

O **HOSPISIM** é uma aplicação web completa, desenvolvida em ASP.NET Core MVC, que simula um sistema de gestão para o Hospital Vida Plena. O projeto foi criado com o objetivo de modernizar a gestão clínica, garantindo segurança, rastreabilidade de atendimentos e um controle centralizado das informações dos pacientes.

---

## 📜 Índice

* [Funcionalidades Implementadas](#-funcionalidades-implementadas)
* [Regras de Negócio](#-regras-de-negócio-implementadas)
* [Estrutura do Projeto](#-estrutura-do-projeto)
* [Diagrama de Entidades](#-diagrama-de-entidades)
* [Tecnologias Utilizadas](#️-tecnologias-utilizadas)
* [Como Executar o Projeto](#-como-executar-o-projeto)
* [Próximos Passos (Roadmap)](#-próximos-passos-roadmap)
* [Autor](#-autor)

---

## ✨ Funcionalidades Implementadas

O sistema conta com um conjunto robusto de funcionalidades que cobrem os principais fluxos de trabalho de uma unidade hospitalar.

* **Gestão Completa (CRUDs Polidos):**
    * Módulos completos e refinados para gerenciar **Pacientes**, **Profissionais de Saúde**, **Atendimentos**, **Internações**, **Prescrições** e **Exames**, com interfaces limpas e organizadas.

* **Fluxos de Trabalho Contextuais e Inteligentes:**
    * **Criação a partir do Contexto:** Prescrições, Exames e Internações são criados diretamente a partir da tela de detalhes de um Atendimento, mantendo o fluxo de trabalho do profissional de saúde coeso e intuitivo.
    * **Criação Automática de Prontuário:** O prontuário de um paciente é gerado automaticamente no seu primeiro atendimento, eliminando a necessidade de um cadastro manual.
    * **Redirecionamento Inteligente:** Após criar, editar ou deletar um registro (como uma prescrição), o sistema retorna o usuário para a tela de contexto original (a de detalhes do atendimento), em vez de uma lista genérica.

* **Busca e Filtragem Dinâmica:**
    * As principais telas de listagem (Pacientes, Profissionais, Atendimentos) possuem campos de busca que filtram os resultados em tempo real, facilitando o acesso rápido à informação.

* **Interface de Usuário (UI) Moderna:**
    * Layout profissional com uma barra de navegação lateral fixa, ícones informativos e um dashboard de entrada elegante.
    * Formulários organizados em colunas e com controles amigáveis (como listas suspensas e seletores de data) para melhorar a experiência do usuário e garantir a consistência dos dados.

---

## 🧠 Regras de Negócio Implementadas

Para garantir a integridade e a segurança dos dados, diversas regras de negócio foram implementadas no back-end.

* **Validação de Dados Únicos:** O sistema impede o cadastro de Pacientes com **CPF duplicado** e de Profissionais de Saúde com **Registro de Conselho (CRM, COREN, etc.) duplicado**.
* **Controle de Status:**
    * Uma **Internação** sempre é criada com o status "Ativa" e só pode ser alterada posteriormente.
    * Um **Atendimento** com status "Realizado" ou "Cancelado" não pode ser editado, protegendo o histórico de registros.
    * Apenas **Profissionais Ativos** aparecem na lista de seleção ao criar um novo atendimento.
* **Lógica de Deleção Segura:** As relações no banco de dados foram configuradas com a Fluent API para evitar problemas de deleção em cascata (`ON DELETE RESTRICT`), resolvendo múltiplos ciclos de dependência e garantindo a integridade referencial.

---

## 📁 Estrutura do Projeto

O projeto segue a arquitetura padrão **Model-View-Controller (MVC)** para uma clara separação de responsabilidades.

```
Hospisim/
├── wwwroot/         # Arquivos estáticos (CSS, JS, imagens)
├── Data/            # Configuração do banco: DbContext, Migrations e Seed
├── Controllers/     # O 'cérebro': Lógica de negócio e fluxo de páginas
├── Models/          # As classes C# que representam os dados (nossas entidades)
└── Views/           # As telas (arquivos .cshtml) que o usuário vê
    ├── Home/
    ├── Pacientes/
    ├── Atendimentos/
    └── ...
```

---

##  diagrams Diagrama de Entidades

A estrutura do banco de dados foi modelada para refletir as complexas interações de um ambiente clínico.

```
Especialidade (1) ---< (N) ProfissionalDeSaude
                               |
                               `---(1) -- > (N) Atendimento >-- (1) Prontuário >-- (1) Paciente
                                            |                  |                       |
                                            |                  `----(N) <-------------'
                                            |
                                            |---(1) ---< (N) Prescricao
                                            |
                                            |---(1) ---< (N) Exame
                                            |
                                            `---(1) -- > (0..1) Internacao
                                                                |
                                                                `--(0..1) -- > (1) AltaHospitalar
```
---

## 🛠️ Tecnologias Utilizadas

* **Framework:** ASP.NET Core 8 (MVC)
* **Linguagem:** C# 12
* **Banco de Dados:** SQL Server
* **ORM:** Entity Framework Core 8
* **Front-End:** HTML5, CSS3, Bootstrap 5
* **Ícones:** Font Awesome
* **Ambiente:** Visual Studio 2022

---

## 🚀 Como Executar o Projeto

Siga os passos abaixo para executar o projeto em um ambiente de desenvolvimento local.

### Pré-requisitos

* [.NET 8 SDK](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)
* [SQL Server Express](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) (ou outra edição)
* Um editor de código como [Visual Studio 2022](https://visualstudio.microsoft.com/pt-br/vs/).

### Passos para Execução

1.  **Clone ou Baixe o Projeto:** Faça o download do código-fonte para sua máquina local.
2.  **Configure a Conexão com o Banco:**
    * Abra o arquivo `appsettings.json`.
    * Na seção `ConnectionStrings`, altere o valor de `Server` para o nome da sua instância do SQL Server (ex: `localhost\SQLEXPRESS`).
3.  **Crie e Popule o Banco de Dados:**
    * Abra o terminal na pasta raiz do projeto.
    * Execute o comando abaixo. Ele criará o banco, todas as tabelas e inserirá os dados iniciais (seed) de uma só vez.
    ```powershell
    dotnet ef database update
    ```
4.  **Execute a Aplicação:**
    * No Visual Studio, pressione o botão de Play verde (▶️) ou `F5`.
    * Ou, pelo terminal, use o comando: `dotnet run`.
5.  **Acesse o Sistema:**
    * Abra seu navegador no endereço fornecido, geralmente `https://localhost:XXXX`.

---

## 🗺️ Próximos Passos (Roadmap)

O projeto possui uma base sólida para futuras expansões. Os próximos passos lógicos incluem:
* **Segurança:** Implementar um sistema de autenticação e autorização com **ASP.NET Core Identity** para criar perfis de usuário (Administrador, Médico, Recepcionista) e controlar o acesso a diferentes funcionalidades.
* **Relatórios Avançados:** Criar uma seção de relatórios para extrair inteligência de negócio, como "atendimentos por especialidade" ou "taxa de ocupação de leitos".
* **Melhorar Feedback ao Usuário:** Implementar mensagens de sucesso temporárias (`TempData`) após operações bem-sucedidas (ex: "Paciente cadastrado com sucesso!").

---

## 👨‍💻 Autor

* **Erick**

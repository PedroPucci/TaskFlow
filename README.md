# **Especificação do Desafio**
- Desafio Técnico: Sistema de Gerenciamento de Tarefas
## **Objetivo**
Desenvolver um sistema de gerenciamento de tarefas utilizando C# ASP.NET MVC e Razor Pages. 
O sistema deve permitir que os usuários criem, editem, visualizem e excluam tarefas. Além disso, deve haver um recurso de autenticação para que cada usuário possa gerenciar suas próprias tarefas.
## **Requisitos Funcionais**
1. Autenticação: Implementar recursos de registro e login para os usuários
2. CRUD de Tarefas: Os usuários devem ser capazes de criar, ler, atualizar e excluir tarefas.
3. Categorias de Tarefas: Cada tarefa deve pertencer a uma categoria (ex: Trabalho, Pessoal, Outros) que o usuário pode definir.
4. Data de Conclusão: Cada tarefa deve ter uma data de conclusão definida pelo usuário.
5. Status da Tarefa: Implementar um status de conclusão para as tarefas (ex: Concluída, Pendente).
### **Detalhes Técnicos**
1. Models: Criar modelos para Usuário, Tarefa e Categoria.
2. Views: Utilizar Razor Pages para criar as interfaces de usuário, incluindo páginas de registro/login e páginas de gerenciamento de tarefas.
3. Controllers: Desenvolver controladores para gerenciar a lógica de criação, edição, visualização e exclusão de tarefas.
4. Autenticação e Autorização: Utilizar o ASP.NET Core Identity para implementar autenticação e autorização de usuários.
5. Validações: Implementar validações apropriadas para as entradas do usuário (ex: campos obrigatórios, formatos de data, etc.).
### **Critérios de Avaliação**
1. Funcionalidade: O sistema deve atender aos requisitos funcionais descritos acima.
2. Qualidade do Código: O código deve ser limpo, bem estruturado e comentado.
3. Usabilidade: A interface do usuário deve ser intuitiva e fácil de usar.
4. Segurança: Implementar boas práticas de segurança, especialmente em relação à autenticação e autorização.
5. Documentação: Fornecer um breve documento ou README explicando como configurar e executar o projeto.
---
# **Solução**
## **IDE's Utilizadas**
- Visual Studio 2022
- PostgreSQL
---
## **Recursos do Projeto**
- **Serilog**: Para geração e gerenciamento de logs.
- **FluentValidator**: Para validação de dados e regras de negócios.
- **Entity Framework (ORM)**: Para mapeamento e interação com o banco de dados.
- **Unit of Work**: Padrão de design para gerenciar transações e persistência de dados de forma coesa.
- **Migrations**: Gerenciamento de alterações no banco de dados.
- **Razor**: Para a criação das telas.
---
## **Como Executar o Projeto**
### **1. Configuração Inicial do Banco de Dados**
1. Faça o clone do projeto.
2. Verifique se a pasta `Migrations` no projeto está vazia. Caso contrário, delete todos os arquivos dessa pasta.   
3. Execute os seguintes comandos no **Package Manager Console**:
   - Certifique-se de selecionar o projeto relacionado ao banco de dados no menu "Default project".
   - Execute:
     ```bash
     add-migration PrimeiraMigracao
     update-database
     ```
   - Isso criará e configurará o banco de dados no PostgreSQL.
### **2. Executando o Projeto**
1. Abra o projeto no Visual Studio 2022.
2. Configure o projeto principal para execução:
   - Clique com o botão direito no projeto **TaskFlow** e selecione `Set as Startup Project`.
3. Clique no botão **HTTPS** no menu superior para iniciar a aplicação.

**Observações:**
- Caso seu antivírus exiba alertas ao executar o projeto, será necessário fechar esses avisos para continuar.
- Durante a execução, um console será aberto para a geração de logs. Caso queira, você pode fechá-lo sem impactar a execução do sistema.

### **3. Banco de Dados**
- **Centralização de Exceções:**  
  Implementada a classe `ExceptionMiddleware` para unificar o tratamento de erros no sistema.

- **Alterações Realizadas:**  
  Ajustadas as classes `Program` e `RepositoryUoW` para integrar o middleware.

- **Mensagens de Erro:**  
  - Se o banco de dados não existir, os endpoints retornam:  
    ```text
    The database is currently unavailable. Please try again later.
    ```
  - Para erros inesperados na criação do banco, é exibido:  
    ```text
    An unexpected error occurred. Please contact support if the problem persists.
    ```
### **4. Configuração do Log**
- O sistema gera logs diários com informações sobre os processos executados no projeto.
- O log será salvo no diretório:  
  `C://Users//User//Downloads//logsTaskFlow`.  
  **Nota**: É necessário criar a pasta manualmente nesse caminho ou alterar o diretório no código, caso deseje personalizá-lo.

**Formato do arquivo de log criado**:
- Arquivo diário com informações estruturadas.

### **5. Finalização**
- Após seguir as etapas anteriores, o sistema será iniciado, e uma página com a interface **Home** será aberta automaticamente no navegador configurado no Visual Studio.
---
## **Estrutura do Projeto**
Essa estrutura garante organização, modularidade e escalabilidade ao projeto.
### **1. TestWebBackEndDeveloper (API)**
Contém os endpoints para acesso e execução das funcionalidades:
1. Organização das pastas:
- **Controllers**: Controladores da aplicação.
- **Extensions**:  
  - ExtensionsLogs: Classe para gerar logs.
  - ExceptionMiddleware classe para tratar erro de conexão com o banco de dados.
  - Extensões para a classe `Program`.
- **Appsettings**: Configurações, incluindo conexão com o banco de dados.
- **Program**: Classe principal para inicialização.
- **Views**: Classes referentes ao front do projeto.
- **Models**: Classes referentes ao erro para o front do projeto.

---
### **2. TestWebBackEndDeveloper.Application**
Camada intermediária entre os controladores e o banco de dados. Responsável também por funções específicas, como envio de e-mails.
1. Organização das pastas:
- **ExtensionError**: Contém a classe `Result` para controle de erros, usando FluentValidator.
- **Services**: Contém as classes de serviços e interfaces.
- **UnitOfWork**: Implementação do padrão **Unit of Work**, que gerencia transações e persistência de dados.
---
### **3. TestWebBackEndDeveloper.Domain**
Camada de domínio, responsável pelos dados principais do sistema.
1. Organização das pastas:
- **Entity**: Contém as entidades do projeto.
- **Enum**: Contém enums utilizados no projeto.
- **General**: Contém classes genéricas, incluindo a `BaseEntity`, com propriedades comuns às entidades.
---
### **4. TestWebBackEndDeveloper.Infrastructure**
Camada responsável pela interação com o banco de dados.
1. Organização das pastas:
- **Connection**: Configuração de conexão e mapeamento das entidades para o Entity Framework.
- **Migrations**: Diretório onde as migrations geradas serão armazenadas.
- **Repository**: Contém repositórios e suas interfaces.
---
### **5. TestWebBackEndDeveloper.Shared**
Biblioteca utilizada para validações e compartilhamento de recursos comuns:
1. Organização das pastas:
- **Enums**: Classes de enums para erros.
- **Helpers**: Classe auxiliar para validação de erros.
- **Validator**: Regras de validação para as entidades.
---
### **Bibliotecas (packages) para .NET, instaladas via NuGet**
1. coverlet.collector – Biblioteca para cobertura de código em testes unitários no .NET.
2. FluentValidation – Framework para validação de dados no .NET com sintaxe fluente.
3. Microsoft.EntityFrameworkCore – ORM do .NET para acesso a bancos de dados.
4. Microsoft.EntityFrameworkCore.Tools – Ferramentas para migrações e gestão do Entity Framework Core.
5. Microsoft.VisualStudio.Web.CodeGeneration.Design – Geração automática de código para ASP.NET Core (Scaffolding).
6. Npgsql – Provedor de acesso ao banco de dados PostgreSQL para .NET.
7. Npgsql.EntityFrameworkCore.PostgreSQL – Integração do Entity Framework Core com PostgreSQL.
8. Serilog.AspNetCore – Biblioteca para logging estruturado no ASP.NET Core.
9. Swashbuckle.AspNetCore – Ferramenta para documentar APIs ASP.NET Core com Swagger.
---
### **Executando a solução**
1. cadastre usuarios.
2. cadastre categorias.
3. cadastre tarefas.
- Após concluir esses passos, as demais funcionalidades, como consulta, edição e remoção, estarão disponíveis.

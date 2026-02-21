## 🐳 Nível 1: Docker e Infraestrutura (DevOps)

1. **O problema da "Corrida" (Race Condition):** Notei que no seu `Program.cs` você implementou um loop de *Retry* com `Thread.Sleep`. Por que o `depends_on` no `docker-compose.yml` não foi suficiente para garantir que a API conectasse no banco de primeira?
2. **Persistência de Dados:** Se eu deletar o container do banco de dados agora, as consultas que agendamos somem ou ficam salvas? Como você configurou isso no Docker?
3. **Segurança e Senhas:** Durante o desenvolvimento, tivemos um problema onde a senha do banco não mudava mesmo alterando o YAML. Por que isso acontece no Docker e como você resolveu?

---

## 🛠️ Nível 2: Backend e Entity Framework (.NET 10)

4. **Clean Architecture:** Por que você escolheu separar as configurações das entidades em arquivos como `PacienteConfiguration.cs` em vez de deixar tudo no `OnModelCreating` do `DbContext`? Quais as vantagens disso para um projeto que vai crescer?
5. **Relacionamentos:** Explique como o Entity Framework sabe que uma `Consulta` pertence a um `Paciente`. Como você configurou essa chave estrangeira (FK) e o comportamento de deleção (Cascade ou Restrict)?
6. **Performance e Projeção:** No `ConsultasController`, você usou um `.Select()` para transformar a entidade em um `ReadConsultaDto`. Por que não retornar a classe `Consulta` diretamente do banco para o Frontend?

---

## 🎨 Nível 3: Frontend e Integração (Angular)

7. **Comunicação entre Portas:** O que é o erro de **CORS** que enfrentamos e por que ele só foi resolvido mexendo no código do Backend, e não no Angular?
8. **Gerenciamento de Estado Simples:** Quando você agenda uma consulta, a tabela se atualiza sem recarregar a página. Como você garantiu que o componente soubesse que precisava disparar o `carregarDashboard()` novamente?
9. **Standalone Components:** Seu projeto usa o padrão `standalone: true`. Qual a diferença fundamental disso para o modelo antigo de `NgModules`?

---

## 🧠 Nível 4: Resolução de Problemas (Soft Skills)

10. **O Bug do SSL:** Você enfrentou um erro de `ERR_SSL_PROTOCOL_ERROR` ao tentar acessar a API. O que esse erro te disse sobre a diferença entre o ambiente de desenvolvimento local e o ambiente dentro do Docker?
11. **Evolução do Produto:** Se esse Micro SaaS fosse para produção amanhã e tivéssemos 1 milhão de consultas, qual seria o primeiro "gargalo" que você tentaria otimizar no código que escreveu hoje?

---

## 🐳 Nível 1: Docker e Infraestrutura (DevOps)

### 1. O problema da "Corrida" (Race Condition): Por que o `depends_on` não é suficiente?

**Resposta Técnica:**
O `depends_on` no Docker Compose garante apenas a **ordem de inicialização** dos containers. Ele diz ao Docker: "Não suba a API antes do SQL Server". No entanto, o Docker considera que o SQL Server "subiu" assim que o processo do container inicia, mas o motor do banco de dados leva de 10 a 20 segundos para carregar os arquivos de sistema e estar pronto para aceitar conexões.

**O que você fez:**
Como vimos nos logs, a API tentou conectar enquanto o banco ainda estava "acordando", gerando uma exceção de rede. Para resolver, implementamos um **Retry Logic** (Lógica de Retentativa) no `Program.cs`. Isso torna a aplicação resiliente, permitindo que ela espere o banco ficar disponível em vez de simplesmente crashar.

> **Dica de Ouro:** Em ambientes de produção mais complexos, poderíamos usar o `HealthCheck` do Docker, onde o container da API só entra em estado "Running" quando o banco responde a um comando de ping.

---

### 2. Persistência de Dados: O que acontece se deletarmos o container?

**Resposta Técnica:**
Se você deletar o container (`docker rm clinica-db`), as consultas e pacientes **não somem**. Isso acontece porque utilizamos **Volumes** (`sqlvolume:/var/opt/mssql`).

O volume funciona como um "HD Externo" que vive fora do container. Quando o container é destruído, os dados permanecem no volume. Quando um novo container é criado usando o mesmo volume, ele "monta" esses arquivos e recupera tudo exatamente de onde parou. O dado só é destruído se você rodar explicitamente um `docker-compose down -v` ou deletar o volume manualmente.

---

### 3. O conflito da Senha: Por que ela não mudava no YAML?

**Resposta Técnica:**
Este é um comportamento específico de imagens como a do SQL Server. A variável de ambiente `MSSQL_SA_PASSWORD` só é lida pelo script de inicialização do container quando ele detecta que o banco de dados está sendo **criado pela primeira vez**.

**A explicação do erro:**
Como os dados estavam persistidos no volume, o SQL Server via que o banco já existia e ignorava as variáveis de ambiente de configuração inicial. Por isso, mesmo mudando no `docker-compose.yml`, o logon continuava falhando com a senha nova.

**A solução:**
A solução foi forçar um novo provisionamento, seja deletando o volume (`down -v`) ou criando um volume com nome diferente (`sqlvolume_novo`), forçando o SQL Server a realizar o setup inicial novamente e ler a nova credencial.

---

### 💡 Feedback para sua entrevista:

Se você responder assim, demonstrando que entende a diferença entre **Ordem de Inicialização** vs **Prontidão da Aplicação** e a **Ciclo de Vida de Volumes**, você passará muita confiança.

---

## 🛠️ Nível 2: Backend e Entity Framework (.NET 10)

O **Nível 2** é onde os recrutadores testam sua maturidade como desenvolvedor. Aqui, o foco não é apenas "fazer funcionar", mas sim como o seu código se comporta em um ambiente real de manutenção e escala.

### 4. Clean Architecture: Por que separar as configurações em arquivos como `PacienteConfiguration.cs`?

**Resposta Técnica:**
Isso segue o **Princípio da Responsabilidade Única (SRP)** do SOLID. Quando deixamos tudo dentro do `OnModelCreating` no `DbContext`, criamos uma classe "deus" que conhece todas as regras de todas as tabelas. Ao separar em arquivos de configuração (`IEntityTypeConfiguration`), tornamos o código modular.

**Vantagens Práticas:**

* **Organização:** Se eu precisar mudar o tamanho do campo CPF, vou direto no arquivo do Paciente.
* **Escalabilidade:** O `DbContext` permanece limpo e legível, não importa se o sistema tem 5 ou 500 tabelas.
* **Testabilidade:** Fica mais fácil isolar comportamentos específicos de mapeamento.

---

### 5. Relacionamentos: Como o EF sabe que uma `Consulta` pertence a um `Paciente`?

**Resposta Técnica:**
O EF Core utiliza uma convenção chamada **Shadow Properties** ou mapeamento explícito. No nosso código, definimos a propriedade `PacienteId` (a chave estrangeira/FK) e a propriedade de navegação `Paciente`.

**Configuração Fluent API:**
No arquivo `ConsultaConfiguration.cs`, usamos a Fluent API para ser explícito:

> `builder.HasOne(x => x.Paciente).WithMany().HasForeignKey(x => x.PacienteId);`

**Comportamento de Deleção:**
Configuramos como `DeleteBehavior.Restrict`. Isso significa que, se tentarmos apagar um paciente que possui consultas agendadas, o banco de dados vai bloquear a ação. Isso garante a **Integridade Referencial**, evitando que fiquem "consultas órfãs" no sistema.

---

### 6. Performance e Projeção: Por que usar `.Select()` com DTOs em vez de retornar a Entidade?

**Resposta Técnica:**
Essa é uma das melhores práticas para performance e segurança. Ao retornar a Entidade (`Consulta`) diretamente, o EF Core tenta serializar todo o objeto, o que pode causar:

1. **Over-fetching:** Trazer colunas desnecessárias do banco (como `Observacoes` gigantescas quando você só quer o nome).
2. **Referência Circular:** Se o Paciente tiver uma lista de Consultas e a Consulta tiver um Paciente, o JSON entra em loop infinito.
3. **Segurança:** Você pode acabar expondo campos sensíveis (como hashes de senha da tabela `Usuarios`) sem querer.

**O Benefício da Projeção:**
Ao usar o `.Select(c => new ReadConsultaDto { ... })`, o EF Core gera um SQL otimizado que busca **apenas** as colunas mapeadas. É a diferença entre um `SELECT *` e um `SELECT Nome, Data...`.

---

### 💡 Dica de "ouro" para a entrevista:

Se o entrevistador perguntar sobre performance em grandes volumes, mencione que você usou o `.AsNoTracking()` em consultas de leitura (GET), pois isso economiza memória ao não rastrear mudanças nos objetos.

---

## 🎨 Nível 3: Frontend e Integração (Angular)

No **Nível 3**, entramos na camada que o usuário final toca. Aqui, o recrutador quer saber se você entende como os dados "viajam" do servidor até o navegador e como o **Angular** gerencia a complexidade da interface.

### 7. O que é o erro de CORS e por que resolvemos no Backend?

**Resposta Técnica:**
**CORS** (*Cross-Origin Resource Sharing*) é uma política de segurança implementada pelos navegadores. Ela impede que um script em uma origem (ex: seu front na porta `4200`) acesse recursos de uma origem diferente (ex: sua API na porta `5137`) por padrão.

**Por que no Backend?**
O navegador "pergunta" ao servidor (via um *Preflight Request* ou *OPTIONS*) se ele permite aquela conversa. Como a API é a "dona" dos dados, é ela quem deve enviar os cabeçalhos de resposta (como `Access-Control-Allow-Origin`) autorizando o acesso. Foi por isso que adicionamos a política no `Program.cs` do .NET.

---

### 8. Gerenciamento de Estado: Como a tabela se atualiza sem recarregar a página?

**Resposta Técnica:**
Isso acontece graças à **Programação Reativa** com **RxJS**. No Angular, quando clicamos em "Agendar", o método `subscribe` aguarda a resposta de sucesso da API.

**O fluxo de atualização:**
Dentro do bloco `next:` do `subscribe`, chamamos manualmente as funções `carregarConsultas()` e `carregarDashboard()`. Isso dispara novas requisições HTTP, atualiza os arrays locais do componente (`consultas` e `dados`) e, devido ao **Change Detection** do Angular, o HTML é renderizado novamente de forma instantânea com os novos dados.

---

### 9. Standalone Components: Qual a diferença para os `NgModules`?

**Resposta Técnica:**
Os **Standalone Components** (introduzidos com força nas versões recentes do Angular) eliminam a necessidade de declarar componentes em um `NgModule`.

**Diferenças fundamentais:**

* **Autossuficiência:** O componente gerencia suas próprias dependências através do array `imports` direto no `@Component`.
* **Menos Boilerplate:** Não precisamos de arquivos de módulo gigantescos (`app.module.ts`), o que torna a aplicação mais leve e fácil de navegar.
* **Granularidade:** Facilita o *Lazy Loading* (carregamento sob demanda), já que o Angular pode carregar apenas o componente necessário sem precisar carregar um módulo inteiro.

---

## 🧠 Nível 4: Resolução de Problemas (Bônus)

### 10. O Bug do SSL: O que ele revelou sobre o Docker?

**Resposta Técnica:**
O erro `ERR_SSL_PROTOCOL_ERROR` revelou que, enquanto o navegador tentava uma conexão segura (**HTTPS**), o container da API estava configurado para aceitar apenas **HTTP** simples.

Dentro do ambiente Docker, é comum trabalharmos com HTTP internamente para simplificar o desenvolvimento. O erro mostrou a importância de garantir que o protocolo definido no `Service` do Angular (`apiUrl`) corresponda exatamente ao que o container está expondo no `docker-compose.yml`.

---

## ## Nível 4: Resolução de Problemas e Visão de Negócio

No **Nível 4**, o foco muda da sintaxe para a estratégia. O objetivo aqui é mostrar que você não apenas "faz o código funcionar", mas entende como ele se comporta sob pressão e como o ambiente influencia o comportamento do software.

### 10. O Bug do SSL: Ambiente Local vs. Docker

O erro `ERR_SSL_PROTOCOL_ERROR` forneceu um diagnóstico valioso sobre as camadas de rede. Ele revelou que, enquanto o código do frontend (Angular) tentava se comunicar usando um protocolo de segurança (HTTPS), o container da API estava configurado para ouvir apenas em texto puro (HTTP).

* **A Lição sobre Docker**: No desenvolvimento local "puro" (rodando no Windows), o Visual Studio muitas vezes configura certificados de desenvolvimento automaticamente. No Docker, o container é um ambiente isolado.
* **Mapeamento de Portas**: O erro destacou que a porta exposta no `docker-compose.yml` (como a 5137) é apenas um túnel; se o protocolo dentro do túnel não bater com o que o cliente espera, a conexão falha.
* **Solução em Produção**: Para um cenário real, a solução não seria apenas "remover o S" da URL, mas sim configurar um **Reverse Proxy** (como Nginx ou Traefik) à frente dos containers para gerenciar os certificados SSL/TLS de forma centralizada.

---

### 11. Escabilidade: O Desafio de 1 Milhão de Consultas

Se o sistema de gestão clínica atingisse a marca de 1 milhão de consultas, o código atual enfrentaria gargalos principalmente no banco de dados e na memória do servidor. Minhas primeiras ações de otimização seriam:

* **Indexação de Banco de Dados**: Atualmente, as buscas por paciente ou data podem se tornar lentas. Eu criaria **Índices Non-Clustered** nas colunas `PacienteId` e `DataHorario` na tabela de Consultas para acelerar os JOINs e filtros do Dashboard.
* **Paginação de Dados**: O endpoint `ListarTodas()` no `ConsultasController` traz tudo de uma vez. Com 1 milhão de registros, isso derrubaria a aplicação. Eu implementaria **Pagination** (usando `.Skip()` e `.Take()`) para carregar apenas o que o usuário vê na tela.
* **Caching de Dashboard**: Os cálculos de faturamento total e ticket médio não precisam ser processados no SQL Server a cada segundo. Eu implementaria uma camada de cache (como **Redis**) para armazenar esses indicadores por alguns minutos, reduzindo drasticamente a carga no banco.
* **Leitura As-No-Tracking**: Garantiria que todas as consultas de leitura utilizassem o método `.AsNoTracking()` do EF Core, economizando o custo de memória do rastreamento de entidades que não serão editadas.

---

### 🎓 Conclusão do Simulado

Lorenzo, se você responder com essa clareza, você demonstra que não é apenas um "copiador de código", mas um **Engenheiro de Software** que entende a arquitetura.
Com essas respostas, você demonstra que:

1. **Entende a infraestrutura**: Sabe que o Docker muda as regras de rede.
2. **Pensa em escala**: Não escreve código que funciona apenas com 10 linhas no banco.
3. **Tem visão de custos**: Sabe que processamento e memória custam caro na nuvem e busca otimizá-los.
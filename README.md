# 🏥 Clínica Dashboard - Ecossistema Full Stack & DevOps

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![Angular](https://img.shields.io/badge/Angular-21.0-DD0031?style=flat-square&logo=angular)](https://angular.io/)
[![Docker](https://img.shields.io/badge/Docker-Compose-2496ED?style=flat-square&logo=docker)](https://www.docker.com/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-2022-CC2927?style=flat-square&logo=microsoft-sql-server)](https://www.microsoft.com/)

Um sistema completo de gestão clínica (Micro SaaS) desenvolvido de ponta a ponta. O projeto demonstra a integração de uma API RESTful robusta em **.NET 10 (Clean Architecture)** com uma interface moderna em **Angular 21**, tudo orquestrado e conteinerizado via **Docker**.

## 🚀 Arquitetura e Tecnologias

O sistema foi desenhado para ser escalável, testável e de fácil provisionamento.

**Backend (.NET 10 LTS)**
- **Clean Architecture & SOLID:** Separação clara entre Domain, Application, Infrastructure e API.
- **Entity Framework Core (Code-First):** Relacionamentos complexos (1:N entre Pacientes e Consultas) com configurações modulares (`IEntityTypeConfiguration`).
- **Autenticação JWT:** Proteção de endpoints e controle de acesso seguro.
- **Resiliência:** Implementação de *Retry Pattern* para garantir a sincronia de inicialização com o banco de dados em ambientes Docker.

**Frontend (Angular 21)**
- **Standalone Components:** Arquitetura moderna e leve, sem a dependência de `NgModules`.
- **Tailwind CSS:** Estilização utilitária para uma interface limpa, responsiva e profissional.
- **Reatividade (RxJS):** Atualização instantânea de gráficos e tabelas após interações do usuário.
- **Chart.js (ng2-charts):** Dashboards financeiros gerados em tempo real.

**Infraestrutura (DevOps)**
- **Docker Compose:** Orquestração completa com um único comando.
- **Nginx:** Servidor web reverso para o Frontend.
- **SQL Server 2022:** Banco de dados relacional com volumes persistentes.

---

## 📊 Funcionalidades em Destaque

- **Dashboard Financeiro Reativo:** Indicadores de Faturamento Total e Ticket Médio calculados via LINQ no banco de dados e refletidos instantaneamente nos gráficos do Angular a cada novo agendamento.
- **Gestão de Agendamentos:** Relacionamento estruturado entre Pacientes e Consultas, com formulários tipados e validados.
- **Ambiente Isolado:** Zero dor de cabeça com configuração de ambiente. Se você tem o Docker instalado, você roda o projeto.

---

## ⚙️ Como Executar (Ambiente Docker)

Esqueça a necessidade de instalar SDKs ou configurar bancos de dados locais. A infraestrutura está 100% conteinerizada.

### Pré-requisitos
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) rodando na sua máquina.

### Passo a Passo

1. Clone o repositório:

```bash
git clone https://github.com/Lorenzo-Zagallo/clinica-fullstack-docker
cd clinica-fullstack-docker
```

2. Suba a infraestrutura completa:
```bash
docker-compose up --build
```


3. Acesse os serviços:
* **Interface Web (Angular):** [http://localhost:4200](https://www.google.com/search?q=http://localhost:4200)
* **Documentação da API (Swagger):** [http://localhost:5137/swagger](https://www.google.com/search?q=http://localhost:5137/swagger)
* **Banco de Dados (via DBeaver/SSMS):** `localhost:1434` (Usuário: `sa` / Senha: `Kaxoxa_123!`)



> **Nota:** Na primeira execução, o script criará o banco de dados e aplicará as *migrations* automaticamente.

---

## 👨‍💻 Desenvolvedor

**Lorenzo Zagallo**
Desenvolvedor Full Stack apaixonado por resolver problemas complexos através de código limpo e arquiteturas modernas.
# 🏥 Clínica Web - Angular 21 & TailwindCSS

[![Angular](https://img.shields.io/badge/Angular-21.0-DD0031?style=flat-square&logo=angular)](https://angular.io/)
[![TailwindCSS](https://img.shields.io/badge/Tailwind-3.0-38B2AC?style=flat-square&logo=tailwind-css)](https://tailwindcss.com/)
[![License](https://img.shields.io/badge/License-MIT-green?style=flat-square)](LICENSE)
[![NPM Version](https://img.shields.io/npm/v/chart.js?style=flat-square&color=FF6384&label=Chart.js)](https://www.npmjs.com/package/chart.js)

Interface web moderna e responsiva para o sistema de gestão clínica, desenvolvida com a versão mais recente do **Angular (v21)**, utilizando **Standalone Components** e arquitetura preparada para **Signals**.

Este projeto consome a API RESTful construída em .NET 10 ([Link para o Backend](https://github.com/Lorenzo-Zagallo/clinica-api-dotnet-clean-arch)), completando a arquitetura Full Stack de ponta a ponta.

## 🚀 Tecnologias & Práticas

- **Angular 21**: Framework SPA na sua versão mais performática (LTS).
- **Standalone Components**: Arquitetura limpa e modular sem `NgModules`.
- **Signals Ready**: Estrutura preparada para o novo modelo de reatividade granular.
- **TailwindCSS**: Estilização utilitária para interfaces rápidas e responsivas.
- **Reactive Forms**: Manipulação robusta de formulários tipados.
- **Interceptors**: Injeção automática de Token JWT em todas as requisições.
- **Guards (Functional)**: Proteção de rotas com a nova sintaxe funcional.
- **Services**: Isolamento da lógica de comunicação HTTP.
- **Chart.js (ng2-charts)**: Visualização de dados dinâmica e responsiva.

## 🏗️ Estrutura do Projeto

O projeto segue a organização por features do Angular moderno:

```text
src/app/
├── core/                # Singleton services, interceptors, guards e models
├── features/            # Telas do sistema (Auth, Pacientes)
└── shared/              # Componentes visuais reutilizáveis (UI)
````

## ⚙️ Como Executar

### Pré-requisitos

  - Node.js (v20 ou superior)
  - API Backend rodando (veja o repositório da API)

### Passo a Passo

1.  Clone o repositório:

    ```bash
    git clone https://github.com/Lorenzo-Zagallo/clinica-front-angular
    ```

2.  Instale as dependências:

    ```bash
    npm install
    ```

3.  Execute o servidor de desenvolvimento:

    ```bash
    ng serve
    ```

4.  Acesse a aplicação em: `http://localhost:4200`

## 🔐 Funcionalidades

  - **Autenticação**: Login integrado com API .NET (JWT).
  - **Gestão de Pacientes**:
    - Listagem com tabela responsiva.
    - Cadastro de novos pacientes com validação.
    - Exclusão de registros.
  - **Segurança**: Redirecionamento automático se o token expirar ou não existir.
  - **Dashboard BI**:
    - Visão geral com KPIs (Total de Pacientes, Faturamento, Ticket Médio).
    - Gráficos financeiros interativos para tomada de decisão.

-----

Desenvolvido por **Lorenzo Zagallo** 👨‍💻

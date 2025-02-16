# 🌐 Nerd Store

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/domain-driven-design/refs/heads/main/src/WebApps/NerdStore.WebApp.MVC/wwwroot/prints/logo.png" alt="logo" />
</div>

## 💻 Sobre o projeto
O **NerdStore** é um projeto voltado para aplicabilidade dos conceitos de **Domain-Driven Design (DDD)** e **Arquitetura Orientada a Eventos**. O objetivo foi construir um e-commerce estruturado com base na modelagem estratégica e tática do DDD, garantindo uma arquitetura bem definida e alinhada aos requisitos do negócio.

O processo de desenvolvimento iniciou-se com a **compreensão** dos **processos de negócio**, permitindo a extração de uma **linguagem ubíqua** clara e compartilhada. A partir disso, foram identificados os principais elementos do sistema e organizados em **domínios e subdomínios** conforme sua relevância (Principal, Auxiliar e Genérico).

Com essa estrutura, foram definidos os **contextos delimitados**, elaborando um **mapa de contexto** para mapear as interações entre os módulos. Em seguida, cada contexto recebeu uma **arquitetura** específica de acordo com sua complexidade, aplicando **padrões arquiteturais** e técnicas de **modelagem tática**.

## ✅ Requisitos
- [x] A loja virtual exibirá um **catálogo** de **produtos** de diversas **categorias**
- [x] Um **cliente** pode realizar um **pedido** contendo 1 ou N produtos
- [x] A loja realizará as **vendas** através de **pagamento** por **cartão de crédito**
- [x] O cliente irá realizar o seu **cadastro** para fazer pedidos
- [x] O cliente irá confirmar o pedido, **endereço** de entrega, escolher o tipo de **frete** e realizar o pagamento
- [x] Após o pagamento o pedido mudará de **status** conforme resposta da **transação** via cartão
- [x] Ocorrerá a emissão da **nota fiscal** logo após a confirmação do pagamento

## 📖 Linguagem Ubíqua

- **Catálogo:** Conjunto de produtos disponíveis para compra na loja virtual, organizados por categorias.

- **Produto:** Item disponível para venda no catálogo, podendo pertencer a uma ou mais categorias.

- **Categoria:** Classificação dos produtos para facilitar a navegação e busca dentro do catálogo.

- **Cliente:** Usuário da loja virtual que pode realizar um cadastro e efetuar pedidos.

- **Cadastro:** Processo no qual um cliente informa seus dados pessoais e de contato para realizar pedidos na loja virtual.

- **Pedido:** Solicitação formal de compra realizada pelo cliente, contendo um ou mais produtos, um endereço de entrega e a escolha do frete e pagamento.

- **Carrinho:** Espaço temporário onde o cliente adiciona produtos antes de finalizar o pedido.

- **Debitar Estoque:** Reduzir a quantidade de um produto no estoque após a realização de um pedido.

- **Repor Estoque:** Adicionar a quantidade de um produto de volta ao estoque em casos de cancelamento ou devolução.

- **Voucher:** Cupom de desconto que pode ser aplicado ao pedido para reduzir o valor total da compra.

- **Venda:** Processo que envolve a realização de um pedido, o pagamento e a aprovação do mesmo, resultando na entrega do produto ao cliente.

- **Pagamento:** Transação financeira realizada pelo cliente para concluir uma venda, sendo efetuada por cartão de crédito.

- **Cartão de Crédito:** Meio de pagamento aceito pela loja virtual para a realização de vendas.

- **Endereço:** Local informado pelo cliente para onde os produtos do pedido serão enviados.

- **Frete:** Opção de envio escolhida pelo cliente, determinando o prazo e custo da entrega do pedido.

- **Status do Pedido:** Indica a situação do pedido durante o processo de compra e entrega:

    - **Rascunho:** Pedido ainda não finalizado pelo cliente.
    - **Iniciado:** Pedido confirmado pelo cliente e aguardando pagamento.
    - **Pago:** Pagamento aprovado e pedido pronto para processamento.
    - **Entregue:** Pedido enviado e recebido pelo cliente.
    - **Cancelado:** Pedido cancelado antes da entrega.

- **Transação:** Processo de validação do pagamento junto à operadora do cartão de crédito, podendo resultar na aprovação ou rejeição da venda.

- **Status da Transação:** Indica o resultado da tentativa de pagamento:

    - **Pago:** Transação aprovada com sucesso.
    - **Recusado:** Pagamento negado pela operadora do cartão.

- **Nota Fiscal:** Documento emitido após a confirmação do pagamento, formalizando a venda e garantindo a legalidade da transação.

## 📦 Contextos Delimitados

- **Cadastros:** Responsável pelo gerenciamento dos dados dos clientes, incluindo informações pessoais, endereços e preferências.

- **Catálogo:** Gerencia a vitrine de produtos disponíveis para venda, suas descrições, imagens, preços e categorias.

- **Vendas:** Envolve todo o fluxo de compras, desde a adição de produtos ao carrinho até a finalização do pedido.

- **Pagamentos:** Lida com a validação e processamento de pagamentos via cartão de crédito, além da atualização do status do pedido com base na resposta da transação.

- **Fiscal:** Responsável pela emissão da nota fiscal após a confirmação do pagamento, garantindo conformidade com as obrigações fiscais.

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/domain-driven-design/refs/heads/main/src/WebApps/NerdStore.WebApp.MVC/wwwroot/prints/contextos-delimitados.png" alt="contextos delimitados" />
</div>

## 📚 Subdomínios (Principais, Auxiliares e Genéricos)

- **Domínios Principais:** São o coração do negócio e representam a essência do e-commerce, ou seja, as funcionalidades essenciais para o funcionamento do e-commerce.

    - **Catálogo:** Gerencia os produtos e categorias.
    - **Vendas:** Lida com o fluxo de compras e a finalização do pedido.

- **Domínios Auxiliares:** Apoiam o funcionamento do sistema, mas não representam a essência do negócio, ornecem suporte às operações principais, garantindo que processos complementares, como cadastro e obrigações fiscais, funcionem corretamente.

    - **Cadastro:** Gerencia os dados dos clientes.
    - **Fiscal:** Responsável pela emissão de notas fiscais e conformidade com obrigações tributárias.

- **Domínio Genérico:** Atende necessidades comuns e pode ser reutilizado em diferentes contextos, promovendo maior flexibilidade e redução de complexidade.

    - **Pagamentos:** Processa transações financeiras e atualiza o status dos pedidos.

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/domain-driven-design/refs/heads/main/src/WebApps/NerdStore.WebApp.MVC/wwwroot/prints/subdominios.png" alt="separação de dominios" />
</div>

## 🗺️ Mapa de Contexto

- **Cliente-Fornecedor:** O contexto cliente depende das informações ou serviços do contexto fornecedor, que é responsável por garantir a consistência e disponibilidade dos dados.

    - **Cadastro (Cliente) → Vendas (Fornecedor):** O módulo de **Cadastro** fornece informações do cliente para o processo de Vendas.
    - **Fiscal (Cliente) → Vendas (Fornecedor):** O módulo **Fiscal** depende dos dados do pedido gerenciados pelo contexto de **Vendas** para emitir a nota fiscal.

- **Parceiros:** Contextos que colaboram ativamente entre si, compartilhando responsabilidades e informações.

    - **Catálogo ↔ Vendas:** O **Catálogo** mantém os produtos disponíveis, enquanto **Vendas** consome essas informações para permitir a comercialização.

- **Conformista:** O contexto aceita as regras impostas por outro sem influenciá-las, seguindo suas restrições.

    - **Vendas → Pagamentos:** O módulo de **Vendas** depende das regras e processos de **Pagamentos**, seguindo os padrões exigidos sem impor mudanças.

- **Anti-Corrupção Layer (ACL):** Implementa uma camada de proteção entre um contexto e outro para evitar que regras externas afetem seu funcionamento interno.

    - **Pagamentos (ACL):** Para evitar que mudanças externas interfiram no domínio interno, **Pagamentos** pode ter uma camada de adaptação que traduz dados recebidos para seu próprio modelo.

- **Núcleo Compartilhado:** Contextos que compartilham um modelo de dados comum para evitar duplicação de informações e garantir consistência. Isso ocorre quando múltiplos módulos utilizam as mesmas entidades fundamentais.

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/domain-driven-design/refs/heads/main/src/WebApps/NerdStore.WebApp.MVC/wwwroot/prints/mapa-contexto.png" alt="mapa de contexto" />
</div>

## 🧩 Arquitetura

A arquitetura do e-commerce foi projetada utilizando a abordagem **Monolito Modular Orientado a Eventos**, onde cada módulo representa um contexto delimitado e possui autonomia dentro do sistema. 

Cada contexto adota uma arquitetura específica baseada em sua complexidade e requisitos de negócio, garantindo que as soluções aplicadas sejam adequadas para cada caso.

### 🛠️ Padrões Implementados

- **CQRS (Command Query Responsibility Segregation):** A separação entre comandos (operações de escrita) e consultas (operações de leitura) melhora a escalabilidade e o desempenho do sistema. Cada ação é direcionada para uma camada específica, permitindo otimizações distintas para leitura e escrita.

- **Event Sourcing:** Os eventos de domínio são armazenados como uma sequência imutável de eventos, garantindo rastreabilidade total das mudanças no sistema. Isso permite reverter estados, reconstruir históricos e melhorar a auditoria dos processos.

- **Modelagem Tática do DDD:** Para garantir um design rico e aderente ao domínio de negócio, foram aplicados os seguintes conceitos de modelagem tática:

    - **Agregados:** Agrupamento lógico de entidades que devem ser manipuladas como uma unidade atômica. Um exemplo seria um pedido, que agrupa itens do pedido e as regras de negócio associadas.

    - **Entidades:** Objetos que possuem identidade única dentro do sistema, como um Cliente ou um Item do Pedido.

    - **Objetos de Valor:** Representam conceitos imutáveis que não possuem identidade própria, como um endereço ou um CPF.

    - **Eventos de Domínio:** Capturam mudanças significativas no estado do sistema, como um Pedido Confirmado ou um Pagamento Recusado. Esses eventos são utilizados para propagar informações entre módulos desacoplados.

    - **Serviços de Domínio:** Implementam regras de negócio complexas que não pertencem a uma única entidade ou agregado.

    - **Repositórios:** Responsáveis pelo acesso e persistência dos dados dos agregados, garantindo consistência e encapsulando as operações de banco de dados.

- **MVC (Model-View-Controller):** Para a interface do usuário, foi adotado o padrão MVC (Model-View-Controller), utilizando o ASP.NET MVC.

### 📝 Arquitetura Monolito Modular:

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/domain-driven-design/refs/heads/main/src/WebApps/NerdStore.WebApp.MVC/wwwroot/prints/arquitetura-monolito-modular.png" alt="exemplo de arquitetura"/>
</div>

### 📄 Estrutura do Projeto
<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/domain-driven-design/refs/heads/main/src/WebApps/NerdStore.WebApp.MVC/wwwroot/prints/modules-2.png" alt="modelo de arquitetura no asp net" />
</div>

## 🎨 Layout 

#### Gerenciamento de Produtos
<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/domain-driven-design/refs/heads/main/src/WebApps/NerdStore.WebApp.MVC/wwwroot/prints/tela-de-administracao.png" alt="tela de administração de produtos" />
</div>

#### Catalogo de Produtos
<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/domain-driven-design/refs/heads/main/src/WebApps/NerdStore.WebApp.MVC/wwwroot/prints/tela-de-vitrine.png" alt="tela de vitrine de produtos" />
</div>

#### Carrinho de Compras
<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/domain-driven-design/refs/heads/main/src/WebApps/NerdStore.WebApp.MVC/wwwroot/prints/tela-de-carrinho.png" alt="tela de carrinho de compras" />
</div>

#### Detalhe do Produto
<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/domain-driven-design/refs/heads/main/src/WebApps/NerdStore.WebApp.MVC/wwwroot/prints/tela-de-produto.png" alt="tela do detalhe do produto" />
</div>

#### Pagamento
<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/domain-driven-design/refs/heads/main/src/WebApps/NerdStore.WebApp.MVC/wwwroot/prints/tela-de-pagamento.png" alt="tela de pagamento" />
</div>

## 📝 Licença

Este projeto esta sobe a licença [MIT](https://github.com/Willian-Brito/domain-driven-design/blob/main/LICENSE).

Feito com ❤️ por Willian Brito 👋🏽 [Entre em contato!](https://www.linkedin.com/in/willian-ferreira-brito/)
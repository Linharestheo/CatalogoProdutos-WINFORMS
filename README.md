<h1 align="center">🛒 Catálogo de Produtos</h1>

<p align="center">
  Aplicação desktop para gerenciar um catálogo de produtos, com pesquisa em tempo real,<br>
  filtros combinados, validação visual e cálculo automático de preço promocional.
</p>

<p align="center">
  <img alt="C#" src="https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white">
  <img alt=".NET" src="https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=white">
  <img alt="Windows Forms" src="https://img.shields.io/badge/Windows%20Forms-0078D6?logo=windows&logoColor=white">
  <img alt="Plataforma" src="https://img.shields.io/badge/plataforma-Windows-blue">
</p>

---

## ✨ Destaques

| Recurso | Descrição |
|---|---|
| 🔎 **Pesquisa imediata** | A tabela é filtrada enquanto você digita, por nome ou código, sem diferenciar maiúsculas de minúsculas. |
| 🎚️ **Filtros combinados** | Texto, categoria e "Somente em estoque" funcionam juntos, com contador de resultados. |
| ✅ **Validação visual** | Campos com erro recebem um ícone de alerta e uma mensagem clara (`ErrorProvider`). |
| 🚦 **Alerta de estoque** | Linhas em vermelho (estoque zerado) e amarelo (estoque baixo) para identificar rápido o que precisa de atenção. |
| 🏷️ **Promoções** | Ao ativar a promoção, o desconto (1% a 70%) é aplicado e o preço final é recalculado na hora. |
| 🖼️ **Imagem do produto** | Escolha de arquivos PNG ou JPG, exibidos sem distorção. |
| 🧩 **CRUD completo** | Cadastrar, editar, excluir (com confirmação) e cancelar, tudo na mesma tela. |
| 📐 **Layout adaptável** | A interface se ajusta ao redimensionar a janela. |

## 📋 Regras de negócio

**Validação do cadastro**

| Campo | Regra |
|---|---|
| Código | Obrigatório e único (ignora maiúsculas e minúsculas). Fica bloqueado durante a edição. |
| Nome | Obrigatório |
| Categoria | Deve ser selecionada |
| Preço | Deve ser maior que zero |

**Cores da tabela**

| Estoque | Linha |
|---|---|
| 0 | 🟥 Vermelho claro |
| 1 a 5 | 🟨 Amarelo claro |
| 6 ou mais | ⬜ Sem destaque |

## 🚀 Começando

### Pré-requisitos

- Windows
- [.NET SDK 6.0](https://dotnet.microsoft.com/download) ou superior
- Visual Studio 2022 *(opcional)*

### Executando

```bash
git clone https://github.com/<seu-usuario>/CatalogoProdutos.git
cd CatalogoProdutos
dotnet run
```

Ou abra o projeto no Visual Studio e pressione **F5**.

## 🖱️ Como usar

| Ação | Como fazer |
|---|---|
| **Cadastrar** | Preencha os dados e clique em **Salvar** (ou pressione `Enter`). |
| **Editar** | Clique em uma linha da tabela, altere os dados e clique em **Atualizar**. |
| **Excluir** | Selecione o produto, clique em **Excluir** e confirme. |
| **Pesquisar** | Digite no campo de pesquisa, escolha uma categoria ou marque "Somente em estoque". |
| **Recomeçar** | Use **Cancelar** para voltar ao modo de novo cadastro ou **Limpar filtros** para zerar a busca. |

O programa já inicia com três produtos de exemplo para você testar os filtros e o destaque de estoque.

## 🧱 Tecnologias e conceitos

- **C# / .NET** com **Windows Forms**
- `BindingList<T>` e `DataGridView` com vínculo por `DataPropertyName`
- `ErrorProvider` e evento `CellFormatting`
- `TableLayoutPanel` e `FlowLayoutPanel` para o layout
- `OpenFileDialog` e `PictureBox`
- Classe `partial` separando interface e lógica
- LINQ para filtros e validações

## 📁 Estrutura

```
CatalogoProdutos/
├── CatalogoForm.cs          # Eventos, filtros, validação e CRUD
├── CatalogoForm.Layout.cs   # Montagem da interface e controles
├── Produto.cs               # Modelo de dados
└── Program.cs               # Ponto de entrada
```

## 🗺️ Roadmap

- [x] CRUD de produtos
- [x] Pesquisa imediata com filtros combinados
- [x] Validação visual dos campos
- [x] Destaque de estoque por cor
- [x] Imagem do produto
- [x] Promoção com cálculo de preço final
- [ ] Persistência dos dados (JSON ou SQLite)
- [ ] Salvar imagem e desconto junto com o produto
- [ ] Ordenação da tabela por coluna

## 👤 Autor

**Theo Linhares Mariano**
Sistemas de Informação, IFMG - Campus São João Evangelista

<sub>Projeto acadêmico desenvolvido na disciplina de Linguagem de Programação II.</sub>

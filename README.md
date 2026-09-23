# 🐉 Ruler's Fate

**Ruler's Fate** é um jogo de RPG de turnos desenvolvido em C# (.NET 10) para o console. O projeto possui uma arquitetura fortemente orientada a objetos (POO), implementando mecânicas clássicas de RPG como classes, sistema de "gacha" para raças, progressão de níveis, status e um sistema completo de inventário e equipamentos.

---

## 🎲 Funcionalidades e Mecânicas

### 🗡️ Classes Jogáveis
Cada classe possui atributos e habilidades únicas:
* **Guerreiro**: Especialista em combate físico. Pode usar o *Golpe Esmagador*.
* **Mago**: Mestre das artes arcanas. Lança *Bola de Fogo* que incinera os inimigos (aplica efeito contínuo de Queimadura).
* **Invocador**: Capaz de trazer aliados para o campo de batalha com *Invocar Golem*.
* **Jackpot**: Baseado inteiramente na sorte com o seu *Dado Viciado*.

### 🧬 Sistema de Raças (Gacha)
Ao iniciar a jornada, o destino (Gacha System) define a raça do seu herói, aplicando multiplicadores aos seus atributos base:
* **Humano**: Balanceado.
* **Elfo**: Bônus alto em Inteligência e Mana.
* **Anão**: Bônus em Vida Máxima e Força.
* **Goblin**: Bônus extremo em Velocidade.

### 🎒 Inventário e Equipamentos
O herói possui uma mochila para guardar o loot das batalhas.
* **Itens Consumíveis**: Poções de Vida e Mana que são gastas ao usar.
* **Equipamentos**: Armas (ex: *Espada Longa de Aço*) e Armaduras (ex: *Armadura de Couro*). Diferente de poções, equipamentos não somem do inventário; eles são "vestidos" nos slots do corpo (Arma, Armadura, Acessório), modificando os status passivamente.

### 🩸 Efeitos de Status (Buffs e Debuffs)
O jogo conta com um motor completo de ciclo de vida de status (`AoAplicar`, `ProcessarTurno`, `AoRemover`).
Exemplos: *Queimando* (dano fixo por turno), *Envenenado* (dano progressivo), *Fortalecido* (aumenta a força temporariamente e reverte ao final).

### ⚔️ Campo Aberto e Combate
Em campo aberto, o jogador pode checar seus status completos, abrir a mochila ou sair para "Caçar Mobs". O combate é por turnos, onde jogador e inimigo trocam golpes até que um caia. Vencer concede **XP** (que leva a Levels Ups, melhorando atributos) e chance de **Drop de Itens e Equipamentos Raros**.

---

## 🏗️ Estrutura do Projeto

A arquitetura do projeto foi desenhada para ser modular e fácil de expandir:
* `/Entidades/Personagens/`: Contém a classe `PersonagemBase`, inimigos e as classes jogáveis.
* `/Entidades/Itens/`: Contém os itens consumíveis e a base de `Equipamento`.
* `/Sistemas/Core/`: Motores centrais como Inventário, Efeitos de Status (DoTs e Buffs) e o Gacha.
* `/Sistemas/Racas/`: Modificadores raciais do jogo.
* `/Testes/`: Projeto paralelo em **xUnit** contendo dezenas de testes automatizados garantindo o funcionamento de toda a lógica do RPG.

---

## 🚀 Como Rodar o Jogo

Certifique-se de ter o [SDK do .NET 10.0](https://dotnet.microsoft.com/) instalado na sua máquina.

Pelo terminal, navegue até a raiz do repositório (`Ruler's Fate`) e execute:

```bash
# Para compilar e executar o jogo:
dotnet run --project "Ruler's Fate/Ruler's Fate.csproj"
```

## 🧪 Como Rodar os Testes

O projeto conta com testes unitários para garantir a estabilidade do combate, itens e progressão.

```bash
# Para rodar a suíte de testes xUnit:
dotnet test "Ruler's Fate/Testes/RulersFate.Testes.csproj"
```
=======


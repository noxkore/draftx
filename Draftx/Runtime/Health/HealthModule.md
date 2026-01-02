# Documentação do Módulo Health (Draftx)

A seguir, apresentamos a documentação básica para o módulo `/Health` do repositório `draftx`. Este módulo é projetado para gerenciar o estado de saúde de entidades, incorporando um sistema de pipeline para modificação de dano, o que sugere uma aplicação em um ambiente de jogo (dada a dependência de `UnityEngine`).

## 1. Interfaces (Contratos)

O módulo é construído em torno de três interfaces principais que definem o contrato para o gerenciamento de saúde e processamento de dano.

### 1.1. `IHealthComponent`

Define o contrato fundamental para qualquer componente que possua saúde.

| Membro | Tipo | Descrição |
| :--- | :--- | :--- |
| `currentHealth` | `float` (somente leitura) | A quantidade de saúde atual da entidade. |
| `maxHealth` | `float` (somente leitura) | A quantidade máxima de saúde da entidade. |
| `OnHealthChanged` | `event Action<float, float>` | Disparado quando a saúde é alterada. Recebe `(currentHealth, maxHealth)`. |
| `OnDamageTaken` | `event Action<float>` | Disparado quando a entidade sofre dano. Recebe o valor do dano. |
| `OnHealed` | `event Action<float>` | Disparado quando a entidade é curada. Recebe o valor da cura. |
| `OnDeath` | `event Action` | Disparado quando a saúde atinge zero ou menos. |
| `Die()` | `void` | Método para forçar a morte da entidade. |
| `SufferDamage(float ammount)` | `void` | Aplica dano à entidade. |
| `Heal(float ammount)` | `void` | Aplica cura à entidade. |

### 1.2. `ISufferModifier`

Define o contrato para modificadores que podem alterar o valor do dano antes que ele seja aplicado.

| Membro | Tipo | Descrição |
| :--- | :--- | :--- |
| `Modify(float baseAmount, SufferContext context)` | `float` | Recebe o valor base do dano e um contexto, e retorna o valor modificado. |

### 1.3. `ISufferPipeline`

Define o contrato para o componente responsável por aplicar a lógica de modificação de dano.

| Membro | Tipo | Descrição |
| :--- | :--- | :--- |
| `Suffer(float amount, SufferContext context)` | `void` | Inicia o processo de dano, aplicando modificadores e, em seguida, o dano final ao `IHealthComponent`. |

## 2. Bases (Implementações Abstratas)

As classes base fornecem implementações parciais e a infraestrutura necessária para o funcionamento do módulo.

### 2.1. `BaseHealthComponent`

Esta classe abstrata implementa a interface `IHealthComponent` e herda de `MonoBehaviour` (indicando uso no Unity).

**Lógica Principal:**

* **Propriedades:** `currentHealth` e `maxHealth` são protegidas, permitindo que classes derivadas as inicializem.
* **Eventos:** Todos os eventos de `IHealthComponent` são implementados e invocados nos métodos correspondentes.
* **`Die()`:** Invoca o evento `OnDeath` e destrói o objeto de jogo (`gameObject`).
* **`Heal(float ammount)`:** Invoca `OnHealthChanged` e `OnHealed`, e adiciona o valor de cura à `currentHealth`.
* **`SufferDamage(float ammount)`:** Invoca `OnHealthChanged` e `OnDamageTaken`, subtrai o dano da `currentHealth` e chama `Die()` se a saúde for menor ou igual a zero.

### 2.2. `BaseSufferModifier`

Uma classe abstrata simples que implementa `ISufferModifier` e herda de `MonoBehaviour`. Requer que classes derivadas implementem a lógica de modificação de dano no método `Modify`.

### 2.3. `BaseSufferPipeline`

Esta classe implementa `ISufferPipeline` e é o **coração do sistema de contratos**.

**Lógica Principal:**

1. **Inicialização (`Awake()`):**
    * Obtém a referência para o `BaseHealthComponent` no mesmo objeto de jogo.
    * Obtém todas as implementações de `ISufferModifier` anexadas ao mesmo objeto de jogo e as armazena em `modifiers`.
2. **Processamento de Dano (`Suffer()`):**
    * Recebe o `amount` de dano base e o `context`.
    * Itera sobre todos os `modifiers` coletados.
    * Para cada modificador, chama `Modify()`, atualizando o `finalAmount` de dano.
    * Após a aplicação de todos os modificadores, chama `health.SufferDamage(finalAmount)`, aplicando o dano final ao componente de saúde.

## 3. Estruturas de Dados e Bases

O módulo utiliza estruturas de dados para enriquecer o contexto das operações.

### 3.1. `SufferContext` (Estrutura de Dados)

Uma estrutura (`struct`) que encapsula informações adicionais sobre a fonte ou o tipo de dano.

| Membro | Tipo | Descrição |
| :--- | :--- | :--- |
| `Type` | `DamageType` | O tipo de dano (ex: Fogo, Gelo, Físico). |
| `Default` | `static SufferContext` | Uma instância padrão de contexto, onde `Type` é nulo. |

### 3.2. `DamageType` (Base de Dados)

Uma classe que herda de `ScriptableObject` (indicando que é um ativo de dados configurável no Unity) e é usada para definir o tipo de elemento do dano.

| Membro | Tipo | Descrição |
| :--- | :--- | :--- |
| `elementName` | `string` | O nome do elemento de dano (ex: "Fogo"). |

Esta classe é usada dentro do `SufferContext` para permitir que os `ISufferModifier` implementem lógicas baseadas no tipo de dano (ex: resistência a fogo).

---
*Documentação gerada por Manus AI.*

# Documentação do Módulo Movement (Draftx)

A seguir, apresentamos a documentação básica para o módulo `/Movement` do repositório `draftx`. Este módulo é projetado para gerenciar o movimento de entidades em um ambiente de jogo (dada a dependência de `UnityEngine`), incorporando componentes para entrada de movimento, cálculo de velocidade e aplicação de movimento.

## 1. Interfaces (Contratos)

O módulo é construído em torno de quatro interfaces principais que definem o contrato para o gerenciamento de movimento e velocidade.

### 1.1. `IMovementComponent`

Define o contrato fundamental para qualquer componente que possa realizar movimento.

| Membro | Tipo | Descrição |
| :--- | :--- | :--- |
| `Move(Vector2 direction)` | `void` | Aplica movimento à entidade em uma `direction` especificada. |

### 1.2. `IMovementInputComponent`

Define o contrato para componentes que fornecem entrada de movimento.

| Membro | Tipo | Descrição |
| :--- | :--- | :--- |
| `Tick()` | `void` | Método chamado a cada frame para processar a entrada de movimento e acionar o `IMovementComponent`. |

### 1.3. `ISpeedModifier`

Define o contrato para modificadores que podem alterar a velocidade de movimento.

| Membro | Tipo | Descrição |
| :--- | :--- | :--- |
| `GetMultiplier()` | `float` | Retorna um valor multiplicador que será aplicado à velocidade base. |

### 1.4. `ISpeedProvider`

Define o contrato para componentes que fornecem a velocidade de movimento atual.

| Membro | Tipo | Descrição |
| :--- | :--- | :--- |
| `GetSpeed()` | `float` | Retorna a velocidade de movimento atual, considerando quaisquer modificadores. |

## 2. Bases (Implementações Abstratas)

As classes base fornecem implementações parciais e a infraestrutura necessária para o funcionamento do módulo.

### 2.1. `BaseMovementComponent`

Esta classe abstrata implementa a interface `IMovementComponent` e herda de `MonoBehaviour` (indicando uso no Unity).

**Lógica Principal:**

* **Dependência:** Requer um `ISpeedProvider` no mesmo objeto de jogo para determinar a velocidade de movimento.
* **`Awake()`:** Obtém a referência para o `ISpeedProvider`.
* **`Move(Vector2 direction)`:**
  * Verifica se há um `speedProvider` e se a `direction` é significativa.
  * Obtém a velocidade atual do `speedProvider`.
  * Calcula o deslocamento (`delta`) usando a direção normalizada, velocidade e `Time.deltaTime`.
  * Atualiza a posição do objeto (`transform.position`) pelo `delta` calculado.

### 2.2. `BaseMovementInputComponent`

Esta classe abstrata implementa a interface `IMovementInputComponent` e herda de `MonoBehaviour`.

**Lógica Principal:**

* **Dependência:** Requer um `IMovementComponent` no mesmo objeto de jogo para aplicar o movimento.
* **`Awake()`:** Obtém a referência para o `IMovementComponent`.
* **`Update()`:** Chama o método `Tick()` a cada frame.
* **`Tick()`:**
  * Verifica se há um `movement` component.
  * Chama o método abstrato `ReadDirection()` para obter a direção de movimento.
  * Chama `movement.Move(direction)` para aplicar o movimento.
* **`ReadDirection()`:** Método abstrato que deve ser implementado por classes derivadas para fornecer a direção de entrada.

### 2.3. `BaseSpeedProvider`

Esta classe implementa a interface `ISpeedProvider` e herda de `MonoBehaviour`.

**Lógica Principal:**

* **`baseSpeed`:** Um campo serializado (`[SerializeField]`) que define a velocidade base da entidade.
* **`speedModifiers`:** Um array de `ISpeedModifier` que são obtidos no mesmo objeto de jogo.
* **`Awake()`:** Chama `UpdateModifiers()` para inicializar os modificadores de velocidade.
* **`GetSpeed()`:**
  * Retorna a `baseSpeed` multiplicada pelos valores de todos os `ISpeedModifier` encontrados.
  * Se não houver modificadores, retorna apenas a `baseSpeed`.
* **`UpdateModifiers()`:** Obtém todas as implementações de `ISpeedModifier` anexadas ao mesmo objeto de jogo e as armazena em `speedModifiers`.

## 3. Sistemas (Implementações Concretas)

Estas são implementações específicas das classes base e interfaces, fornecendo funcionalidades prontas para uso.

### 3.1. `PlayerKeyboardMovementInput`

Esta classe herda de `BaseMovementInputComponent` e fornece entrada de movimento baseada no teclado.

**Lógica Principal:**

* **`ReadDirection()`:** Lê a entrada horizontal (`Horizontal`) e vertical (`Vertical`) do sistema de entrada do Unity (geralmente mapeado para as setas do teclado ou teclas WASD) e retorna um `Vector2` correspondente à direção.

### 3.2. `Rigidbody2DMovementComponent`

Esta classe herda de `BaseMovementComponent` e é projetada para aplicar movimento a um `Rigidbody2D` (componente de física do Unity).

**Lógica Principal:**

* **`RequireComponent(typeof(Rigidbody2D))`:** Garante que um `Rigidbody2D` esteja presente no mesmo objeto de jogo.
* **`useVelocity`:** Um campo serializado que determina se o movimento deve ser aplicado diretamente via `Rigidbody2D.velocity` ou usando `Rigidbody2D.AddForce`.
* **`Awake()`:** Chama o `base.Awake()` e obtém a referência para o `Rigidbody2D`.
* **`Move(Vector2 direction)`:**
  * Se `useVelocity` for `true`:
    * Define `rb.velocity` diretamente para a direção normalizada multiplicada pela velocidade.
    * Se a direção for zero, define `rb.velocity` como `Vector2.zero` para parar o movimento.
  * Se `useVelocity` for `false`:
    * Aplica uma força (`rb.AddForce`) na direção normalizada multiplicada pela velocidade e `Time.deltaTime`, usando `ForceMode2D.Force`.

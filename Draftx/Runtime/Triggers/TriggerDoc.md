# Documentação do Módulo Triggers (Draftx)

A seguir, apresentamos a documentação básica para o módulo `/Triggers` do repositório `draftx`. Este módulo é projetado para gerenciar interações e ações baseadas em eventos dentro de um ambiente de jogo (dada a dependência de `UnityEngine`), fornecendo um sistema flexível para criar comportamentos reativos.

## 1. Interfaces (Contratos)

O módulo é construído em torno de três interfaces principais que definem o contrato para o gerenciamento de eventos e ações.

### 1.1. `IContext`

Define um contrato genérico para o contexto de eventos de gatilho, permitindo que informações adicionais sejam passadas quando um gatilho é ativado.

| Membro | Tipo | Descrição |
| :--- | :--- | :--- |
| (Nenhum) | | Esta interface serve como um marcador para contextos de gatilho. |

### 1.2. `ITrigger`

Define o contrato para qualquer componente que possa iniciar uma ação ou série de ações.

| Membro | Tipo | Descrição |
| :--- | :--- | :--- |
| `OnTriggered` | `event Action<IContext>` | Disparado quando o gatilho é ativado. Recebe um `IContext` com informações relevantes. |

### 1.3. `ITriggerAction`

Define o contrato para qualquer componente que possa ser executado em resposta a um gatilho.

| Membro | Tipo | Descrição |
| :--- | :--- | :--- |
| `Execute(IContext context)` | `void` | Método chamado para executar a ação. Recebe o `IContext` do gatilho. |

## 2. Bases (Implementações Abstratas)

As classes base fornecem implementações parciais e a infraestrutura necessária para o funcionamento do módulo.

### 2.1. `ActionBase`

Esta classe abstrata implementa a interface `ITriggerAction` e herda de `MonoBehaviour` (indicando uso no Unity). Ela gerencia a subscrição e desubscrição de eventos de `ITrigger`.

**Lógica Principal:**

* **Subscrição:** No `Awake()`, obtém todos os `ITrigger` anexados ao mesmo GameObject e subscreve o método `Execute` a cada evento `OnTriggered`.
* **Desubscrição:** No `OnDestroy()`, desubscreve o método `Execute` dos eventos `OnTriggered` para evitar vazamentos de memória.
* **`Execute(IContext context)`:** Método virtual que deve ser implementado por classes derivadas para definir a lógica da ação.

### 2.2. `ToggleTriggerBase`

Esta classe abstrata implementa a interface `ITrigger` e herda de `MonoBehaviour`. Ela fornece a funcionalidade básica para gatilhos que podem ser ativados e desativados.

**Lógica Principal:**

* **`startEnabled`:** Um campo serializado que define se o gatilho começa habilitado.
* **`isEnabled`:** Um campo interno que rastreia o estado atual do gatilho.
* **`TryTrigger(IContext context)`:** Invoca o evento `OnTriggered` apenas se o gatilho estiver habilitado.
* **`Toggle()`:** Alterna o estado de `isEnabled`.
* **`SetState(bool value)`:** Define explicitamente o estado de `isEnabled`.

### 2.3. `TriggerContext` (Estrutura de Dados)

Uma estrutura (`struct`) que encapsula informações adicionais sobre o evento que ativou o gatilho. Implementa `IContext`.

| Membro | Tipo | Descrição |
| :--- | :--- | :--- |
| `Source` | `GameObject` | O GameObject que iniciou o evento de gatilho. |
| `Collider` | `Collider2D` | O Collider2D envolvido no evento de gatilho (se aplicável). |

## 3. Implementações (Triggers e Actions)

Estas são implementações específicas das classes base e interfaces, fornecendo funcionalidades prontas para uso.

### 3.1. Triggers

#### 3.1.1. `AreaInputTrigger`

Um gatilho que é ativado quando uma tecla específica é pressionada enquanto um GameObject com uma tag requerida está dentro de uma área de colisão 2D.

**Lógica Principal:**

* **`key`:** A tecla que deve ser pressionada para ativar o gatilho.
* **`requiredTag`:** A tag do GameObject que deve estar dentro da área.
* **`OnTriggerEnter2D` / `OnTriggerExit2D`:** Detecta a entrada e saída de GameObjects na área.
* **`Update()`:** Verifica se a tecla foi pressionada enquanto um GameObject com a tag correta está dentro da área e invoca `OnTriggered`.

#### 3.1.2. `AreaTriggerEnter`

Um gatilho que é ativado automaticamente quando um GameObject com uma tag específica entra em uma área de colisão 2D.

**Lógica Principal:**

* **`requiredTag`:** A tag do GameObject que deve entrar na área.
* **`OnTriggerEnter2D`:** Detecta a entrada de GameObjects na área e invoca `OnTriggered` se a tag corresponder.

#### 3.1.3. `InputTrigger`

Um gatilho que é ativado quando uma tecla específica é pressionada, independentemente da posição ou colisão.

**Lógica Principal:**

* **`key`:** A tecla que deve ser pressionada para ativar o gatilho.
* **`Update()`:** Verifica se a tecla foi pressionada e invoca `OnTriggered`.

#### 3.1.4. `AITT` (AreaInputToggleTrigger)

Um gatilho alternável (`ToggleTriggerBase`) que é ativado por entrada do usuário dentro de uma área de colisão 2D. (Nome completo: AreaInputToggleTrigger)

**Lógica Principal:**

* Combina a lógica de `AreaInputTrigger` com a funcionalidade de alternância de `ToggleTriggerBase`.
* Ativa `TryTrigger` quando a tecla é pressionada dentro da área, se o gatilho estiver habilitado.

#### 3.1.5. `ToggleTriggerInput`

Um gatilho alternável (`ToggleTriggerBase`) que é ativado por entrada do usuário (pressionar uma tecla).

**Lógica Principal:**

* **`key`:** A tecla que deve ser pressionada para ativar o gatilho.
* **`Update()`:** Verifica se a tecla foi pressionada e invoca `TryTrigger`.
* **Métodos Públicos:** `ToggleTrigger()`, `EnableTrigger()`, `DisableTrigger()` para controle externo do estado do gatilho.

### 3.2. Actions

#### 3.2.1. `MoveAction`

Uma ação que move o GameObject ao qual está anexada por um deslocamento especificado, com opções de duração e curva de easing.

**Lógica Principal:**

* **`MoveX`, `MoveY`:** Deslocamento nos eixos X e Y.
* **`DurationX`, `DurationY`:** Duração do movimento em cada eixo.
* **`EaseX`, `EaseY`:** Curvas de animação para o easing do movimento.
* **`EaseStrengthX`, `EaseStrengthY`:** Força do easing.
* **`Execute(IContext context)`:** Inicia uma corrotina para mover o GameObject da posição atual para a posição alvo, aplicando easing.

#### 3.2.2. `RotateAction`

Uma ação que gira o GameObject ao qual está anexada por um ângulo especificado, com opções de duração, pivô e curva de easing.

**Lógica Principal:**

* **`RotateZ`:** Ângulo de rotação no eixo Z.
* **`Pivot`:** Ponto de pivô para a rotação em espaço local.
* **`Duration`:** Duração da rotação.
* **`Ease`:** Curva de animação para o easing da rotação.
* **`EaseStrength`:** Força do easing.
* **`Execute(IContext context)`:** Inicia uma corrotina para girar o GameObject da rotação atual para a rotação alvo, aplicando easing e ajustando a posição para manter o pivô.

#### 3.2.3. `RotateToggleAction`

Uma ação que alterna a rotação do GameObject entre dois ângulos predefinidos (`ClosedAngle` e `OpenAngle`). Herda de `RotateAction`.

**Lógica Principal:**

* **`ClosedAngle`:** Ângulo quando a ação está no estado "fechado".
* **`OpenAngle`:** Ângulo quando a ação está no estado "aberto".
* **`isOpen`:** Um campo interno que rastreia o estado atual (aberto/fechado).
* **`Execute(IContext context)`:** Alterna o estado `isOpen` e inicia a rotação para o ângulo correspondente (aberto ou fechado), utilizando a lógica de rotação de `RotateAction`.

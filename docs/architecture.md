# System Architecture

## 1. Architecture Overview

Parallel Merge Sort es una aplicación en C# diseñada para comparar una implementación secuencial de Merge Sort con una implementación paralela utilizando la Task Parallel Library (TPL).

La implementación paralela utiliza descomposición recursiva para dividir el problema de ordenamiento en subproblemas independientes más pequeños que pueden ejecutarse de forma concurrente.

La arquitectura separa la aplicación en lógica de ordenamiento, ejecución y pruebas de rendimiento (benchmarking), pruebas automatizadas, documentación y métricas de rendimiento.

---

## 2. System Components

El proyecto contiene los siguientes componentes principales:

### `Program.cs`
Actúa como el punto de entrada de la aplicación.

**Responsabilidades:**
- Generar datos de prueba.
- Ejecutar el Merge Sort secuencial.
- Ejecutar el Merge Sort paralelo.
- Medir los tiempos de ejecución.
- Calcular el Speedup (Aceleración).
- Calcular la Eficiencia.
- Ejecutar benchmarks de rendimiento.
- Comparar diferentes profundidades de paralelización.
- Validar los resultados del ordenamiento.

### `MergeSorter.cs`
Contiene la implementación secuencial de Merge Sort.

**Responsabilidades:**
- Dividir el arreglo de forma recursiva.
- Ordenar cada porción secuencialmente.
- Fusionar (*merge*) las porciones ordenadas.
- Proveer la operación `Merge` utilizada por las implementaciones de ordenamiento.

### `ParallelMergeSorter.cs`
Contiene la implementación paralela de Merge Sort.

**Responsabilidades:**
- Aplicar descomposición recursiva.
- Crear tareas concurrentes utilizando `Task.Run`.
- Ejecutar las ramas izquierda y derecha de manera concurrente.
- Controlar la profundidad máxima de recursión paralela.
- Sincronizar las tareas utilizando `Task.WaitAll`.
- Utilizar ejecución secuencial tras alcanzar la profundidad configurada.

### `ParallelMergeSort.Tests`
Contiene pruebas automatizadas desarrolladas con xUnit.

Las pruebas verifican que las implementaciones de ordenamiento procesen correctamente diferentes tipos de entrada.

### `metrics/`
Contiene mediciones de rendimiento y resultados de benchmarks.

**Las métricas incluyen:**
- Tiempo de ejecución secuencial.
- Tiempo de ejecución paralelo.
- Speedup.
- Eficiencia.
- Resultados de escalabilidad.
- Comparaciones de profundidad de paralelización.

---

## 3. Execution Flow

El flujo general de ejecución es el siguiente:

                    Start
                      |
                      v
                Generate data
                      |
             +--------+--------+
             |                 |
             v                 v
      Sequential Sort    Parallel Sort
             |                 |
             |          Recursive division
             |                 |
             |          +------+------+
             |          |             |
             |          v             v
             |      Left Task     Right Task
             |          |             |
             |          +------+------+
             |                 |
             |            Task.WaitAll
             |                 |
             |               Merge
             |                 |
             +--------+--------+
                      |
                      v
              Validate results
                      |
                      v
              Calculate metrics
                      |
                      v
                     End

La aplicación genera los mismos datos de entrada para ambas implementaciones.

El algoritmo secuencial ordena una copia de los datos, mientras que el algoritmo paralelo ordena otra copia.

Los tiempos de ejecución se miden de manera independiente y luego se utilizan para calcular las métricas de rendimiento.

---

## 4. Parallel Decomposition

El algoritmo paralelo utiliza descomposición recursiva:

                    Array
                   /     \
                  /       \
          Left half       Right half
             /               \
            /                 \
       Left task          Right task

Cada rama se puede procesar de forma independiente porque las dos porciones del arreglo no se superponen.

La implementación paralela crea una tarea para cada rama:

                    Array
                   /     \
                  /       \
            Task 1       Task 2
              |             |
        Left recursion  Right recursion
              |             |
              +------+------+
                     |
                   Merge

La recursión continúa hasta alcanzar la profundidad máxima paralela configurada.

Tras alcanzar este límite, las operaciones recursivas restantes se ejecutan secuencialmente. Esto evita que la aplicación cree un número excesivo de tareas.

---

## 5. Concurrency Model

La aplicación utiliza la Task Parallel Library (TPL).

Las operaciones concurrentes principales se crean utilizando `Task.Run`.

Conceptualmente, el algoritmo paralelo realiza:

Task.Run(Left branch)
Task.Run(Right branch)        |
        v
Task.WaitAll()                |
        v
      Merge

Las ramas izquierda y derecha pueden ejecutarse concurrentemente en diferentes núcleos del procesador.

La implementación utiliza una profundidad máxima para controlar la cantidad de concurrencia. El entorno de pruebas actual cuenta con cuatro procesadores lógicos.

---

## 6. Synchronization

La sincronización es requerida antes de que las dos porciones ordenadas puedan fusionarse.

La implementación utiliza:

`Task.WaitAll(leftTask, rightTask);`

Esto garantiza que ambas tareas recursivas hayan completado antes de que comience la operación `Merge`.

La secuencia de sincronización es:

Left Task  --------\
                    >---- Task.WaitAll ----> Merge
Right Task --------/

Sin esta sincronización, la operación de fusión podría ejecutarse antes de que una de las porciones haya terminado de ordenarse.

---

## 7. Data Flow

La aplicación utiliza arreglos de enteros como la estructura de datos principal.

El flujo de datos es:

Random data
    |
    +--------------------+
    |                    |
    v                    v
Sequential copy    Parallel copy
    |                    |
    v                    v
Merge Sort          Parallel Merge Sort
    |                    |
    v                    v
Validation          Validation
    |                    |
    +---------+----------+
              |
              v
       Performance metrics

Ambos algoritmos reciben datos de entrada equivalentes para que la comparación de rendimiento sea significativa.

---

## 8. Scalability

La aplicación evalúa la escalabilidad probando diferentes tamaños de entrada.

El benchmark incluye:
- 100,000 elementos.
- 500,000 elementos.
- 1,000,000 elementos.
- 2,000,000 elementos.

Los resultados demuestran que la implementación paralela mantiene una ventaja de rendimiento a medida que aumenta el tamaño de la entrada.

Para conjuntos de datos más grandes, existe suficiente trabajo computacional para compensar parte del *overhead* (sobrecoste) introducido por la gestión de tareas y la sincronización.

---

## 9. Performance Metrics

La arquitectura soporta el cálculo de diversas métricas de rendimiento:

- **Tiempo de Ejecución:** Mide cuánto tiempo le toma a cada implementación ordenar la entrada.
- **Speedup (Aceleración):**
  $$\text{Speedup} = \frac{\text{Tiempo Secuencial}}{\text{Tiempo Paralelo}}$$
  Un valor mayor a 1 significa que la implementación paralela es más rápida.
- **Eficiencia:**
  $$\text{Eficiencia} = \frac{\text{Speedup}}{\text{Número de Procesadores}} \times 100$$
  Indica con qué eficacia se están utilizando los recursos de procesamiento disponibles.

  ---

  ## 10. Architecture Summary

El sistema sigue una arquitectura centrada en el algoritmo Merge Sort.

Las implementaciones secuencial y paralela comparten la misma operación de fusión (*merge*) mientras utilizan diferentes estrategias de ejecución.

La implementación paralela utiliza descomposición recursiva, `Task` y `Task.WaitAll` para ejecutar ramas independientes de forma concurrente.

La arquitectura también incluye pruebas automatizadas y benchmarks de rendimiento para verificar la corrección y evaluar los beneficios de la ejecución paralela.
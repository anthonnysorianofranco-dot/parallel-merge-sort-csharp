# Project Documentation

## 1. Project Overview

Parallel Merge Sort es un proyecto final para la asignatura de Programación Paralela.

El proyecto demuestra cómo un algoritmo secuencial tradicional se puede adaptar para utilizar ejecución paralela.

El algoritmo seleccionado es Merge Sort, utilizando la descomposición recursiva como estrategia de paralelización.

El proyecto compara la ejecución secuencial y paralela utilizando mediciones reales de rendimiento.

---

## 2. Project Objectives

El objetivo principal es implementar y evaluar un algoritmo paralelo de Merge Sort.

**Objetivos específicos:**
- Implementar Merge Sort secuencial.
- Implementar Merge Sort paralelo.
- Utilizar descomposición recursiva.
- Utilizar la Task Parallel Library (TPL).
- Ejecutar tareas independientes de forma concurrente.
- Controlar la profundidad máxima de recursión paralela.
- Medir el tiempo de ejecución.
- Calcular el Speedup (Aceleración).
- Calcular la Eficiencia.
- Evaluar la escalabilidad.
- Validar la corrección de las implementaciones.
- Documentar los resultados.

---

## 3. Technologies

El proyecto utiliza las siguientes tecnologías:

| Tecnología | Propósito |
|---|---|
| C# | Lenguaje de programación |
| .NET 10 | Framework de la aplicación |
| Task Parallel Library | Ejecución paralela |
| xUnit | Pruebas automatizadas |
| Git | Control de versiones |
| GitHub | Repositorio de código fuente |
| Visual Studio | Entorno de desarrollo |

---

## 4. Project Structure

El repositorio está organizado de la siguiente manera:

parallel-merge-sort-csharp/
|
+-- docs/
|   +-- architecture.md
|   +-- technical-design.md
|   +-- project-documentation.md
|
+-- metrics/
|   +-- benchmark-results.md
|
+-- src/
|   +-- ParallelMergeSort.App/
|       +-- Program.cs
|       +-- MergeSorter.cs
|       +-- ParallelMergeSorter.cs
|       +-- ParallelMergeSort.App.csproj
|
+-- tests/
|   +-- ParallelMergeSort.Tests/
|       +-- ParallelMergeSort.Tests.csproj
|       +-- UnitTest1.cs
|
+-- README.md
+-- LICENSE
+-- .gitignore

---

## 5. Sequential Implementation

La implementación secuencial está contenida en `MergeSorter.cs`.

El algoritmo sigue el proceso tradicional de Merge Sort:
1. Dividir el arreglo en dos mitades.
2. Ordenar recursivamente la mitad izquierda.
3. Ordenar recursivamente la mitad derecha.
4. Fusionar (*merge*) las dos mitades ordenadas.

El algoritmo tiene una complejidad temporal de:

$$O(n \log n)$$

La implementación secuencial sirve como línea base para evaluar la versión paralela.

---

## 6. Parallel Implementation

La implementación paralela está contenida en `ParallelMergeSorter.cs`.

Utiliza descomposición recursiva y la Task Parallel Library (TPL).

Cuando no se ha alcanzado la profundidad máxima configurada, el algoritmo crea dos tareas:
- **Tarea 1:** Ordenar mitad izquierda
- **Tarea 2:** Ordenar mitad derecha

La aplicación espera por ambas tareas mediante:

`Task.WaitAll(leftTask, rightTask);`

Después de que ambas ramas terminan, el algoritmo realiza la operación de fusión.

Cuando se alcanza la profundidad máxima paralela, el trabajo restante se ejecuta secuencialmente.

---

## 7. Performance Evaluation

El proyecto mide el rendimiento de ambas implementaciones.

Las métricas principales son:

- **Tiempo de Ejecución:** El tiempo requerido para completar la operación de ordenamiento.
- **Speedup (Aceleración):**
  $$\text{Speedup} = \frac{\text{Tiempo Secuencial}}{\text{Tiempo Paralelo}}$$
- **Eficiencia:**
  $$\text{Eficiencia} = \frac{\text{Speedup}}{\text{Número de Procesadores}} \times 100$$

El entorno de prueba actual cuenta con cuatro procesadores lógicos.

---

## 8. Benchmark Results

El benchmark de escalabilidad evaluó cuatro tamaños de entrada:

| Input Size | Sequential Average | Parallel Average | Speedup | Efficiency |
| ---------: | -----------------: | ---------------: | ------: | ---------: |
|    100,000 |           31.33 ms |         19.33 ms |   1.62x |     40.52% |
|    500,000 |          169.33 ms |         93.00 ms |   1.82x |     45.52% |
|  1,000,000 |          340.67 ms |        189.33 ms |   1.80x |     44.98% |
|  2,000,000 |          686.33 ms |        390.00 ms |   1.76x |     44.00% |

La implementación paralela fue más rápida para todos los tamaños de entrada probados.

El mejor Speedup en este benchmark fue de **1.82x** con 500,000 elementos.

---

## 9. Parallelization Depth Results

El proyecto también evalúa diferentes profundidades máximas de paralelización.

Los resultados recientes del benchmark para 1,000,000 de elementos fueron:

| MaxDepth | Average Sequential | Average Parallel | Speedup | Efficiency |
| -------: | -----------------: | ---------------: | ------: | ---------: |
|        1 |          358.20 ms |        249.40 ms |   1.44x |     35.91% |
|        2 |          330.40 ms |        185.20 ms |   1.78x |     44.60% |
|        3 |          333.60 ms |        201.60 ms |   1.65x |     41.37% |

La mejor configuración en este benchmark fue **MaxDepth = 2**, con un Speedup de **1.78x** y una Eficiencia del **44.60%**.

Los resultados demuestran que aumentar la cantidad de paralelismo no produce necesariamente un mejor rendimiento, ya que una mayor profundidad puede introducir sobrecostes adicionales de gestión de tareas y sincronización.

---

## 10. Automated Testing

El proyecto utiliza xUnit para pruebas automatizadas.

La suite de pruebas actualmente reporta:
- Pruebas totales: 10
- Pasadas: 10
- Falladas: 0
- Omitidas: 0

Las pruebas verifican que las implementaciones de ordenamiento manejen correctamente diferentes condiciones de entrada.

La validación de la corrección es crucial porque las mejoras de rendimiento solo son útiles si el resultado final ordenado sigue siendo correcto.

---

## 11. Scalability

La escalabilidad se evaluó incrementando el número de elementos procesados:
- 100,000
- 500,000
- 1,000,000
- 2,000,000

Los resultados muestran que la implementación paralela mantiene una ventaja significativa de rendimiento a medida que aumenta el tamaño de la entrada.

Para entradas más grandes, hay más trabajo computacional disponible para las tareas paralelas, permitiendo que los beneficios de la ejecución concurrente sean más visibles.

Sin embargo, el Speedup está limitado por la planificación de tareas, la sincronización, el acceso a memoria y el sobrecoste de fusión.

---

## 12. Correctness Validation

Después de cada ejecución de benchmark, la aplicación verifica que los arreglos resultantes estén ordenados.

La validación comprueba cada par de elementos adyacentes:

`array[i - 1] <= array[i]`

Si algún elemento viola esta condición, el resultado se considera incorrecto.

Las ejecuciones del benchmark completaron con los resultados de ordenamiento secuencial y paralelo validados correctamente.

---

## 13. Conclusions

El proyecto demuestra que la descomposición recursiva se puede aplicar eficazmente a Merge Sort.

La implementación paralela logró tiempos de ejecución menores que la implementación secuencial en todos los tamaños de entrada probados.

Los resultados también demuestran que:
- La ejecución paralela puede mejorar el rendimiento.
- Conjuntos de datos más grandes brindan mayor oportunidad para la paralelización.
- La cantidad de tareas debe ser controlada.
- La profundidad máxima de recursión afecta el rendimiento.
- La sincronización introduce sobrecoste (*overhead*).
- La ejecución paralela no garantiza un Speedup lineal.
- La corrección funcional debe validarse junto con el rendimiento.

La implementación actual demuestra exitosamente los conceptos principales requeridos para un proyecto de programación paralela: ejecución concurrente, datos compartidos, sincronización, descomposición recursiva, medición de rendimiento y escalabilidad.